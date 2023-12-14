using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommTcper
{
    public class ConfigPath
    {
        private static string _config { get; set; }
        public static string _config1 { get; set; }
        public static void Path()
        {
            _config = string.Concat(Application.StartupPath, "\\Config");
            if (!Directory.Exists(_config))
            {
                Directory.CreateDirectory(_config);
            }
            else
            {
                _config1 = string.Concat(_config, "\\config.ini");
            }
           

        }
    }
}
