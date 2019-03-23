using System.Collections.Generic;
using PersonListApp.Models;

namespace PersonListApp.Tools.DataStorage
{
    interface IDataStorage
    {
        void AddPerson(Person person);
        void DeletePerson(Person person);
        void EditPerson(Person person, Person resPerson);
        void SaveChanges();
        List<Person> PersonsList { get; set; }
    }
}
