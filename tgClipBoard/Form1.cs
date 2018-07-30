using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tgClipBoard
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            WindowState = FormWindowState.Minimized;                        
        }

        private void button1_ClipboardChanged(object sender, ClipboardChangedEventArgs e)
        {
            try
            {             
                //string clipboardContentType = e.DataObject.GetType().ToString(); ;
                //if (clipboardContentType == DataFormats.Text)                
                if (!e.DataObject.GetDataPresent(DataFormats.Text))
                    return;
                string g = e.DataObject.GetData(DataFormats.Text).ToString();
                string patternVola, patternMega, patternSendspace, patternMediafire;
                patternVola = "^[/]?r/.+";
                patternMega = "^[/]?#F!.+";
                patternSendspace = "^(?:[/]?file/.{6}|[/]?filegroup/.+)$";
                //patternSendspaceGroup = "[/]?file[group]?.{6}";
                patternMediafire = "^[/]?(?:file|folder)/.{10,}";
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add(patternVola, "volafile.org");
                dic.Add(patternMega, "mega.nz");
                dic.Add(patternSendspace, "sendspace.com");
                dic.Add(patternMediafire, "mediafire.com");
                List<string> patterns = new List<string> { patternVola, patternSendspace, patternMega, patternMediafire };
                foreach(string pattern in dic.Keys)
                {
                    if (Regex.Match(g, pattern).Success)
                    {
                        if (!g.StartsWith("/"))
                            g = "/" + g;
                        System.Diagnostics.Process.Start("http://" + dic[pattern] + g);
                        //notifyIcon1.BalloonTipText = dic[pattern];
                        //notifyIcon1.ShowBalloonTip(1);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                // Swallow or pop-up, not sure
                // Trace.Write(e.ToString());
                MessageBox.Show(ex.ToString());
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            //if the form is minimized  
            //hide it from the task bar  
            //and show the system tray icon (represented by the NotifyIcon control)  
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
        
