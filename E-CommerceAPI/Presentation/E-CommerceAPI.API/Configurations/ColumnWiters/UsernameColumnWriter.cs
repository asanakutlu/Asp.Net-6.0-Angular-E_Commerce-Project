﻿using NpgsqlTypes;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Serilog.Sinks.PostgreSQL;

namespace E_CommerceAPI.API.Configurations.ColumnWiters
{
    public class UsernameColumnWriter : ColumnWriterBase
    {
        public UsernameColumnWriter() : base(NpgsqlDbType.Varchar)
        {
        }

        public override object GetValue(LogEvent logEvent, IFormatProvider formatProvider = null)
        {
           var (username,value)= logEvent.Properties.FirstOrDefault(x => x.Key == "user_name");
            return value?.ToString() ?? null;
        }
    }
}
