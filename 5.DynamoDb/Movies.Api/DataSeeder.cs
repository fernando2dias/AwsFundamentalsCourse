using System.Text.Json;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

namespace Movies.Api;

public class DataSeeder
{
    public async Task ImportDataAsync()
    {
        var dynamoDb = new AmazonDynamoDBClient();
        var lines = await File.ReadAllLinesAsync("./movies.csv");
        for (int i = 0; i < lines.Length; i++)
        {
            if (i == 0)
            {
                continue; //Skip header
            }

            var line = lines[i];
            var commaSplit = line.Split(',');

            var title = commaSplit[0];
            var year = int.Parse(commaSplit[1]);
            var ageRestriction = int.Parse(commaSplit[2]);
            var rottenTomatoes = int.Parse(commaSplit[3]);

            var movie = new Movie1
            {
                Id = Guid.NewGuid(),
                Title = title,
                AgeRestriction = ageRestriction,
                ReleaseYear = year,
                RottenTomatoesPercentage = rottenTomatoes
            };
            
            var movieAsJson = JsonSerializer.Serialize(movie);
            var itemAsDocument = Document.FromJson(movieAsJson);
            var itemAsAttributes = itemAsDocument.ToAttributeMap();

            var createItemRequest = new PutItemRequest
            {
                TableName = "movies",
                Item = itemAsAttributes
            };

            var response = await dynamoDb.PutItemAsync(createItemRequest);
            await Task.Delay(300);
        }
    }
}
