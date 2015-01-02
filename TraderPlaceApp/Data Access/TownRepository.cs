using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Data_Access
{
    public class TownRepository: ConnectionClass
    {

        public TownRepository() : base() { }

        public IEnumerable<Town> GetallTowwns()
        {

            return entities.Towns.AsEnumerable();

        }
        
    }
}
