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



        private LogContextInfo GetInfo()
        {

        }

        /// <summary>
        /// 创建日志上下文信息
        /// </summary>
        protected virtual LogContextInfo CreateInfo()
        {
            return new LogContextInfo
            {
                TraceId = GetTraceId(),
                Stopwatch = GetStopwatch(),
                Ip = Web.Ip,
                Host = Web.Host,
                Browser = Web.Browser,
                Url = Web.Url
            };
        }

        /// <summary>
        /// 获取跟踪号
        /// </summary>
        protected string GetTraceId()
        {
            var traceId = Context.TraceId;
            return string.IsNullOrWhiteSpace(traceId) ? Guid.NewGuid().ToString() : traceId;
        }

        /// <summary>
        /// 获取计时器
        /// </summary>
        protected Stopwatch GetStopwatch()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            return stopwatch;
        }

        public string TraceId => throw new NotImplementedException();

        public Stopwatch Stopwatch => throw new NotImplementedException();

        public string Ip => throw new NotImplementedException();

        public string Host => throw new NotImplementedException();

        public string Browser => throw new NotImplementedException();

        public string Url => throw new NotImplementedException();
    }
}
