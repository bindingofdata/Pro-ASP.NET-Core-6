using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApp.Validation
{
    public static class ModelStateExtensions
    {
        public static void PromotePropertyErrors(this ModelStateDictionary modelState, string propertyName)
        {
            foreach (KeyValuePair<string, ModelStateEntry> errors in modelState)
            {
                if (errors.Key == propertyName
                    && errors.Value.ValidationState == ModelValidationState.Invalid)
                {
                    foreach (ModelError error in errors.Value.Errors)
                    {
                        modelState.AddModelError(string.Empty, error.ErrorMessage);
                    }
                }
            }
        }
    }
}
