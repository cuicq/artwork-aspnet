using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Artworks.Container.Interceptors.InterceptionBehaviors
{
    /// <summary>
    /// 拦截行为帮助类。
    /// </summary>
    internal class InterceptionBehaviorHelper
    {

        /// <summary>
        /// 获取拦截行为属性集合
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        internal static InterceptionBehaviorAttribute[] GetInterceptionBehaviors(MethodBase methodInfo)
        {

            List<InterceptionBehaviorAttribute> behaviors = new List<InterceptionBehaviorAttribute>();

            var ownAttrs = methodInfo.GetCustomAttributes(typeof(InterceptionBehaviorAttribute), false).Select(ent => (InterceptionBehaviorAttribute)ent);
            behaviors.AddRange(ownAttrs);  //优先级1：当前方法

            var ownTypeAttrs = methodInfo.DeclaringType.GetCustomAttributes(typeof(InterceptionBehaviorAttribute), false).Select(ent => (InterceptionBehaviorAttribute)ent).Except(behaviors);
            behaviors.AddRange(ownTypeAttrs);  //优先级2：当前类

            var inheritAttrs = methodInfo.GetCustomAttributes(typeof(InterceptionBehaviorAttribute), true).Select(ent => (InterceptionBehaviorAttribute)ent).Where(ent => ent.IsAutoInherit).Except(behaviors);
            behaviors.AddRange(inheritAttrs);  //优先级3：父类方法

            var inheritTypeAttrs = methodInfo.DeclaringType.GetCustomAttributes(typeof(InterceptionBehaviorAttribute), true).Select(ent => (InterceptionBehaviorAttribute)ent).Where(ent => ent.IsAutoInherit).Except(behaviors);
            behaviors.AddRange(inheritTypeAttrs);  //优先级4：父类

            //移除阻塞不触发的特性
            var preventAttrs = ownAttrs.Where(ent => ent.IsPrevent).Union(ownTypeAttrs.Where(ent => ent.IsPrevent));

            foreach (var preventAttr in preventAttrs)
            {
                behaviors.Remove(preventAttr);
            }

            return behaviors.OrderBy(ent => ent.Order).ToArray();
        }


    }
}
