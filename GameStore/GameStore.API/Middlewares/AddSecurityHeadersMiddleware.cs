﻿namespace GameStore.API;

public class AddSecurityHeadersMiddleware
{
    private readonly RequestDelegate next;

    public AddSecurityHeadersMiddleware(RequestDelegate next)
    {
        this.next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var headers = context.Response.Headers;

        headers.Add("X-Frame-Options", "DENY");
        headers.Add("X-XSS-Protection", "1; mode=block");
        headers.Add("X-Content-Type-Options", "nosniff");
        headers.Add("Strict-Transport-Security", "max-age=31536000; includeSubDomains; preload");


        headers.Add("x-header-to-remove", "somevalue");


        await this.next(context);
    }
}
