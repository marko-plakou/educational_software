using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;

namespace educational_soft_
{   
    class User
    {
        private string user_email;
       private readonly DbConnector db = new DbConnector();

        public User(string user_email) { this.user_email = user_email; }
        public string get_User_email() { return user_email; }





        public void Submit_user_answers(Quiz quiz,int question_id,Boolean is_correct)//Submits user's latest answers.
        {
            
            NpgsqlConnection con = new NpgsqlConnection(db.get_connection_string());
            con.Open();
            NpgsqlCommand command= new NpgsqlCommand();
            command.Connection = con;
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from user_answers where quiz_id=" + quiz.get_Quiz_id() + " and question_id=" + question_id + "and email='"+get_User_email()+"';";
            NpgsqlDataReader dr = command.ExecuteReader();
            dr.Read();
            if (!dr.HasRows)
            {
                Insert_submition(quiz, question_id, is_correct);

               
            }
            else {
                Update_submission(quiz, question_id, is_correct);
            }
            command.Dispose();
            con.Close();







        }



        private void Insert_submition(Quiz quiz, int question_id, Boolean is_correct) {//Inserts the data in the case of a new quiz.
            
            NpgsqlConnection con = new NpgsqlConnection(db.get_connection_string());
            con.Open();
           
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = con;
            command.CommandType = CommandType.Text;
            command.CommandText ="insert into user_answers(email,quiz_id,question_id,is_correct) values('" + get_User_email() + "'," + quiz.get_Quiz_id() + "," +question_id + ","+is_correct+");";
            NpgsqlDataReader dr2 = command.ExecuteReader();
            command.Dispose();
            con.Close();


        }

        private void Update_submission(Quiz quiz, int question_id, Boolean is_correct) {//Updates the answers in the case of a repeated quiz submition.
            
            NpgsqlConnection con = new NpgsqlConnection(db.get_connection_string());
            con.Open();

            NpgsqlCommand command2 = new NpgsqlCommand();

            command2.Connection = con;

            command2.CommandType = CommandType.Text;
            command2.CommandText = "update user_answers set is_correct=" + is_correct + " where email='" + get_User_email() + "' and quiz_id=" + quiz.get_Quiz_id() +  " and question_id=" + question_id + ";";
           
            NpgsqlDataReader dr2 = command2.ExecuteReader();
            command2.Dispose();
            con.Close();

        }

        public void Submit_revision_answers(int quiz_id, int question_id, Boolean is_correct)//Submits the revision's answers.
        {

            NpgsqlConnection con = new NpgsqlConnection(db.get_connection_string());
            con.Open();
            NpgsqlCommand command2 = new NpgsqlCommand();
            command2.Connection = con;
            command2.CommandType = CommandType.Text;
            command2.CommandText = "update user_answers set is_correct=" + is_correct + " where email='" + get_User_email() + "' and quiz_id=" + quiz_id + " and question_id=" + question_id + ";";
            NpgsqlDataReader dr2 = command2.ExecuteReader();
            command2.Dispose();
            con.Close();

        }

        public void Completed_quiz(int quiz_id) {//Updates the situtation of the quiz.
            NpgsqlConnection con = new NpgsqlConnection(db.get_connection_string());
            con.Open();

            NpgsqlCommand command3 = new NpgsqlCommand();

            command3.Connection = con;

            command3.CommandType = CommandType.Text;
            command3.CommandText = "update quiz_completition set is_complete=true where email='"+get_User_email()+"' and quiz_id="+quiz_id+";" ;
           
            NpgsqlDataReader dr2 = command3.ExecuteReader();
            command3.Dispose();
            con.Close();
        }


        public void Submit_success_rate(int quiz_id,double success_rate) {//Submits the success rate on the quiz.

            NpgsqlConnection con = new NpgsqlConnection(db.get_connection_string());
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = con;
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from user_quiz_stats where quiz_id=" + quiz_id +  "and email='" + get_User_email() + "';";
            NpgsqlDataReader dr = command.ExecuteReader();
            dr.Read();
            if (!dr.HasRows)
            {
                Insert_success_rate(quiz_id, success_rate);


            }
            else
            {
                Update_quiz_success_rate(quiz_id, success_rate);
            }
            command.Dispose();
            con.Close();
        }


        private void Insert_success_rate(int quiz_id,double success_rate) {//Inserts the success rate in the case of a new quiz or revision.

            NpgsqlConnection con = new NpgsqlConnection(db.get_connection_string());
            con.Open();

            NpgsqlCommand command2 = new NpgsqlCommand();
            command2.Connection = con;
            command2.CommandType = CommandType.Text;
            command2.CommandText = "insert into user_quiz_stats(email,quiz_id,success_rate) values('" + get_User_email() + "'," + quiz_id + "," + success_rate +  ");";
            NpgsqlDataReader dr2 = command2.ExecuteReader();
            command2.Dispose();
            con.Close();
        }
        private void Update_quiz_success_rate(int quiz_id,double success_rate)//Updates the success rate in the case of a new quiz or revision.
        {
            NpgsqlConnection con = new NpgsqlConnection(db.get_connection_string());
            con.Open();
            NpgsqlCommand command2 = new NpgsqlCommand();
            command2.Connection = con;
            command2.CommandType = CommandType.Text;
            command2.CommandText = "update user_quiz_stats set success_rate=" + success_rate + " where email='" + get_User_email() + "' and quiz_id=" + quiz_id  + ";";
            NpgsqlDataReader dr2 = command2.ExecuteReader();
            command2.Dispose();
            con.Close();
        }

       

        public void get_user_stats() {
            
            NpgsqlConnection con = new NpgsqlConnection(db.get_connection_string());
            con.Open();
            NpgsqlCommand command2 = new NpgsqlCommand();
            command2.Connection = con;
            command2.CommandType = CommandType.Text;
            command2.CommandText = "select quiz_id,success_rate from user_quiz_stats where email='" + get_User_email() + "' order by quiz_id desc;";
            NpgsqlDataReader dr2 = command2.ExecuteReader();
            command2.Dispose();
            con.Close();


        }








    }

   
    }

