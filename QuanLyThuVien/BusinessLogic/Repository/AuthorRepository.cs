using DataProvider.Model;
using DataProvider.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public interface IAuthorRepository:IRepository<Author>
    {
        void LayDanhSach();

    }
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        public void LayDanhSach()
        {
            
        }
    }
}
