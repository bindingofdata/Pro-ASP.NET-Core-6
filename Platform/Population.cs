namespace Platform
{
    public sealed partial class Population
    {
        public static async Task Endpoint(HttpContext context, ILogger<Population> logger)
        {
            StartingResponse(logger, context.Request.Path);
            string city = context.Request.RouteValues["city"] as string ?? "london";
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
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            }
            logger.LogDebug($"Finished processing for {context.Request.Path}");
        }

        [LoggerMessage(0, LogLevel.Debug, "Starting response for {path}")]
        public static partial void StartingResponse(ILogger logger, string path);
    }
}