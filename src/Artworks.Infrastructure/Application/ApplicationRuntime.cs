
namespace Artworks.Infrastructure.Application
{
    /// <summary>
    /// 应用程序运行时。
    /// </summary>
    public class ApplicationRuntime
    {
        private static readonly ApplicationRuntime instanceObj = new ApplicationRuntime();
        private static readonly object lockObj = new object();
        private IApplication currentApplication = null;

        /// <summary>
        /// 当前应用app
        /// </summary>
        public IApplication CurrentApp
        {
            get { return currentApplication; }
        }

        static ApplicationRuntime() { }
        private ApplicationRuntime() { }

        /// <summary>
        /// Gets the instance of the current <c>ApplicationRuntime</c> class.
        /// </summary>
        public static ApplicationRuntime Instance
        {
            get { return instanceObj; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="application"></param>
        public void Regist(IApplication application)
        {
            lock (lockObj)
            {
                if (currentApplication == null)
                {
                    currentApplication = application;
                }
            }
        }

    }

}
