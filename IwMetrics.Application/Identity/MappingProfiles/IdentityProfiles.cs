using AutoMapper;
using IwMetrics.Application.Identity.Dtos;
using IwMetrics.Domain.Aggregates.UserProfileAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwMetrics.Application.Identity.MapingProfiles
{
    public class IdentityProfiles : Profile
    {
        public IdentityProfiles()
        {
            CreateMap<UserProfile, IdentityUserProfileDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.BasicInfo.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.BasicInfo.LastName))
                .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.BasicInfo.EmailAddress))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.BasicInfo.Phone))
                .ForMember(dest => dest.CurrentCity, opt => opt.MapFrom(src => src.BasicInfo.CurrentCity));
        }


    }
}