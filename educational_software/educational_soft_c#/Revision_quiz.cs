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
     partial class Revision_quiz : Form
    {
        List<Revision> qna_sheet = new List<Revision>();//The list containing all the data for each question.
        int[] user_answersheet = new int[15];//Users answersheet
        Revision revision;
        User user;
        int correct_answers = 0;
        double score = 0;
        
        public  Revision_quiz(User user)
        {
            this.user = user;
            InitializeComponent();
            this.AutoScroll = true;
            revision = new Revision(user);
            qna_sheet = revision.get_revision_questionsheet();


            create_quiz(0, label1, label2, label3, label4, radioButton1, radioButton2, radioButton3, label16);
            create_quiz(1, label15, label14, label13, label12, radioButton6, radioButton5, radioButton4, label17);
            create_quiz(2, label32, label31, label30, label29, radioButton12, radioButton11, radioButton10, label18);
            create_quiz(3, label40, label39, label38, label37, radioButton15, radioButton14, radioButton13, label19);
            create_quiz(4, label48, label47, label46, label45, radioButton18, radioButton17, radioButton16, label20);
            create_quiz(5, label56, label55, label54, label53, radioButton21, radioButton20, radioButton19, label21);
       

        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void revision_quiz_Load(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }


        public void create_quiz(int qid, Label the_question, Label answer1, Label answer2, Label answer3, RadioButton choice1, RadioButton choice2, RadioButton choice3, Label quiz_name)
        {

            revision = qna_sheet[qid];
            the_question.Text = revision.question;
            answer1.Text = revision.answer1;
            answer2.Text = revision.answer2;
            answer3.Text = revision.answer3;
            Quiz quiz = new Quiz(revision.get_revision_quiz_id(),user);
            quiz_name.Text = quiz.get_Name().ToUpper();

            choice1.Name = "1";
            choice2.Name = "2";
            choice3.Name = "3";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                get_users_answersheet();
                compare_answersheets();
                display_quiz_stats();
                user.Submit_success_rate(10, score);
            this.Cursor = Cursors.No;
            button1.Enabled = false;
               







        }


        public void get_users_answersheet()//It gets the users answers.
        {
            int i = 0;

            var checkedRadio = new[] { groupBox1, groupBox2, groupBox4, groupBox5, groupBox6, groupBox7, groupBox8, groupBox9, groupBox10, groupBox11, groupBox12, groupBox13, groupBox14, groupBox15, groupBox16 }
                    .SelectMany(g => g.Controls.OfType<RadioButton>()
                                             .Where(r => r.Checked));
            
            foreach (var c in checkedRadio)
            {
                user_answersheet[i] = Int16.Parse(c.Name);
                i++;
               


            }
        }

        public void compare_answersheets()
        {
            int j = 0;
            while (j < 15)
            {
                revision = qna_sheet[j];
                if (user_answersheet[j].Equals(revision.correct_answer))
                {
                    correct_answers++;
                    user.Submit_revision_answers(revision.get_revision_quiz_id(), revision.question_id, true);
                    switch (j)
                    {
                        case 0: display_if_correct(label23, true); break;
                        case 1: display_if_correct(label8, true); break;
                        case 2: display_if_correct(label25, true); break;
                        case 3: display_if_correct(label33, true); break;
                        case 4: display_if_correct(label41, true); break;
                        case 5: display_if_correct(label49, true); break;
                     

                    }
                }
                else { user.Submit_revision_answers(revision.get_revision_quiz_id(), revision.question_id, false);

                    switch (j)
                    {
                        case 0: display_if_correct(label23, false); break;
                        case 1: display_if_correct(label8, false); break;
                        case 2: display_if_correct(label25, false); break;
                        case 3: display_if_correct(label33, false); break;
                        case 4: display_if_correct(label41, false); break;
                        case 5: display_if_correct(label49, false); break;
                       


                    }
                }
                j++;

            }

        }

        public void display_quiz_stats()//It displays the user's success rate.
        {
            score = (100 / 15) * correct_answers;
            if (correct_answers.Equals(15)) { score = 100.0000; }
            MessageBox.Show("In this Revision quiz you correctly answered:" + correct_answers.ToString() + "/" + 15 + " of the questions and  your score is: " + score.ToString() + "%", "RESULTS");


        }

        public void display_if_correct(Label label, Boolean is_correct)
        {
            if (is_correct) { label.Text = "CORRECT"; label.ForeColor = Color.Green; }
            label.Visible = true;


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Quiz_list quiz_list = new Quiz_list();
            quiz_list.Show();
            this.Close();
        }
    }
}
