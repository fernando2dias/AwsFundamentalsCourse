using Amazon.Runtime.Internal;
using Amazon.SQS;
using Amazon.SQS.Model;
using SqsPublisher;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

var sqsClient = new AmazonSQSClient();

var customer = new CustomerCreated
{
    Id = Guid.NewGuid(),
    Email = "ednaldo@gmail.com",
    FullName = "ednaldo",
    DateOfBirth = new DateTime(1986, 9, 21),
    GitHubUsername = "fernando2dias"
};

var queueUrlResponse = await sqsClient.GetQueueUrlAsync("customers");

var sendMessageRequest = new SendMessageRequest
{
    QueueUrl = queueUrlResponse.QueueUrl,
    MessageBody = JsonSerializer.Serialize(customer),
    MessageAttributes = new Dictionary<string, MessageAttributeValue>
    {
        {
            "MessageType", new MessageAttributeValue
            {
                DataType = "String",
                StringValue = nameof(CustomerCreated)
            }
        }
    }
};

var response = await sqsClient.SendMessageAsync(sendMessageRequest);

Console.WriteLine();