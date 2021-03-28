using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class startGame : Form
    {
        IPAddress localhost;
        int port;
        public TcpClient client;
        byte[] b;
        NetworkStream networkStream;
        public BinaryReader br;
        public  BinaryWriter bw;
       
        int category, level;
        string category_name, level_name;

        public startGame()
        {
           
            InitializeComponent();
            port = 2021;
            b = new byte[] { 127, 0, 0, 1 };
            localhost = new IPAddress(b);
        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            close_connect();
            this.Close();
        }
        public void close_connect() {
            br.Close();
            bw.Close();
            networkStream.Close();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ShowScore show =new ShowScore();
            show.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            play();
        }

        public void play()
        {
            try
            {
                client = new TcpClient();
                client.Connect(localhost, port);
                networkStream = client.GetStream();
                br = new BinaryReader(networkStream);
                bw = new BinaryWriter(networkStream);
                string word = "";
                if (client.Connected)
                {
                    category = br.ReadInt32();
                    level = br.ReadInt32();

                    if (category == 1) { category_name = "Football"; }
                    else if (category == 2) { category_name = "Actors"; }
                    else if (category == 3) { category_name = "Films"; }
                    else if (category == 4) { category_name = "Politions"; }
                    else if (category == 5) { category_name = "Countrys"; }
                    if (level == 1) { level_name = "Easy"; }
                    else if (level == 2) { level_name = "Meduim"; }
                    else if (level == 3) { level_name = "Hard"; }
                }


                DialogResult result2 = MessageBox.Show($"Do You Want to Start Game?! with Category:{category_name} and Level:{level_name}",
                  "Important Query", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result2 == DialogResult.Yes)
                {
                    bw.Write(true);
                    word = br.ReadString();
                    MainBody mainBody = new MainBody(word, this);
                    mainBody.ShowDialog();
                }
                else if (result2 == DialogResult.No)
                {
                    bw.Write(false);
                    MessageBox.Show("press play again to play");
                    close_connect();
                }
            }
            catch
            {
                MessageBox.Show("No server to connect");
            }
        }

    }
}

