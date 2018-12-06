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
    public interface IBookCategoryRepository:IRepository<BookCategory>
    {
        IPagedList<BookCategory> GetAllWithPageListSearch(int? page, int pageSize, string searchString);
    }
    public class BookCategoryRepository : BaseRepository<BookCategory>, IBookCategoryRepository
    {
        public IPagedList<BookCategory> GetAllWithPageListSearch(int? page, int pageSize, string searchString)
        {
            int pageNumber = (page ?? 1);
            IQueryable<BookCategory> result = _dbContext.BookCategories;
            int id = 0;
            Int32.TryParse(searchString, out id);

            if (!(string.IsNullOrEmpty(searchString)))
            {
                result = result.Where(x => x.CategoryID == id||x.CategoryName.Contains(searchString));
            }
            return result.OrderBy(x => x.CategoryID).ToPagedList(pageNumber, pageSize);
        }
    }
}
