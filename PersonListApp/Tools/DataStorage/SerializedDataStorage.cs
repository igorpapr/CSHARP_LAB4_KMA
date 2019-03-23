using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PersonListApp.Models;
using PersonListApp.Tools.Managers;

namespace PersonListApp.Tools.DataStorage
{
    class SerializedDataStorage:IDataStorage
    {
        private List<Person> _persons;

        internal SerializedDataStorage()
        {
            try
            {
                _persons = SerializationManager.Deserialize<List<Person>>(FileFolderHelper.StorageFilePath);
            }
            catch (FileNotFoundException)
            {
                _persons = new List<Person>();
                FillWithInitialPersons();
                SaveChanges();
            }
        }

        private void FillWithInitialPersons()
        {
            Random rand = new Random();
            for (int i = 0; i < 50; i++)
            {
                AddPerson(new Person($"{rand.Next(1,200)}Test{i}",
                    $"LastTest{i}",
                    $"init{rand.Next(1, 101)}@test.tes",
                    new DateTime(rand.Next(1950, 2019), rand.Next(1, 13), rand.Next(1, 27))));
            }

        }

        public List<Person> PersonsList
        {
            get
            {
                return _persons.ToList();

            }
            set
            {
                _persons = value; 

            }
        }

        public void EditPerson(Person prevPerson, Person resPerson)
        {
            if (canAddOrChange(resPerson))
                _persons[_persons.IndexOf(prevPerson)] = resPerson;
            else throw new ArgumentException("Bad values");
        }

        public void SaveChanges()
        {
            SerializationManager.Serialize(_persons, FileFolderHelper.StorageFilePath);
        }

        public void AddPerson(Person person)
        {
            if (canAddOrChange(person))
            { 
                _persons.Add(person);
                SaveChanges();
            }
            else throw new ArgumentException("Bad values");
        }

        public void DeletePerson(Person person)
        {
            _persons.Remove(person);
            SaveChanges();
        }

        private bool canAddOrChange(Person p)
        {
            return !string.IsNullOrWhiteSpace(p.FirstName) && !string.IsNullOrWhiteSpace(p.LastName) && !string.IsNullOrWhiteSpace(p.Email);
        }
    }
}
