using asp.netCoreREST.Model;
using System;
using System.Collections.Generic;
using System.Threading;

namespace asp.netCoreREST.Services.Implementation {
    public class PersonServiceImplementation : IPersonService {
        private volatile int count;

        public Person Create(Person person) {
            return person;
        }

        public Person FindById(long id) {
            return MockPerson(id);
        }

        public List<Person> FindAll() {
            List<Person> persons = new List<Person>();
            for(int i = 0; i < 8; i++) {
                Person person = MockPerson(i);
                persons.Add(person);
            }
            return persons;
        }

        public Person Update(Person person) {
            return person;
        }

        public void Delete(long id) { }

        private Person MockPerson(long i) {
            return new Person {
                Id = IncrementAndGet(),
                FirstName = "New Name" + i,
                LastName = "New Lastname" + i,
                Address = "New Address" + i,
                Gender = "New Gender"

            };
        }

        private long IncrementAndGet() {
            return Interlocked.Increment(ref count);
        }
    }
}
