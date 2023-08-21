using API.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace API.DAL
{
    public class DataAccess:IDataAccess

    {
        private readonly IConfiguration config;
        private readonly string DbCon;
        public DataAccess(IConfiguration _config)
        {
                config = _config;
                DbCon = config["ConnectionStrings:LibraryConnection"] ?? "";
        }


        public int CreateUser(User user)
        {
            try
            {
                //var result = 0;
                //using (var conObj = new SqlConnection(DbCon))
                //{
                //    var parameters = new
                //    {
                //        fn = user.FirstName,
                //        ln = user.LastName,
                //        em = user.Email,
                //        mb = user.Mobile,
                //        pwd = user.Password,
                //        blk = user.Blocked,
                //        act = user.Active,
                //        con = user.CreatedOn,
                //        type = user.UserType.ToString()
                //    };

                //    var sql = "insert into Users (FirstName, LastName, Email, Mobile, Password, Blocked, Active, CreatedOn, UserType) values (@fn, @ln, @em, @mb, @pwd, @blk, @act, @con, @type);";
                //    result = conObj.Execute(sql, parameters);

                //}

                using (var conObj = new SqlConnection(DbCon))
                {
                    //Set up DynamicParameters object to pass parameters  
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@fn", user.FirstName, DbType.String);
                    parameters.Add("@ln", user.LastName, DbType.String);
                    parameters.Add("@em", user.Email, DbType.String);
                    parameters.Add("@mb", user.Mobile, DbType.String);
                    parameters.Add("@pwd", user.Password, DbType.String);
                    parameters.Add("@blk", user.Blocked, DbType.Boolean);
                    parameters.Add("@act", user.Active, DbType.Boolean);
                    parameters.Add("@con", user.CreatedOn, DbType.DateTime);
                    parameters.Add("@type", user.UserType.ToString(), DbType.String);

                    //Execute stored procedure and map the returned result to a Customer object  
                    // result = conObj.QuerySingleOrDefault<int>("SP_I_CreateUser", parameters, commandType: CommandType.StoredProcedure);
                    return conObj.ExecuteScalar<int>("SP_I_CreateUser", parameters, commandType: CommandType.StoredProcedure);
                }
                //return result;
            }
            catch (Exception e)
            {

                throw new Exception (e.Message);
            }
            //throw new NotImplementedException();
        }
        public bool IsEmailAvailable(string email)
        {
            try
            {
                var result = false;
                using (var conObj = new SqlConnection(DbCon))
                {
                    result = conObj.ExecuteScalar<bool>("select count(*) from users where email=@email;", new {email});
                }

                return !result;

            }
            catch (Exception e)
            {

                Console.WriteLine("Something went wrong.");
                throw  new Exception(e.Message);
            }
            //finally
            //{
            //    throw new NotImplementedException();
            //}

        }
    }
}
