using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Zaka.Core.API.Application.Dtos;
using Zaka.Core.API.Application.Interfaces;

namespace Zaka.Core.API.Controllers
{
    [EnableCors("_zaka_cors_policy")]
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accounService;
        private readonly ITwilioService _twilioService;

        public AccountController(ILogger<AccountController> logger
        , IAccountService accounService
        , ITwilioService twilioService)
        {
            _twilioService = twilioService;
            _accounService = accounService;
            _logger = logger;
        }


        [HttpGet("{phoneNumber}")]
        public async Task<IActionResult> GetAccount(string phoneNumber)
        {
            var response = await _accounService.GetAccount(phoneNumber);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(CreateAccountDto model)
        {
            var response = await _accounService.CreateAccount(model);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        public IActionResult UpdateAccount()
        {
            return StatusCode(200, null);
        }

        [HttpPost("wallet/top-up")]
        public async Task<IActionResult> TopUpWallet(TopUpWalletDto model)
        {
            var response = await _accounService.TopUpWallet(model);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("send-money")]
        public async Task<IActionResult> SendMoney(SendMoneyDto model)
        {
            var response = await _accounService.SendMoney(model);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("generate-otp")]
        public async Task<IActionResult> GenerateOtp(GenerateOtpDto model)
        {
            var response = await _twilioService.GenerateOtp(model);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("validate-otp")]
        public async Task<IActionResult> ValidateOtp(ValidateOtpDto model)
        {
            var response = await _twilioService.ValidateOtp(model);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var response = await _accounService.Login(model);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("update-credentials")]
        public async Task<IActionResult> UpdateCredentials(UpdateCredentialsDto model)
        {
            var response = await _accounService.UpdateCredentials(model);
            return StatusCode(response.StatusCode, response);
        }

    }
}