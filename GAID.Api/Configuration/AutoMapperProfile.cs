using System.Web;
using AutoMapper;
using GAID.Api.Dto.Attachment;
using GAID.Api.Dto.Donation;
using GAID.Api.Dto.Enrollment.Response;
using GAID.Api.Dto.Page.Request;
using GAID.Api.Dto.Page.Response;
using GAID.Api.Dto.Partner.Request;
using GAID.Api.Dto.Partner.Response;
using GAID.Api.Dto.Payment.Response;
using GAID.Api.Dto.Program.Request;
using GAID.Api.Dto.Program.Response;
using GAID.Api.Dto.User.Response;
using GAID.Domain.Models.Attachment;
using GAID.Domain.Models.Donation;
using GAID.Domain.Models.Enrollment;
using GAID.Domain.Models.Page;
using GAID.Domain.Models.Partner;
using GAID.Domain.Models.User;
using Newtonsoft.Json;

namespace GAID.Api.Configuration;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        //UserDTO mapping
        CreateMap<User, UserInfoResponse>();
        
        CreateMap<PartnerDetailRequest, Partner>();
        CreateMap<Partner, PartnerListingDto>();
        CreateMap<Partner, PartnerDetailDto>()
            .ForMember(x => x.CreatedById, opt => opt.MapFrom(src => src.CreatedBy.Id))
            .ForMember(x => x.CreatedByName, opt => opt.MapFrom(src => src.CreatedBy.FullName))
            .ForMember(x => x.ModifiedById, opt => opt.MapFrom(src => src.ModifiedBy.Id))
            .ForMember(x => x.ModifiedByName, opt => opt.MapFrom(src => src.ModifiedBy.FullName));

        CreateMap<ProgramDetailRequest, Domain.Models.Program.Program>();
        CreateMap<Domain.Models.Program.Program, ProgramListingDto>();
        CreateMap<Domain.Models.Program.Program, ProgramDetailDto>()
            .ForMember(x => x.DonationReason, opt => opt.MapFrom(src => JsonConvert.DeserializeObject<List<string>>(src.DonationReason)))
            .ForMember(x => x.CreatedById, opt => opt.MapFrom(src => src.CreatedBy.Id))
            .ForMember(x => x.CreatedByName, opt => opt.MapFrom(src => src.CreatedBy.FullName))
            .ForMember(x => x.ModifiedById, opt => opt.MapFrom(src => src.ModifiedBy.Id))
            .ForMember(x => x.ModifiedByName, opt => opt.MapFrom(src => src.ModifiedBy.FullName));
        
        CreateMap<Attachment, AttachmentDetailDto>()
            .ForMember(x => x.CreatedById, opt => opt.MapFrom(src => src.CreatedBy.Id))
            .ForMember(x => x.CreatedByName, opt => opt.MapFrom(src => src.CreatedBy.FullName))
            .ForMember(x => x.ModifiedById, opt => opt.MapFrom(src => src.ModifiedBy.Id))
            .ForMember(x => x.ModifiedByName, opt => opt.MapFrom(src => src.ModifiedBy.FullName));

        CreateMap<Page, PageDetailDto>()
            .ForMember(x => x.Content, opt => opt.MapFrom(src => HttpUtility.HtmlDecode(src.Content)))
            .ForMember(x => x.CreatedById, opt => opt.MapFrom(src => src.CreatedBy.Id))
            .ForMember(x => x.CreatedByName, opt => opt.MapFrom(src => src.CreatedBy.FullName))
            .ForMember(x => x.ModifiedById, opt => opt.MapFrom(src => src.ModifiedBy.Id))
            .ForMember(x => x.ModifiedByName, opt => opt.MapFrom(src => src.ModifiedBy.FullName));
        CreateMap<PageDetailRequest, Page>();

        CreateMap<Enrollment, EnrollmentListingDto>()
            .ForMember(x => x.CreatedById, opt => opt.MapFrom(src => src.CreatedBy.Id))
            .ForMember(x => x.CreatedByName, opt => opt.MapFrom(src => src.CreatedBy.FullName));
        CreateMap<DonationDetailRequest, Donation>();
        CreateMap<PaymentCreateRequest, Donation>();
        CreateMap<Donation, DonationDto>()
            .ForMember(x => x.CreatedById, opt => opt.MapFrom(src => src.CreatedBy.Id))
            .ForMember(x => x.CreatedByName, opt => opt.MapFrom(src => src.CreatedBy.FullName));
        CreateMap<Donation, DonationAdminDto>()
            .ForMember(x => x.ProgramId, opt => opt.MapFrom(src => src.Enrollment.Program.ProgramId))
            .ForMember(x => x.ProgramName, opt => opt.MapFrom(src => src.Enrollment.Program.Name))
            .ForMember(x => x.PartnerId, opt => opt.MapFrom(src => src.Enrollment.Program.Partner.PartnerId))
            .ForMember(x => x.PartnerName, opt => opt.MapFrom(src => src.Enrollment.Program.Partner.Name))
            .ForMember(x => x.CreatedByName, opt => opt.MapFrom(src => src.CreatedBy.FullName));
    }
}