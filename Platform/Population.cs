namespace Platform
{
    public sealed class Population
    {
        private RequestDelegate? _next;

        public Population() { }

        public Population(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string[] parts = context.Request.Path.ToString()
                .Split("/", StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 2 && parts[0] == "population")
            {
                string city = parts[1];
                int? pop = null;
                switch (city.ToLower())
                {
                    case "london":
                        pop = 8_136_000;
                        break;
                    case "paris":
                        pop = 2_141_000;
                        break;
                    case "monoco":
                        pop = 39_000;
                        break;
                }
                if (pop.HasValue)
                {
                    await context.Response.WriteAsync($"{city} has a population of {pop:N0}");
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
