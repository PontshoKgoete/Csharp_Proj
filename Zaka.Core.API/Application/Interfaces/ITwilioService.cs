using Twilio.Rest.Verify.V2.Service;
using Zaka.Core.API.Application.Dtos;

namespace Zaka.Core.API.Application.Interfaces
{
    public interface ITwilioService
    {
        Task<ResponseDto> GenerateOtp(GenerateOtpDto generateOtp);
        Task<ResponseDto> ValidateOtp(ValidateOtpDto validateOtp);
    }
}