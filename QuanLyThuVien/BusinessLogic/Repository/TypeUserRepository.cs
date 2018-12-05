using DataProvider.Model;
using DataProvider.Repository;

namespace BusinessLogic.Repository
{
    public interface ITypeUserRepository : IRepository<TypeUser>
    {
    }

    public class TypeUserRepository : BaseRepository<TypeUser>, ITypeUserRepository
    {
        public TypeUserRepository()
        {

        }
    }
}