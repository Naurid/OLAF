using DisneyBattle.WebAPI.Infrastructure;
using DisneyBattle.WebAPI.Mappers;
using DisneyBattle.WebAPI.Models;
using DisneyBattle.WebAPI.Models.Dto;
using DisneyBattle.WebAPI.Services.IServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace DisneyBattle.WebAPI.Controllers
{

    [ApiController]
        [Route("[controller]")]
        public class AccountController : Controller
        {
            private readonly JwtOptions _jwtOption;
            private readonly IUserService _userService; 

        public AccountController(JwtOptions jwtoption, IUserService userService )
            {
                _jwtOption = jwtoption;
                _userService = userService; 
            }


            [HttpPost("login")]
            public IActionResult Login(LoginModel lm)
            {
                if (lm == null)
                {
                    return new NotFoundResult();
                }
                if (!ModelState.IsValid)
                {
                    return new BadRequestObjectResult(ModelState);
                }
                UtilisateurModel? u = _userService.Authenticate(lm.Pseudo, lm.Password);
                if (u == null)
                {
                    return new BadRequestObjectResult(lm);
                }

            //UserDto user = _mapper.Map<UserDto>(u);
            // string leTokenArenvoyer = JwtManager.GenerateToken(_jwtOption, user);


            //Sauvegarder dans La DB
            //_userService.UpdateAsync(_mapper.Map<UtilisateurModel>(user));



            //return Ok(new JwtResponse { Access_Token = leTokenArenvoyer, Refresh_Token = user.RefreshToken });
            return Ok();

            }


            [HttpPost("register")]
            public async Task<IActionResult> Register(UserForm lm)
            {
                if (lm == null)
                {
                    return new NotFoundResult();
                }
                if (!ModelState.IsValid)
                {
                    return new BadRequestObjectResult(ModelState);
                }

                try
                {
                    //Sauvegarder dans La DB
                    //await _userService.AddAsync(_mapper.Map<UtilisateurModel>(lm));

                    return NoContent();
                }
                catch (Exception ex)
                {
                    return UnprocessableEntity("Impossible de traiter la demande.");
                }

            }
            [HttpPost("logout")]
            [Authorize]
            public async Task<IActionResult> Logout()
            {
                try
                {
                    // Récupérer l'ID de l'utilisateur depuis les claims
                    var userId = User.FindFirst(ClaimTypes.Sid)?.Value;
                    if (string.IsNullOrEmpty(userId))
                    {
                        return BadRequest("Utilisateur non identifié");
                    }

                    // Récupérer l'utilisateur
                    var user = await _userService.GetByIdAsync(int.Parse(userId));
                    if (user == null)
                    {
                        return NotFound("Utilisateur non trouvé");
                    }

                    // Invalider le refresh token
                    //user.RefreshToken = null;
                    //user.AccessToken = null;

           
                    await _userService.UpdateAsync(user);
                    await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                return Ok(new { message = "Déconnexion réussie" });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "Une erreur est survenue lors de la déconnexion");
                }
            }

            [HttpPost("refresh")]
            public IActionResult Refresh(JwtResponse tokenResponse)
            {
                List<Claim> mesClaims = null;
                //Récupération des claims du user (idéalement dans une db car normalement l'user n'existe plus si j'ai besoin 
                // de faire un refresh
                if (User != null)
                {
                    mesClaims = User.Claims.ToList();

                }
                else
                {
                    //rechercher les rôles dans
                }

                //Vérification si le token correspond
                if (_userService.Checkrefresh(tokenResponse.Access_Token, tokenResponse.Refresh_Token))
                {
                    string newAccessToken = JwtManager.GenerateAccessTokenFromRefreshToken(tokenResponse.Refresh_Token, _jwtOption, mesClaims);

                    JwtResponse response = new JwtResponse
                    {
                        Access_Token = newAccessToken,
                        Refresh_Token = JwtManager.GenerateRefreshToken() // Return the same refresh token
                    };
                    //On doit ici resauvegarder en db les infos (ici ce n'est pas fait car pas de db mais une liste statique)

                    return Ok(response);
                }
                else
                {
                    return BadRequest();
                }


            }
        }

    public class UserForm
    {
    }
}
