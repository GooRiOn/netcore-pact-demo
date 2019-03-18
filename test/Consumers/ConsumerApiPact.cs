using System;
using Newtonsoft.Json;
using PactNet;
using PactNet.Mocks.MockHttpService;

namespace NetCore.Pact.Demo.Consumers
{
    public class ConsumerApiPact : IDisposable
    {
        public IPactBuilder PactBuilder { get; private set; }
        public IMockProviderService MockProviderService { get; private set; }

        public int MockServerPort => 5000;
        public string MockProviderServiceBaseUri => String.Format("http://localhost:{0}", MockServerPort);

        public ConsumerApiPact()
        {
            PactBuilder = new PactBuilder(new PactConfig { PactDir = @"C:\Users\xxfqa\Desktop", LogDir = @"c:\temp\logs" }); //Configures the PactDir and/or LogDir.

            PactBuilder
                .ServiceConsumer("Consumer")
                .HasPactWith("Something API");

            MockProviderService = PactBuilder.MockService(MockServerPort);
        }

        public void Dispose()
        {
            PactBuilder.Build(); //NOTE: Will save the pact file once finished
        }
    }
}