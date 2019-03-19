using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CarsApiConsumer.Messages;
using Newtonsoft.Json;

namespace CarsApiConsumer
{
    class Program
    {
        private static readonly string CarsApiUrl = "http://localhost:5000/api/cars";
        private static readonly HttpClient HttpClient = new HttpClient();
        
        static async Task Main(string[] args)
        {
            Console.WriteLine("Making call to Cars API....");
            var response = await HttpClient.GetAsync(CarsApiUrl);
            
            Console.WriteLine("Deserializing response....");
            var json = await response.Content.ReadAsStringAsync();
            var cars = JsonConvert.DeserializeObject<List<CarDto>>(json);

            Console.WriteLine($"Received {cars.Count} cars:");
            cars.ForEach(c => Console.WriteLine($"{c.Id}. {c.Brand} {c.Model}, Color: {c.Color}"));
        }
    }
}
