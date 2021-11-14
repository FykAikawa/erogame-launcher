using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreTweet;
using System.Configuration;
using System.Xml.Serialization;
using System.IO;

namespace erlauncher
{
    public class TweetManager
    {
        public struct TwitterTokens
        {
            public string accessToken { get; set; }
            public string accessTokenSecret { get; set; }
            public string ID { get; set; }
        }

        private string consumerKey;
        private string consumerKeySecret;

        private TwitterTokens keys = new TwitterTokens();
        private Tokens token { get; set; }
        private OAuth.OAuthSession session;
        public bool hasAuthorized;
        private string TwitterInfoFile;
        
        public TweetManager()
        {
            hasAuthorized = false;

            //input your consumner keys
            var reader = new StreamReader("consumerKeys.csv");
            string[] consumerKeys = reader.ReadLine().Split(',');
            consumerKey = consumerKeys[0];
            consumerKeySecret = consumerKeys[1];

            TwitterInfoFile = ConfigurationManager.AppSettings["TwitterInfoFile"];
            LoadKeys();
        }
        ~TweetManager()
        {
            SaveKeys();
        }
        private void SaveKeys()
        {
            var serializer = new XmlSerializer(typeof(TwitterTokens));
            var sw = new StreamWriter(TwitterInfoFile, false, new UTF8Encoding(false));
            serializer.Serialize(sw, keys);
            sw.Close();
        }
        private void LoadKeys()
        {
            try
            {
                var serializer = new XmlSerializer(typeof(TwitterTokens));
                var sr = new StreamReader(TwitterInfoFile, new UTF8Encoding(false));
                keys = (TwitterTokens)serializer.Deserialize(sr);
                sr.Close();
                hasAuthorized = true;
            }
            catch
            {
                hasAuthorized = false;
            }
        }

        public void GetPincode()
        {
            session = OAuth.Authorize(consumerKey, consumerKeySecret);
            System.Diagnostics.Process.Start(session.AuthorizeUri.AbsoluteUri);
        }
        public bool CreateToken()
        {
            try
            {
                token = Tokens.Create(consumerKey, consumerKeySecret, keys.accessToken, keys.accessTokenSecret);
                hasAuthorized = true;
            }
            catch
            {
                hasAuthorized = false;
            }
            return hasAuthorized;
        }
        public bool CreateToken(string pincode)
        {
            try
            {
                token = OAuth.GetTokens(session, pincode);
                keys.accessToken = token.AccessToken;
                keys.accessTokenSecret = token.AccessTokenSecret;
                keys.ID = "@" + token.ScreenName;
                hasAuthorized = true;
            }
            catch
            {
                hasAuthorized = false;
            }
            return hasAuthorized;
        }

        public void PostTweet(string text, string imagePath)
        {
            var mediaID = token.Media.Upload(media: new System.IO.FileInfo(imagePath)).MediaId;
            token.Statuses.Update(new { status = text, media_ids = new long[] { mediaID } });
        }

        public string GetID()
        {
            return keys.ID;
        }
    }
}
