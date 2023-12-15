using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommTcper
{
    public partial class Allocation : Form
    {
        public delegate void UpShow();
        public UpShow upShow;
        public Allocation()
        {
            InitializeComponent();
            Ini();
        }
        private void Ini()
        {
            ConfigPath.Path();
            IniHelper1.Ini.path = ConfigPath._config1;
            Reder();
        }
        private bool Write()
        {
            try
            {
                string srt = this.tb_url.Text;
                if (!IsUrl(srt))
                {
                    MessageBox.Show("输入格式不对");
                    return false;
                }
                IniHelper1.Ini.WriteString("Config", "URL", this.tb_url.Text);
                IniHelper1.Ini.WriteString("Config", "Userld", this.tb_mUserld.Text);
                IniHelper1.Ini.WriteString("Config", "Facilityld", this.tb_mFacilityld.Text);
                IniHelper1.Ini.WriteString("Config", "Eqpld", this.tb_mEqpld.Text);
                IniHelper1.Ini.WriteString("Modbus", "Address",this.tb_address.Text);
                switch (this.cb_mModel.Text)
                {
                    case "模型1":
                        IniHelper1.Ini.WriteString("LableModels", "Model1","Model1-1.btw");
                        IniHelper1.Ini.WriteString("LableModels", "Mode12", "Model1-2.btw");
                        IniHelper1.Ini.WriteString("LableModels", "SelectMode", "M1");
                        break;
                    case "模型2":
                        IniHelper1.Ini.WriteString("LableModels", "Model1", "Model2-1.btw");
                        IniHelper1.Ini.WriteString("LableModels", "Mode12",  "Model2-1.btw");
                        IniHelper1.Ini.WriteString("LableModels", "SelectMode", "M2");
                        break;
                    case "模型3":
                        IniHelper1.Ini.WriteString("LableModels", "Model1",  "Model3-1.btw");
                        IniHelper1.Ini.WriteString("LableModels", "Mode12",  "Model3-2.btw");
                        IniHelper1.Ini.WriteString("LableModels", "SelectMode", "M3");
                        break;
                }
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        private bool IsUrl(string srt)
        { 
                // 使用正则表达式来验证URL格式
                string pattern = @"^(https?|ftp)://[^\s/$.?#].[^\s]*$";
                Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
                return regex.IsMatch(srt);
        }

        private void Reder()
        {
            this.tb_url.Text = IniHelper1.Ini.IniReadValue("Config", "URL");
            this.tb_mUserld.Text = IniHelper1.Ini.IniReadValue("Config", "Userld");
            this.tb_mFacilityld.Text = IniHelper1.Ini.IniReadValue("Config", "Facilityld");
            this.tb_mEqpld.Text = IniHelper1.Ini.IniReadValue("Config", "Eqpld");
            this.tb_address.Text = IniHelper1.Ini.IniReadValue("Modbus", "Address");
            string str = IniHelper1.Ini.IniReadValue("LableModels", "SelectMode");
            switch (str)
            {
                case "M1":
                    cb_mModel.Text = cb_mModel.Items[0] as string;
                    break;
                case "M2":
                    cb_mModel.Text = cb_mModel.Items[1] as string;
                    break;
                case "M3":
                    cb_mModel.Text = cb_mModel.Items[2] as string;
                    break;
            }

        }

        private void AppconfigUrl()
        {
            // 加载配置文件
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // 获取system.serviceModel节
            ServiceModelSectionGroup serviceModel = ServiceModelSectionGroup.GetSectionGroup(config);

            // 获取客户端节
            ClientSection clientSection = serviceModel.Client;

            // 获取指定名称的endpoint
            ChannelEndpointElement endpoint = clientSection.Endpoints.Cast<ChannelEndpointElement>()
                .FirstOrDefault(e => e.Name == "JobManagementWebService");

            if (endpoint != null)
            {
                // 修改endpoint的address
                endpoint.Address = new Uri(this.tb_url.Text);

                // 保存配置更改
                config.Save(ConfigurationSaveMode.Modified);

                // 触发配置更改
                ConfigurationManager.RefreshSection("system.serviceModel/client");
              
            }
        }
        private void bt_mOK_Click(object sender, EventArgs e)
        {
            bool _bool = Write();
            if (_bool)
            {
                upShow.Invoke();
                MessageBox.Show("保存成功");
               
                AppconfigUrl();
                Close();
            }
            else
            {
                MessageBox.Show("保存失败");
            }
        }
    }
}

