using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace educational_soft_
{

    class DbConnector
    {
        private readonly string connection_string = "Server=localhost;Port=5432;Database=edu_soft;User Id = postgres; Password=????;";

        public string get_connection_string() { return connection_string; }

    }



}
