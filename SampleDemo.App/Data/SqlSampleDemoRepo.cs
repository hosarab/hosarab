using System;
using System.Collections.Generic;
using System.Linq;
using SampleDemo.Models;

namespace SampleDemo.Data
{
    public class SqlSampleDemoRepo : ISampleDemoRepo
    {
        private readonly SampleDemoContext _context;

        public SqlSampleDemoRepo(SampleDemoContext context)
        {
            _context = context;
        }

        public void CreatePerson(Person cmd)
        {
            if(cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }

            _context.Persons.Add(cmd);
        }

        public void DeletePerson(Person cmd)
        {
            if(cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }
            _context.Persons.Remove(cmd);
        }

        public IEnumerable<Person> GetAllPersons()
        {
            return _context.Persons.ToList();
        }

        public Person GetPersonById(int id)
        {
            return _context.Persons.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdatePerson(Person cmd)
        {
            //Nothing
        }
    }
}