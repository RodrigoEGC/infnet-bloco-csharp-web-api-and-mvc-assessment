﻿namespace Domain.Model.Options
{
    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretKey { get; set; }
        public int ExpiryDays { get; set; }
    }
}
