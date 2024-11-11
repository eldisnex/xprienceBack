using RestSharp;
public class Api
{
    string baseUrl = "https://api.foursquare.com/v3/places/";
    string authorization = "fsq3yeHpUcjL2qJBwbXjnEkJ629BT1E38hjFCxQN/M7ujx4=";
    RestRequest request;
    public Api()
    {
        request = new RestRequest("");
        request.AddHeader("accept", "application/json");
        request.AddHeader("Authorization", authorization);
    }

    public async Task<string> makeRequest(string latitude, string longitude, string query, int min, int max, string catCode)
    {
        RestClientOptions options = new RestClientOptions(baseUrl + $"search?ll={latitude},{longitude}&query={query}&categories={catCode}");
        RestClient client = new RestClient(options);
        var response = await client.GetAsync(request);
        Console.WriteLine(response.Content);
        return response.Content;
    }

    public async Task<string> getImage(string id)
    {
        RestClientOptions options = new RestClientOptions(baseUrl + $"{id}/photos");
        RestClient client = new RestClient(options);
        var response = await client.GetAsync(request);
        return response.Content;
    }
}