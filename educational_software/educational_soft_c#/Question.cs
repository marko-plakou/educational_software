using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;
namespace educational_soft_
{
    class Question
    {
        public int question_id { get; set; }
        public int correct_answer { get; set; }

        private readonly DbConnector db = new DbConnector();

        public Question(int question_id)
        {
            this.question_id = question_id;
        }
        public Question() { }

        public string question { get; set; }
        public string answer1  {get; set;}
        public string answer2 { get; set; }
        public string answer3 { get; set; }


        
       

    

        public void Get_question_data(int quiz_id)//It gets the quiz questions.
        {
           
            Question question = new Question(question_id);
            NpgsqlConnection con = new NpgsqlConnection(db.get_connection_string());
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = con;
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from quiz_qna where quiz_id=" + quiz_id + " and question_id="+question_id+";";
            NpgsqlDataReader dr = command.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                this.question = dr.GetString(2);
                this.answer1 = dr.GetString(3);
                this.answer2 = dr.GetString(4);
                this.answer3 = dr.GetString(5);
                }
            command.Dispose();
            con.Close();
            



        }

    }




    
}
