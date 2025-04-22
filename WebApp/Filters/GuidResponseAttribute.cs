using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace WebApp.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public sealed class GuidResponseAttribute : Attribute, IAsyncAlwaysRunResultFilter, IFilterFactory
    {
        private int _counter = 0;
        private string _guid = Guid.NewGuid().ToString();

        public bool IsReusable => false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            return ActivatorUtilities.GetServiceOrCreateInstance<GuidResponseAttribute>(serviceProvider);
        }

        public Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            Dictionary<string, string> resultData;
            if (context.Result is ViewResult viewResult
                && viewResult.ViewData.Model is Dictionary<string,string> data)
            {
                resultData = data;
            }
            else
            {
                resultData = new();
                context.Result = new ViewResult()
                {
                    ViewName = "/Views/Shared/Message.cshtml",
                    ViewData = new ViewDataDictionary(
                        new EmptyModelMetadataProvider(),
                        new ModelStateDictionary())
                    {
                        Model = resultData
                    }
                };
            }

            while (resultData.ContainsKey($"Counter_{_counter}"))
            {
                _counter++;
            }
            resultData[$"Counter_{_counter}"] = _guid;
            return next();
        }
    }
}
