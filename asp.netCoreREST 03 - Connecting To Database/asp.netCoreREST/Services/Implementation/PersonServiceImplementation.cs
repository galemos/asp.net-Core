using asp.netCoreREST.Model;
using asp.netCoreREST.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace asp.netCoreREST.Services.Implementation
{
    public class PersonServiceImplementation : IPersonService {

        private MySQLContext _context;

        public PersonServiceImplementation(MySQLContext context)
        {
            _context = context;
        }

        public Person Create(Person person) {
            try
            {
                _context.Add(person);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return person;
        }

        public Person FindById(long id) {
            return _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
        }

        public List<Person> FindAll() {            
            return _context.Persons.ToList();
        }

        public Person Update(Person person) {
            try
            {
                if (!Exist(person.Id))
                    return new Person();

                var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(person.Id));

                _context.Entry(result).CurrentValues.SetValues(person);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return person;
        }

        private bool Exist(long id)
        {
            return _context.Persons.Any(p => p.Id.Equals(id));
        }

        public void Delete(long id) {
            
            try
            {
                var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
                if (result != null)
                    _context.Persons.Remove(result);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
