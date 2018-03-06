using Artworks.Log.Persistence;

namespace Artworks.Log.Internal
{
    /// <summary>
    /// 日志实现。
    /// </summary>
    internal class MyLog : ILog
    {
        public string Name { get; private set; }

        public MyLog(string name)
        {
            this.Name = name;
        }

        public void Log(string message, System.Exception exception)
        {
            this.Repository.Execute(message, exception);
        }

        public ILoggerRepository Repository
        {
            get
            {
                return RepositorySelector.GetRepository(this.Name.ToLower());
            }
        }


    }
}
