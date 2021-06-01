using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;

namespace educational_soft_
{
    class Statistics:Quiz
    {
        private Statistics stats;
       
        private double course_completition_rate;
      
        private readonly DbConnector db = new DbConnector();

        User user;
        Lesson lesson; 
        
       
        List<Statistics> user_quiz_stats = new List<Statistics>();//Holds all the quiz_stats.
        List<Lesson> user_lesson_stats = new List<Lesson>();//Holds all the lesson stats.
        Statistics[] three_worst_quiz=new Statistics[3];//Holds the quiz stats for the three quiz with the lowest success rate.
        

       
        public double get_course_completitionRate() { Calculate_completition_rate(); return course_completition_rate; }
        public List<Statistics> get_user_quiz_stats() { Get_users_quiz_statistics(); return user_quiz_stats; }
        public List<Lesson> get_user_lesson_stats() { Get_users_lesson_statistics(); return user_lesson_stats; }
        public Statistics[] get_worst_stats() { Get_users_worst_quiz(); return three_worst_quiz; }
        

        public Statistics(User user)
        {
            
            this.user = user;
            
        }

      

        private void Get_users_worst_quiz()//It gets the three quiz with the user's lowest success rate. 
        {

            
            NpgsqlConnection con = new NpgsqlConnection(db.get_connection_string());
            con.Open();
            NpgsqlCommand command2 = new NpgsqlCommand();
            command2.Connection = con;
            command2.CommandType = CommandType.Text;
            command2.CommandText = "select  user_quiz_stats.quiz_id,success_rate,lesson_completition.is_unlocked,lesson_completition.is_complete from user_quiz_stats inner join (lesson_completition inner join quiz_completition on lesson_id=quiz_id and lesson_completition.email=quiz_completition.email) on user_quiz_stats.quiz_id=quiz_completition.quiz_id and user_quiz_stats.email=quiz_completition.email where user_quiz_stats.email='"+user.get_User_email()+"' order by success_rate,user_quiz_stats.quiz_id asc limit 3;";
            Console.WriteLine(command2.ToString());
            NpgsqlDataReader dr2 = command2.ExecuteReader();
            int i= 0;
            while (dr2.Read())
            {
                stats = new Statistics(user);
                stats.set_Quiz_id(dr2.GetInt16(0));
                stats.set_Success_rate(dr2.GetDouble(1));
                stats.set_Isunlocked(dr2.GetBoolean(2));
                stats.set_Iscomplete(dr2.GetBoolean(3));
                three_worst_quiz[i] = stats;
                i++;

                
            }
          
            command2.Dispose();
            con.Close();

        }


        private void Get_users_quiz_statistics() {//It fetches the user's quiz statistics.
            user_quiz_stats.Clear();
            Recalculate_success_rate();
            NpgsqlConnection con = new NpgsqlConnection(db.get_connection_string());
            con.Open();
            NpgsqlCommand command2 = new NpgsqlCommand();
            command2.Connection = con;
            command2.CommandType = CommandType.Text;
            command2.CommandText = "select user_quiz_stats.quiz_id,is_unlocked,is_complete,success_rate from quiz_completition inner join user_quiz_stats on quiz_completition.quiz_id = user_quiz_stats.quiz_id and user_quiz_stats.email = quiz_completition.email where user_quiz_stats.email = '"+user.get_User_email()+"' order by quiz_id asc; ";
            NpgsqlDataReader dr2 = command2.ExecuteReader();

            while (dr2.Read())
            {
                stats = new Statistics(user);
                stats.set_Quiz_id(dr2.GetInt16(0));
                stats.set_Isunlocked(dr2.GetBoolean(1));
                stats.set_Iscomplete(dr2.GetBoolean(2));
                stats.set_Success_rate(Check_if_99(dr2.GetDouble(3)));
                user_quiz_stats.Add(stats);


            }



            command2.Dispose();
            con.Close();
        }


        private void Get_users_lesson_statistics()//It fetches the user's lessons statistics.
        {
            user_lesson_stats.Clear();
            NpgsqlConnection con = new NpgsqlConnection(db.get_connection_string());
            con.Open();
            NpgsqlCommand command2 = new NpgsqlCommand();
            command2.Connection = con;
            command2.CommandType = CommandType.Text;
            command2.CommandText = "select lesson_id,is_complete,is_unlocked,num_of_visits from lesson_completition where email='"+user.get_User_email()+"'; ";
            NpgsqlDataReader dr2 = command2.ExecuteReader();

            while (dr2.Read())
            {
                lesson = new Lesson();
                
                lesson.lesson_id = dr2.GetInt16(0);
                lesson.lesson_IsCompleted = dr2.GetBoolean(1);
                lesson.lesson_isUnlocked = dr2.GetBoolean(2);
                lesson.num_of_visits = dr2.GetInt32(3);
                user_lesson_stats.Add(lesson);


            }




        }



        public void Recalculate_success_rate()//It recalculates the success rate using a sql stored procedure.
        {

            NpgsqlConnection con = new NpgsqlConnection(db.get_connection_string());
            con.Open();
            NpgsqlCommand command2 = new NpgsqlCommand();
            command2.Connection = con;
            command2.CommandType = CommandType.Text;
            command2.CommandText = "call update_success_rate('" + user.get_User_email() + "');";
            NpgsqlDataReader dr2 = command2.ExecuteReader();
            command2.Dispose();
            con.Close();
        }


        private int Get_num_of_lessons_completed()//It gets the number of completed lessons.
        {
            int num_of_lesson_completed;
            NpgsqlConnection con = new NpgsqlConnection(db.get_connection_string());
            con.Open();
            NpgsqlCommand command2 = new NpgsqlCommand();
            command2.Connection = con;
            command2.CommandType = CommandType.Text;
            command2.CommandText = "select count(is_complete) from lesson_completition where email='" + user.get_User_email() + "' and is_complete=true;";
            NpgsqlDataReader dr2 = command2.ExecuteReader();
            dr2.Read();
            if (dr2.HasRows) { num_of_lesson_completed = dr2.GetInt16(0); }
            else { num_of_lesson_completed = 0; }
            command2.Dispose();
            con.Close();
            return num_of_lesson_completed; 
        }




        private int Get_num_of_quiz_completed()//It gets the number of completed quiz.
        {
            int num_of_completed_quiz;
            NpgsqlConnection con = new NpgsqlConnection(db.get_connection_string());
            con.Open();
            NpgsqlCommand command2 = new NpgsqlCommand();
            command2.Connection = con;
            command2.CommandType = CommandType.Text;
            command2.CommandText = "select count(is_complete) from quiz_completition where email='" + user.get_User_email() + "' and is_complete=true;";
            NpgsqlDataReader dr2 = command2.ExecuteReader();
            dr2.Read();
            if (dr2.HasRows) { num_of_completed_quiz= dr2.GetInt16(0); }
            else { num_of_completed_quiz = 0; }
            command2.Dispose();
            con.Close();
            return num_of_completed_quiz;
        }


        private void Calculate_completition_rate()//It calculates the overall course completition rate.
        {
            course_completition_rate = (Get_num_of_lessons_completed() + Get_num_of_quiz_completed()) * 5;//The calculation formula.
            
        }

        private double Check_if_99(Double success_rate) {//It checks if the success rate is 99 as it stores in db that way.
            if (success_rate.Equals(99)) { return 100.00; }
            else { return success_rate; }
        }

       



    }

  
}
