using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class MainBody : Form
    {

        string name, complete;
        char name_letter;
        int width_body, height_body;
        int X_line_start, Y_line_start, X_line_end, Y_line_end, left_border;
        int flag_correct = 0;
        int score_between_Client_server = 0, score_between_server_client = 0;
        public static int flag_turn = 0;
        Boolean rightorwrong_r;
        Boolean complete_r;
        Boolean rightorwrong_s;
        Boolean complete_s;
        startGame s1;
        Button[] btnArray;

        public MainBody(string word,startGame game)
        {
           
            InitializeComponent();

            width_body = this.Width;
            height_body = this.Height;
            X_line_start = (width_body / 2) / 2; Y_line_start = 380;
            X_line_end = X_line_start + 40; Y_line_end = Y_line_start;
            
            btnArray = new[] { A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z };
            s1 = game;
            name = word;

            //for rematch 
            complete_r = complete_s = rightorwrong_s = rightorwrong_r = false;
            flag_correct = 0;
            name_letter = '\0';
            flag_turn = 0;
            ResetForm();

            backgroundWorker1.RunWorkerAsync();

            if (flag_turn == 0)
            {
                Keyboard_buttons.Enabled = true;
            }


        }

        private void MainBody_Paint(object sender, PaintEventArgs e)
        {
            DrawLines(name);
        }

        private void DrawLines( string word) 
        {
            int word_length = word.Length;
            
            Pen blackPen = new Pen(Color.Black,5);
            Graphics graphicsObj = this.CreateGraphics();

            for (int i = 0; i < word_length; i++)
            {
                left_border = X_line_end + (60 * i);
                graphicsObj.DrawLine(blackPen, X_line_start + (60 * i), Y_line_start, left_border, Y_line_end);
            }
            if (left_border > width_body) { this.Width = left_border+60; }
        }
        
        private void checked_letter(string w,char letter)
        {
            rightorwrong_s = false;
            s1.bw.Write(letter);
            string word = name.ToLower();
            char[] ch = word.ToCharArray();
            for (int i = 0; i < word.Length; i++)
            {
                if (ch[i] == letter)
                {
                    flag_correct++;
                    rightorwrong_s = true;
                    Rectangle rect = new Rectangle(X_line_start + (60 * i), Y_line_start - 35, X_line_end + (60 * i), 30);
                    Graphics graphicsObj = this.CreateGraphics();
                    graphicsObj.DrawString(letter.ToString(), new Font("Helvetica", 25), new SolidBrush(Color.Black), rect);
                }
            }

            if (flag_correct == word.Length)
            {
                complete_s = true;
                score_between_Client_server++;
                complete = $"Server: lost\tClient: win\nServer_Score:{score_between_server_client}\tClient_Score:{score_between_Client_server}";
                MessageBox.Show("You Won");
            }

            if (!rightorwrong_s)
            {
                flag_turn = 1;
                Keyboard_buttons.Enabled = false;
            }

            if (complete_s)
            {
                backgroundWorker1.Dispose();
                DialogResult result2 = MessageBox.Show($"Do You want to play again?",
              "Repeat",
              MessageBoxButtons.YesNo,
              MessageBoxIcon.Question);

                if (result2 == DialogResult.Yes)
                {
                    s1.bw.Write(true);
                    if (s1.br.ReadBoolean())
                    {
                        playagain();
                        MessageBox.Show("client start checked");
                    }
                    else
                    {
                        MessageBox.Show("the other player refused");
                        
                        s1.close_connect();
                    }
                }
                else
                {
                    s1.close_connect();
                    this.Close();
                }

            }
        }

        public void check_letter(char letter)
        {
            rightorwrong_r = false;
            string word = name.ToLower();
            char[] ch = word.ToCharArray();
            btnArray[letter - 'a'].Enabled = false;
            for (int i = 0; i < word.Length; i++)
            {
                if (ch[i] == letter)
                {
                    rightorwrong_r = true;
                    flag_correct++;
                    Rectangle rect = new Rectangle(X_line_start + (60 * i), Y_line_start - 35, X_line_end + (60 * i), 30);
                    Graphics graphicsObj = this.CreateGraphics();
                    graphicsObj.DrawString(letter.ToString(), new Font("Helvetica", 25), new SolidBrush(Color.Black), rect);
                }
            }
            if (flag_correct == word.Length)
            {
                complete_r = true;
                score_between_server_client++;
                complete = $"Server: lost\tClient: win\nServer_Score:{score_between_server_client}\tClient_Score:{score_between_Client_server}";
                MessageBox.Show("You lost");
            }
            if (!rightorwrong_r)
            {
                flag_turn = 0;
                Keyboard_buttons.Enabled = true;
            }
            if (complete_r)
            {
                backgroundWorker1.Dispose();
                DialogResult result2 = MessageBox.Show($"Do You want to play again?",
              "Repeat",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

                if (result2 == DialogResult.Yes)
                {
                    s1.bw.Write(true);
                    if (s1.br.ReadBoolean())
                    {
                        playagain();
                        MessageBox.Show("client start");
                    }
                    else
                    {
                        MessageBox.Show("the other player refused");
                        WriteToFile(complete);
                        s1.close_connect();
                        this.Close();
                    }
                }
                else
                {
                    s1.close_connect();
                    this.Close();
                }

            }

        }

        private void playagain()
        {
            if (s1.client.Connected)
            {
                MessageBox.Show("conneccted 1");
                string input = s1.br.ReadString();
                //input = "MAhmoud";

                //while ((input += s1.br.ReadByte().ToString()) != null){ MessageBox.Show(input); }

                //MessageBox.Show("conneccted 2");

                MainBody m = new MainBody(input, s1);

                MessageBox.Show("object created");
                this.Hide();
                m.ShowDialog();
                this.Show();
                /*
                complete_r = complete_s = rightorwrong_s = false;
                flag_correct = 0;
                //DrawLines(name);*/
            }
        }
        private void ResetForm()
        {
            this.Refresh(); this.Invalidate();
        }
        
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                while (s1.client.Connected)
                {
                    Char input;
                    if (s1.br != null)
                    {
                        input = s1.br.ReadChar();
                        // MessageBox.Show(input.ToString());
                        if (input >= 'a' && input <= 'z')
                        {
                            //complete = s1.br.ReadBoolean();
                            // MessageBox.Show(input.ToString());
                            check_l(input);
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("connectoin terminated");
                WriteToFile("connectoin terminated\n" + complete);
                this.Close();
            }
        }

        private void check_l(char l)
        {
            Invoke(new MethodInvoker(
                            delegate { check_letter(l); }
                            ));

        }

        private void WriteToFile(string source)
        {
            DirectoryInfo dir = new DirectoryInfo(".");
            string Path_SaveFile = dir.FullName + "\\saveFile.txt";
            if (!File.Exists(Path_SaveFile)) // If file does not exists
            {
                File.Create(Path_SaveFile).Close(); // Create file
                TextWriter txt = File.CreateText(Path_SaveFile);
                txt.WriteLine(source);
                txt.WriteLine("Date: " + DateTime.Now);
                txt.Close();
            }
            else // If file already exists
            {
                // MessageBox.Show("exist");
                TextWriter txt = File.AppendText(Path_SaveFile);
                txt.WriteLine(source);
                txt.WriteLine("Date: " + DateTime.Now);
                txt.Close();
            }
        }

        private void A_Click(object sender, EventArgs e)
        {
            name_letter = 'a';A.Enabled = false;
            checked_letter(name.ToLower(), name_letter);
        }
        private void W_Click(object sender, EventArgs e)
        {
            name_letter = 'w'; W.Enabled = false;
            checked_letter(name.ToLower(), name_letter);
        }

        private void E_Click(object sender, EventArgs e)
        {
            name_letter = 'e'; E.Enabled = false;
            checked_letter(name.ToLower(), name_letter);
        }

        private void R_Click(object sender, EventArgs e)
        {
            name_letter = 'r'; R.Enabled = false;
            checked_letter(name.ToLower(), name_letter);
        }

        private void T_Click(object sender, EventArgs e)
        {
            name_letter = 't'; T.Enabled = false;
            checked_letter(name.ToLower(), name_letter);
        }

        private void Y_Click(object sender, EventArgs e)
        {
            name_letter = 'y'; Y.Enabled = false;
            checked_letter(name.ToLower(), name_letter);
        }

        private void U_Click(object sender, EventArgs e)
        {
            name_letter = 'u'; U.Enabled = false;
            checked_letter(name.ToLower(), name_letter);
        }

        private void I_Click(object sender, EventArgs e)
        {
            name_letter = 'i'; I.Enabled = false;
            checked_letter(name.ToLower(), name_letter);
        }

        private void O_Click(object sender, EventArgs e)
        {
            name_letter = 'o'; O.Enabled = false;
            checked_letter(name.ToLower(), name_letter);
        }

        private void P_Click(object sender, EventArgs e)
        {
            name_letter = 'p'; P.Enabled = false;
            checked_letter(name.ToLower(), name_letter);
        }

        private void S_Click(object sender, EventArgs e)
        {
            name_letter = 's'; S.Enabled = false;
            checked_letter(name.ToLower(), name_letter);
        }

        private void D_Click(object sender, EventArgs e)
        {
            name_letter = 'd'; D.Enabled = false;
            checked_letter(name.ToLower(), name_letter);
        }

        private void F_Click(object sender, EventArgs e)
        {
            name_letter = 'f'; F.Enabled = false;
            checked_letter(name.ToLower(), name_letter);
        }

        private void G_Click(object sender, EventArgs e)
        {
            name_letter = 'g'; G.Enabled = false;
            checked_letter(name.ToLower(), name_letter);
        }

        private void H_Click(object sender, EventArgs e)
        {
            name_letter = 'h'; H.Enabled = false;
            checked_letter(name.ToLower(), name_letter);
        }

        private void J_Click(object sender, EventArgs e)
        {
            name_letter = 'j'; J.Enabled = false;
            checked_letter(name.ToLower(), name_letter);
        }

        private void K_Click(object sender, EventArgs e)
        {
            name_letter = 'K'; K.Enabled = false;
            checked_letter(name.ToLower(), name_letter);
        }

        private void L_Click(object sender, EventArgs e)
        {
            name_letter = 'l'; L.Enabled = false;
            checked_letter(name.ToLower(), name_letter);
        }

        private void Z_Click(object sender, EventArgs e)
        {
            name_letter = 'z'; Z.Enabled = false;
            checked_letter(name.ToLower(), name_letter);
        }

        private void X_Click(object sender, EventArgs e)
        {
            name_letter = 'x'; X.Enabled = false;
            checked_letter(name.ToLower(), name_letter);
        }

        private void C_Click(object sender, EventArgs e)
        {
            name_letter = 'c'; C.Enabled = false;
            checked_letter(name.ToLower(), name_letter);
        }

        private void Q_Click(object sender, EventArgs e)
        {
            name_letter = 'q'; Q.Enabled = false;
            checked_letter(name.ToLower(), name_letter);
        }
       
        private void V_Click(object sender, EventArgs e)
        {
            name_letter = 'v'; V.Enabled = false;
            checked_letter(name.ToLower(), name_letter);
        }

        private void B_Click(object sender, EventArgs e)
        {
            name_letter = 'b'; B.Enabled = false;
            checked_letter(name.ToLower(), name_letter);
        }

        private void N_Click(object sender, EventArgs e)
        {
            name_letter = 'n'; N.Enabled = false;
            checked_letter(name.ToLower(), name_letter);
        }

        private void M_Click(object sender, EventArgs e)
        {
            name_letter = 'm'; M.Enabled = false;
            checked_letter(name.ToLower(), name_letter);
        }

    }
}
