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

namespace erlauncher
{
    public partial class Form1 : Form
    {
        public List<GameInfo> GameList { set; get; } = new List<GameInfo>();
        public List<FolderInfo> FolderList { set; get; } = new List<FolderInfo>();
        private string ListName;
        private string BaseFolderName;
        private float defaultFontSize;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ListName = ConfigurationManager.AppSettings["ListName"];
            BaseFolderName = ConfigurationManager.AppSettings["BaseFolderName"];
            defaultFontSize = Title.Font.SizeInPoints;

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
        }
        private void Form1_Close(object sender, FormClosingEventArgs e)
        {
            SaveFolderList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start((string)playButton.Tag);
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

            int idx = GameList.Count;
            foreach(var g in cgiForm.newGameList)
            {
                allIconList.Images.Add(Icon.ExtractAssociatedIcon(g.Path));
                g.id = idx++;
                GameList.Add(g);
            }
            FolderList.Find(x => x.Name == BaseFolderName).GameList = GameList;
            ResetListView(BaseFolderName);
            groupTree.SelectedNode = groupTree.Nodes[0];
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

                var canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                var g = Graphics.FromImage(canvas);
                var largeImagePath = GameList.Find(x => x.id == listView1.SelectedItems[0].ImageIndex).imagepath;
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
                    Title.Font = new Font(label1.Font.FontFamily, defaultFontSize * limWidth / width);
                }
                else
                {
                    Title.Font = new Font(label1.Font.FontFamily, defaultFontSize);
                }
            }
        }

        private void setLargeImage(object sender, EventArgs e)
        {
            if (openImageFileDialog.ShowDialog() == DialogResult.OK)
            {
                GameList.Find(x => x.id == listView1.SelectedItems[0].ImageIndex).imagepath= openImageFileDialog.FileName;                
            }

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
}
