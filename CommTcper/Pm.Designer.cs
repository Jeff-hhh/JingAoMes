namespace CommTcper
{
    partial class Pm
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
            this.label6 = new System.Windows.Forms.Label();
            this.tb_mm = new System.Windows.Forms.TextBox();
            this.btn_dqmm = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.bt_write = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(59, 9);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "请输入密码";
            // 
            // tb_mm
            // 
            this.tb_mm.Location = new System.Drawing.Point(15, 32);
            this.tb_mm.Margin = new System.Windows.Forms.Padding(2);
            this.tb_mm.Multiline = true;
            this.tb_mm.Name = "tb_mm";
            this.tb_mm.PasswordChar = '*';
            this.tb_mm.Size = new System.Drawing.Size(155, 24);
            this.tb_mm.TabIndex = 10;
            // 
            // btn_dqmm
            // 
            this.btn_dqmm.Location = new System.Drawing.Point(15, 82);
            this.btn_dqmm.Margin = new System.Windows.Forms.Padding(2);
            this.btn_dqmm.Name = "btn_dqmm";
            this.btn_dqmm.Size = new System.Drawing.Size(155, 37);
            this.btn_dqmm.TabIndex = 12;
            this.btn_dqmm.Text = "确定";
            this.btn_dqmm.UseVisualStyleBackColor = true;
            this.btn_dqmm.Click += new System.EventHandler(this.btn_dqmm_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(13, 187);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "-";
            // 
            // bt_write
            // 
            this.bt_write.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_write.Location = new System.Drawing.Point(15, 139);
            this.bt_write.Name = "bt_write";
            this.bt_write.Size = new System.Drawing.Size(155, 45);
            this.bt_write.TabIndex = 14;
            this.bt_write.Text = "修改密码";
            this.bt_write.UseVisualStyleBackColor = true;
            this.bt_write.Click += new System.EventHandler(this.bt_write_Click);
            // 
            // Pm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(194, 216);
            this.Controls.Add(this.bt_write);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_dqmm);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tb_mm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Pm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "权限校验";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_mm;
        private System.Windows.Forms.Button btn_dqmm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bt_write;
    }
}