using AutoMapper;
using Domain.Entities;
using Util.Models;
using Util.DTO;
using Domain.Entities.DTO;
using System.Net.Sockets;
using System.IO;
using Service.IServices;
using System;

namespace API.Helpers
{
    public class MappingProfile : Profile
    {
        string ImgDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), GlobalConstants.ImgsUploadPath);
        public MappingProfile()
        {
            CreateMap(typeof(PagedResponse<>), typeof(PagedResponseDTO<>)).ReverseMap();
            CreateMap<SocailMediaDto, SocailMedia>().ReverseMap();
            CreateMap<AddContractDto,Contract>().ReverseMap();
            
            
            CreateMap<Contract,ReturnedContract>()
                .ForMember(dest => dest.ContractItems, opt => opt.MapFrom(src => src.Items))
                .ReverseMap();

            CreateMap<PersonDto, Person>()
                .ReverseMap();
            CreateMap<talentDto, Talent>().ReverseMap();

            CreateMap<Media,MediaDto> ()
                .ForMember(dest => dest.content, opt => opt.MapFrom(src => File.ReadAllBytes(Path.Combine(ImgDirectoryPath, src.Path))))
                .ReverseMap();

            CreateMap<Talent,ReturnedTalent>()
                .ForMember(dest => dest.Name,opt => opt.MapFrom(src => src.Person.Name))
                .ForMember(dest => dest.IsApproved, opt => opt.MapFrom(src => src.Person.IsApproved))
                .ForMember(dest => dest.Medias, opt => opt.MapFrom(src => src.Person.Medias))
                .ForMember(dest => dest.Img, opt => opt.MapFrom(src => File.ReadAllBytes(Path.Combine(ImgDirectoryPath, src.Person.ImgPath))))
                .ReverseMap();

            CreateMap<Star, ReturnedStar>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Person.Name))
                .ForMember(dest => dest.IsApproved, opt => opt.MapFrom(src => src.Person.IsApproved))
                .ForMember(dest => dest.Img, opt => opt.MapFrom(src => src.Person.ImgPath))
                .ReverseMap();

            CreateMap<Preference,PreferenceDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Person.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Person.Name))
                .ForMember(dest => dest.Img, opt => opt.MapFrom(src => (src.Person.PersonType == PersonType.Talent) ? Convert.ToBase64String(File.ReadAllBytes(Path.Combine(ImgDirectoryPath, src.Person.ImgPath))) : src.Person.ImgPath))
                .ReverseMap();

        }

    }
}
