using System;
using System.Collections.Generic;
using System.Text;
using Banana.Uow.Models;

namespace AdminLTE.Application
{
    public class AppConfig
    {
        public DBSetting dBSetting { get; set; }
    }


    public class DBSetting
    {
        public string ConnectionString { get; set; }

        public DBType DBType { get; set; }
    }
}
