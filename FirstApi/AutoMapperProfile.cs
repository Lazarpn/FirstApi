using AutoMapper;
using FirstApi.Dtos.Character;
using FirstApi.Models;

namespace FirstApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto, Character>();
            CreateMap<UpdateCharacterDto, Character>();

        }
    }
}
