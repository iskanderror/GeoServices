using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServices.Model
{
  interface IGeoDataProvider
  {
    Task<string> GetGeoData(string query, int rareFactor);
  }
}
