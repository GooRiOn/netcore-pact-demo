using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using PactNet;
using PactNet.Infrastructure.Outputters;
using Provider;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace NetCore.Pact.Demo.Provider
{
    public class DataApiTests
    {
        [Fact]
        public void EnsureDataApiHonoursPactWithConsumer()
        {
            //Arrange
            const string serviceUri = "http://localhost:5000";
            var config = new PactVerifierConfig
            {
                Outputters = new List<IOutput>
                {
                  new XUnitOutput(new TestOutputHelper())  
                },
                Verbose = true //Output verbose verification logs to the test output
            };

            IPactVerifier pactVerifier = new PactVerifier(config);
            
            pactVerifier
                .ProviderState()
                .ServiceProvider("Something API", serviceUri)
                .HonoursPactWith("Consumer")
                .PactUri(@"C:\Users\xxfqa\Desktop\consumer-something_api.json")
                .Verify();
        }
        
        public class XUnitOutput : IOutput
        {
            private readonly ITestOutputHelper _output;

            public XUnitOutput(ITestOutputHelper output)
            {
                _output = output;
            }

            public void WriteLine(string line)
            {
                _output.WriteLine(line);
            }
        }
    }
}