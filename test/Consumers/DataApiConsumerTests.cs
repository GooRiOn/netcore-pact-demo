using System;
using System.Collections.Generic;
using NetCore.Pact.Demo.Shared;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using Xunit;

namespace NetCore.Pact.Demo.Consumers
{
    public class SomethingApiConsumerTests : IClassFixture<ConsumerApiPact>
    {
        private IMockProviderService _mockProviderService;
        private string _mockProviderServiceBaseUri;

        public SomethingApiConsumerTests(ConsumerApiPact data)
        {
            _mockProviderService = data.MockProviderService;
            _mockProviderService.ClearInteractions();
            _mockProviderServiceBaseUri = data.MockProviderServiceBaseUri;
        }

        [Fact]
        public void GetSomething_WhenTheTesterSomethingExists_ReturnsTheSomething()
        {
            var id = Guid.NewGuid();
            
            //Arrange
            _mockProviderService
                .Given($"There is a data with id '{id}'")
                .UponReceiving("A GET request to retrieve the something")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Get,
                    Path = $"/data/{id}",
                    Headers = new Dictionary<string, object>
                    {
                        { "Accept", "application/json" }
                    }
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = 200,
                    Headers = new Dictionary<string, object>
                    {
                        { "Content-Type", "application/json; charset=utf-8" }
                    },
                    Body = new Data
                    {
                        Id = id,
                        Value1 = "Totally",
                        Value2 = 2
                    }
                }); //NOTE: WillRespondWith call must come last as it will register the interaction

            var consumer = new DataApiClient(_mockProviderServiceBaseUri);

            //Act
            var result = consumer.GetData(id);

            //Assert
            Assert.Equal(id, result.Id);

            _mockProviderService.VerifyInteractions(); //NOTE: Verifies that interactions registered on the mock provider are called at least once
        }
    }
}