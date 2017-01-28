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
    public class lmAlbumDataController
    {
        public static SqlDatabase db;

        /// <summary>
        /// The beginning of the lmAlbumDataController constructor establishes db connection with "DefaultConnection"
        /// </summary>

        public lmAlbumDataController(string connectionstring)
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

        public List<albumModel> GetListAlbum()
        {
            ///uses read procedure by primary ID and returns corresponding values
            DbCommand get_Album = db.GetStoredProcCommand("sp_ReadlmAlbum");
            db.AddInParameter(get_Album, "@albumID", DbType.Int32);
            DataSet ds = db.ExecuteDataSet(get_Album);

            var albumData = (from drRow in ds.Tables[0].AsEnumerable()
                            select new albumModel()
                            {
                                albumID = drRow.Field<int>("albumID"),
                                albumDescription = drRow.Field<string>("albumDescription"),
                                albumDate = drRow.Field<DateTime>("albumDate"),
                                userIDFK = drRow.Field<int>("userIDFK"),

                            }).ToList();
            return albumData;
        }
        public albumModel CreateAlbum(albumModel currentAlbum)
        {
            ///uses create procedure to insert values into the model parameters
            DbCommand create_Album = db.GetStoredProcCommand("sp_CreatelmAlbum");
            db.AddInParameter(create_Album, "@albumID", DbType.Int32, currentAlbum.albumID);
            db.AddInParameter(create_Album, "@albumDescription", DbType.String, currentAlbum.albumDescription);
            db.AddInParameter(create_Album, "@albumDate", DbType.DateTime, currentAlbum.albumDate);
            db.AddInParameter(create_Album, "@userIDFK", DbType.Int32,currentAlbum.userIDFK);
            
            db.ExecuteNonQuery(create_Album);

            return currentAlbum;
        }

        public bool UpdateListalbum(albumModel selectedAlbum)
        {
            ///uses update procedure to make changes to parameter values
            Boolean success = false;
            DbCommand update_Album = db.GetStoredProcCommand("sp_UpdatelmAlbum");

            db.AddInParameter(update_Album, "@albumID", DbType.Int32, selectedAlbum.albumID);
            db.AddInParameter(update_Album, "@albumDescription", DbType.String, selectedAlbum.albumDescription);
            db.AddInParameter(update_Album, "@albumDate", DbType.DateTime, selectedAlbum.albumDate);
            db.AddInParameter(update_Album, "@userIDFK", DbType.Int32, selectedAlbum.userIDFK);

            success = Convert.ToBoolean(update_Album.ExecuteNonQuery());

            return success;
        }
        public bool DeleteAlbum(albumModel deleteAlbum)
        {
            ///uses delete procedure to remove list and all its values by the primary ID
            DbCommand delete_Album = db.GetStoredProcCommand("sp_DeletelmAlbum");

            db.AddInParameter(delete_Album, "@albumID", DbType.Int32, deleteAlbum.albumID);

            db.AddOutParameter(delete_Album, "@success", DbType.Boolean, 1);

            bool success;
            try
            {
                db.ExecuteNonQuery(delete_Album);
                success = Convert.ToBoolean(db.GetParameterValue(delete_Album, "@success"));
            }
            catch
            {
                success = false;
            }

            return success;
        }

    }
}