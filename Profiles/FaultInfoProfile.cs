using AutoMapper;
using RMSPrivateServerAPI.DTOs;
using RMSPrivateServerAPI.Entities;
using System.Threading.Tasks;

namespace RMSPrivateServerAPI.Profiles;

/// <summary>
/// Mapping
/// </summary>
public class FaultInfoProfile : Profile
{
    /// <summary>
    /// Mapping POCO - DTO
    /// </summary>
    public FaultInfoProfile()
    {
        CreateMap<FaultInfoDto, FaultInfo>()
                .ForMember(f => f.FaultId, flt => flt.MapFrom(fDto => fDto.FaultId))
                .ForMember(f => f.Source, flt => flt.MapFrom(fDto => fDto.Source))
                .ForMember(f => f.SourceId, flt => flt.MapFrom(fDto => fDto.SourceId))
                .ForMember(f => f.Code,  flt => flt.MapFrom(fDto => fDto.Code))
                .ForMember(f => f.Title, flt => flt.MapFrom(fDto => fDto.Title))
                .ReverseMap();
    }    
}
