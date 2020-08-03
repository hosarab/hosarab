using System.Collections.Generic;
using SampleDemo.Models;

namespace SampleDemo.Data
{
    public interface ISampleDemoRepo
    {
        bool SaveChanges();

        IEnumerable<Person> GetAllPersons();
        Person GetPersonById(int id);
        void CreatePerson(Person cmd);
        void UpdatePerson(Person cmd);
        void DeletePerson(Person cmd);
    }
}