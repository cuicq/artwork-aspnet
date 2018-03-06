using Artworks.Infrastructure.Application.Validation.CommonModel;
using Artworks.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artworks.Infrastructure.Application.Validation
{
    /// <summary>
    /// 验证实体服务。
    /// </summary>
    public class ValidationService : IValidationService
    {
        public IValidator<T> GetValidatorFor<T>()
        {
            return ServiceLocator.Instance.GetService<IValidator<T>>();
        }

        public ValidationState Validate<T>(T model)
        {
            IValidator<T> validator = this.GetValidatorFor<T>();
            if (validator == null)
                throw new System.Exception(string.Format(Resource.EXCEPTION_NOTFOUND_VALIDATOR, model.GetType()));
            return validator.Validate(model);
        }
    }
}
