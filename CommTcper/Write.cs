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
    public partial class Write : Form
    {
        public String write { get; set; }

        public Write()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tb_write.Text !="")
            {
                Class1 class1 = new Class1();
                write = tb_write.Text;
                class1.WriteText(write);
                Close();
                Dispose();
                Class2.__result = false;
            }
            else
            {
                label1.Text = "错误密码不能为空";
            }
         }
    }
}
