﻿using Platform.Services;

namespace Platform
{
    public sealed class WeatherEndpoint
    {
        //private readonly IResponseFormatter _formatter;

        //public WeatherEndpoint(IResponseFormatter formatter)
        //{
        //    _formatter = formatter;
        //}

        public async Task Endpoint(HttpContext context, IResponseFormatter formatter)
        {
            await formatter.Format(context, "Endpoint Class: It is cloudy in Milan");
        }
    }
}
