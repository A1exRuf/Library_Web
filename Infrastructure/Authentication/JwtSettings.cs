﻿namespace Infrastructure.Authentication;

public class JwtSettings
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int AccessTokenLifetime { get; set; }
    public string Key { get; set; }
}
