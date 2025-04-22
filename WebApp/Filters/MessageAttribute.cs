using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.VisualBasic;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApp.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public sealed class MessageAttribute : Attribute, IAsyncAlwaysRunResultFilter, IOrderedFilter
    {
        private int _counter = 0;
        private string _message;

        public int Order { get; set; }

        public MessageAttribute(string message) => _message = message;

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            Dictionary<string, string> resultData;
            if (context.Result is ViewResult viewResult
                && viewResult.ViewData.Model is Dictionary<string, string> data)
            {
                resultData = data;
            }
            else
            {
                resultData = new Dictionary<string, string>();
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

            while (resultData.ContainsKey($"Message_{_counter}"))
            {
                _counter++;
            }
            resultData[$"Message_{_counter}"] = _message;
            await next();
        }
    }
}
