
namespace erlauncher
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.playButton = new System.Windows.Forms.Button();
            this.groupTree = new System.Windows.Forms.TreeView();
            this.listView1 = new System.Windows.Forms.ListView();
            this.allIconList = new System.Windows.Forms.ImageList(this.components);
            this.PlusButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.MinusButton = new System.Windows.Forms.Button();
            this.rmGroupButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.groupLabel = new System.Windows.Forms.Label();
            this.getGameButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.checkDirectory = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Title = new System.Windows.Forms.Label();
            this.openImageFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // playButton
            // 
            this.playButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(64)))));
            this.playButton.FlatAppearance.BorderSize = 0;
            this.playButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.playButton.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.playButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.playButton.Location = new System.Drawing.Point(419, 466);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(152, 53);
            this.playButton.TabIndex = 0;
            this.playButton.Text = "play";
            this.playButton.UseVisualStyleBackColor = false;
            this.playButton.Visible = false;
            this.playButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupTree
            // 
            this.groupTree.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(36)))));
            this.groupTree.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.groupTree.HideSelection = false;
            this.groupTree.Location = new System.Drawing.Point(13, 68);
            this.groupTree.Name = "groupTree";
            this.groupTree.ShowPlusMinus = false;
            this.groupTree.ShowRootLines = false;
            this.groupTree.Size = new System.Drawing.Size(121, 451);
            this.groupTree.TabIndex = 1;
            this.groupTree.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.groupTree_afterCheck);
            this.groupTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect_1);
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(36)))));
            this.listView1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(141, 68);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(241, 451);
            this.listView1.SmallImageList = this.allIconList;
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // allIconList
            // 
            this.allIconList.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.allIconList.ImageSize = new System.Drawing.Size(16, 16);
            this.allIconList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // PlusButton
            // 
            this.PlusButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.PlusButton.FlatAppearance.BorderSize = 0;
            this.PlusButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PlusButton.Font = new System.Drawing.Font("Meiryo UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PlusButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.PlusButton.Location = new System.Drawing.Point(90, 46);
            this.PlusButton.Name = "PlusButton";
            this.PlusButton.Size = new System.Drawing.Size(19, 19);
            this.PlusButton.TabIndex = 3;
            this.PlusButton.Text = "＋";
            this.PlusButton.UseVisualStyleBackColor = false;
            this.PlusButton.Click += new System.EventHandler(this.PlusButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.label1.Location = new System.Drawing.Point(12, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "GROUP";
            // 
            // MinusButton
            // 
            this.MinusButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.MinusButton.FlatAppearance.BorderSize = 0;
            this.MinusButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MinusButton.Font = new System.Drawing.Font("Meiryo UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.MinusButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.MinusButton.Location = new System.Drawing.Point(115, 46);
            this.MinusButton.Name = "MinusButton";
            this.MinusButton.Size = new System.Drawing.Size(19, 19);
            this.MinusButton.TabIndex = 5;
            this.MinusButton.Text = "－";
            this.MinusButton.UseVisualStyleBackColor = false;
            this.MinusButton.Click += new System.EventHandler(this.MinusButton_Click);
            // 
            // rmGroupButton
            // 
            this.rmGroupButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.rmGroupButton.FlatAppearance.BorderSize = 0;
            this.rmGroupButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rmGroupButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.rmGroupButton.Location = new System.Drawing.Point(79, 12);
            this.rmGroupButton.Name = "rmGroupButton";
            this.rmGroupButton.Size = new System.Drawing.Size(65, 23);
            this.rmGroupButton.TabIndex = 6;
            this.rmGroupButton.Text = "削除";
            this.rmGroupButton.UseVisualStyleBackColor = false;
            this.rmGroupButton.Visible = false;
            this.rmGroupButton.Click += new System.EventHandler(this.rmGroupButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CancelButton.FlatAppearance.BorderSize = 0;
            this.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.CancelButton.Location = new System.Drawing.Point(8, 12);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(65, 23);
            this.CancelButton.TabIndex = 7;
            this.CancelButton.Text = "戻る";
            this.CancelButton.UseVisualStyleBackColor = false;
            this.CancelButton.Visible = false;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // groupLabel
            // 
            this.groupLabel.AutoSize = true;
            this.groupLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.groupLabel.Location = new System.Drawing.Point(158, 50);
            this.groupLabel.Name = "groupLabel";
            this.groupLabel.Size = new System.Drawing.Size(0, 15);
            this.groupLabel.TabIndex = 8;
            // 
            // getGameButton
            // 
            this.getGameButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.getGameButton.FlatAppearance.BorderSize = 0;
            this.getGameButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.getGameButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.getGameButton.Location = new System.Drawing.Point(282, 42);
            this.getGameButton.Name = "getGameButton";
            this.getGameButton.Size = new System.Drawing.Size(100, 23);
            this.getGameButton.TabIndex = 9;
            this.getGameButton.Text = "ゲームの取得";
            this.getGameButton.UseVisualStyleBackColor = false;
            this.getGameButton.Click += new System.EventHandler(this.getGames);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "\"起動ファイル|*.exe;*.eXe\"";
            // 
            // checkDirectory
            // 
            this.checkDirectory.AutoSize = true;
            this.checkDirectory.Font = new System.Drawing.Font("Meiryo UI", 8F);
            this.checkDirectory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.checkDirectory.Location = new System.Drawing.Point(282, 18);
            this.checkDirectory.Name = "checkDirectory";
            this.checkDirectory.Size = new System.Drawing.Size(104, 18);
            this.checkDirectory.TabIndex = 10;
            this.checkDirectory.Text = "フォルダを探索する";
            this.checkDirectory.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(419, 141);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(480, 270);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            this.pictureBox1.DoubleClick += new System.EventHandler(this.setLargeImage);
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Meiryo UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.Title.Location = new System.Drawing.Point(413, 68);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(0, 35);
            this.Title.TabIndex = 12;
            this.Title.Visible = false;
            // 
            // openImageFileDialog
            // 
            this.openImageFileDialog.FileName = "openFileDialog2";
            this.openImageFileDialog.Filter = "\"画像ファイル|*.bmp;*.jpg;*.jpeg;*.png;*.PNG\"";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(933, 562);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.checkDirectory);
            this.Controls.Add(this.getGameButton);
            this.Controls.Add(this.groupLabel);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.rmGroupButton);
            this.Controls.Add(this.MinusButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PlusButton);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.groupTree);
            this.Controls.Add(this.playButton);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Game Launcher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_Close);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.TreeView groupTree;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ImageList allIconList;
        private System.Windows.Forms.Button PlusButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button MinusButton;
        private System.Windows.Forms.Button rmGroupButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Label groupLabel;
        private System.Windows.Forms.Button getGameButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.CheckBox checkDirectory;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.OpenFileDialog openImageFileDialog;
    }
}

