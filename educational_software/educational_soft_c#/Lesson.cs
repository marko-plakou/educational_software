using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace educational_soft_
{
    class Lesson
    { 
        public  int lesson_id { get; set; } 
        public Boolean lesson_IsCompleted { get; set; }
        public Boolean lesson_isUnlocked { get; set; }
        public int num_of_visits { get; set; }
    }
}
