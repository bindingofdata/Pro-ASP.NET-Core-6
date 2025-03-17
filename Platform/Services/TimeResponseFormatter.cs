

namespace Platform.Services
{
    public sealed class TimeResponseFormatter : IResponseFormatter
    {
        private readonly ITimeStamper _timeStamper;

        public TimeResponseFormatter(ITimeStamper timeStamper)
        {
            _timeStamper = timeStamper;
        }

        public async Task Format(HttpContext context, string content)
        {
            await context.Response.WriteAsync($"{_timeStamper.TimeStamp}: {content}");
        }
    }
}
