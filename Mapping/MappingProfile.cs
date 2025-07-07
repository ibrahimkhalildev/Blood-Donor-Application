using AutoMapper;
using BloodDonar.MVC.Models.Entities;
using BloodDonar.MVC.Models.ViewModel;

namespace BloodDonar.MVC.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BloodDonorListViewModel, BloodDonor>();
        }
    }
}
