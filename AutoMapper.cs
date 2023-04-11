using AutoMapper;
using InsureityAPI.Controllers;
using InsureityAPI.Models;

namespace InsureityAPI
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<LoginDetails, LoginDTO>().ReverseMap();
        }

    }
}
