namespace TakeHomeAssignment.Services
{
    public interface IPeopleService
    {
        Person GetPersonById(int id);
        void AddPerson(Person person);
        void DeletePerson(int id);
    }
}
