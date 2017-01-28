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
    public class lmAlbumItemDataController
    {
        public static SqlDatabase db;

        /// <summary>
        /// The beginning of the lmAlbumItemDataController constructor establishes db connection with "DefaultConnection"
        /// </summary>

        public lmAlbumItemDataController(string connectionstring)
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

        public List<albumItemModel> GetListAlbumItem()
        {
            ///uses read procedure by primary ID and returns corresponding values
            DbCommand get_AlbumItem = db.GetStoredProcCommand("sp_ReadlmAlbumItem");
            db.AddInParameter(get_AlbumItem, "@albumItemID", DbType.Int32);
            DataSet ds = db.ExecuteDataSet(get_AlbumItem);

            var albumItemData = (from drRow in ds.Tables[0].AsEnumerable()
                             select new albumItemModel()
                             {
                                 albumItemID = drRow.Field<int>("albumItemID"),
                                 albumItemDescription = drRow.Field<string>("albumItemDescription"),
                                 albumDate = drRow.Field<DateTime>("albumDate"),
                                 userIDFK = drRow.Field<int>("userIDFK"),

                             }).ToList();
            return albumItemData;
        }
        public albumItemModel CreateAlbumItem(albumItemModel currentAlbumItem)
        {
            ///uses create procedure to insert values into the model parameters
            DbCommand create_AlbumItem = db.GetStoredProcCommand("sp_SavelmAlbumItem");
            db.AddInParameter(create_AlbumItem, "@albumItemID", DbType.Int32, currentAlbumItem.albumItemID);
            db.AddInParameter(create_AlbumItem, "@albumItemDescription", DbType.String, currentAlbumItem.albumItemDescription);
            db.AddInParameter(create_AlbumItem, "@albumDate", DbType.DateTime, currentAlbumItem.albumDate);
            db.AddInParameter(create_AlbumItem, "@userIDFK", DbType.Int32, currentAlbumItem.userIDFK);

            db.ExecuteNonQuery(create_AlbumItem);

            return currentAlbumItem;
        }

        public bool UpdateListAlbumItem(albumItemModel selectedAlbumItem)
        {
            ///uses update procedure to make changes to parameter values
            Boolean success = false;
            DbCommand update_AlbumItem = db.GetStoredProcCommand("sp_SavelmAlbumItem");

            db.AddInParameter(update_AlbumItem, "@albumItemID", DbType.Int32, selectedAlbumItem.albumItemID);
            db.AddInParameter(update_AlbumItem, "@albumItemDescription", DbType.String, selectedAlbumItem.albumItemDescription);
            db.AddInParameter(update_AlbumItem, "@albumDate", DbType.DateTime, selectedAlbumItem.albumDate);
            db.AddInParameter(update_AlbumItem, "@userIDFK", DbType.Int32, selectedAlbumItem.userIDFK);

            success = Convert.ToBoolean(update_AlbumItem.ExecuteNonQuery());

            return success;
        }
        public bool DeleteAlbumItem(albumItemModel deleteAlbumItem)
        {
            ///uses delete procedure to remove list and all its values by the primary ID
            DbCommand delete_AlbumItem = db.GetStoredProcCommand("sp_DeletelmAlbumItem");

            db.AddInParameter(delete_AlbumItem, "@albumItemID", DbType.Int32, deleteAlbumItem.albumItemID);
            
            db.AddOutParameter(delete_AlbumItem, "@success", DbType.Boolean, 1);

            bool success;
            try
            {
                db.ExecuteNonQuery(delete_AlbumItem);
                success = Convert.ToBoolean(db.GetParameterValue(delete_AlbumItem, "@success"));
            }
            catch
            {
                success = false;
            }

            return success;
        }
    }
}