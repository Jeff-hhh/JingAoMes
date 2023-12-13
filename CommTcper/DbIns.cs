using Architecture.TCS.Base.Operations;
using Architecture.TCS.Base.Types;
using Architecture.TCS.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace CommTcper
{
    public abstract class DbIns
    {
        static DbIns()
        {
            var _conn = string.Concat("Data Source=", BaseOperation.GetAssemblyPath(), ConfigurationManager.AppSettings["DatabaseConStr"]);
            SysDb = IDBRepository.GetCommandInstance(DataBaseType.SQLite, _conn, string.Empty, string.Empty);
        }

        public static IDBRepository SysDb { get; set; }
    }
}
