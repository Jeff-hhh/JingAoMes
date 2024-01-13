namespace CommTcper
{
    partial class Frm_Query
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Query));
            this.btn_cx = new System.Windows.Forms.Button();
            this.btn_dyjl = new System.Windows.Forms.Button();
            this.pagerControl1 = new cassControl.PagerControl();
            this.dtpk_qs = new System.Windows.Forms.DateTimePicker();
            this.dtpk_js = new System.Windows.Forms.DateTimePicker();
            this.lb_qs = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmb_bb = new System.Windows.Forms.ComboBox();
            this.btn_qktm = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.skinDataGridView1 = new CCWin.SkinControl.SkinDataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.skinDataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_cx
            // 
            this.btn_cx.Location = new System.Drawing.Point(14, 23);
            this.btn_cx.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_cx.Name = "btn_cx";
            this.btn_cx.Size = new System.Drawing.Size(82, 32);
            this.btn_cx.TabIndex = 12;
            this.btn_cx.Text = "查询";
            this.btn_cx.UseVisualStyleBackColor = true;
            this.btn_cx.Click += new System.EventHandler(this.btn_cx_Click);
            // 
            // btn_dyjl
            // 
            this.btn_dyjl.Location = new System.Drawing.Point(101, 23);
            this.btn_dyjl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_dyjl.Name = "btn_dyjl";
            this.btn_dyjl.Size = new System.Drawing.Size(82, 32);
            this.btn_dyjl.TabIndex = 19;
            this.btn_dyjl.Text = "导出";
            this.btn_dyjl.UseVisualStyleBackColor = true;
            this.btn_dyjl.Click += new System.EventHandler(this.btn_dyjl_Click);
            // 
            // pagerControl1
            // 
            this.pagerControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pagerControl1.BackColor = System.Drawing.SystemColors.Control;
            this.pagerControl1.CurrentPage = 0;
            this.pagerControl1.DataCount = 0;
            this.pagerControl1.DataGridView = null;
            this.pagerControl1.DataSource = null;
            this.pagerControl1.ItemForeColor = System.Drawing.SystemColors.ControlText;
            this.pagerControl1.Location = new System.Drawing.Point(5, 18);
            this.pagerControl1.Margin = new System.Windows.Forms.Padding(5);
            this.pagerControl1.Name = "pagerControl1";
            this.pagerControl1.PageCount = 0;
            this.pagerControl1.PageSize = 100;
            this.pagerControl1.Size = new System.Drawing.Size(1299, 42);
            this.pagerControl1.TabIndex = 20;
            // 
            // dtpk_qs
            // 
            this.dtpk_qs.Location = new System.Drawing.Point(276, 25);
            this.dtpk_qs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpk_qs.Name = "dtpk_qs";
            this.dtpk_qs.Size = new System.Drawing.Size(161, 25);
            this.dtpk_qs.TabIndex = 22;
            // 
            // dtpk_js
            // 
            this.dtpk_js.Location = new System.Drawing.Point(446, 25);
            this.dtpk_js.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpk_js.Name = "dtpk_js";
            this.dtpk_js.Size = new System.Drawing.Size(166, 25);
            this.dtpk_js.TabIndex = 24;
            // 
            // lb_qs
            // 
            this.lb_qs.AutoSize = true;
            this.lb_qs.Location = new System.Drawing.Point(189, 30);
            this.lb_qs.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_qs.Name = "lb_qs";
            this.lb_qs.Size = new System.Drawing.Size(75, 15);
            this.lb_qs.TabIndex = 23;
            this.lb_qs.Text = "创建日期:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(643, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 15);
            this.label1.TabIndex = 25;
            this.label1.Text = "条码:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(872, 32);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 26;
            this.label2.Text = "工单:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(697, 27);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(166, 25);
            this.textBox1.TabIndex = 27;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(914, 28);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(139, 25);
            this.textBox2.TabIndex = 28;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1045, 32);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 15);
            this.label3.TabIndex = 29;
            this.label3.Text = "班别:";
            // 
            // cmb_bb
            // 
            this.cmb_bb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_bb.FormattingEnabled = true;
            this.cmb_bb.Items.AddRange(new object[] {
            "全部",
            "白班",
            "夜班"});
            this.cmb_bb.Location = new System.Drawing.Point(1113, 27);
            this.cmb_bb.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmb_bb.Name = "cmb_bb";
            this.cmb_bb.Size = new System.Drawing.Size(111, 23);
            this.cmb_bb.TabIndex = 30;
            // 
            // btn_qktm
            // 
            this.btn_qktm.Location = new System.Drawing.Point(1232, 23);
            this.btn_qktm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_qktm.Name = "btn_qktm";
            this.btn_qktm.Size = new System.Drawing.Size(82, 32);
            this.btn_qktm.TabIndex = 31;
            this.btn_qktm.Text = "清空";
            this.btn_qktm.UseVisualStyleBackColor = true;
            this.btn_qktm.Click += new System.EventHandler(this.btn_qktm_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_cx);
            this.panel1.Controls.Add(this.btn_dyjl);
            this.panel1.Controls.Add(this.btn_qktm);
            this.panel1.Controls.Add(this.lb_qs);
            this.panel1.Controls.Add(this.cmb_bb);
            this.panel1.Controls.Add(this.dtpk_qs);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.dtpk_js);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1331, 72);
            this.panel1.TabIndex = 32;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pagerControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 543);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1331, 65);
            this.panel2.TabIndex = 33;
            // 
            // skinDataGridView1
            // 
            this.skinDataGridView1.AllowUserToAddRows = false;
            this.skinDataGridView1.AllowUserToDeleteRows = false;
            this.skinDataGridView1.AllowUserToResizeColumns = false;
            this.skinDataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(246)))), ((int)(((byte)(253)))));
            this.skinDataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.skinDataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skinDataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.skinDataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.skinDataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.skinDataGridView1.ColumnFont = null;
            this.skinDataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(246)))), ((int)(((byte)(239)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.skinDataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.skinDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.skinDataGridView1.ColumnSelectForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(188)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.skinDataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.skinDataGridView1.EnableHeadersVisualStyles = false;
            this.skinDataGridView1.GridColor = System.Drawing.SystemColors.MenuHighlight;
            this.skinDataGridView1.HeadFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinDataGridView1.HeadSelectForeColor = System.Drawing.SystemColors.HighlightText;
            this.skinDataGridView1.Location = new System.Drawing.Point(5, 62);
            this.skinDataGridView1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.skinDataGridView1.Name = "skinDataGridView1";
            this.skinDataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.skinDataGridView1.RowHeadersWidth = 62;
            this.skinDataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.skinDataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.skinDataGridView1.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.skinDataGridView1.RowTemplate.Height = 18;
            this.skinDataGridView1.Size = new System.Drawing.Size(1321, 473);
            this.skinDataGridView1.SkinGridColor = System.Drawing.SystemColors.MenuHighlight;
            this.skinDataGridView1.TabIndex = 34;
            this.skinDataGridView1.TitleBack = null;
            this.skinDataGridView1.TitleBackColorBegin = System.Drawing.Color.White;
            this.skinDataGridView1.TitleBackColorEnd = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(242)))));
            this.skinDataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.skinDataGridView1_CellContentClick);
            // 
            // Frm_Query
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1331, 608);
            this.Controls.Add(this.skinDataGridView1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Frm_Query";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "查询看板";
            this.Load += new System.EventHandler(this.Frm_Query_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.skinDataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_cx;
        private System.Windows.Forms.Button btn_dyjl;
        private cassControl.PagerControl pagerControl1;
        private System.Windows.Forms.DateTimePicker dtpk_qs;
        private System.Windows.Forms.DateTimePicker dtpk_js;
        private System.Windows.Forms.Label lb_qs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmb_bb;
        private System.Windows.Forms.Button btn_qktm;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private CCWin.SkinControl.SkinDataGridView skinDataGridView1;
    }
}