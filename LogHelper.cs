using System;
using log4net;//
using log4net.Config;

namespace ModeBus
{
    /// <summary>
    /// 日志帮助类（log4net）
    /// </summary>
    public class LogHelper
    {
        private static LogHelper _instance = null;
        private static ILog ILog;

        public static LogHelper Log
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LogHelper();

                    XmlConfigurator.Configure();
                    ILog = LogManager.GetLogger("Log");
                }
                return _instance;
            }
        }

        /// <summary>
        /// 写普通信息
        /// </summary>
        /// <param name="msg">消息</param>
        public void WriteInfo(string msg)
        {
            ILog.Info(msg);
        }

        /// <summary>
        /// 写调试信息
        /// </summary>
        /// <param name="msg">消息</param>
        public void WriteDebug(string msg)
        {
            ILog.Debug(msg);
        }

        /// <summary>
        /// 写错误信息
        /// </summary>
        /// <param name="msg">消息</param>
        public void WriteError(string msg)
        {
            ILog.Error(msg);
        }

        /// <summary>
        /// 写错误信息
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="ex">错误信息</param>
        public void WriteError(string msg, Exception ex)
        {
            ILog.Error(msg, ex);
        }

    }
}
