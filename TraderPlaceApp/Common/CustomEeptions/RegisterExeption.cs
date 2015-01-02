using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Common.CustomEeptions
{
    class RegisterExeption : Exception
    {

        public RegisterExeption()
            : base("An error occured during your registration.")
        {
        }

        public RegisterExeption(string m)
            : base(m)
        {
        }

        public RegisterExeption(string m, Exception regException)
            : base(m, regException)
        {
        }

    }
}
