using Architecture.TCS.Tools.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommTcper
{
    public partial class Frm_Query : Form
    {
        private string _sql { get; set; }

        public Frm_Query()
        {
            InitializeComponent();
            dtpk_qs.Format = DateTimePickerFormat.Custom;
            dtpk_qs.CustomFormat = "yyyy-MM-dd";
            dtpk_js.Format = DateTimePickerFormat.Custom;
            dtpk_js.CustomFormat = "yyyy-MM-dd";
            _sql = "select tm 时间,sn 组件号,orderno 工单,'等待中' 打印状态,'' 打印机名称,'' 打印结果,0 打印次数,'' 比对条码1,'' 比对条码2,'无' 比对条码结果,case when time(current_time,'localtime') between '08:00:00' and '20:00:00' then '白班' else '夜班' end 班别,'' 消息 from messn where 1=1 {0}";
            cmb_bb.SelectedIndex = 0;
        }

        private void btn_cx_Click(object sender, EventArgs e)
        {
            string sw = string.Empty;
            string swl = string.Empty;
            sw += " and tm between '" + dtpk_qs.Text + " 00:00:00' and '" + dtpk_js.Text + " 23:59:59' ";
            if (textBox1.Text != "")
            {
                sw += " and sn='" + textBox1.Text + "' ";
                swl += " and sn='" + textBox1.Text + "' ";
            }
            if (textBox2.Text != "")
            {
                sw += " and orderno='" + textBox2.Text + "' ";
                swl += " and orderno='" + textBox2.Text + "' ";
            }
            if (cmb_bb.Text != "全部")
            {
                sw += " and (case when time(tm) between '08:00:00' and '20:00:00' then '白班' else '夜班' end) = '" + cmb_bb.Text + "' ";
                swl += " and (case when time(current_time,'localtime') between '08:00:00' and '20:00:00' then '白班' else '夜班' end) = '" + cmb_bb.Text + "' ";
            }
            DataTable _dt = DbIns.SysDb.ExecuteSql(string.Format(_sql + " union all " + string.Format("select tm 时间,sn 组件号,orderno 工单,'已完成' 打印状态,pts 打印机名称,ptsts 打印结果,ptms 打印次数,sn 比对条码1,sn 比对条码2,'OK' 比对条码结果,case when time(tm) between '08:00:00' and '20:00:00' then '白班' else '夜班' end 班别,'' 消息 from mesprint where 1=1 {0} order by tm desc;", sw), swl)).Result as DataTable;
            skinDataGridView1.DataSource = _dt;
            //skinDataGridView1.Columns[0].Width = 150;
            skinDataGridView1.Columns[1].Width = 150;
            pagerControl1.DataGridView = skinDataGridView1;
            pagerControl1.DataSource = _dt;
        }

        private void btn_dyjl_Click(object sender, EventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                btn_dyjl.Enabled = false;
                string sw = string.Empty;
                string swl = string.Empty;
                sw += " and tm between '" + dtpk_qs.Text + " 00:00:00' and '" + dtpk_js.Text + " 23:59:59' ";
                if (textBox1.Text != "")
                {
                    sw += " and sn='" + textBox1.Text + "' ";
                    swl += " and sn='" + textBox1.Text + "' ";
                }
                if (textBox2.Text != "")
                {
                    sw += " and orderno='" + textBox2.Text + "' ";
                    swl += " and orderno='" + textBox2.Text + "' ";
                }
                if (cmb_bb.Text != "全部")
                {
                    sw += " and (case when time(tm) between '08:00:00' and '20:00:00' then '白班' else '夜班' end) = '" + cmb_bb.Text + "' ";
                    swl += " and (case when time(current_time,'localtime') between '08:00:00' and '20:00:00' then '白班' else '夜班' end) = '" + cmb_bb.Text + "' ";
                }
                DataTable _dt = DbIns.SysDb.ExecuteSql(string.Format(_sql + " union all " + string.Format("select tm 时间,sn 组件号,orderno 工单,'已完成' 打印状态,pts 打印机名称,ptsts 打印结果,ptms 打印次数,sn 比对条码1,sn 比对条码2,'OK' 比对条码结果,case when time(tm) between '08:00:00' and '20:00:00' then '白班' else '夜班' end 班别,'' 消息 from mesprint where 1=1 {0} order by tm desc;", sw), swl)).Result as DataTable;
                if (_dt.Rows.Count > 0)
                {
                    string _dir = Application.StartupPath + "\\History\\";
                    if (!Directory.Exists(_dir))
                        Directory.CreateDirectory(_dir);
                    string _filename = _dir + "打印记录_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xlsx";
                    ExcelTool.SaveExcelNew(_dt, _filename);
                    MessageBox.Show("文件导出成功,路径:" + _filename, "导出成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("无待导出数据!", "导出失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                btn_dyjl.Enabled = true;
            }));
        }

        private void Frm_Query_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            btn_cx_Click(null, null);
        }

        private void btn_qktm_Click(object sender, EventArgs e)
        {
            DialogResult Dt = MessageBox.Show("请确认要清空所有打印记录?", "通讯程序", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (Dt == DialogResult.OK)
            {
                DbIns.SysDb.ExecuteSql("delete from mesprint;");
                btn_cx_Click(null, null);
            }
        }

        private void skinDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
