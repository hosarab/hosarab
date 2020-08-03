using System;
using System.Collections.Generic;
using SampleDemo.Models;

namespace SampleDemo.Data
{
    public class MockSampleDemoRepo : ISampleDemoRepo
    {
        public void CreatePerson(Person cmd)
        {
            throw new System.NotImplementedException();
        }

        public void DeletePerson(Person cmd)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Person> GetAllPersons()
        {
            var commands = new List<Person>
            {
                new Person{Id=0, Name="David", CreatedDate= DateTime.Now},
                new Person{Id=1, Name="Peter", CreatedDate= DateTime.Now},
                new Person{Id=2, Name="Richard", CreatedDate= DateTime.Now},
            };

            return commands;
        }

        public Person GetPersonById(int id)
        {
            return new Person{Id=0,  Name="Boil an egg", CreatedDate = DateTime.Now};
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdatePerson(Person cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}