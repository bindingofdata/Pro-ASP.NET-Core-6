using Microsoft.AspNetCore.Http.Features;

namespace Platform
{
    public sealed class ConsentMiddleware
    {
        private readonly RequestDelegate _next;

        public ConsentMiddleware(RequestDelegate nextDelegate)
        {
            _next = nextDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (string.Equals("/consent", context.Request.Path))
            {
                ITrackingConsentFeature? consentFeature = context.Features.Get<ITrackingConsentFeature>();
                if (consentFeature != null)
                {
                    if (!consentFeature.HasConsent)
                        consentFeature.GrantConsent();
                    else
                        consentFeature.WithdrawConsent();

                    await context.Response.WriteAsync(consentFeature.HasConsent ? "Consent granted" : "Consent withdrawn");
                }
            }
            else
                await _next(context);
        }
    }
}
