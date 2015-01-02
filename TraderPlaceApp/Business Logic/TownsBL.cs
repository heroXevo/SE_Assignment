using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Data_Access;

namespace Business_Logic
{
    public class TownsBL
    {

        public IEnumerable<Town> GetAllTowns()
        {

            return new TownRepository().GetallTowwns();

        }

    }
}
