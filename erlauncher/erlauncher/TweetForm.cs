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
    public partial class TweetForm : Form
    {
        TweetManager tweetManager;
        string imagePath;
        public TweetForm(TweetManager _tweetManager,string _imagePath)
        {
            InitializeComponent();

            tweetManager = _tweetManager;
            imagePath = _imagePath;
            pictureBox1.ImageLocation = imagePath;

            if (tweetManager.hasAuthorized && tweetManager.CreateToken())
            {
                authPanel.Visible = false;
                tweetPanel.Visible = true;
                twitterIDLabel.Text = tweetManager.GetID();
            }
            else
            {
                tweetManager.GetPincode();
                authPanel.Visible = true;
                tweetPanel.Visible = false;
            }
        }

        private void pincodeBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var success = tweetManager.CreateToken(pincodeBox.Text);
                if (success)
                {
                    authPanel.Visible = false;
                    tweetPanel.Visible = true;
                    twitterIDLabel.Text = tweetManager.GetID();
                }
                else
                {
                    failedMessage.Visible = true;
                }
            }
        }

        private void tweetButton_Click(object sender, EventArgs e)
        {
            tweetManager.PostTweet(tweetText.Text, imagePath);
            this.Close();
        }
    }
}
