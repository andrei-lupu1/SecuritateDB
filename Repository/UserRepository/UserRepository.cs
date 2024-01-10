using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models.Users;
using Oracle.ManagedDataAccess.Client;
using Repository.GenericRepository;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.UserRepository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly Context _context;
        public UserRepository(Context context) : base(context)
        {
            _context = context;
        }

        public int Register(string username, string password)
        {
            OracleParameter param1 = new OracleParameter("USERNAME", OracleDbType.Varchar2, username, System.Data.ParameterDirection.Input);
            OracleParameter param2 = new OracleParameter("PASS", OracleDbType.Varchar2, password, System.Data.ParameterDirection.Input);
            OracleParameter param3 = new OracleParameter("RESULT", OracleDbType.Int32, System.Data.ParameterDirection.Output);
            OracleParameter[] parameters = new OracleParameter[] { param1, param2, param3 };
            _context.Database.ExecuteSqlRaw("BEGIN REGISTER(:USERNAME, :PASS, :RESULT); END;", parameters);
            var result = Convert.ToInt32(param3.Value.ToString());
            return result;
        }

        public int Login(string username, string password)
        {
            OracleParameter param1 = new OracleParameter("V_USERNAME", OracleDbType.Varchar2, username, System.Data.ParameterDirection.Input);
            OracleParameter param2 = new OracleParameter("V_PASS", OracleDbType.Varchar2, password, System.Data.ParameterDirection.Input);
            OracleParameter param3 = new OracleParameter("RESULT", OracleDbType.Int32, System.Data.ParameterDirection.Output);
            OracleParameter[] parameters = new OracleParameter[] { param1, param2, param3 };
            _context.Database.ExecuteSqlRaw("BEGIN LOGIN(:V_USERNAME, :V_PASS, :RESULT); END;", parameters);
            var result = Convert.ToInt32(param3.Value.ToString());
            return result;
        }
    }
}
