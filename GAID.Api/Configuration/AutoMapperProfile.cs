using AutoMapper;
using GAID.Api.Dto.Attachment;
using GAID.Api.Dto.Page.Request;
using GAID.Api.Dto.Page.Response;
using GAID.Api.Dto.Partner.Request;
using GAID.Api.Dto.Partner.Response;
using GAID.Api.Dto.Program.Request;
using GAID.Api.Dto.Program.Response;
using GAID.Domain.Models.Attachment;
using GAID.Domain.Models.Donation;
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
            .ForMember(x => x.CreatedById, opt => opt.MapFrom(src => src.CreatedBy.Id))
            .ForMember(x => x.CreatedByName, opt => opt.MapFrom(src => src.CreatedBy.FullName))
            .ForMember(x => x.ModifiedById, opt => opt.MapFrom(src => src.ModifiedBy.Id))
            .ForMember(x => x.ModifiedByName, opt => opt.MapFrom(src => src.ModifiedBy.FullName));
        CreateMap<PageDetailRequest, Page>();

        CreateMap<DonationDetailRequest, Donation>();
    }
}