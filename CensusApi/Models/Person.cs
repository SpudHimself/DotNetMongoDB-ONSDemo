using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CensusApi.Models;

public class Person
{
    //BsonID makes the string Id this models primary key
    //BsonRepresentation allows the parameter to be passed as a string rather than an ObjectId. The conversion is handled by Mongo itself.
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    //name, age, residence, occupation
    public string Name { get; set; } = null!;
    public int Age { get; set; }
    public string Residence { get; set; } = null!;
    public string Occupation { get; set; } = null!;
}