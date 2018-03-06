using System;
using System.Collections.Generic;
using System.Linq;

namespace Artworks.Infrastructure.Application.Validation.CommonModel
{
    /// <summary>
    /// 验证状态字典。
    /// </summary>
    public class ValidationStateDictionary : Dictionary<Type, ValidationState>
    {
        public ValidationStateDictionary() { }

        public ValidationStateDictionary(Type type, ValidationState validationState)
        {
            Add(type, validationState);
        }

        public bool IsValid
        {
            get
            {
                return this.All(validationState => validationState.Value.IsValid);
            }
        }
    }
}
