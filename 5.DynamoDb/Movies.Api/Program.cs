using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Movies.Api;
using System.Net;
using System.Text.Json;

await new DataSeeder().ImportDataAsync();

//var newMovie1 = new Movie1
//{
//    Id = Guid.NewGuid(),
//    Title = "Titanic",
//    AgeRestriction = 18,
//    ReleaseYear = 2001,
//    RottenTomatoesPercentage = 92,
//};

//var newMovie2 = new Movie2
//{
//    Id = Guid.NewGuid(),
//    Title = "Titanic",
//    AgeRestriction = 18,
//    ReleaseYear = 2001,
//    RottenTomatoesPercentage = 92,
//};

//var asJson1 = JsonSerializer.Serialize(newMovie1);
//var attibuteMap1 = Document.FromJson(asJson1).ToAttributeMap();

//var asJson2 = JsonSerializer.Serialize(newMovie2);
//var attibuteMap2 = Document.FromJson(asJson2).ToAttributeMap();

//var transactionRequest = new TransactWriteItemsRequest
//{
//    TransactItems = new List<TransactWriteItem>
//    {
//        new()
//        {
//            Put = new Put
//            {
//                TableName = "movies-year-title",
//                Item = attibuteMap1
//            }
//        },
//        new()
//        {
//            Put = new Put
//            {
//                TableName = "movies-title-rotten",
//                Item = attibuteMap2
//            }
//        }
//    }
//};

//var dynamoDb = new AmazonDynamoDBClient();

//var response = await dynamoDb.TransactWriteItemsAsync(transactionRequest);

//Console.ReadLine();