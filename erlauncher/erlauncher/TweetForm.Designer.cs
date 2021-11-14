
namespace erlauncher
{
    partial class TweetForm
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
            this.authPanel = new System.Windows.Forms.Panel();
            this.failedMessage = new System.Windows.Forms.Label();
            this.pincodeBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tweetPanel = new System.Windows.Forms.Panel();
            this.tweetButton = new System.Windows.Forms.Button();
            this.tweetText = new System.Windows.Forms.RichTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.twitterIDLabel = new System.Windows.Forms.Label();
            this.authPanel.SuspendLayout();
            this.tweetPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // authPanel
            // 
            this.authPanel.Controls.Add(this.failedMessage);
            this.authPanel.Controls.Add(this.pincodeBox);
            this.authPanel.Controls.Add(this.label1);
            this.authPanel.Location = new System.Drawing.Point(13, 13);
            this.authPanel.Name = "authPanel";
            this.authPanel.Size = new System.Drawing.Size(416, 156);
            this.authPanel.TabIndex = 0;
            // 
            // failedMessage
            // 
            this.failedMessage.AutoSize = true;
            this.failedMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.failedMessage.Location = new System.Drawing.Point(258, 34);
            this.failedMessage.Name = "failedMessage";
            this.failedMessage.Size = new System.Drawing.Size(100, 15);
            this.failedMessage.TabIndex = 2;
            this.failedMessage.Text = "認証に失敗しました";
            this.failedMessage.Visible = false;
            // 
            // pincodeBox
            // 
            this.pincodeBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(36)))));
            this.pincodeBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pincodeBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.pincodeBox.Location = new System.Drawing.Point(36, 53);
            this.pincodeBox.Name = "pincodeBox";
            this.pincodeBox.Size = new System.Drawing.Size(331, 23);
            this.pincodeBox.TabIndex = 1;
            this.pincodeBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.pincodeBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pincodeBox_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.label1.Location = new System.Drawing.Point(33, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "PINコードを入力してください";
            // 
            // tweetPanel
            // 
            this.tweetPanel.Controls.Add(this.twitterIDLabel);
            this.tweetPanel.Controls.Add(this.tweetButton);
            this.tweetPanel.Controls.Add(this.tweetText);
            this.tweetPanel.Controls.Add(this.pictureBox1);
            this.tweetPanel.Location = new System.Drawing.Point(16, 4);
            this.tweetPanel.Name = "tweetPanel";
            this.tweetPanel.Size = new System.Drawing.Size(416, 372);
            this.tweetPanel.TabIndex = 1;
            // 
            // tweetButton
            // 
            this.tweetButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(238)))));
            this.tweetButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tweetButton.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Bold);
            this.tweetButton.ForeColor = System.Drawing.Color.White;
            this.tweetButton.Location = new System.Drawing.Point(107, 303);
            this.tweetButton.Name = "tweetButton";
            this.tweetButton.Size = new System.Drawing.Size(192, 41);
            this.tweetButton.TabIndex = 2;
            this.tweetButton.Text = "Tweet";
            this.tweetButton.UseVisualStyleBackColor = false;
            this.tweetButton.Click += new System.EventHandler(this.tweetButton_Click);
            // 
            // tweetText
            // 
            this.tweetText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.tweetText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tweetText.Location = new System.Drawing.Point(36, 151);
            this.tweetText.MaxLength = 140;
            this.tweetText.Name = "tweetText";
            this.tweetText.Size = new System.Drawing.Size(331, 136);
            this.tweetText.TabIndex = 1;
            this.tweetText.Text = "";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(97, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(228, 128);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // twitterIDLabel
            // 
            this.twitterIDLabel.AutoSize = true;
            this.twitterIDLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.twitterIDLabel.Location = new System.Drawing.Point(314, 329);
            this.twitterIDLabel.Name = "twitterIDLabel";
            this.twitterIDLabel.Size = new System.Drawing.Size(0, 15);
            this.twitterIDLabel.TabIndex = 3;
            // 
            // TweetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(441, 387);
            this.Controls.Add(this.tweetPanel);
            this.Controls.Add(this.authPanel);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "TweetForm";
            this.Text = "スクリーンショットをツイートする";
            this.authPanel.ResumeLayout(false);
            this.authPanel.PerformLayout();
            this.tweetPanel.ResumeLayout(false);
            this.tweetPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel authPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox pincodeBox;
        private System.Windows.Forms.Panel tweetPanel;
        private System.Windows.Forms.Button tweetButton;
        private System.Windows.Forms.RichTextBox tweetText;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label failedMessage;
        private System.Windows.Forms.Label twitterIDLabel;
    }
}