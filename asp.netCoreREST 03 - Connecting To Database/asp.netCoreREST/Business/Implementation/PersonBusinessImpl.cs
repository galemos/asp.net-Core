using asp.netCoreREST.Model;
using asp.netCoreREST.Repository;
using System.Collections.Generic;

namespace asp.netCoreREST.Business.Implementation {
    public class PersonBusinessImpl : IPersonBusiness {

        private IPersonRepository _repository;

        public PersonBusinessImpl(IPersonRepository repository)
        {
            _repository = repository;
        }

        public Person Create(Person person) {           
            return _repository.Create(person);
        }

        public Person FindById(long id) {
            return _repository.FindById(id);
        }

        public List<Person> FindAll() {
            return _repository.FindAll();
        }

        public Person Update(Person person) {
            return _repository.Update(person);
        }

        public void Delete(long id) {
            _repository.Delete(id);            
        }
    }
}
