using Amazon.S3;
using Amazon.S3.Model;
using System.Text;

var s3Client = new AmazonS3Client();

//DOWNALOAD
var getObjectRequest = new GetObjectRequest
{
    BucketName = "fernando2dias",
    Key = "files/movies.csv"
};

var response = await s3Client.GetObjectAsync(getObjectRequest);

using var memoryStream = new MemoryStream();
response.ResponseStream.CopyTo(memoryStream);

var text = Encoding.Default.GetString(memoryStream.ToArray());

Console.WriteLine(text);



//UPLOAD
//using var inputStream = new FileStream("./face.jpg", FileMode.Open, FileAccess.Read);
//using var inputStream = new FileStream("./movies.csv", FileMode.Open, FileAccess.Read);

//var putObjectRequest = new PutObjectRequest
//{
//    BucketName = "fernando2dias",
//    Key = "images/face.jpg",
//    ContentType = "image/jpeg",
//    InputStream = inputStream
//};

//var putObjectRequest = new PutObjectRequest
//{
//    BucketName = "fernando2dias",
//    Key = "files/movies.csv",
//    ContentType = "text/csv",
//    InputStream = inputStream
//};

//await s3Client.PutObjectAsync(putObjectRequest);