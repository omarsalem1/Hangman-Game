using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace Project
{
    public partial class Form1 : Form
    {
        public int port;
        public Socket connection;
        public NetworkStream nstream;
        public BinaryReader bR;
        public BinaryWriter bW;
        public TcpListener server;
        int flag_level = 0;
        int flag_category = 0;
        int flag_back = 0;
        bool flag_next = false;
        public Form1()
        {
            InitializeComponent();
            port = 2021;
            server = new TcpListener(IPAddress.Any, port);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           Categrory1.Enabled = Categrory2.Enabled = Categrory3.Enabled = button1.Enabled=button2.Enabled = false;
            
        }

        private void Easy_Click(object sender, EventArgs e)
        {
            flag_back = 1; flag_level = 1; 
     
            Categrory1.Enabled = Categrory2.Enabled = Categrory3.Enabled = button1.Enabled = button2.Enabled = true;
            Easy.Enabled = Meduim.Enabled = Hard.Enabled = false;
        }

        private void Meduim_Click(object sender, EventArgs e)
        {
            flag_back = 1; flag_level = 2; 
            
            Categrory1.Enabled = Categrory2.Enabled = Categrory3.Enabled = button1.Enabled = button2.Enabled = true;
            Easy.Enabled = Meduim.Enabled = Hard.Enabled = false;
        }

        private void Hard_Click(object sender, EventArgs e)
        {
            flag_back = 1; flag_level = 3; 
            
            Categrory1.Enabled = Categrory2.Enabled = Categrory3.Enabled = button1.Enabled = button2.Enabled = true;
            Easy.Enabled = Meduim.Enabled = Hard.Enabled = false;
        }

        private void Categrory1_Click(object sender, EventArgs e)
        {
            if (flag_level == 1|| flag_level == 2|| flag_level == 3) 
            {
                Easy.Enabled = Meduim.Enabled = Hard.Enabled = false;
                flag_back = 2; flag_category = 1;
                flag_next = true;
            }
         
        }

        private void Categrory2_Click(object sender, EventArgs e)
        {
            if (flag_level == 1 || flag_level == 2 || flag_level == 3)
            {
                Easy.Enabled = Meduim.Enabled = Hard.Enabled = false;
                flag_back = 1; flag_category = 2;
                flag_next = true;
            }
        }

        private void Categrory3_Click(object sender, EventArgs e)
        {
            if (flag_level == 1 || flag_level == 2 || flag_level == 3)
            {
                Easy.Enabled = Meduim.Enabled = Hard.Enabled = false;
                flag_back = 1; flag_category = 3;
                flag_next = true;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (flag_level == 1 || flag_level == 2 || flag_level == 3)
            {
                Easy.Enabled = Meduim.Enabled = Hard.Enabled = false;
                flag_back = 1; flag_category = 4;
                flag_next = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (flag_level == 1 || flag_level == 2 || flag_level == 3)
            {
                Easy.Enabled = Meduim.Enabled = Hard.Enabled = false;
                flag_back = 1; flag_category = 5;
                flag_next = true;
            }
        }

        private void Next_Click(object sender, EventArgs e)
        {
            if (flag_next)
            { 
                server.Start();
                connection = server.AcceptSocket();
                   
                nstream = new NetworkStream(connection);
                bW = new BinaryWriter(nstream);
                bR = new BinaryReader(nstream);

                bW.Write(flag_category);
                bW.Write(flag_level);

                if (connection.Connected && bR.ReadBoolean())
                {
                    this.Hide();
                    MainBody body = new MainBody(flag_category, flag_level, this);
                    body.ShowDialog();
                    this.Show();
                }
                else { Next_Click(sender, e); }
            }
            else { MessageBox.Show("Please Choose Level and Category."); }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            if (flag_back == 0) { this.Close(); }
            else if (flag_back == 1)
            {
                flag_back--; flag_category = 0; flag_level = 0;
                Categrory1.Enabled = Categrory2.Enabled = Categrory3.Enabled = button1.Enabled = button2.Enabled = false;
                Easy.Enabled = Meduim.Enabled = Hard.Enabled = true;
                flag_next = false;
            }

        }
        public void close_server()
        {
            bR.Close();
            bW.Close();
            nstream.Close();
            connection.Shutdown(SocketShutdown.Both);
            connection.Close();
            this.Close();
        }

        
    }
}
