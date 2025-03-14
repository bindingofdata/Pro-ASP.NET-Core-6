﻿

namespace Platform.Services
{
    public sealed class GuidService : IResponseFormatter
    {
        private Guid guid  = Guid.NewGuid();

        public async Task Format(HttpContext context, string content)
        {
            await context.Response.WriteAsync($"Guid: {guid}\n{content}");
        }
    }
}
