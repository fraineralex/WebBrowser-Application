using EasyTabs;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASUS_Browser
{
    public partial class FrainerBrowser : Form
    {
        public FrainerBrowser()
        {
            InitializeComponent();

            var appName = System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".exe";
            RegistryKey Key = Registry.CurrentUser.OpenSubKey
            (@"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", true);

            if (Key == null)
            {
                RegistryKey registryFolder = Registry.CurrentUser.OpenSubKey
                    (@"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl", true);
                Key = registryFolder.CreateSubKey("FEATURE_BROWSER_EMULATION");
            }

            Key.SetValue(appName, 99999, RegistryValueKind.DWord);
            webBrowser1.Navigate("https://www.bing.com/?setlang=es");
            btnRefresh.Image = imgRefresh.Image;


        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            
            txtSearchingBar.Text = webBrowser1.Url.AbsoluteUri;
            btnRefresh.Image = imgRefresh.Image;

        }

        protected TitleBarTabs parentTabs
        {
            get
            {
                return (ParentForm as TitleBarTabs);
            }

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoBack) webBrowser1.GoBack();
        }

        private void btnSecure_Click(object sender, EventArgs e)
        {
            txtSearchingBar.Text = "";
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoForward) webBrowser1.GoForward();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();

        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://www.bing.com/?setlang=es");

        }

        private void txtSearchingBar_TextChanged(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtSearchingBar.Text.Trim().Length > 0) 
            {
                if (txtSearchingBar.Text.Contains("."))
                {
                    webBrowser1.Navigate(txtSearchingBar.Text.Trim());
                }
                else
                {
                    webBrowser1.Navigate("https://www.bing.com/search?q=" + txtSearchingBar.Text.Trim().Replace(" ", "+") + "&qs=n&form=QBRE&sp=-1&pq=" + txtSearchingBar.Text.Trim().Replace(" ", "+") + "&sc=8-8&sk=&cvid=77E0EBABF0B44E61AB6DCE894DF6E943&ghsh=0&ghacc=0");
                }

            }
        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            btnRefresh.Image = imgSpiner.Image;
        }
    }
}
