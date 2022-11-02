namespace CensusApi.Models;

public class CensusDatabaseSettings
{
    ///The IP information that will be passed for connection between the program and the database
    public string ConnectionString {get; set;} = null!;

    //The name of database that the program will access
    public string DatabaseName {get; set;} =null!;
    
    //The collection of data within a database that the program will parse
    public string CollectionName {get; set;} = null!;
}