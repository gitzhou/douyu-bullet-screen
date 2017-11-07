namespace DouyuBulletScreen
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.labelRoomID = new System.Windows.Forms.Label();
            this.textBoxRoomID = new System.Windows.Forms.TextBox();
            this.messageRichTextBox = new System.Windows.Forms.RichTextBox();
            this.textBoxGroupID = new System.Windows.Forms.TextBox();
            this.labelGroupID = new System.Windows.Forms.Label();
            this.clearTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(516, 20);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(135, 50);
            this.buttonConnect.TabIndex = 4;
            this.buttonConnect.Text = "连 接";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.ButtonConnect_Click);
            // 
            // labelRoomID
            // 
            this.labelRoomID.AutoSize = true;
            this.labelRoomID.Location = new System.Drawing.Point(15, 32);
            this.labelRoomID.Name = "labelRoomID";
            this.labelRoomID.Size = new System.Drawing.Size(82, 24);
            this.labelRoomID.TabIndex = 0;
            this.labelRoomID.Text = "房间号";
            // 
            // textBoxRoomID
            // 
            this.textBoxRoomID.Location = new System.Drawing.Point(110, 26);
            this.textBoxRoomID.Name = "textBoxRoomID";
            this.textBoxRoomID.Size = new System.Drawing.Size(115, 35);
            this.textBoxRoomID.TabIndex = 1;
            // 
            // messageRichTextBox
            // 
            this.messageRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageRichTextBox.Location = new System.Drawing.Point(5, 82);
            this.messageRichTextBox.Name = "messageRichTextBox";
            this.messageRichTextBox.Size = new System.Drawing.Size(653, 868);
            this.messageRichTextBox.TabIndex = 5;
            this.messageRichTextBox.Text = "";
            this.messageRichTextBox.TextChanged += new System.EventHandler(this.MessageRichTextBox_TextChanged);
            // 
            // textBoxGroupID
            // 
            this.textBoxGroupID.Location = new System.Drawing.Point(362, 26);
            this.textBoxGroupID.Name = "textBoxGroupID";
            this.textBoxGroupID.Size = new System.Drawing.Size(115, 35);
            this.textBoxGroupID.TabIndex = 3;
            // 
            // labelGroupID
            // 
            this.labelGroupID.AutoSize = true;
            this.labelGroupID.Location = new System.Drawing.Point(267, 32);
            this.labelGroupID.Name = "labelGroupID";
            this.labelGroupID.Size = new System.Drawing.Size(82, 24);
            this.labelGroupID.TabIndex = 2;
            this.labelGroupID.Text = "分组号";
            // 
            // clearTimer
            // 
            this.clearTimer.Tick += new System.EventHandler(this.ClearTimer_Tick);
            // 
            // MainForm
            // 
            this.AcceptButton = this.buttonConnect;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 954);
            this.Controls.Add(this.textBoxGroupID);
            this.Controls.Add(this.labelGroupID);
            this.Controls.Add(this.messageRichTextBox);
            this.Controls.Add(this.textBoxRoomID);
            this.Controls.Add(this.labelRoomID);
            this.Controls.Add(this.buttonConnect);
            this.MinimumSize = new System.Drawing.Size(685, 1025);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "斗鱼弹幕";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Label labelRoomID;
        private System.Windows.Forms.TextBox textBoxRoomID;
        private System.Windows.Forms.RichTextBox messageRichTextBox;
        private System.Windows.Forms.TextBox textBoxGroupID;
        private System.Windows.Forms.Label labelGroupID;
        private System.Windows.Forms.Timer clearTimer;
    }
}

