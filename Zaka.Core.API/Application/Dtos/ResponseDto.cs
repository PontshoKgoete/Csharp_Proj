namespace Zaka.Core.API.Application.Dtos
{
    public class ResponseDto
    {
        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public string Details { get; set; }
        public object Data { get; set; }
    }
}