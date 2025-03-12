
namespace Platform.Services
{
    public sealed class TextResponseFormatter : IResponseFormatter
    {
        private int _responseCounter;

        public async Task Format(HttpContext context, string content)
        {
            await context.Response
                .WriteAsync($"Response {++_responseCounter}:\n{content}");
        }
    }
}
