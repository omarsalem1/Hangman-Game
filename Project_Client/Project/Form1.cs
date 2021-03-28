using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class Form1 : Form
    {
        int flag_level = 0;
        int flag_category = 0;
        int flag_back = 0;
        bool flag_next = false;
        public int Category_value { get { return flag_level; } set { flag_level = value; } }
        public int Levels_value { get { return flag_category; } set { flag_category = value; } }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           Categrory1.Enabled = Categrory2.Enabled = Categrory3.Enabled  = false;
            
        }

        private void Easy_Click(object sender, EventArgs e)
        {
            flag_back = 1; flag_level = 1; 
     
            Categrory1.Enabled = Categrory2.Enabled = Categrory3.Enabled = true;
            Easy.Enabled = Meduim.Enabled = Hard.Enabled = false;
        }

        private void Meduim_Click(object sender, EventArgs e)
        {
            flag_back = 1; flag_level = 2; 
            
            Categrory1.Enabled = Categrory2.Enabled = Categrory3.Enabled = true;
            Easy.Enabled = Meduim.Enabled = Hard.Enabled = false;
        }

        private void Hard_Click(object sender, EventArgs e)
        {
            flag_back = 1; flag_level = 3; 
            
            Categrory1.Enabled = Categrory2.Enabled = Categrory3.Enabled = true;
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

        private void Next_Click(object sender, EventArgs e)
        {
            if (flag_next)
            {
              //  MainBody body = new MainBody(flag_category, flag_level);
             //   DialogResult dialogResult = body.ShowDialog(); 
            }
            else { MessageBox.Show("Please Choose Level and Category."); }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            if (flag_back == 0) { MessageBox.Show("HI"); }
            else if (flag_back == 1)
            {
                flag_back--; flag_category = 0;flag_level = 0;
                Categrory1.Enabled = Categrory2.Enabled = Categrory3.Enabled = false;
                Easy.Enabled = Meduim.Enabled = Hard.Enabled = true;
                flag_next = false;
            }
            
        }
    }
}
