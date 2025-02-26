namespace Platform
{
    public sealed class Capital
    {
        private RequestDelegate? _next;

        public Capital() { }

        public Capital(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string[] parts = context.Request.Path.ToString()
                .Split("/", StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 2 && parts[0] == "capital")
            {
                string? capital = null;
                string country = parts[1];
                switch (country.ToLower())
                {
                    case "uk":
                        capital = "London";
                        break;
                    case "france":
                        capital = "Paris";
                        break;
                    case "monoco":
                        context.Response.Redirect($"/population/{country}");
                        return;
                }
                if (capital != null)
                {
                    await context.Response.WriteAsync($"{capital} is the capital of {country}");
                    return;
                }
            }
            if (_next != null)
            {
                await _next(context);
            }
        }
    }
}
