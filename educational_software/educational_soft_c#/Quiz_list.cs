using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace educational_soft_
{
     partial class Quiz_list : Form
    {
        User user = new User("markos9916@gmail.com");
        public Quiz_list()//User user It should get a user as a parameter)
        {
            InitializeComponent();
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            

            change_ui(pictureBox1, label1, label10, linkLabel1, Check_quiz(1).get_isUnlocked(), Check_quiz(1).get_isComplete());
            change_ui(pictureBox2, label2, label11, linkLabel2, Check_quiz(2).get_isUnlocked(), Check_quiz(2).get_isComplete());
            change_ui(pictureBox3, label3, label12, linkLabel3, Check_quiz(3).get_isUnlocked(), Check_quiz(3).get_isComplete());
            change_ui(pictureBox4, label4, label13, linkLabel4, Check_quiz(4).get_isUnlocked(), Check_quiz(4).get_isComplete());
            change_ui(pictureBox5, label5, label14, linkLabel5, Check_quiz(5).get_isUnlocked(), Check_quiz(5).get_isComplete());
            change_ui(pictureBox6, label6, label15, linkLabel6, Check_quiz(6).get_isUnlocked(), Check_quiz(6).get_isComplete());
            change_ui(pictureBox7, label7, label16, linkLabel7, Check_quiz(7).get_isUnlocked(), Check_quiz(7).get_isComplete());
            change_ui(pictureBox8, label8, label17, linkLabel8, Check_quiz(8).get_isUnlocked(), Check_quiz(8).get_isComplete());
            change_ui(pictureBox9, label9, label18, linkLabel9, Check_quiz(9).get_isUnlocked(), Check_quiz(9).get_isComplete());




        }

        public Quiz Check_quiz(int quiz_id)
        {
            
            Quiz quiz = new Quiz(quiz_id,user);
            quiz.Check_quiz_status();
            quiz.get_isComplete();
            quiz.get_isUnlocked();
            return quiz;
        }

       

        public void change_ui(PictureBox pictureBox, Label locker, Label completition, LinkLabel link, Boolean quiz_is_unlocked,Boolean quiz_is_complete) {
            if (quiz_is_unlocked)
            {
                pictureBox.Image = Properties.Resources.unlocked;
                locker.Text = "UNLOCKED";
                locker.ForeColor = Color.Green;
                link.Enabled = true;
                if (quiz_is_complete) { completition.Text = "COMPLETE";completition.ForeColor =Color.Green; }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Unit_quiz quiz1 = new Unit_quiz(1,user);
            quiz1.Show();
            this.Hide();
            
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            Unit_quiz quiz1 = new Unit_quiz(2,user);
            quiz1.Show();
            this.Hide();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Unit_quiz quiz1 = new Unit_quiz(3,user);
            quiz1.Show();
            this.Hide();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Unit_quiz quiz1 = new Unit_quiz(4,user);
            quiz1.Show();
            this.Hide();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Unit_quiz quiz1 = new Unit_quiz(5,user);
            quiz1.Show();
            this.Hide();
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Unit_quiz quiz1 = new Unit_quiz(6,user);
            quiz1.Show();
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Unit_quiz quiz1 = new Unit_quiz(7,user);
            quiz1.Show();
            this.Hide();
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Unit_quiz quiz1 = new Unit_quiz(8,user);
            quiz1.Show();
            this.Hide();
        }

        private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Unit_quiz quiz1 = new Unit_quiz(9,user);
            quiz1.Show();
            this.Hide();
        }

        private void label19_Click(object sender, EventArgs e)
        {
            Revision_quiz revision = new Revision_quiz(user);
            revision.Show();
            this.Hide();
        }

        private void label20_Click(object sender, EventArgs e)
        {
            User_stats user_stats = new User_stats(user);
            user_stats.Show();
            this.Hide();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}