using System.Collections.Generic;

namespace Artworks.Anomaly.Internal
{
    /// <summary>
    /// 异常处理比较。
    /// </summary>
    internal class ExceptionHandlerComparer : IEqualityComparer<IExceptionHandler>
    {
        public bool Equals(IExceptionHandler x, IExceptionHandler y)
        {
            return x.GetType().AssemblyQualifiedName.Equals(y.GetType().AssemblyQualifiedName);
        }

        public int GetHashCode(IExceptionHandler obj)
        {
            return obj.GetHashCode();
        }
    }
}
