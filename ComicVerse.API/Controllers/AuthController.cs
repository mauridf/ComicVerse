using ComicVerse.Application.DTOs;
using ComicVerse.Application.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("registrar")]
    public async Task<ActionResult<UsuarioRespostaDto>> Registrar(UsuarioRegistroDto registroDto)
    {
        var resposta = await _authService.Registrar(registroDto);
        return Ok(resposta);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UsuarioRespostaDto>> Login(UsuarioLoginDto loginDto)
    {
        var resposta = await _authService.Login(loginDto);
        return Ok(resposta);
    }
}