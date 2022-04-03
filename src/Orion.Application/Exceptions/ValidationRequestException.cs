using Orion.Application.Exceptions;
using FluentValidation.Results;

namespace Orion.Application.Exceptions
{
    public class ValidationRequestException : BaseRequestException
    {
        private readonly Dictionary<string, object> _errors;
        public override IReadOnlyDictionary<string, object> Errors => _errors;

        private ValidationRequestException() : base("Some fields are invalid") => _errors = new Dictionary<string, object>();

        public ValidationRequestException(IEnumerable<ValidationFailure> failures) : this()
        {
            foreach (var validationFailure in failures.GroupBy(e => e.PropertyName))
            {
                if (validationFailure.Key.Contains("."))
                {
                    var childProps = validationFailure.Key.Split('.').ToArray();

                    if (_errors.ContainsKey(childProps.First()))
                    {
                        AddPropertyErrorRecursive(childProps[1], _errors[childProps.First()] as IDictionary<string, object>, childProps, validationFailure.Select(e => e.ErrorMessage).ToList());
                    }
                    else
                    {
                        _errors.Add(childProps.First(), RecursiveSetPropertyError(childProps[1], childProps, validationFailure.Select(e => e.ErrorMessage).ToList()));
                    }
                }
                else
                {
                    _errors.Add(validationFailure.Key, validationFailure.Select(e => e.ErrorMessage).ToList());
                }
            }
        }

        private void AddPropertyErrorRecursive(string currentPropName, IDictionary<string, object> commandPropertyError, string[] childProps, List<string> errorsMessage, int propIndexMessage = 1)
        {
            if (commandPropertyError == null)
                commandPropertyError = new Dictionary<string, object>();

            if (commandPropertyError.ContainsKey(currentPropName))
            {
                AddPropertyErrorRecursive(childProps[propIndexMessage + 1], commandPropertyError[currentPropName] as IDictionary<string, object>, childProps, errorsMessage, propIndexMessage + 1);
                return;
            }

            if (currentPropName == childProps.LastOrDefault())
                commandPropertyError.Add(currentPropName, errorsMessage);
            else
            {
                commandPropertyError.Add(currentPropName,
                    RecursiveSetPropertyError(childProps[propIndexMessage + 1], childProps, errorsMessage, propIndexMessage + 1));
            }
        }

        private IDictionary<string, object> RecursiveSetPropertyError(string currentPropName, string[] childProps, List<string> errorsMessage, int propIndexMessage = 1)
        {
            if (propIndexMessage != childProps.Length - 1)
            {
                var propertyErrors = RecursiveSetPropertyError(
                    childProps[propIndexMessage + 1],
                    childProps,
                    errorsMessage,
                    propIndexMessage + 1);

                return new Dictionary<string, object>()
                {
                    { currentPropName, propertyErrors }
                };
            }


            return new Dictionary<string, object>()
            {
                { currentPropName, errorsMessage }
            };
        }
    }
}
