
namespace CommTcper
{
    partial class Allocation
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_url = new System.Windows.Forms.TextBox();
            this.cb_mModel = new System.Windows.Forms.ComboBox();
            this.tb_mUserld = new System.Windows.Forms.TextBox();
            this.tb_mEqpld = new System.Windows.Forms.TextBox();
            this.tb_mFacilityld = new System.Windows.Forms.TextBox();
            this.bt_mOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(37, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "接口地址：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(41, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "用户名：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(37, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "车间号：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(41, 180);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "设备号：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(37, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 20);
            this.label5.TabIndex = 5;
            this.label5.Text = "标签模型：";
            // 
            // tb_url
            // 
            this.tb_url.Location = new System.Drawing.Point(132, 29);
            this.tb_url.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tb_url.Name = "tb_url";
            this.tb_url.Size = new System.Drawing.Size(462, 25);
            this.tb_url.TabIndex = 6;
            // 
            // cb_mModel
            // 
            this.cb_mModel.FormattingEnabled = true;
            this.cb_mModel.Items.AddRange(new object[] {
            "模型1",
            "模型2",
            "模型3"});
            this.cb_mModel.Location = new System.Drawing.Point(132, 68);
            this.cb_mModel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cb_mModel.Name = "cb_mModel";
            this.cb_mModel.Size = new System.Drawing.Size(462, 23);
            this.cb_mModel.TabIndex = 7;
            // 
            // tb_mUserld
            // 
            this.tb_mUserld.Location = new System.Drawing.Point(132, 102);
            this.tb_mUserld.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tb_mUserld.Name = "tb_mUserld";
            this.tb_mUserld.Size = new System.Drawing.Size(462, 25);
            this.tb_mUserld.TabIndex = 8;
            // 
            // tb_mEqpld
            // 
            this.tb_mEqpld.Location = new System.Drawing.Point(132, 177);
            this.tb_mEqpld.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tb_mEqpld.Name = "tb_mEqpld";
            this.tb_mEqpld.Size = new System.Drawing.Size(462, 25);
            this.tb_mEqpld.TabIndex = 9;
            // 
            // tb_mFacilityld
            // 
            this.tb_mFacilityld.Location = new System.Drawing.Point(132, 140);
            this.tb_mFacilityld.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tb_mFacilityld.Name = "tb_mFacilityld";
            this.tb_mFacilityld.Size = new System.Drawing.Size(462, 25);
            this.tb_mFacilityld.TabIndex = 10;
            // 
            // bt_mOK
            // 
            this.bt_mOK.Location = new System.Drawing.Point(255, 283);
            this.bt_mOK.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bt_mOK.Name = "bt_mOK";
            this.bt_mOK.Size = new System.Drawing.Size(93, 33);
            this.bt_mOK.TabIndex = 11;
            this.bt_mOK.Text = "确认";
            this.bt_mOK.UseVisualStyleBackColor = true;
            this.bt_mOK.Click += new System.EventHandler(this.bt_mOK_Click);
            // 
            // Allocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 467);
            this.Controls.Add(this.bt_mOK);
            this.Controls.Add(this.tb_mFacilityld);
            this.Controls.Add(this.tb_mEqpld);
            this.Controls.Add(this.tb_mUserld);
            this.Controls.Add(this.cb_mModel);
            this.Controls.Add(this.tb_url);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Allocation";
            this.Text = "Allocation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_url;
        private System.Windows.Forms.ComboBox cb_mModel;
        private System.Windows.Forms.TextBox tb_mUserld;
        private System.Windows.Forms.TextBox tb_mEqpld;
        private System.Windows.Forms.TextBox tb_mFacilityld;
        private System.Windows.Forms.Button bt_mOK;
    }
}