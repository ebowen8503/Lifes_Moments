using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using LifesMoments.Models;


namespace LifesMoments.Controllers.DataControllers
{
    public class lmUserDataController
    {
        public static SqlDatabase db;

        /// <summary>
        /// The beginning of the lmUserDataController constructor establishes db connection with "DefaultConnection"
        /// </summary>

        public lmUserDataController(string connectionstring)
        {
            if (connectionstring.Length > 0)
            {
                db = new SqlDatabase(connectionstring);
            }
            else
            {
                if (db == null)
                {
                    db = new SqlDatabase(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                }
            }
        }

        public List<userModel> GetListUser()
        {
            ///uses read procedure by primary ID and returns corresponding values
            DbCommand get_User = db.GetStoredProcCommand("sp_ReadlmUser");
            db.AddInParameter(get_User, "@userID", DbType.Int32);
            DataSet ds = db.ExecuteDataSet(get_User);

            var userData = (from drRow in ds.Tables[0].AsEnumerable()
                            select new userModel()
                            {
                                userID = drRow.Field<int>("userID"),
                                firstName = drRow.Field<string>("firstName"),
                                lastName = drRow.Field<string>("LastName"),
                                emailAddress = drRow.Field<string>("emailAddress"),
                                userPassword = drRow.Field<string>("userPassword"),

                            }).ToList();
            return userData;
        }
        public userModel CreateUser(userModel currentUser)
        {
            ///uses create procedure to insert values into the model parameters
            DbCommand create_User = db.GetStoredProcCommand("sp_SavelmUser");
            db.AddInParameter(create_User, "@userID", DbType.Int32, currentUser.userID);
            db.AddInParameter(create_User, "@firstName", DbType.String, currentUser.firstName);
            db.AddInParameter(create_User, "@lastName", DbType.String, currentUser.lastName);
            db.AddInParameter(create_User, "@emailAddress", DbType.String, currentUser.emailAddress);
            db.AddInParameter(create_User, "@userPassword", DbType.String, currentUser.userPassword);

            db.ExecuteNonQuery(create_User);

            return currentUser;
        }

        public bool UpdateListuser(userModel selectedUser)
        {
            ///uses update procedure to make changes to parameter values
            Boolean success = false;
            DbCommand update_User = db.GetStoredProcCommand("sp_SavelmUser");

            db.AddInParameter(update_User, "@userID", DbType.Int32, selectedUser.userID);
            db.AddInParameter(update_User, "@firstName", DbType.String, selectedUser.firstName);
            db.AddInParameter(update_User, "@lastName", DbType.String, selectedUser.lastName);
            db.AddInParameter(update_User, "@emailAddress", DbType.String, selectedUser.emailAddress);
            db.AddInParameter(update_User, "@userPassword", DbType.String, selectedUser.userPassword);
            
            success = Convert.ToBoolean(update_User.ExecuteNonQuery());

            return success;
        }
        public bool DeleteUser(userModel deleteUser)
        {
            ///uses delete procedure to remove list and all its values by the primary ID
            DbCommand delete_User = db.GetStoredProcCommand("sp_DeletelmUser");

            db.AddInParameter(delete_User, "@userID", DbType.Int32, deleteUser.userID);

            db.AddOutParameter(delete_User, "@success", DbType.Boolean, 1);

            bool success;
            try
            {
                db.ExecuteNonQuery(delete_User);
                success = Convert.ToBoolean(db.GetParameterValue(delete_User, "@success"));
            }
            catch
            {
                success = false;
            }

            return success;
        }

        public userModel GetLogin(loginModel login)
        {
            userModel currentUser = new userModel();

            ///uses read procedure by login properties and returns corresponding user
            DbCommand get_Login = db.GetStoredProcCommand("sp_ReadlmLoginUser");
            db.AddInParameter(get_Login, "@emailAddress", DbType.String, login.emailAddress);
            db.AddInParameter(get_Login, "@userPassword", DbType.String, login.userPassword);
            DataSet ds = db.ExecuteDataSet(get_Login);

            return currentUser;
        }

    }
}