    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistanceCalCulator
{
    public class Airport
    {
        public string ID;
        public string  ident;
        public string type;
        public string name;
        public string latitude_deg;
        public string longitude_deg;
        public string elev_ft;
        public string municipality;
        public string frequency_khz;
        public string gps_code;
        public string iata_code;
        public string magnetic_variation_deg;
        public string associated_airport;

        public Airport()
        {
            

        }

        public string getDisplaytext()
        {
            string disptext = this.ID + "\t" + this.ident  + "\t" + this.type + "\t" + this.name + "\t" + this.latitude_deg + "\t" + this.longitude_deg + "\t" + this.elev_ft + "\t" + this.municipality + "\t" + this.frequency_khz + "\t" + this.gps_code + "\t" + this.iata_code + "\t" + this.magnetic_variation_deg + "\t" + this.associated_airport;
            return disptext;
        }

        public Airport(string identPar,string typePar,string namePar,string longPar,string latPar)
        {
            this.ident = identPar;
            this.type = typePar;
            this.name = namePar;
            this.longitude_deg = longPar;
            this.latitude_deg = latPar;
            

        }
    }
}
