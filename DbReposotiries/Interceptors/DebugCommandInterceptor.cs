using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbReposotiries.Interceptors
{
    public class DebugCommandInterceptor : IDbCommandInterceptor
    {
        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            LogInfo(nameof(NonQueryExecuted), command.CommandText);
        }

        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            LogInfo(nameof(NonQueryExecuting), command.CommandText);
        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            LogInfo(nameof(ReaderExecuted), command.CommandText);
        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            LogInfo(nameof(ReaderExecuting), command.CommandText);
        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            LogInfo(nameof(ScalarExecuted), command.CommandText);
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            LogInfo(nameof(ScalarExecuting), command.CommandText);
        }

        private void LogInfo(string command, string commandText)
        {
            Console.WriteLine($"{DateTime.Now} {command} : {commandText}");
        }
    }
}
