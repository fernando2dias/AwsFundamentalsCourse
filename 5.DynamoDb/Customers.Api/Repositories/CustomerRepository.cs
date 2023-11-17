using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Customers.Api.Contracts.Data;
using System.Net;
using System.Text.Json;

namespace Customers.Api.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly IAmazonDynamoDB _dynamoDb;
    private readonly string _tableName = "customers";

    public CustomerRepository(IAmazonDynamoDB dynamoDb)
    {
        _dynamoDb = dynamoDb;
    }

    public async Task<bool> CreateAsync(CustomerDto customer)
    {
        customer.UpdatedAt = DateTime.UtcNow;
        var customerAsJson = JsonSerializer.Serialize(customer);
        var customerAsAttributes = Document.FromJson(customerAsJson).ToAttributeMap();
        var createItemRequest = new PutItemRequest
        {
            TableName = _tableName,
            Item = customerAsAttributes,
            ConditionExpression = "attribute_not_exists(pk) and attribute_not_exists(sk)"
        };

        var response = await _dynamoDb.PutItemAsync(createItemRequest);

        return response.HttpStatusCode == HttpStatusCode.OK;
    }

    public async Task<CustomerDto?> GetAsync(Guid id)
    {
        var getItemRequest = new GetItemRequest
        {
            TableName = _tableName,
            Key = new Dictionary<string, AttributeValue>
            {
                { "pk", new AttributeValue{ S = id.ToString()}},
                { "sk", new AttributeValue{ S = id.ToString()}},
            }
        };

        var response = await _dynamoDb.GetItemAsync(getItemRequest);
        if(response.Item.Count == 0)
        {
            return null;
        }

        var itemAsDocument = Document.FromAttributeMap(response.Item);
        return JsonSerializer.Deserialize<CustomerDto>(itemAsDocument.ToJson());

    }

    public async Task<IEnumerable<CustomerDto>> GetAllAsync()
    {
        var scanRequest = new ScanRequest { 
            TableName = _tableName, 
            Limit = 20//Pode colocar um limite de busca, para não ficar lento.. podendo paginar
        };

        var response = await _dynamoDb.ScanAsync(scanRequest);
        return response.Items.Select(x => {
            var json = Document.FromAttributeMap(x).ToJson();
            return JsonSerializer.Deserialize<CustomerDto>(json);
        })!;
    }

    public async Task<bool> UpdateAsync(CustomerDto customer, DateTime requestedStarted)
    {
        customer.UpdatedAt = DateTime.UtcNow;
        var customerAsJson = JsonSerializer.Serialize(customer);
        var customerAsAttributes = Document.FromJson(customerAsJson).ToAttributeMap();
        var updateItemRequest = new PutItemRequest
        {
            TableName = _tableName,
            Item = customerAsAttributes,
            ConditionExpression = "UpdatedAt < :requestStarted",
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                {
                    ":requestStarted", new AttributeValue
                    {
                        S = requestedStarted.ToString("O")
                    }
                }
            }
        };

        var response = await _dynamoDb.PutItemAsync(updateItemRequest);

        return response.HttpStatusCode == HttpStatusCode.OK;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var deletedItemRequest = new DeleteItemRequest
        {
            TableName = _tableName,
            Key = new Dictionary<string, AttributeValue>
            {
                { "pk", new AttributeValue{ S = id.ToString()}},
                { "sk", new AttributeValue{ S = id.ToString()}},
            }
        };

        var response = await _dynamoDb.DeleteItemAsync(deletedItemRequest);
        return response.HttpStatusCode == HttpStatusCode.OK;
    }

    public async Task<CustomerDto?> GetByEmailAsync(string email)
    {
        var queryRequest = new QueryRequest
        {
            TableName = _tableName,
            IndexName = "email-id-index",
            KeyConditionExpression = "Email = :v_Email",
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                {
                    ":v_Email", new AttributeValue{ S = email }
                }
            }
        };

        var response = await _dynamoDb.QueryAsync(queryRequest);
        if(response.Items.Count == 0)
        {
            return null;
        }

        var itemAsDocument = Document.FromAttributeMap(response.Items[0]);
        return JsonSerializer.Deserialize<CustomerDto>(itemAsDocument.ToJson());
    }
}
