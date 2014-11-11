using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common; 

namespace Data_Access
{
    public class ConnectionClass
    {

        public SE_DatabaseEntities entities { get; set; }

        public ConnectionClass()
        {

            entities = new SE_DatabaseEntities();

        }

    }
}
