using DataProvider.Model;
using DataProvider.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public interface IAudioBookRepository : IRepository<AudioBook>
    {
        //Phần Giao Diện
        AudioBook ListAudioByBook(int idBook);
        List<AudioBook> ListAllAudioBook();
        AudioBook ListAudioByNameAudio(string nameAudio);
    }

    public class AudioBookRepository : BaseRepository<AudioBook>, IAudioBookRepository
    {
        public List<AudioBook> ListAllAudioBook()
        {
            return _dbContext.AudioBooks.ToList();
        }

        //Lấy ra danh sách các Audio Book thuộc quyển sách đó
        public AudioBook ListAudioByBook(int idBook)
        {
            //var category = _dbContext.Books.Find(categoryID);
            return _dbContext.AudioBooks.Where(x => x.BookID == idBook).SingleOrDefault();
        }

        public AudioBook ListAudioByNameAudio(string nameAudio)
        {
            return _dbContext.AudioBooks.Where(x => x.Alias == nameAudio).SingleOrDefault();
        }
    }
}
