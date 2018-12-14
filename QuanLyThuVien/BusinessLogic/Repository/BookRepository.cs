using DataProvider.Model;
using DataProvider.ModelView;
using DataProvider.Repository;
using PagedList;
using System;
using System.Linq;

namespace BusinessLogic.Repository
{
    public interface IBookRepository : IRepository<Book>
    {
        IPagedList<Book> GetAllWithPageListSearch(int? page, int pageSize, string searchString);
        IPagedList<ChapterDetail> GetAllChapterByIDBook(int? page, int pageSize, int id);
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


        public IPagedList<ChapterDetail> GetAllChapterByIDBook(int? page,int pageSize,int id)
        {
            int pageNumber = (page ?? 1);
            var result = _dbContext.ChapterDetails.Where(x => x.IDBook == id).OrderBy(x => x.ChapterID).ToPagedList(pageNumber, pageSize);
            return result;
        }


        public IPagedList<EbookModelView> GetAllEbookByIDBook(int? page, int pageSize, int id)
        {
            int pageNumber = (page ?? 1);
            var ketqua = (from e in _dbContext.Ebooks
                          join te in _dbContext.TypeEbooks on e.TypeEbook equals te.TypeID
                          where e.BookID == id
                          select new EbookModelView
                          {
                              BookID = id,
                              TypeEbookID = e.TypeEbook,
                              TypeEbookName = te.Name,
                              Link = e.Link
                          }).AsEnumerable<EbookModelView>().OrderBy(x=>x.TypeEbookName).ToPagedList(pageNumber, pageSize);
            return ketqua;
        }

    }
}