using DataProvider.Model;
using DataProvider.Repository;
using PagedList;
using System;
using System.Linq;

namespace BusinessLogic.Repository
{
    public interface IBookRepository : IRepository<Book>
    {
        IPagedList<Book> GetAllWithPageListSearch(int? page, int pageSize, string searchString);
    }

    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public IPagedList<Book> GetAllWithPageListSearch(int? page, int pageSize, string searchString)
        {
            int pageNumber = (page ?? 1);
            IQueryable<Book> result = _dbContext.Books;
            int id = 0;
            Int32.TryParse(searchString, out id);

            if (!(string.IsNullOrEmpty(searchString)))
            {
                result = result.Where(x => x.BookID == id || x.BookName.Contains(searchString));
            }
            return result.OrderBy(x => x.BookID).ToPagedList(pageNumber, pageSize);
        }
    }
}