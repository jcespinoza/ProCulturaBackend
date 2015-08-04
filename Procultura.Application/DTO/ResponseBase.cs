namespace Procultura.Application.DTO
{
    using System;

    public class ResponseBase
    {
        public Exception Exception { get; set; }
        public string Message { get; set; }
    }
}