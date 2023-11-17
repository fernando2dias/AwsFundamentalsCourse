
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using SnsPublisher;
using System.Text.Json;

var customer = new CustomerCreated
{
    Id = Guid.NewGuid(),
    Email = "fernando@gmail.com",
    FullName = "Fernando Motta",
    DateOfBirth = new DateTime(1986, 9, 21),
    GitHubUsername = "fernando2dias"
};

var snsClient = new AmazonSimpleNotificationServiceClient();
var topicArnResponse = await snsClient.FindTopicAsync("customers");
var publishRequest = new PublishRequest
{
    TopicArn = topicArnResponse.TopicArn,
    Message = JsonSerializer.Serialize(customer),
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

var response =  await  snsClient.PublishAsync(publishRequest);