using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistanceCalCulator
{
    class Pilot
    {
        public string FName;
        public string LName;
        public double Weight;

        public Pilot(string FName,string LName,double Weight)
        {
            this.FName = FName;
            this.LName = LName;
            this.Weight = Weight;
        }

        public Pilot()
        {

        }
    }



}
