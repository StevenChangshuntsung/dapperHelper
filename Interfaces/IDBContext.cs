using dapperHelper.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dapperHelper.Interfaces
{
    public interface IDBContext
    {
        Task<T> QuerySingleAsync<T>(string queryStr, object paramObj = null);
        Task<List<T>> QueryAsync<T>(string queryStr, object paramObj = null, bool isCache = true);
        Task<int> ExecuteAsync(string queryStr, object paramObj = null);
        int Execute(string queryStr, object paramObj = null);

        List<ErrorMsgModel> ErrorMessages(SqlException ex);
    }
}
