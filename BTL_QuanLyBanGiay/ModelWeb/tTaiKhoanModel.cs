using ModelWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelWeb
{
    
    class tTaiKhoanModel
    {
        BanGiayDbContext context = null;
        public tTaiKhoanModel()
        {
            context = new BanGiayDbContext();
        }
        public bool isLogin(string user,string pass)
        {
            object[] sqlparams =
            {
                new SqlParameter("@username",user),
                new SqlParameter("@password",pass)
            };
            var res = context.Database.SqlQuery<bool>("sp_login @username @password", sqlparams).SingleOrDefault();
            return res;
        }
    }
}
