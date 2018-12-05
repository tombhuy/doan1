using DataProvider.Model;
using DataProvider.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public interface IAboutRepository:IRepository<About>
    {

    }
    public class AboutRepository: BaseRepository<About>, IAboutRepository
    {

    }
}
