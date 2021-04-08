using System.Threading.Tasks;
using GeoServices.Classes;
using RestSharp;

namespace GeoServices.Model
{
  class OsmDataProvider: NotifiedEntity, IGeoDataProvider
  {
    public async Task<string> GetGeoData(string query, float simplificationFactor)
    {
        var client = new RestClient("https://nominatim.openstreetmap.org");
        client.Timeout = -1;
        var request = new RestRequest("search")
          .AddParameter("q", query)
          .AddParameter("format", "json")
          .AddParameter("polygon_geojson", "1");
        if (simplificationFactor > 0)
        {
          request.AddParameter("polygon_threshold", simplificationFactor.ToString().Replace(',','.'));
        }
        var response = await client.ExecuteGetAsync(request);
        return response.Content;
    }
  }
}
