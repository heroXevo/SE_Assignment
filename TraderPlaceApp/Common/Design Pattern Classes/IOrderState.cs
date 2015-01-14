using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Common.Design_Pattern_Classes
{
    interface IOrderState
    {

       
        
        void NewOrderPlaced();
        void Registered();
        void Dispatch();
        void Approved();

    }
}
