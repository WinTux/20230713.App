using _20230713.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Diagnostics;

namespace _20230713.ConexionDatos
{
    class RestConexionDatos : IRestConexionDatos
    {
        public readonly HttpClient httpClient;
        public readonly string dominio;
        public readonly string url;
        public readonly JsonSerializerOptions opcionesJson;
        public RestConexionDatos()
        {
            httpClient = new HttpClient();
            dominio = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5145" : "http://localhost:5145";
            url = $"{dominio}/api";
            opcionesJson = new JsonSerializerOptions { 
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task AddPlatoAsync(Plato plato)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("[Conexión] Sin acceso a internet");
                return;
            }
            try
            {
                string platoSer = JsonSerializer.Serialize<Plato>(plato, opcionesJson);
                StringContent contenido = new StringContent(platoSer, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync($"{url}/plato", contenido);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("[Conexión]Se registró correctamente");
                }
                else
                {
                    Debug.WriteLine("[Conexión] Sin respuesta exitosa HTTP (2XX)");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Conexión] {ex.Message}");
            }
        }

        public async Task DeletePlatoAsync(int id)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("[Conexión] Sin acceso a internet");
                return;
            }
            try
            {
                HttpResponseMessage response = await httpClient.DeleteAsync($"{url}/plato/{id}");
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("[Conexión]Se eliminó correctamente");
                }
                else
                {
                    Debug.WriteLine("[Conexión] Sin respuesta exitosa HTTP (2XX)");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Conexión] {ex.Message}");
            }
        }

        public async Task<List<Plato>> GetPaltosAsync()
        {
            List<Plato> platos = new List<Plato>();
            if(Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("[Conexión] Sin acceso a internet");
                return platos;
            }
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync($"{url}/plato");
                if (response.IsSuccessStatusCode)
                {
                    var contenido = await response.Content.ReadAsStringAsync();
                    platos = JsonSerializer.Deserialize<List<Plato>>(contenido, opcionesJson);
                }
                else
                {
                    Debug.WriteLine("[Conexión] Sin respuesta exitosa HTTP (2XX)");
                }
            }catch (Exception e)
            {
                Debug.WriteLine($"[Conexión] {e.Message}");
            }
            return platos;
        }

        public async Task UpdatePlatoAsync(Plato plato)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("[Conexión] Sin acceso a internet");
                return;
            }
            try
            {
                string platoSer = JsonSerializer.Serialize<Plato>(plato, opcionesJson);
                StringContent contenido = new StringContent(platoSer, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PutAsync($"{url}/plato/{plato.id}", contenido);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("[Conexión]Se modificó correctamente");
                }
                else
                {
                    Debug.WriteLine("[Conexión] Sin respuesta exitosa HTTP (2XX)");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Conexión] {ex.Message}");
            }
        }
    }
}
