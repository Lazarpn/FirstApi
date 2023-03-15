using FirstApi.Dtos.Character;
using FirstApi.Models;
using FirstApi.Services.CharacterService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FirstApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CharacterController : ControllerBase
{
    private readonly ICharacterService characterService;

    public CharacterController(ICharacterService characterService)
    {
        this.characterService = characterService;
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get()
    {
        return Ok(await characterService.GetAllCharacter());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingle(int id)
    {
        return Ok(await characterService.GetCharacterById(id));
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto newCharacter)
    {
      
        return Ok(await characterService.AddCharacter(newCharacter));
    }

    [HttpPut]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
    {
        var response = await characterService.UpdateCharacter(updatedCharacter);
        if(response.Data == null)
        {
            return NotFound(response);
        }
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> DeleteCharacter(int id)
    {
        var response = await characterService.DeleteCharacter(id);
        if (response.Data == null)
        {
            return NotFound(response);
        }
        return Ok();
    }

}