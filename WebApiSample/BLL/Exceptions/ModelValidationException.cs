using FluentValidation.Results;

namespace WebApiSample.BLL.Exceptions
{
    public class ModelValidationException : Exception
    {
        public List<string> ValdationErrors { get; set; }

        public ModelValidationException(ValidationResult validationResult)
        {
            ValdationErrors = new List<string>();

            foreach (var validationError in validationResult.Errors)
            {
                ValdationErrors.Add(validationError.ErrorMessage);
            }
        }
    }
}
