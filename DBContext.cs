using Dapper;
using dapperHelper.Interfaces;
using dapperHelper.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace dapperHelper
{
    public class DBContext : IDBContext
    {
        public DBContext(string connectionStr)
        {
            ConnectionStr = connectionStr;
        }
        /// <summary>
        /// 
        /// 
        /// 
        /// 
        /// </summary>
        public string ConnectionStr { get; set; }

        /// <summary>
        /// 抓取各種DB連線物件(必覆寫)
        /// </summary>
        /// <returns></returns>
        private SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionStr);
        }
        public async Task<T> QuerySingleAsync<T>(string queryStr, object paramObj = null)
        {
            try
            {
                using (var con = GetConnection())
                {
                    return await con.QuerySingleOrDefaultAsync<T>(queryStr, paramObj);
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<List<T>> QueryAsync<T>(string queryStr, object paramObj = null, bool isCache = true)
        {
            try
            {
                using (var con = GetConnection())
                {
                    CommandDefinition cmd = new CommandDefinition(queryStr, paramObj, flags: isCache ? CommandFlags.Buffered : CommandFlags.NoCache);
                    var result = await con.QueryAsync<T>(cmd);
                    return result.AsList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> ExecuteAsync(string queryStr, object paramObj = null)
        {
            try
            {
                using (var con = GetConnection())
                {
                    var result = await con.ExecuteAsync(queryStr, paramObj);
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int Execute(string queryStr, object paramObj = null)
        {
            try
            {
                using (var con = GetConnection())
                {
                    var result = con.Execute(queryStr, paramObj);
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ErrorMsgModel> ErrorMessages(SqlException ex)
        {
            List<ErrorMsgModel> errMsgs = new List<ErrorMsgModel>();
            for (int i = 0; i < ex.Errors.Count; i++)
            {
                errMsgs.Add(new ErrorMsgModel() 
                {
                    Message = ex.Errors[i].Message,
                    LineNumber = ex.Errors[i].LineNumber,
                    Source = ex.Errors[i].Source,
                    Procedure = ex.Errors[i].Procedure,
                });
            }
            return errMsgs;
        }
    }
}
