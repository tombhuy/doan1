using DataProvider.Model;
using DataProvider.Repository;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public interface IChapterDetailRepository : IRepository<ChapterDetail>
    {
        IPagedList<ChapterDetail> GetAllWithPageListSearch(int? page, int pageSize, string searchString);
    }
    public class ChapterDetailRepository:BaseRepository<ChapterDetail>,IChapterDetailRepository
    {
        public ChapterDetail getByID2(int bookid,int chapterid)
        {
            if (bookid != 0)
            {
                if (chapterid != 0)
                {
                    return _dbContext.ChapterDetails.Where(x => x.IDBook == bookid & x.ChapterID == chapterid).FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }
            else
                return null;
        }
        public IPagedList<ChapterDetail> GetAllWithPageListSearch(int? page, int pageSize, string searchString)
        {
            int pageNumber = (page ?? 1);
            IQueryable<ChapterDetail> result = _dbContext.ChapterDetails;
            int id = 0;
            Int32.TryParse(searchString, out id);

            if (!(string.IsNullOrEmpty(searchString)))
            {
                result = result.Where(x => x.ChapterID == id || x.NameChapter.Contains(searchString));
            }
            return result.OrderBy(x => x.ChapterID).ToPagedList(pageNumber, pageSize);
        }

        public bool IsContainInListChapterBook(int idBook,int idChapter)
        {
            var result = _dbContext.ChapterDetails.Where(x => x.IDBook == idBook && x.ChapterID == idChapter).FirstOrDefault();
            if(result!=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
