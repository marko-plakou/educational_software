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
     partial class User_stats : Form
    {
        List<Statistics> user_quiz_statistics = new List<Statistics>();
        List<Lesson> user_lesson_statistics = new List<Lesson>();
        Statistics[] user_worst_stats = new Statistics[3];
        Statistics statistics;
        User user;
        int repeat_quiz_id1,repeat_quiz_id2,repeat_quiz_id3;
        public User_stats(User user)
        {
            this.AutoScroll = true;
            InitializeComponent();

            quiz_chart_ui();
            lesson_chart_ui();
            
            this.user = user;
            statistics = new Statistics(user);

            user_worst_stats = statistics.get_worst_stats();
            user_quiz_statistics = statistics.get_user_quiz_stats();
            user_lesson_statistics = statistics.get_user_lesson_stats();
            
            create_progress_bar();
            worst_stats_ui();


            create_unit_quiz_chart();
            create_unit_lesson_chart();

            create_quiz_stats_ui(user_quiz_statistics[0], label13);
            create_quiz_stats_ui(user_quiz_statistics[1], label14);
            create_quiz_stats_ui(user_quiz_statistics[2], label15);
            create_quiz_stats_ui(user_quiz_statistics[3], label16);
            create_quiz_stats_ui(user_quiz_statistics[4], label17);
            create_quiz_stats_ui(user_quiz_statistics[5], label18);
        
            create_lesson_stats_ui(user_lesson_statistics[0], label32);
            create_lesson_stats_ui(user_lesson_statistics[1], label33);
            create_lesson_stats_ui(user_lesson_statistics[2], label34);
            create_lesson_stats_ui(user_lesson_statistics[3], label35);
            create_lesson_stats_ui(user_lesson_statistics[4], label36);
            create_lesson_stats_ui(user_lesson_statistics[5], label37);
          















        }

        

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        public void create_quiz_stats_ui(Statistics stats ,Label rate) {
            
            if (!stats.get_isUnlocked()) { rate.Text = "Locked"; }
            else if (!stats.get_isComplete()) { rate.Text = "Uncomplete"; }

            else { rate.Text = stats.get_SuccessRate().ToString() + "%"; }
        
        

          
        }

        public void create_lesson_stats_ui(Lesson stats, Label num_of_visits)
        {

            if (!stats.lesson_isUnlocked) { num_of_visits.Text = "Locked"; }
            else if (!stats.lesson_IsCompleted) { num_of_visits.Text = "Uncomplete"; }

            else { num_of_visits.Text = stats.num_of_visits.ToString() + " Times"; }




        }

        public void create_unit_quiz_chart()
        {
            foreach (Statistics st in user_quiz_statistics)
            {
                if (st.get_Quiz_id().Equals(10)) { create_revision_chart(st); }
                else { this.chart1.Series["UNIT QUIZ"].Points.AddXY("UNIT:" + st.get_Quiz_id().ToString(), st.get_SuccessRate()); };
            }


        }


        public void create_unit_lesson_chart()
        {
            foreach (Lesson st in user_lesson_statistics)
            {
                if (!st.lesson_IsCompleted || !st.lesson_isUnlocked) { continue; }
                else
                {
                    if (st.lesson_id.Equals(0)) { this.chart2.Series["VISITS"].Points.AddXY("INTRODUCTION:", st.num_of_visits); }
                    else { this.chart2.Series["VISITS"].Points.AddXY("UNIT:" + st.lesson_id.ToString(), st.num_of_visits); };
                }
            }


        }




        public void create_revision_chart(Statistics stats) {
            this.chart1.Series["REVISION"].Points.AddXY("REVISION", stats.get_SuccessRate());
            chart1.AlignDataPointsByAxisLabel();
        }
        public void create_progress_bar() {
            label1.Text = statistics.get_course_completitionRate().ToString() + "%";
            progressBar1.Value = Convert.ToInt32(statistics.get_course_completitionRate());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
            Quiz_list quiz_list = new Quiz_list();
            quiz_list.Show();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {
            Unit_quiz quiz = new Unit_quiz(repeat_quiz_id1,user);
            quiz.Show();
            this.Close();
        }

        private void label30_Click(object sender, EventArgs e)
        {
            Unit_quiz quiz = new Unit_quiz(repeat_quiz_id2, user);
            quiz.Show();
            this.Close();
        }

        private void label31_Click(object sender, EventArgs e)
        {
            Unit_quiz quiz = new Unit_quiz(repeat_quiz_id3, user);
            quiz.Show();
            this.Close();
        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        public void worst_stats_ui()
        {


            if (user_worst_stats[0].get_SuccessRate().Equals(99)||!user_worst_stats[0].get_isUnlocked()||!user_worst_stats[0].get_isComplete()) { label29.Enabled = false; label29.Visible = false; }
            else { label29.Text = "UNIT:" + user_worst_stats[0].get_Quiz_id().ToString(); repeat_quiz_id1 = user_worst_stats[0].get_Quiz_id(); }
            if (user_worst_stats[1].get_SuccessRate().Equals(99) || !user_worst_stats[1].get_isUnlocked() || !user_worst_stats[1].get_isComplete()) { label30.Enabled = false; label30.Visible = false; }
             else { label30.Text = "UNIT:" + user_worst_stats[1].get_Quiz_id().ToString(); repeat_quiz_id2 = user_worst_stats[1].get_Quiz_id(); }
            if (user_worst_stats[2].get_SuccessRate().Equals(99) || !user_worst_stats[2].get_isUnlocked() || !user_worst_stats[2].get_isComplete()) { label31.Enabled = false; label31.Visible = false; }
             else { label31.Text = "UNIT:" + user_worst_stats[2].get_Quiz_id().ToString(); repeat_quiz_id3 = user_worst_stats[2].get_Quiz_id(); }




        }

        private void User_stats_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click_2(object sender, EventArgs e)
        {

        }

        public void quiz_chart_ui()
        {
            chart1.Titles.Add("QUIZ SUCCESS ");
            chart1.ChartAreas[0].AxisX.Title = "QUIZ ";
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.Maximum = 11;
            chart1.ChartAreas[0].AxisY.Title = "SUCCESS RATE %";
            chart1.ChartAreas[0].AxisY.Maximum = 0;
            chart1.ChartAreas[0].AxisY.Interval = 10;
            chart1.ChartAreas[0].AxisY.Maximum = 100;
        }

        public void lesson_chart_ui() {

            chart2.Titles.Add("UNIT VISIT ");
            chart2.ChartAreas[0].AxisX.Title = "UNIT ";
            chart2.ChartAreas[0].AxisX.Minimum = -1;
            chart2.ChartAreas[0].AxisX.Interval = 1;
            chart2.ChartAreas[0].AxisX.Maximum = 11;
            chart2.ChartAreas[0].AxisY.Title = "VISITS PER UNIT";
            chart2.ChartAreas[0].AxisY.Maximum = -1;
            chart2.ChartAreas[0].AxisY.Interval = 1;
            chart2.ChartAreas[0].AxisY.Maximum = 10;
        }
    }
}
