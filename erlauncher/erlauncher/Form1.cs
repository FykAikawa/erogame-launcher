using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Configuration;
using System.IO;
using System.Runtime.InteropServices;

namespace erlauncher
{
    public partial class Form1 : Form
    {
        public List<GameInfo> GameList { set; get; } = new List<GameInfo>();
        public List<FolderInfo> FolderList { set; get; } = new List<FolderInfo>();
        private string ListName;
        private string BaseFolderName;
        private string ScreenShotsDir;
        private float defaultFontSize;
        private float defaultPathFontSize;
        private Process gameProcess;
        private GameInfo playingGame;
        private List<ScreenShotInfo> thumbList = new List<ScreenShotInfo>();
        private KeyboardHook keyboardHook = new KeyboardHook();
        private TweetManager tweetManager = new TweetManager();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ListName = ConfigurationManager.AppSettings["ListName"];
            BaseFolderName = ConfigurationManager.AppSettings["BaseFolderName"];
            ScreenShotsDir = ConfigurationManager.AppSettings["ScreenShotDir"];
            defaultFontSize = Title.Font.SizeInPoints;
            defaultPathFontSize = selectedGamePathLabel.Font.SizeInPoints;

            LoadFolderList();
            Load_AllGame();

            for(int j = 0; j < FolderList.Count; ++j)
            {
                var gameNum = FolderList[j].GameList.Count;
                for(int i=0;i<gameNum;++i)
                {
                    FolderList[j].GameList[i] = GameList.Find(x => x.Path == FolderList[j].GameList[i].Path);
                }
            }

            groupTree.SelectedNode = groupTree.Nodes[0];

            if (!Directory.Exists(ScreenShotsDir))
            {
                Directory.CreateDirectory(ScreenShotsDir);
            }

            keyboardHook.KeyDownEvent += screenShotEvent;
            keyboardHook.Hook();

        }
        private void Form1_Close(object sender, FormClosingEventArgs e)
        {
            keyboardHook.UnHook();
            SaveFolderList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (gameProcess != null && !gameProcess.HasExited)
                {
                    MessageBox.Show($"起動中のゲームを終了してから起動してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    gameProcess = Process.Start((string)playButton.Tag);
                    playingGame = GameList.Find(x => x.Path == (string)playButton.Tag);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        private void ResetGroupTree(string selectedGroupName=null)
        {
            groupTree.Nodes.Clear();
            var children = new List<TreeNode>();
            foreach (var fi in FolderList)
            {
                var groupName = fi.Name;
                if (groupName != BaseFolderName)
                {
                    var node = new TreeNode(groupName);
                    node.Name = groupName;
                    children.Add(node);
                }
            }
            var baseNode = new TreeNode(BaseFolderName, children.ToArray());
            baseNode.Name = BaseFolderName;
            groupTree.Nodes.Add(baseNode);
            groupTree.ExpandAll();
            if (selectedGroupName == null || selectedGroupName==BaseFolderName)
            {
                groupTree.SelectedNode = groupTree.Nodes[0];
            }
            else
            {
                groupTree.SelectedNode = groupTree.Nodes[0].Nodes[FolderList.FindIndex(x => x.Name == selectedGroupName) - 1];
            }
        }
        private void ResetListView(string groupName)
        {
            listView1.Items.Clear();
            if (!FolderList.Exists(x => x.Name == groupName)) groupName = BaseFolderName;
            groupLabel.Text = groupName;
            FolderInfo selectedFolder = FolderList.Find(x => x.Name == groupName);

            foreach (var g in selectedFolder.GameList)
            {
                var lvi = new ListViewItem(g.displayName, g.id);
                lvi.Tag = g.Path;
                listView1.Items.Add(lvi);
            }
        }
        private void treeView1_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            ResetListView(e.Node.Name);
        }
        private void SaveFolderList()
        {
            var serializer = new XmlSerializer(typeof(List<FolderInfo>));
            var sw = new StreamWriter(ListName, false, new UTF8Encoding(false));
            serializer.Serialize(sw, FolderList);
            sw.Close();
        }
        private void LoadFolderList()
        {
            try
            {
                var serializer = new XmlSerializer(typeof(List<FolderInfo>));
                var sr = new StreamReader(ListName, new UTF8Encoding(false));
                FolderList = (List<FolderInfo>)serializer.Deserialize(sr);

                var children = new List<TreeNode>();
                foreach (var fi in FolderList)
                {
                    var groupName = fi.Name;
                    if (groupName != BaseFolderName)
                    {
                        var node = new TreeNode(groupName);
                        node.Name = groupName;
                        children.Add(node);
                    }
                }
                var baseNode = new TreeNode(BaseFolderName, children.ToArray());
                baseNode.Name = BaseFolderName;
                groupTree.Nodes.Add(baseNode);
                groupTree.ExpandAll();
                sr.Close();
            }
            catch
            {
                //Initialize Folder List
                var baseFolder = new FolderInfo(BaseFolderName);
                FolderList.Add(baseFolder);
                var node = new TreeNode(BaseFolderName);
                node.Name = BaseFolderName;
                groupTree.Nodes.Add(node);
            }
        }

        private void Load_AllGame()
        {
            try
            {
                var allGameList = FolderList.Find(x => x.Name == BaseFolderName).GameList;
                int idx = 0;
                var rmGameList = new List<GameInfo>();
                foreach (var gi in allGameList)
                {
                    if (!File.Exists(gi.Path))
                    {
                        MessageBox.Show($"{gi.Path}は存在しません。\nリストから削除します。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        rmGameList.Add(gi);
                        continue;
                    }
                    if (gi.imagepath!=null && !File.Exists(gi.imagepath))
                    {
                        MessageBox.Show($"{gi.imagepath}が見つかりません。\n表示画像を設定しなおしてください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        gi.imagepath = null;
                    }
                    gi.id = idx++;
                    var icon = Icon.ExtractAssociatedIcon(gi.Path);
                    allIconList.Images.Add(icon);
                }
                GameList = allGameList;

                if (rmGameList.Count != 0)
                {
                    removeGame(rmGameList);
                }
            }catch(Exception e)
            {
                MessageBox.Show(e.Message);
                this.Close();
            }
        }

        private void PlusButton_Click(object sender, EventArgs e)
        {
            var inputForm = new AddGroupForm(FolderList);
            inputForm.ShowDialog();
            var targetFolder = inputForm.targetFolder;
            if (targetFolder != null)
            {

                if (inputForm.isNewGroup)
                {
                    var newGroupName = targetFolder.Name;
                    var newNode = new TreeNode(newGroupName);
                    newNode.Name = newGroupName;
                    groupTree.TopNode.Nodes.Add(newNode);
                    //targetFolder.GameList = inputForm.gameList;
                    FolderList.Add(targetFolder);
                }
                else
                {
                    FolderList.Find(x => x.Name == targetFolder.Name).GameList = targetFolder.GameList;
                }
                ResetListView(targetFolder.Name);
                groupTree.SelectedNode = groupTree.Nodes[0].Nodes[FolderList.IndexOf(targetFolder) - 1];
            }
        }
        private void getGames(object sender, EventArgs e)
        {
            var newGameList = new List<GameInfo>();
            if (checkDirectory.Checked)
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    var dirName = folderBrowserDialog1.SelectedPath;
                    var files = Directory.EnumerateFiles(dirName, "*.*", SearchOption.AllDirectories).Where(x => x.EndsWith(".exe") || x.EndsWith(".eXe"));
                    foreach (var path in files)
                    {
                        if (GameList.Find(x => x.Path == path) == null)
                        {
                            var gi = new GameInfo(Path.GetFileName(Path.GetDirectoryName(path)), path);
                            newGameList.Add(gi);
                        }
                    }

                    if (newGameList.Count == 0)
                    {
                        MessageBox.Show("未追加のファイルは見つかりませんでした。", "", MessageBoxButtons.OK);
                        return;
                    }
                }
            }
            else if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var path = openFileDialog1.FileName;
                if (GameList.Find(x => x.Path == path) == null)
                {
                    var gi = new GameInfo(Path.GetFileName(Path.GetDirectoryName(path)), path);
                    newGameList.Add(gi);
                }

                if (newGameList.Count == 0)
                {
                    MessageBox.Show($"{path}は既に追加されています。", "", MessageBoxButtons.OK);
                    return;
                }
            }

            if (newGameList.Count == 0)
            {
                return;
            }

            var cgiForm = new createGameInfoForm(newGameList);
            cgiForm.ShowDialog();
            if (cgiForm.dialogResult)
            {
                int idx = GameList.Count;
                foreach (var g in cgiForm.newGameList)
                {
                    allIconList.Images.Add(Icon.ExtractAssociatedIcon(g.Path));
                    g.id = idx++;
                    GameList.Add(g);
                }
                FolderList.Find(x => x.Name == BaseFolderName).GameList = GameList;
                ResetListView(BaseFolderName);
                groupTree.SelectedNode = groupTree.Nodes[0];
            }
        }

        private async void MinusButton_Click(object sender, EventArgs e)
        {
            groupTree.CheckBoxes = true;
            listView1.CheckBoxes = true;
            foreach (Control ctrls in this.Controls)
            {
                if (ReferenceEquals(ctrls, PlusButton) ||
                    ReferenceEquals(ctrls, MinusButton) ||
                    ReferenceEquals(ctrls, getGameButton) ||
                    ReferenceEquals(ctrls, playButton) ||
                    ReferenceEquals(ctrls, checkDirectory)
                    ) ctrls.Enabled = false;
            }
            rmGroupButton.Visible = true;
            CancelButton.Visible = true;
        }
        private void CancelButton_Click(object sender, EventArgs e)
        {
            foreach (Control ctrls in this.Controls)
            {
                if (ReferenceEquals(ctrls, PlusButton) ||
                    ReferenceEquals(ctrls, MinusButton) ||
                    ReferenceEquals(ctrls, getGameButton) ||
                    ReferenceEquals(ctrls, playButton) ||
                    ReferenceEquals(ctrls, checkDirectory)
                    ) ctrls.Enabled = true;
            }
            groupTree.CheckBoxes = false;
            listView1.CheckBoxes = false;
            rmGroupButton.Visible = false;
            CancelButton.Visible = false;

            ResetGroupTree();
        }

        private void rmGroupButton_Click(object sender, EventArgs e)
        {
            //groupTreeがチェックされているかの代わり
            if (listView1.Enabled)
            {
                if (listView1.CheckedItems.Count < 1) return;

                if (groupTree.SelectedNode.Name == BaseFolderName)
                {
                    var result = MessageBox.Show("選択されたゲームをランチャーから削除しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2);
                    if(result== DialogResult.Yes)
                    {
                        foreach (ListViewItem lvi in listView1.CheckedItems)
                        {
                            removeGame(lvi.ImageIndex);
                        }

                        foreach (Control ctrls in this.Controls)
                        {
                            ctrls.Enabled = true;
                        }
                        groupTree.CheckBoxes = false;
                        listView1.CheckBoxes = false;
                        rmGroupButton.Visible = false;
                        CancelButton.Visible = false;

                        ResetGroupTree();
                    }
                }
                else
                {
                    var targetFolderName = groupTree.SelectedNode.Name;
                    var result = MessageBox.Show($"選択されたゲームを{targetFolderName}から削除しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.Yes)
                    {
                        var targetFolder = FolderList.Find(x => x.Name == targetFolderName);
                        foreach (ListViewItem lvi in listView1.CheckedItems)
                        {
                            removeGame(lvi.ImageIndex, targetFolder);
                        }

                        foreach (Control ctrls in this.Controls)
                        {
                            ctrls.Enabled = true;
                        }
                        groupTree.CheckBoxes = false;
                        listView1.CheckBoxes = false;
                        rmGroupButton.Visible = false;
                        CancelButton.Visible = false;

                        ResetGroupTree(targetFolderName);
                    }
                }

            }
            else
            {
                var targetFolderName = new List<string>();
                var message = "";
                foreach (TreeNode node in groupTree.TopNode.Nodes)
                {
                    if (node.Checked)
                    {
                        targetFolderName.Add(node.Name);
                        if (message != "") message += ",";
                        message += node.Name;
                    }
                }
                var result = MessageBox.Show($"グループ:{message} を削除しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    FolderList.RemoveAll(x => targetFolderName.Contains(x.Name));

                    foreach (Control ctrls in this.Controls)
                    {
                        ctrls.Enabled = true;
                    }
                    groupTree.CheckBoxes = false;
                    listView1.CheckBoxes = false;
                    rmGroupButton.Visible = false;
                    CancelButton.Visible = false;

                    ResetListView(groupTree.SelectedNode.Name);
                    ResetGroupTree();
                }
                else
                {
                    foreach (TreeNode node in groupTree.TopNode.Nodes)
                    {
                        node.Checked = false;
                    }
                }
            }

        }

        private void groupTree_afterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Name == BaseFolderName)
            {
                if (e.Node.Checked)
                {
                    MessageBox.Show($"{BaseFolderName}は変更できません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Node.Checked = false;
                }
            }
            else
            {
                groupTree.SelectedNode = e.Node;
                ResetListView(e.Node.Name);
                foreach (ListViewItem item in listView1.Items)
                {
                    item.Checked = (e.Node.Checked);
                }
                listView1.Enabled = !e.Node.Checked;
            }

        }

        private void removeGame(int gameid,FolderInfo targetFolder = null)
        {
            //ランチャーから削除
            if (targetFolder == null || targetFolder.Name==BaseFolderName)
            {
                foreach (var fi in FolderList)
                {
                    fi.GameList.RemoveAll(x => x.id == gameid);
                }
            }
            //フォルダから削除
            else
            {
                targetFolder.GameList.RemoveAll(x => x.id == gameid);
            }
        }
        private void removeGame(List<GameInfo> rmGameList)
        {
            foreach (var fi in FolderList)
            {
                fi.GameList.RemoveAll(x => rmGameList.Exists(l => l.Path == x.Path));
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                playButton.Visible = true;
                playButton.Tag = listView1.SelectedItems[0].Tag;
                Title.Visible = true;
                Title.Text = listView1.SelectedItems[0].Text;
                pictureBox1.Visible = true;
                selectedGamePathLabel.Visible = true;
                selectedGamePathLabel.Text = (string)listView1.SelectedItems[0].Tag;
                screenShotPanel.Visible = true;

                var largeImagePath = GameList.Find(x => x.id == listView1.SelectedItems[0].ImageIndex).imagepath;
                var scDir = $"{ScreenShotsDir}\\{listView1.SelectedItems[0].Text}";
                thumbList.Clear();
                if (Directory.Exists(scDir))
                {
                    LoadThumbnail(scDir);
                    if (thumbList.Count > 0)
                    {
                        if (largeImagePath == null || !File.Exists(largeImagePath))
                        {
                            selectedThumbIndex = 0;
                            largeImagePath = thumbList[selectedThumbIndex].FilePath;
                        }
                        else
                        {
                            selectedThumbIndex = thumbList.FindIndex(x => x.FilePath == largeImagePath);
                            if (selectedThumbIndex > 0)
                            {
                                largeImagePath = thumbList[selectedThumbIndex].FilePath;
                            }
                        }
                    }
                    else
                    {
                        screenShotPanel.Visible = false;
                    }
                }
                else
                {
                    screenShotPanel.Visible = false;
                }

                if (thumbList.Count > 0)
                {
                    label2.Visible = false;
                }
                else
                {
                    largeImagePath = null;
                    label2.Visible = true;
                }
                
                var canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                var g = Graphics.FromImage(canvas);
                
                var src = (largeImagePath == null) ?
                    listView1.SmallImageList.Images[listView1.SelectedItems[0].ImageIndex]
                    : Image.FromFile(largeImagePath);
                var f = Math.Min((float)canvas.Width / src.Width, (float)canvas.Height / src.Height);
                var sx = Math.Abs((canvas.Width - src.Width * f) / 2.0f);
                var sy = Math.Abs((canvas.Height - src.Height * f) / 2.0f);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(src, sx, sy, src.Width * f, src.Height * f);
                pictureBox1.Image = canvas;
                
                float limWidth = 400;
                float width = defaultFontSize * Title.Text.Length;
                if (width > limWidth)
                {
                    Title.Font = new Font(Title.Font.FontFamily, defaultFontSize * limWidth / width);
                }
                else
                {
                    Title.Font = new Font(label1.Font.FontFamily, defaultFontSize);
                }

                limWidth = 450;
                width = defaultPathFontSize * selectedGamePathLabel.Text.Length;
                if (width > limWidth)
                {
                    selectedGamePathLabel.Font = new Font(selectedGamePathLabel.Font.FontFamily, defaultPathFontSize * limWidth / width);
                }
                else
                {
                    selectedGamePathLabel.Font = new Font(selectedGamePathLabel.Font.FontFamily, defaultPathFontSize);
                }
            }
        }

        private void setLargeImage(object sender, EventArgs e)
        {
            if (openImageFileDialog.ShowDialog() == DialogResult.OK)
            {
                GameList.Find(x => x.id == listView1.SelectedItems[0].ImageIndex).imagepath= openImageFileDialog.FileName;
                var canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                var g = Graphics.FromImage(canvas);
                var largeImagePath = GameList.Find(x => x.id == listView1.SelectedItems[0].ImageIndex).imagepath;
                var src = Image.FromFile(largeImagePath);
                var f = Math.Min((float)canvas.Width / src.Width, (float)canvas.Height / src.Height);
                var sx = Math.Abs((canvas.Width - src.Width * f) / 2.0f);
                var sy = Math.Abs((canvas.Height - src.Height * f) / 2.0f);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(src, sx, sy, src.Width * f, src.Height * f);
                pictureBox1.Image = canvas;
            }
        }

        private void setLargeImage(string path)
        {
            GameList.Find(x => x.Path == selectedGamePathLabel.Text).imagepath = path;
            var canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            var g = Graphics.FromImage(canvas);
            var largeImagePath = GameList.Find(x => x.Path == selectedGamePathLabel.Text).imagepath;
            var src = Image.FromFile(largeImagePath);
            var f = Math.Min((float)canvas.Width / src.Width, (float)canvas.Height / src.Height);
            var sx = Math.Abs((canvas.Width - src.Width * f) / 2.0f);
            var sy = Math.Abs((canvas.Height - src.Height * f) / 2.0f);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(src, sx, sy, src.Width * f, src.Height * f);
            pictureBox1.Image = canvas;
        }

        private void selectedGamePath_DoubleClick(object sender, EventArgs e)
        {
            openFileDialog1.FileName = Path.GetFileName(((Label)sender).Text);
            openFileDialog1.InitialDirectory = Path.GetDirectoryName(((Label)sender).Text);
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var path = openFileDialog1.FileName;
                var selectedGameInfo = GameList.Find(x => x.displayName == Title.Text);
                selectedGameInfo.Path = path;
                selectedGamePathLabel.Text = path;

                allIconList.Images[selectedGameInfo.id] = (Image)(new Bitmap(Icon.ExtractAssociatedIcon(path).ToBitmap(), allIconList.ImageSize));
                if (selectedGameInfo.imagepath == null)
                {
                    var canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                    var g = Graphics.FromImage(canvas);
                    var src = listView1.SmallImageList.Images[selectedGameInfo.id];
                    var f = Math.Min((float)canvas.Width / src.Width, (float)canvas.Height / src.Height);
                    var sx = Math.Abs((canvas.Width - src.Width * f) / 2.0f);
                    var sy = Math.Abs((canvas.Height - src.Height * f) / 2.0f);
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(src, sx, sy, src.Width * f, src.Height * f);
                    pictureBox1.Image = canvas;
                }

                var limWidth = 450;
                var width = defaultPathFontSize * selectedGamePathLabel.Text.Length;
                if (width > limWidth)
                {
                    selectedGamePathLabel.Font = new Font(selectedGamePathLabel.Font.FontFamily, defaultPathFontSize * limWidth / width);
                }
                else
                {
                    selectedGamePathLabel.Font = new Font(selectedGamePathLabel.Font.FontFamily, defaultPathFontSize);
                }
                
                ResetListView(groupTree.SelectedNode.Name);
            }

        }

        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
            public Size Size
            {
                get { return new Size(Right - Left, Bottom - Top); }
            }
        }

        [DllImport("User32.dll")]
        private static extern bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags);
        [DllImport("User32.dll")]
        private static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);
        private void screenShotEvent(object sender, KeyEventArg e)
        {
            //F12:0x7b
            //PrintScreen:0x2c
            if (e.KeyCode == 0x2c && gameProcess != null && !gameProcess.HasExited)
            {
                try
                {
                    var scDir = $"{ScreenShotsDir}\\{playingGame.displayName}";
                    if (!Directory.Exists(scDir))
                    {
                        Directory.CreateDirectory(scDir);
                    }

                    var handle = gameProcess.MainWindowHandle;
                    var rec = new RECT();
                    GetClientRect(handle, out rec);

                    var img = new Bitmap(rec.Right - rec.Left, rec.Bottom - rec.Top);
                    using (var g = Graphics.FromImage(img))
                    {
                        IntPtr dc = g.GetHdc();
                        PrintWindow(handle, dc, 1);
                        g.ReleaseHdc(dc);
                        g.Dispose();
                    }

                    var date = DateTime.Now.ToString("yyyyMMddHHmmss");
                    img.Save($"{scDir}\\{date}.png");
                    thumbList.Clear();
                    screenShotPanel.Visible = true;
                    LoadThumbnail(scDir);
                    pictureBox2.Invalidate();
                }catch(Exception err)
                {
                    MessageBox.Show($"スクリーンショットの取得に失敗しました。\n{err.Message}","エラー", MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }
        //const int ThumbWidth = 150;
        const int ThumbHeight = 70;
        int ItemWidth;
        const int LestMargin = 12;

        int selectedThumbIndex = -1;

        private void LoadThumbnail(string targertFolder)
        {
            var files = Directory.GetFiles(targertFolder, "*.png");
            Array.Reverse(files);
            foreach (string path in files)
            {
                var size = Image.FromFile(path).Size;
                double f = (double)ThumbHeight / size.Height;
                int ThumbWidth = (int)Math.Round(f * size.Width);
                ItemWidth = ThumbWidth + 24;
                thumbList.Add(new ScreenShotInfo(path, ThumbWidth, ThumbHeight));
                if (!hScrollBar1.Enabled)
                {
                    pictureBox2.Invalidate();
                }

                if (ItemWidth * thumbList.Count <= pictureBox2.Width)
                {
                    hScrollBar1.Value = 0;
                    hScrollBar1.Enabled = false;
                }
                else
                {
                    int largeChange = pictureBox2.Width;
                    hScrollBar1.Maximum = ItemWidth * thumbList.Count - pictureBox2.Width + largeChange;
                    hScrollBar1.LargeChange = largeChange;
                    hScrollBar1.SmallChange = largeChange / 4;
                    hScrollBar1.Enabled = true;
                }
            }
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            if (thumbList.Count > 0)
            {
                int itemIndex = hScrollBar1.Value / ItemWidth;

                int y = (pictureBox2.ClientSize.Height - ThumbHeight) / 2;
                for (int x = -(hScrollBar1.Value % ItemWidth); x < pictureBox2.Width && itemIndex < thumbList.Count; x += ItemWidth)
                {
                    Brush br = (itemIndex == selectedThumbIndex) ? SystemBrushes.Highlight : new SolidBrush(Color.FromArgb(32, 32, 38));
                    e.Graphics.FillRectangle(br, x, 0, ItemWidth, pictureBox2.ClientSize.Height);

                    e.Graphics.DrawImage(thumbList[itemIndex].Thumbnail, x+LestMargin, y);

                    ++itemIndex;
                }
            }
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            int itemIndex = (e.Location.X + hScrollBar1.Value) / ItemWidth;
            if (itemIndex < thumbList.Count)
            {
                selectedThumbIndex = itemIndex;
                pictureBox2.Invalidate();
                setLargeImage(thumbList[selectedThumbIndex].FilePath);
            }
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            pictureBox2.Invalidate();
        }

        private void pictureBox2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //delete:0x2e
            MessageBox.Show("presse");
            MessageBox.Show(e.KeyCode.ToString());
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            
            GC.Collect();
            GC.WaitForPendingFinalizers();
            File.Delete(thumbList[selectedThumbIndex].FilePath);
            thumbList.Clear();
            var scDir = $"{ScreenShotsDir}\\{Title.Text}";
            LoadThumbnail(scDir);
            if (thumbList.Count > 0)
            {
                if (selectedThumbIndex > 0)
                    --selectedThumbIndex;
                setLargeImage(thumbList[selectedThumbIndex].FilePath);
            }
            else
            {
                screenShotPanel.Visible = false;
                label2.Visible = true;
            }

            
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            var scDir = $"{ScreenShotsDir}\\{Title.Text}";
            if (Directory.Exists(scDir))
            {
                Process.Start("EXPLORER.EXE", scDir);
            }
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var imagepath = GameList.Find(x => x.Path == selectedGamePathLabel.Text).imagepath;
            var tweetForm = new TweetForm(tweetManager, imagepath);
            tweetForm.ShowDialog();
        }

        private void GameNameChangeItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                listView1.SelectedItems[0].BeginEdit();
            }
        }

        private void GameRemoveItem_Click(object sender, EventArgs e)
        {
            if (groupTree.SelectedNode.Name == BaseFolderName)
            {
                var result = MessageBox.Show("選択されたゲームをランチャーから削除しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    foreach (ListViewItem lvi in listView1.SelectedItems)
                    {
                        removeGame(lvi.ImageIndex);
                    }

                    ResetGroupTree();
                }
            }
            else
            {
                var targetFolderName = groupTree.SelectedNode.Name;
                var result = MessageBox.Show($"選択されたゲームを{targetFolderName}から削除しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    var targetFolder = FolderList.Find(x => x.Name == targetFolderName);
                    foreach (ListViewItem lvi in listView1.SelectedItems)
                    {
                        removeGame(lvi.ImageIndex, targetFolder);
                    }

                    ResetGroupTree(targetFolderName);
                }
            }
        }

        private void GameListContextMenu_Opening(object sender, CancelEventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                GameNameChangeItem.Enabled = true;
                GameInfoChangeItem.Enabled = true;
                GameRemoveItem.Enabled = true;
            }
            else
            {
                GameNameChangeItem.Enabled = false;
                GameInfoChangeItem.Enabled = false;
                GameRemoveItem.Enabled = false;
            }
        }

        private void listView1_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            GameList.Find(x => x.id == listView1.SelectedItems[0].ImageIndex).displayName = e.Label;
            Title.Text = e.Label;
        }

        private void GameInfoChangeItem_Click(object sender, EventArgs e)
        {
            var nowPath = GameList.Find(x => x.id == listView1.SelectedItems[0].ImageIndex).Path;
            openFileDialog1.FileName = Path.GetFileName(nowPath);
            openFileDialog1.InitialDirectory = Path.GetDirectoryName(nowPath);
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var path = openFileDialog1.FileName;
                var selectedGameInfo = GameList.Find(x => x.displayName == Title.Text);
                selectedGameInfo.Path = path;
                selectedGamePathLabel.Text = path;

                allIconList.Images[selectedGameInfo.id] = (Image)(new Bitmap(Icon.ExtractAssociatedIcon(path).ToBitmap(), allIconList.ImageSize));
                if (selectedGameInfo.imagepath == null)
                {
                    var canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                    var g = Graphics.FromImage(canvas);
                    var src = listView1.SmallImageList.Images[selectedGameInfo.id];
                    var f = Math.Min((float)canvas.Width / src.Width, (float)canvas.Height / src.Height);
                    var sx = Math.Abs((canvas.Width - src.Width * f) / 2.0f);
                    var sy = Math.Abs((canvas.Height - src.Height * f) / 2.0f);
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(src, sx, sy, src.Width * f, src.Height * f);
                    pictureBox1.Image = canvas;
                }

                var limWidth = 450;
                var width = defaultPathFontSize * selectedGamePathLabel.Text.Length;
                if (width > limWidth)
                {
                    selectedGamePathLabel.Font = new Font(selectedGamePathLabel.Font.FontFamily, defaultPathFontSize * limWidth / width);
                }
                else
                {
                    selectedGamePathLabel.Font = new Font(selectedGamePathLabel.Font.FontFamily, defaultPathFontSize);
                }

                ResetListView(groupTree.SelectedNode.Name);
            }
        }
    }
}
