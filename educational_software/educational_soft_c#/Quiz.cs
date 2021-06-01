using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace educational_soft_
{
    
     class Quiz { 
        private int quiz_id;
        private Boolean is_unlocked;
        private Boolean is_complete;
        private double success_rate;
        private string quiz_name;
        User user;
        private int[,] quiz_answersheet = new int[3,2];
        private readonly DbConnector db = new DbConnector();



        protected void set_Quiz_id(int quiz_id) { this.quiz_id = quiz_id; }
        protected void set_Isunlocked(Boolean is_unlocked) { this.is_unlocked = is_unlocked; }

        protected void set_Iscomplete(Boolean is_complete) { this.is_complete = is_complete; }

        protected void set_Success_rate(Double success_rate) { this.success_rate = success_rate; }

        public Boolean get_isUnlocked() { return is_unlocked; }
        public Boolean get_isComplete() { return is_complete; }
        public double get_SuccessRate() { return success_rate; }

        public string get_Name() { Get_quiz_name(); return quiz_name; }
        public int[,] get_Quiz_answers() { Get_correct_answersheet(); return quiz_answersheet; }
        public Quiz(int quiz_id,User user)
        {
            this.user = user;
            this.quiz_id = quiz_id;
        }
        public Quiz() { }

        public int get_Quiz_id() {
            return quiz_id;
        }

        
        
      

        public void Check_quiz_status()//It checks the status of the quiz.
        {
            
            NpgsqlConnection con = new NpgsqlConnection(db.get_connection_string());
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = con;
            command.CommandType = CommandType.Text;
            command.CommandText = "select  is_unlocked,is_complete from quiz_completition where email='"+user.get_User_email()+"'  and quiz_id="+quiz_id+";";
            NpgsqlDataReader dr = command.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                
                this.is_unlocked = dr.GetBoolean(0);
              
                this.is_complete = dr.GetBoolean(1);
                
                command.Dispose();
                con.Close(); 
            }

        }

        public void Get_correct_answersheet()//Fetches the answersheet with the correct answers.
        {
            
            NpgsqlConnection con = new NpgsqlConnection(db.get_connection_string());
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = con;
            command.CommandType = CommandType.Text;
            command.CommandText = "select question_id,correct_answer from quiz_answers where quiz_id=" + quiz_id + ";";
            NpgsqlDataReader dr = command.ExecuteReader();
            
            int i= 0;
            
            while (dr.Read())
            {

                quiz_answersheet[i, 0] = dr.GetInt16(0);
                quiz_answersheet[i, 1] = dr.GetInt16(1);

                i++;
               

            }
            command.Dispose();
            con.Close();


        }

        public void Get_quiz_name() {//It gets the quiz name.

            NpgsqlConnection con = new NpgsqlConnection(db.get_connection_string());
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = con;
            command.CommandType = CommandType.Text;
            command.CommandText = "select  quiz_name from quiz where quiz_id=" + quiz_id + ";";
            NpgsqlDataReader dr = command.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {

                this.quiz_name = dr.GetString(0);
                
                command.Dispose();
                con.Close();
            }
        }


        


    }
}
