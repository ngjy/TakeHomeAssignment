using Newtonsoft.Json;
using TakeHomeAssignment.Services;

public class PeopleService : IPeopleService
{
    private readonly string filePath = "Data/people.json";
    private List<Person> listPeople;

    public PeopleService()
    {
        if (File.Exists(filePath))
        {
            var jsonData = File.ReadAllText(filePath);
            listPeople = JsonConvert.DeserializeObject<List<Person>>(jsonData) ?? new List<Person>();
        }
        else
        {
            listPeople = new List<Person>();
        }
    }

    public Person GetPersonById(int id) {
        return listPeople.FirstOrDefault(p => p.Id == id);
    } 
    
    public void AddPerson(Person person)
    {
        listPeople.Add(person);
        SaveChanges();
    }

    public void DeletePerson(int id)
    {
        Person person = GetPersonById(id);
        if (person != null)
        {
            listPeople.Remove(person);
            SaveChanges();
        }
    }

    private void SaveChanges()
    {
        var jsonData = JsonConvert.SerializeObject(listPeople, Formatting.Indented);
        File.WriteAllText(filePath, jsonData);
    }
}