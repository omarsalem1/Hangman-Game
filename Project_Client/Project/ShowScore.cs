using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class ShowScore : Form
    {
        public ShowScore()
        {
            InitializeComponent();
        }

        private void ShowScore_Load(object sender, EventArgs e)
        {
           
                DirectoryInfo dir = new DirectoryInfo(".");
                string Path_SaveFile = dir.FullName + "\\saveFile.txt";
                try
                {
                    StreamReader txt = File.OpenText(Path_SaveFile);
                    textBox1.Text = txt.ReadToEnd();
                    txt.Close();
                }
                catch (FileNotFoundException err) { MessageBox.Show("No file to read");this.Close(); }
           
        }
    }
}
