using AutoMapper;
using VillaProject_WEB.Models.DTO;

namespace VillaProject_WEB
{
	public class MappingConfig : Profile
	{
		public MappingConfig() 
		{
			CreateMap<VillaDTO, VillaUpdateDTO>().ReverseMap();
			CreateMap<VillaDTO, VillaCreateDTO>().ReverseMap();

			CreateMap<VillaNumberDTO, VillaNumberCreateDTO>().ReverseMap();
			CreateMap<VillaNumberDTO, VillaNumberUpdateDTO>().ReverseMap();
		}
	}
}
