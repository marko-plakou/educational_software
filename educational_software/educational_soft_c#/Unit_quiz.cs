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
     partial class Unit_quiz : Form
    {
        int[,] user_answersheet = new int[3,2];
        int[,] correct_answersheet = new int[3, 2];
        User user;
        Quiz quiz;
        int correct_answers;
        int num_of_questions=3;
        double score;
        int unit_id;

        public Unit_quiz(int unit_id,User user)
        {
            
            InitializeComponent();
            this.user = user;
            this.unit_id = unit_id;
            this.AutoScroll = true;

            Question question1 = new Question(unit_id);
            quiz = new Quiz(unit_id,user);

            create_quiz(1,label1,label2,label3,label4,radioButton1,radioButton2,radioButton3);
            create_quiz(2, label14, label13, label12,label11,radioButton4,radioButton5,radioButton6);
            create_quiz(3, label21, label20, label19, label18,radioButton7,radioButton8,radioButton9);
            




        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }


        public void create_quiz(int question_id,Label the_question,Label answer1,Label answer2,Label answer3,RadioButton choice1, RadioButton choice2, RadioButton choice3) {
            label22.Text = "QUIZ " + unit_id.ToString() + ": " + quiz.get_Name().ToUpper(); ;
            Question question = new Question(question_id);
            question.Get_question_data(unit_id);
            the_question.Text = question.question;
            answer1.Text = question.answer1;
            answer2.Text = question.answer2;
            answer3.Text = question.answer3;
            choice1.Name = "1";
            choice2.Name = "2";
            choice3.Name = "3";

        }

        private void button1_Click(object sender, EventArgs e)//Submit answerheet.
        {
            get_user_answersheet();

            get_correct_answersheet();
            compare_answersheets(user_answersheet, correct_answersheet);
            user.Completed_quiz(quiz.get_Quiz_id());
            display_quiz_stats();
            user.Submit_success_rate(unit_id, score);
            this.Cursor = Cursors.No;
            button1.Enabled = false;
           
            
         
        }


        public void get_user_answersheet()
        {
            int i = 0;
            int j = 1;
            var checkedRadio = new[] { groupBox1, groupBox2, groupBox3 }
                    .SelectMany(g => g.Controls.OfType<RadioButton>()
                                             .Where(r => r.Checked));
           
            foreach (var c in checkedRadio)
            {

                user_answersheet[i,0] = j;
                user_answersheet[i,1] = Int16.Parse( c.Name);
                j++;
                i++;
            }
        }

        public void get_correct_answersheet()
        {
             
            correct_answersheet=quiz.get_Quiz_answers();
            
                
            
            
           
        }

        public void compare_answersheets(int[,] users_answersheet, int[,] correct_answersheet) {
            
            int i = 0;
            while (i<3)
            {
               
                if (users_answersheet[i, 1].Equals(correct_answersheet[i, 1]))
                {
                    correct_answers++;
                    user.Submit_user_answers(quiz, i + 1, true);
                    switch (i)
                    { 
                     case 0:  change_ui(label23, true);  break;
                     case 1: change_ui(label24, true);  break;
                     case 2: change_ui(label25, true); break;
                     
                    }


                }
                else
                {
                   
                    user.Submit_user_answers(quiz, i + 1, false);
                    switch (i)
                    {
                        case 0: change_ui(label23, false); break;
                        case 1: change_ui(label24, false); break;
                        case 2: change_ui(label25, false); break;
                        
                    }
                }
                i++;

            }


        }

        public void change_ui(Label label, Boolean is_correct) {
            if (is_correct) { label.Text = "CORRECT";label.ForeColor = Color.Green; }
            label.Visible = true;
        
        
        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        public void display_quiz_stats() {//It displays the user's success rate.
             score = (100 / 3) * correct_answers;
            if (correct_answers.Equals(3)) { MessageBox.Show("You correctly answered:" + correct_answers.ToString() + "/" + num_of_questions.ToString() + " of the questions and  your score is: 100%", "RESULTS"); }

            else { MessageBox.Show("You correctly answered:" + correct_answers.ToString() + "/" + num_of_questions.ToString() + " of the questions and  your score is: " + score.ToString() + "%", "RESULTS"); }


        }

        private void pictureBox1_Click(object sender, EventArgs e)//Go back
        {
            Quiz_list quiz_list = new Quiz_list();
            quiz_list.Show();
            this.Close();
        }
    }
}
