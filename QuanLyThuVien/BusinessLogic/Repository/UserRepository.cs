using DataProvider.Model;
using DataProvider.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using PagedList.Mvc;

namespace BusinessLogic.Repository.Base
{
    public interface IUserRepository:IRepository<User>
    {
        int checkLogin(string userName, string passWord);
        IPagedList<User> GetAllWithPageList(int? page, int pageSize);
        IPagedList<User> GetAllWithPageListSearch(int? page, int pageSize, string searchString);
        User GetUserByUsername(string username);
    }
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public int checkLogin(string userName, string passWord)
        {
            var usr = _dbContext.Users.FirstOrDefault(u => u.UserName == userName);

            if(usr!=null)
            {
                if(usr.Password==passWord)
                {
                    if (usr.Status==true)
                    {
                        return 1;// Đăng nhập thành công
                    }
                    else
                    {
                        return -1;//Tài khoả đã bị khoá
                    }
                }
                else
                {
                    return 0;//Sai mật khẩu
                }
            }
            else
            {
                return -2;//Tài khoản không tồn tại
            }
        }

        public IPagedList<User> GetAllWithPageList(int? page, int pageSize)
        {
            int pageNumber = (page ?? 1);
            return _dbContext.Users.ToList().OrderBy(x => x.UserID).ToPagedList(pageNumber, pageSize);
        }

        public IPagedList<User> GetAllWithPageListSearch(int? page, int pageSize,string searchString)
        {
            int pageNumber = (page ?? 1);
            IQueryable<User> result = _dbContext.Users;
            int id = 0;
            Int32.TryParse(searchString, out id);

            if (!(string.IsNullOrEmpty(searchString)))
            {
                result = result.Where(x => x.UserID==id||x.UserName.Contains(searchString) || x.Password.Contains(searchString) || x.Name.Contains(searchString) || x.Address.Contains(searchString) || x.Email.Contains(searchString) || x.Phone.Contains(searchString));
            }
            return result.OrderBy(x => x.UserID).ToPagedList(pageNumber, pageSize);
        }

        public User GetUserByUsername(string username)
        {
            var usr = _dbContext.Users.FirstOrDefault(u => u.UserName == username);
            return usr;
        }
    }
}
