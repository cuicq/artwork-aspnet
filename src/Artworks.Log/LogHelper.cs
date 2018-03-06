using Artworks.Log.Internal;

namespace Artworks.Log
{
    /// <summary>
    /// 日志帮助类
    /// </summary>
    public class LogHelper
    {
        #region 属性

        private const string debug = "debug";
        private const string error = "error";
        private const string fatal = "fatal";
        private const string info = "info";
        private const string warn = "warn";

        #endregion

        #region 构造方法

        static LogHelper()
        {

        }

        #endregion

        #region Debug

        public static void Debug(string message)
        {
            Debug(message, null);
        }

        public static void Debug(string message, System.Exception exception)
        {
            Logger.GetLogger(debug).Log(message, exception);
        }

        #endregion

        #region Error

        public static void Error(string message)
        {
            Error(message, null);
        }
        public static void Error(string message, System.Exception exception)
        {
            Logger.GetLogger(error).Log(message, exception);
        }

        #endregion

        #region Fatal

        public static void Fatal(string message)
        {
            Fatal(message, null);
        }

        public static void Fatal(string message, System.Exception exception)
        {
            Logger.GetLogger(fatal).Log(message, exception);
        }

        #endregion

        #region Info

        public static void Info(string message)
        {
            Info(message, null);
        }

        public static void Info(string message, System.Exception exception)
        {
            Logger.GetLogger(info).Log(message, exception);
        }

        #endregion

        #region Warn

        public static void Warn(string message)
        {
            Warn(message, null);
        }

        public static void Warn(string message, System.Exception exception)
        {
            Logger.GetLogger(warn).Log(message, exception);
        }

        #endregion
    }
}
