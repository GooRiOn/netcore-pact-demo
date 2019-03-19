using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CarsApiConsumer.Messages;
using Newtonsoft.Json;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using Xunit;

namespace CarsApiConsumerTests
{
    public class ConsumerPactTests : IClassFixture<ConsumerPactClassFixture>
    {
        private readonly IMockProviderService _mockProviderService;
        private string _mockProviderServiceBaseUri;

        public ConsumerPactTests(ConsumerPactClassFixture fixture)
        {
            _mockProviderService = fixture.MockProviderService;
            _mockProviderService.ClearInteractions();
            _mockProviderServiceBaseUri = fixture.MockProviderServiceBaseUri;
        }
        
        [Fact]
        public async Task Consumer_Gets_Three_Cars_From_Cars_Api()
        {
            _mockProviderService
                .Given("There are 3 cars")
                .UponReceiving("A GET request to retrieve car dtos")
                .With(new ProviderServiceRequest 
                {
                    Method = HttpVerb.Get,
                    Path = "/api/cars"
                })
                .WillRespondWith(new ProviderServiceResponse {
                    Status = 200,
                    Headers = new Dictionary<string, object>
                    {
                        { "Content-Type", "application/json; charset=utf-8" }
                    },
                    Body = new List<CarDto>
                    {
                        new CarDto
                        {
                            Id = 1,
                            Brand = "Volkswagen",
                            Model = "Golf",
                            Color = "Black"
                        },
                        new CarDto
                        {
                            Id = 2,
                            Brand = "Mercedes-Benz",
                            Model = "A200",
                            Color = "Grey"
                        },
                        new CarDto
                        {
                            Id = 3,
                            Brand = "Audi",
                            Model = "Q5",
                            Color = "Black"
                        }
                    }
                });

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{_mockProviderServiceBaseUri}/api/cars");
            var json = await response.Content.ReadAsStringAsync();
            var cars = JsonConvert.DeserializeObject<List<CarDto>>(json);
            
            Assert.Equal(cars.Count, 3);
        }
    }
}