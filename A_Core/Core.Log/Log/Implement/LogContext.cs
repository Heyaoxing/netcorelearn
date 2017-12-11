using Core.Uitl.Log.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Core.Uitl.Contexts;

namespace Core.Uitl.Log.Implement
{
    public class LogContext : ILogContext
    {
        /// <summary>
        /// 日志上下文信息
        /// </summary>
        private LogContextInfo _info;

        /// <summary>
        /// 上下文
        /// </summary>
        public IContext Context { get; set; }

        /// <summary>
        /// 初始化日志上下文
        /// </summary>
        /// <param name="context">上下文</param>
        public LogContext(IContext context)
        {
            Context = context;
        }

        //private LogContextInfo GetInfo()
        //{

        //}

        public string TraceId => throw new NotImplementedException();

        public Stopwatch Stopwatch => throw new NotImplementedException();

        public string Ip => throw new NotImplementedException();

        public string Host => throw new NotImplementedException();

        public string Browser => throw new NotImplementedException();

        public string Url => throw new NotImplementedException();
    }
}
