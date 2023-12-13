using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibrary1;

namespace CommTcper
{
    public partial class Pm : Form
    {
        private string _path { get; set; }
        public string str { get; set; }
     
        public Pm()
        {
            InitializeComponent();
            _path = ConfigurationSettings.AppSettings["Path"];
        }
      
        private void btn_dqmm_Click(object sender, EventArgs e)
        {
                 Class1 class1 = new Class1();
            if (Class2.__result == false)
            {
                if (tb_mm.Text == class1.SaveCode(str))
                {
                    DialogResult = DialogResult.OK;
                    Close();
                    Dispose();
                    Class2.__result = true;
                }
                else
                {
                    label1.Text = "密码错误,请确认!";

                }
            }
         
            
        }

        private void bt_write_Click(object sender, EventArgs e)
        {
            Class1 class1 = new Class1();

            if (Class2.__result == true)
            {

                Write write = new Write();
                write.ShowDialog();
            }
            else
            {
                label1.Text = "请先登录密码";
                Class2.__result = false;
            }
          
           
        }

      
    }
}
