﻿namespace Startly.Domain.DTOs.Base
{
    public class BaseResponse(string mensagem)
    {
        public string Mensagem { get; } = mensagem;
    }
}
