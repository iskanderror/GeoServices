using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoServices.Classes;
using RestSharp;

namespace GeoServices.Model
{
  class OsmDataProvider: NotifiedEntity, IGeoDataProvider
  {
    public async Task<string> GetGeoData(string query, int rareFactor)
    {
        var client = new RestClient("https://nominatim.openstreetmap.org");
        client.Timeout = -1;
        var request = new RestRequest("search")
          .AddParameter("q", query)
          .AddParameter("format", "json")
          .AddParameter("polygon_geojson", "1");
        var response = await client.ExecuteGetAsync(request);
        return response.Content;
    }
  }
}
