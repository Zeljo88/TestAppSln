using AutoMapper;
using Phoenix.Heaven.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Mappings
{
    public class AwsMappingProfile : Profile
    {
        public AwsMappingProfile()
        {
            CreateMap<VM, Models.VM>()
                .ForPath(dest => dest.InstanceId, opt => opt.MapFrom(src => src.VmId))
                .ForPath(dest => dest.InstanceType, opt => opt.MapFrom(src => src.VMType))
                .ForPath(dest => dest.State, opt => opt.MapFrom(src => src.Status.ToString())); 
        }
    }
}
