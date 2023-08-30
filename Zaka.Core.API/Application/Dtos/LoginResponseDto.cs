namespace Zaka.Core.API.Application.Dtos
{
    public class LoginResponseDto
    {
        public string AccessToken { get; set; }
        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public string Details { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }
    }
}