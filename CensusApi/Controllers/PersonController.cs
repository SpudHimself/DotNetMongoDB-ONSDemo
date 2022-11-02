using CensusApi.Models;
using CensusApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CensusApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly PersonService _personService;

    /// <summary>
    /// PersonController constructor that takes a personService and stores it for CRUD related information for the view to display.
    /// </summary>
    /// <param name="personService"></param>
    public PersonController(PersonService personService)
    {
        _personService = personService;
    }

    /// <summary>
    /// Action method supporting HttpGet requests. Uses the GetPersonAsync method from PersonService. Returns every person entry in the collection.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<List<Person>> Get() => await _personService.GetPersonAsync();

    /// <summary>
    /// Action method supporting HttpGet requests. Uses the GetPersonAsync method from PersonService. Returns a person entry in the collection.
    /// </summary>
    /// <param name="id">The Person string ID.</param>
    /// <returns></returns>
    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Person>> Get(string id)
    {
        var person = await _personService.GetPersonAsync(id);

        return person == null ? NotFound() : person;
    }

    /// <summary>
    /// Action method supporting HttpPost requests. Uses the CreatePersonAsync method from PersonService. Creates a person entry in the collection.
    /// </summary>
    /// <param name="newPerson">The Person variable that will be inserted into the collection. </param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post(Person newPerson)
    {
        await _personService.CreatePersonAsync(newPerson);

        return CreatedAtAction(nameof(Get), new { id = newPerson.Id }, newPerson);
    }

    /// <summary>
    /// Action method supporting HttpPut requests. Uses the UpdatePersonAsync method from PersonService. Updates a person entry in the collection.
    /// </summary>
    /// <param name="id">The string ID of the person that will be updated</param>
    /// <param name="updatedPerson">The details that will replace the info stored in the given person ID</param>
    /// <returns></returns>
    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Person updatedPerson)
    {
        var person = await _personService.GetPersonAsync(id);

        if (person == null)
            return NotFound();

        updatedPerson.Id = person.Id;

        await _personService.UpdatePersonAsync(id, updatedPerson);

        return NoContent();
    }

    /// <summary>
    /// Action method supporting HttpDelete requests. Uses the RemovePersonAsync method from PersonService. Deletes a person entry in the collection.
    /// </summary>
    /// <param name="id">The String ID of the person entry that will be deleted.</param>
    /// <returns></returns>
    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var person = await _personService.GetPersonAsync(id);

        if (person == null)
            return NotFound();

        await _personService.RemovePersonAsync(id);

        return NoContent();
    }
}