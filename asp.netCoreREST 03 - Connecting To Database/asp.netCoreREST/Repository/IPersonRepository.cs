using asp.netCoreREST.Model;
using System.Collections.Generic;

namespace asp.netCoreREST.Repository {
    public interface IPersonRepository {
        Person Create(Person person);
        Person FindById(long id);
        List<Person> FindAll();
        Person Update(Person person);
        void Delete(long id);

        bool Exist(long id);
    }
}
