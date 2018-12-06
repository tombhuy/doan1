using DataProvider.Model;
using DataProvider.Repository;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public interface IAuthorRepository:IRepository<Author>
    {
        IPagedList<Author> GetAllWithPageListSearch(int? page, int pageSize, string searchString);

    }
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        public IPagedList<Author> GetAllWithPageListSearch(int? page, int pageSize, string searchString)
        {
            int pageNumber = (page ?? 1);
            IQueryable<Author> result = _dbContext.Authors;
            int id = 0;
            Int32.TryParse(searchString, out id);

            if (!(string.IsNullOrEmpty(searchString)))
            {
                result = result.Where(x => x.AuthorID == id || x.AuthorName.Contains(searchString) || x.DescriptionAuthor.Contains(searchString) || x.Alias.Contains(searchString));
            }
            return result.OrderBy(x => x.AuthorID).ToPagedList(pageNumber, pageSize);
        }
    }
}
