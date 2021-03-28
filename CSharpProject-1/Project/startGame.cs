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
    public partial class startGame : Form
    {
        

        public startGame()
        {
            InitializeComponent();
        }

        private void startGame_Load(object sender, EventArgs e)
        {
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            DialogResult dialogResult = form1.ShowDialog();   
        }

        private void button3_Click(object sender, EventArgs e)
        {
            showscore show = new showscore();
            show.ShowDialog();
        }   
    }
}
