using System.Collections.ObjectModel;

namespace Artworks.Infrastructure.Application.Validation.CommonModel
{
    /// <summary>
    /// 异常错误集合。
    /// </summary>
    public class ValidationErrorCollection : Collection<ValidationError>
    {
        public void Add(string name, object attemptedValue, string message)
        {
            Add(new ValidationError(name, attemptedValue, message));
        }

        public void Add(string name, object attemptedValue, System.Exception exception)
        {
            Add(new ValidationError(name, attemptedValue, exception));
        }
    }
}
