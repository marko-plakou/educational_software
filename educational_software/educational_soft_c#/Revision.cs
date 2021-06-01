using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;
namespace educational_soft_
{
    class Revision:Question
    {
        private Revision rq;
        private int revision_quiz_id;
   
        private List<Revision> revision_questionsheet = new List<Revision>();
        
        private readonly DbConnector db = new DbConnector();
        User user;
        public Revision(User user)
        {
            
            this.user = user;
            Statistics stats = new Statistics(user);//It recalculates the success rate in each quiz,considering and the latest revision answers,in order to be up to date.
            stats.Recalculate_success_rate();

        }

        public int get_revision_quiz_id() { return this.revision_quiz_id; }
       

        public List<Revision> get_revision_questionsheet() { Get_worst_quiz_questions(); Get_random_questions(); return this.revision_questionsheet; }


        private void Get_worst_quiz_questions()//It fetches the three quiz with lowest success rate.
        {

            NpgsqlConnection con = new NpgsqlConnection(db.get_connection_string());
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = con;
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from quiz_qna inner join (select quiz_id from user_quiz_stats where email = '" + user.get_User_email() + "' and quiz_id!=10 order by  success_rate,quiz_id asc limit 3) t2 on quiz_qna.quiz_id = t2.quiz_id; ";//find the 3 quiz with the lowest success rate
            NpgsqlDataReader dr = command.ExecuteReader();

            while (dr.Read())
            {

                rq = new Revision(user);
                rq.revision_quiz_id = dr.GetInt16(0);
                rq.question_id = dr.GetInt16(1);
                rq.question = dr.GetString(2);
                rq.answer1 = dr.GetString(3);
                rq.answer2 = dr.GetString(4);
                rq.answer3 = dr.GetString(5);
                rq.correct_answer = Get_question_answer(rq.get_revision_quiz_id(), rq.question_id);

                revision_questionsheet.Add(rq);
            }
            command.Dispose();
            con.Close();



        }


        private void Get_random_questions()//It gets one random question from every other unit in order for the revision to consist with questions from all units.
        {
            Random rnd = new Random();
            int random_question_id = rnd.Next(1, 4);
            NpgsqlConnection con = new NpgsqlConnection(db.get_connection_string());
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = con;
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from quiz_qna where question_id=" + random_question_id + ";";//find one random question from each quiz
            NpgsqlDataReader dr = command.ExecuteReader();

            while (dr.Read())
            {
                if (revision_questionsheet.Exists(x => x.revision_quiz_id == dr.GetInt16(0))) { continue; }
                else
                {
                    Revision rq = new Revision(user);
                    rq.revision_quiz_id = dr.GetInt16(0);
                    rq.question_id = dr.GetInt16(1);
                    rq.question = dr.GetString(2);
                    rq.answer1 = dr.GetString(3);
                    rq.answer2 = dr.GetString(4);
                    rq.answer3 = dr.GetString(5);
                    rq.correct_answer = Get_question_answer(rq.get_revision_quiz_id(), rq.question_id);
                    revision_questionsheet.Add(rq);
                }
            }
            command.Dispose();
            con.Close();

        }

        private int Get_question_answer(int quiz_id, int question_id)
        {//It gets the correct answer to the question.

            int correct_answer;
            NpgsqlConnection con = new NpgsqlConnection(db.get_connection_string());
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = con;
            command.CommandType = CommandType.Text;
            command.CommandText = "select correct_answer from quiz_answers where quiz_id=" + quiz_id + " and question_id=" + question_id + ";";
            NpgsqlDataReader dr = command.ExecuteReader();
            dr.Read();
             if (dr.HasRows)
            {

                correct_answer = dr.GetInt16(0);
            }
            else
            {
                correct_answer= 0;
            }

            command.Dispose();
            con.Close();
            return correct_answer;

        }

    }
}

