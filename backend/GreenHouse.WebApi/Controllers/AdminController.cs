using GreenHouse.Domain.Entities;
using GreenHouse.Domain.Exeptions;
using GreenHouse.Domain.Interfaces;
using GreenHouse.Domain.Services;
using GreenHouse.HttpModels.Requests;
using GreenHouse.HttpModels.Responses;
using GreenHouse.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GreenHouse.WebApi.Controllers
{
    [ApiController]
    [Route("admin")]
    public class AdminController : ControllerBase
    {
        private readonly AdminService _adminService;
        private readonly ITokenService _tokenService;
        public AdminController([FromServices] AdminService adminService, [FromServices] ITokenService tokenService)
        {
            _adminService = adminService ?? throw new ArgumentNullException(nameof(adminService));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        [HttpPost("registration")]
        public async Task<ActionResult<Account>> RegisterAccount(RegisterRequest request, CancellationToken token)
        {
            try
            {
                await _adminService.Register(request.Login, request.Password, token);
                return Ok();
            }
            catch (LoginAlreadyExistsExeption ex)
            {
                return Conflict(new ErrorResponse(ex.Message, System.Net.HttpStatusCode.Conflict));
            }
        }

        [HttpPost("authorisation")]
        public async Task<ActionResult<AuthorisationResponse>> AuthorisationAccount(AuthorisationRequest request, CancellationToken token)
        {
            try
            {
                var admin = await _adminService.Authorisation(request.Login, request.Password, token);
                var adminToken = _tokenService.GenerateToken(admin);
                return new AuthorisationResponse(admin.Id, admin.Login, adminToken);
            }
            catch (AdminNotFoundExeption ex)
            {
                return Conflict(new ErrorResponse(ex.Message, System.Net.HttpStatusCode.Conflict));
            }
            catch (InvalidPasswordException ex)
            {
                return Conflict(new ErrorResponse(ex.Message, System.Net.HttpStatusCode.Conflict));
            }
        }

        [Authorize]
        [HttpGet("current")]
        public async Task<ActionResult<AdminResponse>> GetCurrentAccount(CancellationToken cancellationToken)
        {
            var strId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var guid = Guid.Parse(strId);
            var account = await _adminService.GetAccountById(guid, cancellationToken);
            return new AdminResponse(account.Id, account.Login);
        }
    }
}
