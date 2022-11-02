using CensusApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CensusApi.Services;

public class PersonService
{
    private readonly IMongoCollection<Person> _personCollection;

    /// <summary>
    /// PersonService constructor where the information in the CensusDatabaseSettings file is passed and stored in the _personCollection variable for later CRUD use.
    /// </summary>
    /// <param name="settings"></param>
    public PersonService(IOptions<CensusDatabaseSettings> settings)
    {
        var mongoClient = new MongoClient(settings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);

        _personCollection = mongoDatabase.GetCollection<Person>(settings.Value.CollectionName);
    }

    //Create, Read, Update and Delete methods

    /// <summary>
    /// returns a list of every person in the collection.
    /// (Asynchronous)
    /// </summary>
    /// <returns></returns>
    public async Task<List<Person>> GetPersonAsync() => await _personCollection.Find(_ => true).ToListAsync();

    /// <summary>
    /// returns a specified person via their ID variable from the collection.
    /// (Asynchronous)
    /// </summary>
    /// <param name="id">The string ID of the person you wish to return.</param>
    /// <returns></returns>
    public async Task<Person?> GetPersonAsync(string id) => await _personCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    /// <summary>
    /// Create a new person entry in the person collection.
    /// (Asynchronous)
    /// </summary>
    /// <param name="newEntry">The Person variable that will get inserted into the collection.</param>
    /// <returns></returns>
    public async Task CreatePersonAsync(Person newEntry) => await _personCollection.InsertOneAsync(newEntry);

    /// <summary>
    /// Updates an existing entry in the person collection.
    /// (Asynchronous)
    /// </summary>
    /// <param name="id">The string ID of the person entry that you wish to update.</param>
    /// <param name="updatedEntry">The Person variable that will replace the information stored in the given ID entry.</param>
    /// <returns></returns>
    public async Task UpdatePersonAsync(string id, Person updatedEntry) => await _personCollection.ReplaceOneAsync(x => x.Id == id, updatedEntry);

    /// <summary>
    /// Removes an existing entry in the person collection.
    /// </summary>
    /// <param name="id">The string ID of the person entry that you wish to remove.</param>
    /// <returns></returns>
    public async Task RemovePersonAsync(string id) => await _personCollection.DeleteOneAsync(x => x.Id == id);
}