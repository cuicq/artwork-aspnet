using Artworks.Infrastructure.Application.Validation.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artworks.Infrastructure.Application.Validation
{
    /// <summary>
    /// 验证实体基类。
    /// </summary>
    /// <typeparam name="T">实体</typeparam>
    public abstract class ValidatorBase<T> : IValidator<T>
    {
        public ValidatorBase() { }

        /// <summary>
        /// 验证
        /// </summary>
        public abstract ValidationState Validate(T model);

        protected ValidationError CreateValidationError(object value, string validationKey, string validationMessage, params object[] validationMessageParameters)
        {
            if (validationMessageParameters != null && validationMessageParameters.Length > 0)
            {
                validationMessage = string.Format(validationMessage, validationMessageParameters);
            }

            return new ValidationError(
                validationKey,
                value,
                new InvalidOperationException(this.Localize(validationKey, validationMessage)));
        }


        //通过此方法可以动态获取提示信息
        private string Localize(string key, string defaultValue)
        {

            return defaultValue;
        }
    }
}
