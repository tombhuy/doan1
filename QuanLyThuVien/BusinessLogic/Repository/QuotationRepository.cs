using DataProvider.Model;
using DataProvider.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace BusinessLogic.Repository
{
    public interface IQuotationRepository:IRepository<Quotation>
    {
        IEnumerable<Quotation> ListAllWithPageList(int page, int pageSize);
        IPagedList<Quotation> GetAllWithPageListSearch(int? page, int pageSize, string searchString);
    }
    public class QuotationRepository:BaseRepository<Quotation>,IQuotationRepository
    {
        //Phân Trang
        public IEnumerable<Quotation> ListAllWithPageList(int page, int pageSize)
        {
            return _dbContext.Quotations.OrderBy(x => x.QuotationID).ToPagedList(page, pageSize);
        }

        //End Phân Trang
        public IPagedList<Quotation> GetAllWithPageListSearch(int? page, int pageSize, string searchString)
        {
            int pageNumber = (page ?? 1);
            IQueryable<Quotation> result = _dbContext.Quotations;
            int id = 0;
            Int32.TryParse(searchString, out id);

            if (!(string.IsNullOrEmpty(searchString)))
            {
                result = result.Where(x => x.NameQuotation.Contains(searchString)||x.QuotationID==id);
            }
            return result.OrderBy(x => x.QuotationID).ToPagedList(pageNumber, pageSize);
        }

    }
}
