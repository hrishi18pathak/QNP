using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistanceCalCulator
{
    class IGEPlugin
    {
        public object ALTITUDE_RELATIVE_TO_GROUND { get; set; }

        internal object getView()
        {
            throw new NotImplementedException();
        }

        internal GEPlugin.KmlPlacemarkCoClass createPlacemark(string p)
        {
            throw new NotImplementedException();
        }

        internal GEPlugin.KmlPointCoClass createPoint(string p)
        {
            throw new NotImplementedException();
        }

        internal object getFeatures()
        {
            throw new NotImplementedException();
        }
    }
}
