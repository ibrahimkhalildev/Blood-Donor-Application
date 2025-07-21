using AutoMapper;
using BloodDonar.MVC.Models.Entities;
using BloodDonar.MVC.Models.ViewModel;
using BloodDonar.MVC.Services.Implementations;
using BloodDonar.MVC.Utilities;

namespace BloodDonar.MVC.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BloodDonor, BloodDonorListViewModel>()
                .ForMember(dest => dest.BloodGroup, opt => opt.MapFrom(src => src.BloodGroup.ToString()))
                .ForMember(dest => dest.LastDonationDate, opt => opt.MapFrom(src => DateHelper.GetLastDonationDateString(src.LastDonationDate)))
                .ForMember(dest => dest.isEligible, opt => opt.MapFrom(src => BloodDonorService.IsEligible(src)))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => DateHelper.CalculateAge(src.DateOFBirth)));
        }
    }
}
