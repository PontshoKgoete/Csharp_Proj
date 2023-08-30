using Twilio;
using Twilio.Rest.Verify.V2.Service;
using Zaka.Core.API.Application.Dtos;
using Zaka.Core.API.Application.Interfaces;

namespace Zaka.Core.API.Integration.Twilio
{
    public class TwilioService : ITwilioService
    {
        private readonly IConfiguration _config;
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _verifyServiceSid;
        public TwilioService(IConfiguration config)
        {
            _config = config;
            _accountSid = _config.GetSection("Twilio:TWILIO_ACCOUNT_SID").Value;
            _authToken = _config.GetSection("Twilio:TWILIO_AUTH_TOKEN").Value;
            _verifyServiceSid = _config.GetSection("Twilio:VERIFY_SERVICE_SID").Value;
            TwilioClient.Init(_accountSid, _authToken);
        }

        public async Task<ResponseDto> GenerateOtp(GenerateOtpDto generateOtp)
        {
            ResponseDto response = new();
            try
            {
                response.Data = await VerificationResource.CreateAsync(
                to: generateOtp.PhoneNumber,
                channel: "sms",
                pathServiceSid: _verifyServiceSid);
                response.StatusCode = 200;
                response.StatusDescription = "OTP Generated";
            }
            catch (Exception ex)
            {
                response.StatusCode = 400;
                response.Details = ex.Message;
            }

            return response;
        }

        public async Task<ResponseDto> ValidateOtp(ValidateOtpDto validateOtp)
        {
            ResponseDto response = new();
            try
            {
                response.Data = await VerificationCheckResource.CreateAsync(
                to: validateOtp.PhoneNumber,
                code: validateOtp.Otp,
                pathServiceSid: _verifyServiceSid);
                response.StatusCode = 200;
                response.StatusDescription = "OTP Validated";
            }
            catch (Exception ex)
            {
                response.StatusCode = 400;
                response.Details = ex.Message;
            }

            return response;
        }
    }
}