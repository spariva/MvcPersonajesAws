using MvcPersonajesAws.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace MvcPersonajesAws.Services
{
    public class ServiceApiPersonajes
    {
        private MediaTypeWithQualityHeaderValue header;
        private string urlApi;

        public ServiceApiPersonajes(IConfiguration configuration)
        {
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
            this.urlApi = configuration.GetValue<string>("ApiUrls:ApiPersonajes");
        }


        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            using(HttpClientHandler handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback =
                (message, cert, chain, sslPolicies) =>
                {
                    return true;
                };

                using(HttpClient client = new HttpClient())
                {
                    string request = "api/personajes";
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(header);
                    HttpResponseMessage response = await client.GetAsync(urlApi + request);
                    if(response.IsSuccessStatusCode)
                    {
                        List<Personaje> personajes = await response.Content.ReadAsAsync<List<Personaje>>();
                        return personajes;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public async Task CreatePersonaje(string nombre, string imagen)
        {
            using(HttpClientHandler handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback =
                (message, cert, chain, sslPolicies) =>
                {
                    return true;
                };

                using(HttpClient client = new HttpClient())
                {
                    string request = "api/personajes";
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(header);
                    Personaje personaje = new Personaje()
                    {
                        Nombre = nombre,
                        Imagen = imagen
                    };
                    string jsonPersonaje = JsonConvert.SerializeObject(personaje);
                    StringContent content = new StringContent(jsonPersonaje, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsJsonAsync(urlApi + request, content);
                }
            }
        }
    }
}
