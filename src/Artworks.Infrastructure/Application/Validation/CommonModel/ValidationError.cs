
namespace Artworks.Infrastructure.Application.Validation.CommonModel
{
    /// <summary>
    /// 异常错误。
    /// </summary>
    public class ValidationError
    {
        public string Name { get; private set; }
        public object AttemptedValue { get; private set; }
        public string Message { get; private set; }
        public System.Exception Exception { get; private set; }


        public ValidationError(string name, object attemptedValue, string message)
        {
            Guard.ArgumentNotNullOrEmpty(name, "valdation name");
            Guard.ArgumentNotNullOrEmpty(message, "validation message");

            Name = name;
            AttemptedValue = attemptedValue;
            Message = message;
        }

        public ValidationError(string name, object attemptedValue, System.Exception exception)
        {

            Guard.ArgumentNotNullOrEmpty(name, "valdation name");
            Guard.ArgumentNotNull(exception, "validation exception");

            Name = name;
            AttemptedValue = attemptedValue;
            Exception = exception;
            Message = exception.Message;
        }


    }

}
