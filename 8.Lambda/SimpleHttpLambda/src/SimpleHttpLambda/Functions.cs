using Amazon.Lambda.Annotations;
using Amazon.Lambda.Annotations.APIGateway;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using System.Net;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace SimpleHttpLambda
{
    public class Functions
    {
        /// <summary>
        /// Default constructor that Lambda will invoke.
        /// </summary>
        public Functions()
        {
        }

        [LambdaFunction(Policies = "AWSLambdaBasicExecutionRole", MemorySize = 256, Timeout = 30)]
        [RestApi(LambdaHttpMethod.Get, "/")]
        public IHttpResult Get(ILambdaContext context)
        {
            context.Logger.LogInformation("Handling the 'Get' Request");

            return HttpResults.Ok("Hello AWS Serverless");
        }
    }
}