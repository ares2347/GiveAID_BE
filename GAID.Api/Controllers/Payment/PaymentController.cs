using System.Globalization;
using System.Net;
using AutoMapper;
using GAID.Api.Dto.Payment.Response;
using GAID.Application.Repositories;
using GAID.Domain.Models.Donation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PayPal.Sdk.Checkout.Configuration;
using PayPal.Sdk.Checkout.ContractEnums;
using PayPal.Sdk.Checkout.Core;
using PayPal.Sdk.Checkout.Extensions;
using PayPal.Sdk.Checkout.Orders;

namespace GAID.Api.Controllers.Payment;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly PayPalHttpClient _client;
    static string _clientId = "ATKKXwxgR8fDrgbPBYs0gqU0ZTyTK6sCHOMqGng-eFT798ND8M8Cw3VK0dGx5lP6Vu0pvsjeknvfem0i";
    static string _clientSecret = "ECqWkEl1cA-A8-Khp9FQRELI9rGTZcxVyiZpazLf975EHwYCGQdWOhyzYJOX_eIRFX1unfe4DkBiyQaI";


    public PaymentController(IHttpClientFactory clientFactory, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        var httpClient = clientFactory.CreateClient();
        var options = new OptionsWrapper<PayPalOptions>(
            new PayPalOptions
            {
                ClientId = _clientId,
                ClientSecret = _clientSecret,
                Environment = EPayPalEnvironment.Sandbox
            });
        httpClient.BaseAddress = new Uri(options.Value.BaseUrl);
        // Creating a client for the environment
        _client = new PayPalHttpClient(httpClient, new PayPayEncoder(), options);
    }


    [Authorize]
    [HttpPost("create")]
    public async Task<ActionResult<Order>> CreateOrder(PaymentCreateRequest request, CancellationToken _ = default)
    {
        try
        {
            var donation = _mapper.Map<Donation>(request);
            donation.Status = DonationStatus.Pending;
            var program = await _unitOfWork.ProgramRepository.GetById(request.ProgramId, _);
            if (program is null) return NotFound("Program not found.");
            if (program.CurrentUserEnrollment is null)
                return BadRequest("User need to enroll in order to make a donation");
            donation.EnrollmentId = program.CurrentUserEnrollment.EnrollmentId;
            var result = await _unitOfWork.DonationRepository.Create(donation);
            var order = new OrderRequest()
            {
                CheckoutPaymentIntent = EOrderIntent.Capture,
                ApplicationContext = new ApplicationContext
                {
                    BrandName = program.Name,
                    LandingPage = ELandingPage.Billing,
                    CancelUrl = $"https://www.example.com/donation/{result.DonationId}",
                    ReturnUrl = $"https://www.example.com/donation/{result.DonationId}",
                    UserAction = EUserAction.Continue,
                    ShippingPreference = EShippingPreference.NoShipping,
                },
                PurchaseUnits = new List<PurchaseUnitRequest>
                {
                    new()
                    {
                        Description = "Donation to Give AID NGO.",
                        AmountWithBreakdown = new AmountWithBreakdown
                        {
                            CurrencyCode = "USD",
                            Value = request.Amount.ToString(CultureInfo.InvariantCulture),
                            AmountBreakdown = new AmountBreakdown
                            {
                                ItemTotal = new Money
                                {
                                    CurrencyCode = "USD",
                                    Value = request.Amount.ToString(CultureInfo.InvariantCulture)
                                }
                            }
                        },
                        Items = new List<Item>
                        {
                            new()
                            {
                                Name = request.Reason,
                                UnitAmount = new Money
                                {
                                    CurrencyCode = "USD",
                                    Value = request.Amount.ToString(CultureInfo.InvariantCulture)
                                },
                                Quantity = "1",
                                Category = EItemCategory.DigitalGoods,
                            }
                        }
                    }
                }
            };
            var createOrderRequest = new OrdersCreateRequest();
            createOrderRequest.SetRequestBody(order);
            createOrderRequest.SetPreferReturn(EPreferReturn.Representation);
            var accessToken = await _client.AuthenticateAsync(cancellationToken: _);
            var response =
                await _client.ExecuteAsync<OrdersCreateRequest, OrderRequest, Order>(createOrderRequest, accessToken,
                    _);
            if (response.ResponseStatusCode == HttpStatusCode.Created)
            {
                donation.PaypalOrderId = response.ResponseBody?.Id ?? string.Empty;
                await _unitOfWork.SaveChangesAsync(_);
                return Ok(response.ResponseBody);
            }
            await _unitOfWork.SaveChangesAsync(_);
            return BadRequest(response.ResponseBody);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize]
    [HttpPost("capture/{donationId:guid}")]
    public async Task<ActionResult<Order>> CaptureOrder(Guid donationId, CancellationToken _ = default)
    {
        try
        {
            var donation = await _unitOfWork.DonationRepository.GetById(donationId, _);
            if (donation is null) return NotFound("Donation not found");
            var accessToken = await _client.AuthenticateAsync(cancellationToken: _);
            if (accessToken is null) return BadRequest("Unauthorized.");
            var capture = await _client.CaptureOrderAsync(accessToken, donation.PaypalOrderId, cancellationToken: _);
            if (capture is null) return NotFound();
            if (capture.Status == EOrderStatus.Completed)
            {
                donation.Status = DonationStatus.Completed;
                await _unitOfWork.SaveChangesAsync(_);
            }

            return Ok(capture);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}