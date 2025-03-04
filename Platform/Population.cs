namespace Platform
{
    public sealed class Population
    {
        public static async Task Endpoint(HttpContext context)
        {
            string? city = context.Request.RouteValues["city"] as string;
            int? pop = null;
            switch ((city ?? string.Empty).ToLower())
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
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            }
        }
    }
}
