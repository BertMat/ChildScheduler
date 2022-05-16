using Application.Dto;
using Application.Dto.Requests;
using Application.Dto.Responses;
using Application.Interfaces;
using ChildSchedulerAPI.Configuration;
using ChildSchedulerAPI.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChildSchedulerAPI.Controllers
{
    public class UserController : BaseController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtConfig _jwtConfig; 
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly SchedulerContext _context;
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMailService _mailService;

        public UserController(UserManager<IdentityUser> userManager, 
            IOptionsMonitor<JwtConfig> optionsMonitor, TokenValidationParameters tokenValidationParameters,
            ICurrentUserService currentUserService,
            SchedulerContext context,
            IUserService userService,
            IMailService mailService)
        {
            _context = context;
            _userService = userService;
            _mailService = mailService;
            _currentUserService = currentUserService;
            _tokenValidationParameters = tokenValidationParameters;
            _userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
        }


        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] UserChangeDataRequest userRequest)
        {
            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            var user = _context.Users.FirstOrDefault(p => p.Id == userId);
            if (user == null)
                return BadRequest();
            var result = await _userManager.ChangePasswordAsync(user, userRequest.Password, userRequest.NewPassword);
            if (result.Succeeded)
                return Ok(new { result = "Pomyślnie zmieniono hasło" });
            return BadRequest(new { result = "Błąd podczas zmiany hasła" });
        }

        [HttpPost]
        [Route("SendEmailToken")]
        public async Task SendEmailTokenAsync(string newEmail)
        {
            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            var user = _context.Users.FirstOrDefault(p => p.Id == userId);
            var token = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);

            var link = Url.Action(nameof(ChangeUserEmailAsync), "User", new { userId = userId, newEmail = newEmail, token = token }, Request.Scheme, Request.Host.ToString());
            //var link = Url.Action(nameof(VerifyEmail), "User", new { userId = userId, code = token }, Request.Scheme, Request.Host.ToString());

            MailClass mailClass = new MailClass { Subject = "Zmiana adresu e-mail", ToMailIds = new List<string>() { user.Email }, Body = _mailService.GetChangeEmailBody(link, newEmail) };
            await _mailService.SendMail(mailClass);
        } 

        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto user)
        {
            if(ModelState.IsValid)
            {
                // We can utilise the model
                var existingUser = await _userManager.FindByEmailAsync(user.Email);

                if(existingUser != null)
                {
                    return BadRequest(new RegistrationResponse(){
                            Errors = new List<string>() {
                                "Email already in use"
                            },
                            Success = false
                    });
                }

                var newUser = new IdentityUser() { Email = user.Email, UserName = user.Email};
                var isCreated = await _userManager.CreateAsync(newUser, user.Password);
                if(isCreated.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

                    var link = Url.Action(nameof(VerifyEmail), "User", new { userId = newUser.Id, code }, Request.Scheme, Request.Host.ToString());

                    MailClass mailClass = new MailClass { Subject = "Potwierdzenie utworzenia konta", ToMailIds = new List<string>() { newUser.Email }, Body = _mailService.GetMailBody(link, newUser.Email) };
                    await _mailService.SendMail(mailClass);


                   return Ok(new RegistrationResponse()
                   {

                       Success = true,
                       Errors = new List<string>() {  link},
                       Code = code,
                   });
                } else {
                    return BadRequest(new RegistrationResponse(){
                            Errors = isCreated.Errors.Select(x => x.Description).ToList(),
                            Success = false
                    });
                }
            }

            return BadRequest(new RegistrationResponse(){
                    Errors = new List<string>() {
                        "Invalid payload"
                    },
                    Success = false
            });
        }
        [HttpPost()]
        [Route("Invitation")]
        public async Task<IActionResult> SendInvitation([FromBody]string email)
        {

            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            if (string.IsNullOrEmpty(userId))
                return BadRequest(new { Response = "Brak użytkownika" });

            var inviter = _context.People.Include(p => p.Family).FirstOrDefault(p => p.UserId ==  userId);

            var user = await _context.Users.SingleOrDefaultAsync(p => p.Email == email);
            if (user == null)
            {
                return BadRequest(new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        "Brak takiego użytkownika"
                    },
                    Success = false
                });
            }
            var person = await _context.People.SingleOrDefaultAsync(p => p.UserId == user.Id);
            if(person == null)
            {
                return BadRequest(new { Response = "Użytkownik nie posiada pełnego konta."});
            }
            var invitation = await _context.FamilyInvites
                .FirstOrDefaultAsync(p => p.FamilyId == inviter.FamilyId.GetValueOrDefault() && p.PersonId == person.PersonId);
            
            if(invitation != null)
            {
                _context.FamilyInvites.Remove(invitation);
                _context.SaveChanges();
            }

            var code = _userService.RandomString(30);
            _context.FamilyInvites.Add(new FamilyInvites { FamilyId = inviter.FamilyId.GetValueOrDefault(), PersonId = person.PersonId, InviteKey = code });
            _context.SaveChanges();

            var link = Url.Action("JoinToFamily", "Families", new { key = code }, Request.Scheme, Request.Host.ToString());

            MailClass mailClass = new MailClass { Subject = "Zaproszenie do rodziny", ToMailIds = new List<string>() { email }, Body = _mailService.GetInvitationBody(link, email, inviter.Family.FamilyName) };
            await _mailService.SendMail(mailClass);


            return Ok(new 
            {
                invitationCode = code,
                Success = true
            });
        }

        [HttpGet()]
        [Route("ChangeEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> ChangeUserEmailAsync(string userId, string newEmail, string token) 
        {
            var user = _context.Users.FirstOrDefault(p => p.Id == userId);
            if (user == null)
                return BadRequest();


            await _userManager.ChangeEmailAsync(user, newEmail, token);

            return Ok();
        }
        [HttpGet()]
        [Route("Verify")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyEmail(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return BadRequest();
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                return Ok($"Potwierdzono założenie konta. Można zalogować się do aplikacji oraz utworzyć profil użytkownika.");
            }

            return BadRequest();
        }

        private async Task<AuthResult> VerifyToken(TokenRequest tokenRequest)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            try
            {
                // This validation function will make sure that the token meets the validation parameters
                // and its an actual jwt token not just a random string
                var principal = jwtTokenHandler.ValidateToken(tokenRequest.Token, _tokenValidationParameters, out var validatedToken);

                // Now we need to check if the token has a valid security algorithm
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

                    if (result == false)
                    {
                        return null;
                    }
                }

                // Will get the time stamp in unix time
                var value = principal.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp)?.Value;
                if (value != null)
                {
                    var utcExpiryDate = long.Parse(value);

                    // we convert the expiry date from seconds to the date
                    var expDate = UnixTimeStampToDateTime(utcExpiryDate);

                    if (expDate > DateTime.UtcNow)
                    {
                        return new AuthResult()
                        {
                            Errors = new List<string>() { "We cannot refresh this since the token has not expired" },
                            Success = false
                        };
                    }
                }

                // Check the token we got if its saved in the db
                var storedRefreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == tokenRequest.RefreshToken);

                if (storedRefreshToken == null)
                {
                    return new AuthResult()
                    {
                        Errors = new List<string>() { "refresh token doesnt exist" },
                        Success = false
                    };
                }

                // Check the date of the saved token if it has expired
                if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
                {
                    return new AuthResult()
                    {
                        Errors = new List<string>() { "token has expired, user needs to relogin" },
                        Success = false
                    };
                }

                // check if the refresh token has been used
                if (storedRefreshToken.IsUsed)
                {
                    return new AuthResult()
                    {
                        Errors = new List<string>() { "token has been used" },
                        Success = false
                    };
                }

                // Check if the token is revoked
                if (storedRefreshToken.IsRevoked)
                {
                    return new AuthResult()
                    {
                        Errors = new List<string>() { "token has been revoked" },
                        Success = false
                    };
                }

                // we are getting here the jwt token id
                var jti = principal.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)?.Value;

                // check the id that the recieved token has against the id saved in the db
                if (storedRefreshToken.JwtId != jti)
                {
                    return new AuthResult()
                    {
                        Errors = new List<string>() { "the token doenst mateched the saved token" },
                        Success = false
                    };
                }

                storedRefreshToken.IsUsed = true;
                _context.RefreshTokens.Update(storedRefreshToken);
                await _context.SaveChangesAsync();

                var dbUser = await _userManager.FindByIdAsync(storedRefreshToken.UserId);
                return await GenerateJwtToken(dbUser);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToUniversalTime();
            return dtDateTime;
        }
        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequest tokenRequest)
        {
            if (ModelState.IsValid)
            {
                var res = await VerifyToken(tokenRequest);

                if (res == null)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                    "Invalid tokens"
                },
                        Success = false
                    });
                }

                return Ok(res);
            }

            return BadRequest(new RegistrationResponse()
            {
                Errors = new List<string>() {
                "Invalid payload"
            },
                Success = false
            });
        }
        private async Task<AuthResult> GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(180), // 5-10 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            var refreshToken = new RefreshToken()
            {
                JwtId = token.Id,
                IsUsed = false,
                IsRevoked = false,
                UserId = user.Id,
                AddedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6),
                Token = _userService.RandomString(35) + Guid.NewGuid()
            };

            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            return new AuthResult()
            {
                Token = jwtToken,
                Success = true,
                RefreshToken = refreshToken.Token
            };
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);

                if (existingUser == null)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                                "Invalid login request"
                            },
                        Success = false
                    });
                }

                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);

                if (!isCorrect)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                                "Invalid login request"
                            },
                        Success = false
                    });
                }

                var isActivated = await _userManager.IsEmailConfirmedAsync(existingUser);
                if (!isActivated)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                                "Konto nie jest aktywowane"
                            },
                        EmailConfirmed = false,
                        Success = false,
                    });
                }

                var jwtToken = await GenerateJwtToken(existingUser);
                HttpContext.Session.SetString("token", jwtToken.RefreshToken);
                return Ok(jwtToken);
            }

            return BadRequest(new RegistrationResponse()
            {
                Errors = new List<string>() {
                        "Invalid payload"
                    },
                Success = false
            });
        }
        [HttpGet]
        [Route("Authorization")]
        public async Task CheckAuthorization()
        {
        }
    }
}
