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
    public partial class createGameInfoForm : Form
    {
        public List<GameInfo> newGameList { set; get; } = new List<GameInfo>();
        public bool dialogResult = false;
        public createGameInfoForm()
        {
            InitializeComponent();
        }

        public createGameInfoForm(List<GameInfo> gList)
        {
            InitializeComponent();
            newGameList = gList;
            this.displayName.Width = -2;
            this.path.Width = -2;
            ListView_Load();
        }
        private void ListView_Load()
        {
            listView1.Items.Clear();
            foreach (var g in newGameList)
            {
                string[] row = { g.displayName, g.Path };
                var item = new ListViewItem(row);
                listView1.Items.Add(item);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var ngl = new List<GameInfo>();
            foreach (ListViewItem item in listView1.CheckedItems)
            {
                ngl.Add(newGameList.Find(x => x.Path == item.SubItems[1].Text));
            }
            newGameList = ngl;
            ListView_Load();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dialogResult = true;
            var ngl = new List<GameInfo>();
            foreach (ListViewItem item in listView1.CheckedItems)
            {
                ngl.Add(newGameList.Find(x => x.Path == item.SubItems[1].Text));
            }
            newGameList = ngl;
            this.Close();
        }

        private void listView1_mouseClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                listView1.SelectedItems[0].BeginEdit();
            }
        }

        private void listView1_selectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                listView1.SelectedItems[0].Checked = !listView1.SelectedItems[0].Checked;
            }
        }

        private void listView1_beforeLabelEdit(object sender, LabelEditEventArgs e)
        {
            listView1.FullRowSelect = false;
        }

        private void listView1_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            listView1.FullRowSelect = true;
            if (e.Label != null)
            {
                var path = listView1.SelectedItems[0].SubItems[1].Text;
                newGameList.Find(x => x.Path == path).displayName = e.Label;
            }
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            button2.Enabled = (listView1.CheckedItems.Count > 0);
        }
    }
}
