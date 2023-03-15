using AutoMapper;
using FirstApi.Data;
using FirstApi.Dtos.Character;
using FirstApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstApi.Services.CharacterService;

public class CharacterService : ICharacterService
{
    //private static List<Character> characters = new List<Character>
    //{
    //    new Character(),
    //    new Character { Id = 1, Name = "Sam" }
    //};
    private readonly IMapper mapper;
    private readonly DataContext context;

    public CharacterService(IMapper mapper, DataContext context)
    {
        this.mapper = mapper;
        this.context = context;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
       
        this.context.Add(mapper.Map<Character>(newCharacter));
        await this.context.SaveChangesAsync();
        serviceResponse.Data = await this.context.Characters.Select(c => mapper.Map<GetCharacterDto>(c)).ToListAsync();
        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

        try
        {
            var character = await this.context.Characters.FirstOrDefaultAsync(c => c.Id == id);

            if (character == null)
            {
                throw new Exception($"Character with Id '{id}' not found");
            }

             this.context.Characters.Remove(character);
            await this.context.SaveChangesAsync();

            serviceResponse.Data = await this.context.Characters.Select(c => mapper.Map<GetCharacterDto>(c)).ToListAsync();
        }
        catch (Exception ex)
        {

            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }


        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacter()
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        var dbCharacters = await this.context.Characters.ToListAsync();
        serviceResponse.Data = dbCharacters.Select(c => mapper.Map<GetCharacterDto>(c)).ToList() ;
        return serviceResponse;
    }

    

    public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
    {
        
        var serviceResponse = new ServiceResponse<GetCharacterDto>();
        var dbCharacter = await this.context.Characters.FirstOrDefaultAsync(character => character.Id == id);
  
        serviceResponse.Data = mapper.Map<GetCharacterDto>(dbCharacter);
        
        
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
    {
        var serviceResponse = new ServiceResponse<GetCharacterDto>();

        try
        {
            var character = await this.context.Characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);

            if(character == null)
            {
                throw new Exception($"Character with Id '{updatedCharacter.Id}' not found");
            }

            mapper.Map<Character>(updatedCharacter);

            character.Name = updatedCharacter.Name;
            character.HitPoints = updatedCharacter.HitPoints;
            character.Strength = updatedCharacter.Strength;
            character.Defence = updatedCharacter.Defence;
            character.Intelligence = updatedCharacter.Intelligence;
            character.Class = updatedCharacter.Class;

            await this.context.SaveChangesAsync();

            serviceResponse.Data = mapper.Map<GetCharacterDto>(character);
        }
        catch (Exception ex)
        {

            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
       

        return serviceResponse;



    }
}