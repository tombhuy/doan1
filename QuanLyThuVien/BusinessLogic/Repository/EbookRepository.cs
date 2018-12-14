using DataProvider.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using DataProvider.Repository;
using DataProvider.ModelView;

namespace BusinessLogic.Repository
{
    public interface IEbookRepository : IRepository<Ebook>
    {
        Ebook GetEbookByID2(int idbook, int typeid);
        //IPagedList<Ebook> GetAllWithPageListSearch(int? page, int pageSize, string searchString);

    }
    public class EbookRepository : BaseRepository<Ebook>, IEbookRepository
    {
        //public IPagedList<Author> GetAllWithPageListSearch(int? page, int pageSize, string searchString)
        //{
        //    int pageNumber = (page ?? 1);
        //    IQueryable<Author> result = _dbContext.Authors;
        //    int id = 0;
        //    Int32.TryParse(searchString, out id);

        //    if (!(string.IsNullOrEmpty(searchString)))
        //    {
        //        result = result.Where(x => x.AuthorID == id || x.AuthorName.Contains(searchString) || x.DescriptionAuthor.Contains(searchString) || x.Alias.Contains(searchString));
        //    }
        //    return result.OrderBy(x => x.AuthorID).ToPagedList(pageNumber, pageSize);
        //}
        public Ebook GetEbookByID2(int idbook, int typeid)
        {
            var result = _dbContext.Ebooks.Where(x => x.BookID == idbook && x.TypeEbook == typeid).FirstOrDefault();
            return result;
        }

        public IEnumerable<EbookModelViewName>  GetListEbookViewTypeName()
        {
            var ketqua = (from e in _dbContext.Ebooks
                          join te in _dbContext.TypeEbooks on e.TypeEbook equals te.TypeID
                          select new EbookModelViewName
                          {
                              TypeEbookID = e.TypeEbook,
                              NameTypeEbook = te.Name,
                          }).AsEnumerable<EbookModelViewName>().OrderBy(x => x.NameTypeEbook);
            return ketqua;
        }

    }
}
