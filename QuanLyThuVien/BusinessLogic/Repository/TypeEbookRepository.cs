using DataProvider.Model;
using DataProvider.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    //TypeEbookRepository
    public interface ITypeEbookRepository : IRepository<TypeEbook>
    {

    }
    public class TypeEbookRepository : BaseRepository<TypeEbook>, ITypeEbookRepository
    {

    }
}
