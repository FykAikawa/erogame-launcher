using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace erlauncher
{
    public partial class AddGroupForm : Form
    {
        public bool isNewGroup;
        public FolderInfo targetFolder;
        private string newGroupName;
        private List<FolderInfo> m_FolderList = new List<FolderInfo>();
        private List<GameInfo> m_allGameList = new List<GameInfo>();
        public AddGroupForm()
        {
            InitializeComponent();
        }
        public AddGroupForm(List<FolderInfo> folderList)
        {
            InitializeComponent();
            comboBox1.Visible = false;
            m_FolderList = folderList;
            m_allGameList = m_FolderList[0].GameList;            
            foreach (var fi in folderList)
            {
                if(fi.Name!="ALL")
                    comboBox1.Items.Add(fi.Name);
            }
            foreach(var gi in m_allGameList)
            {
                checkedListBox1.Items.Add(gi.displayName);
            }

            if (folderList.Count < 2)
            {
                checkBox1.Visible = false;
            }
            else
            {
                comboBox1.SelectedIndex = 0;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && textBox1.TextLength>0)
            {
                newGroupName = textBox1.Text;
                textBox1.Enabled = false;
                checkBox1.Enabled = false;
                checkedListBox1.Enabled = true;
                label2.Enabled = true;
                button1.Visible = true;
                label1.Text = "追加するゲームを選択し、決定を押してください。";

                if (m_FolderList.Exists(x => x.Name == newGroupName))
                {
                    var gl = m_FolderList.Find(x => x.Name == newGroupName).GameList;
                    foreach (var g in m_allGameList)
                    {
                        checkedListBox1.SetItemChecked(g.id, gl.Exists(x => x.id == g.id));
                    }
                }
                else
                {
                    for (int i = 0; i < checkedListBox1.Items.Count; ++i)
                    {
                        checkedListBox1.SetItemChecked(i, false);
                    }
                }

            }
        }

        //最終
        private void button1_Click(object sender, EventArgs e)
        {
            isNewGroup = !checkBox1.Checked;

            targetFolder = (isNewGroup) ? new FolderInfo(newGroupName) : m_FolderList[comboBox1.SelectedIndex + 1];
            if (isNewGroup && m_FolderList.Exists(x => x.Name == newGroupName))
            {
                isNewGroup = false;
                targetFolder = m_FolderList.Find(x => x.Name == newGroupName);
            }
            targetFolder.GameList.Clear();
            foreach (var name in checkedListBox1.CheckedItems)
            {
                targetFolder.GameList.Add(m_allGameList.Find(x => x.displayName == name));
            }
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Visible = !textBox1.Visible;
            comboBox1.Visible = !comboBox1.Visible;
            checkedListBox1.Enabled = comboBox1.Visible;
            button1.Visible = comboBox1.Visible;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedFolderGamelist = m_FolderList.Find(x => x.Name == comboBox1.SelectedItem.ToString()).GameList;
            var items = checkedListBox1.Items;
            for (int i = 0; i < items.Count; ++i)
            {
                string gName = items[i].ToString();
                checkedListBox1.SetItemChecked(i, selectedFolderGamelist.Exists(x => x.displayName == gName));
            }
            //foreach(var g in m_allGameList)
            //{
            //    checkedListBox1.SetItemChecked(g.id, gl.Exists(x => x.id == g.id));
            //}
        }
    }
}
