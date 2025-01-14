using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication4.Classes
{
    public class LookUp
    {
        //Menu objMenu;
        protected string mMsgFlg;
        protected Database db;
        protected string mConnectionName;

       




        protected long mObjectUserID;

        public string MsgFlg
        {
            get { return mMsgFlg; }
            set { mMsgFlg = value; }
        }
        public Database Database
        {
            get { return db; }
        }

        

        public string ConnectionName
        {
            get { return mConnectionName; }
        }

        public LookUp(string ConnectionName, long ObjectUserID)
        {
            mObjectUserID = ObjectUserID;
            mConnectionName = ConnectionName;
            db = new DatabaseProviderFactory().Create(ConnectionName);
        }
        //public DataSet getTax(string Currency)
        //{
        //    string str = "SELECT ID,CONVERT(char(10), EffectiveDate,126)EffectiveDate,BandStart,BandEnd,BandRate,Cumulative,Currency FROM TaxTables where Currency = '"+ Currency + "' and EffectiveDate=(Select Max(EffectiveDate) from TaxTables where Currency = '"+ Currency +"') order by EffectiveDate Desc";
        //    return ReturnDs(str);
        //}
        public DataSet getTax(string Currency)
        {
            try
            {
                string sql = "sp_getTax";

                System.Data.Common.DbCommand cmd = db.GetStoredProcCommand(sql);
                db.AddInParameter(cmd, "@Currency", DbType.String, Currency);
                DataSet ds = db.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return ds;

                }
                else
                {
                    return null;
                }
            }

            catch (Exception ex)
            {
                mMsgFlg = ex.Message;
                return null;
            }
        }
        public DataSet GetBands(string Currency)

        {
            try
            {

                System.Data.Common.DbCommand cmd = db.GetStoredProcCommand("sp_TaxTables");
                db.AddInParameter(cmd, "@Currency", DbType.String, Currency);
                DataSet ds = db.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return ds;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                mMsgFlg = ex.Message;
                return null;
            }
        }

        public DataSet getCurrency()
        {
            try
            {
                string sql = "sp_getCurrency";

                System.Data.Common.DbCommand cmd = db.GetStoredProcCommand(sql);
                //db.AddInParameter(cmd, "@Currency", DbType.String, Currency);
                DataSet ds = db.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return ds;

                }
                else
                {
                    return null;
                }
            }

            catch (Exception ex)
            {
                mMsgFlg = ex.Message;
                return null;
            }
        }
        //public DataSet getCurrency()
        //{
        //    string str = "SELECT * from Currency";
        //    return ReturnDs(str);
        //}

        public void SaveRate(DateTime EffectiveDate, double Amount)
        {

            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            SqlConnection sqlCon = null;
            using (sqlCon = new SqlConnection(constr))
            {
                sqlCon.Open();
                SqlCommand sql_cmnd = new SqlCommand("sp_SaveRate", sqlCon);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@EffectiveDate", SqlDbType.Date).Value = EffectiveDate;
                sql_cmnd.Parameters.AddWithValue("@Amount", SqlDbType.Float).Value = Amount;
                sql_cmnd.ExecuteNonQuery();
                sqlCon.Close();

            }
        }
        public void SaveLimit(DateTime EffectiveDate, double Amount)
        {

            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            SqlConnection sqlCon = null;
            using (sqlCon = new SqlConnection(constr))
            {
                sqlCon.Open();
                SqlCommand sql_cmnd = new SqlCommand("sp_SaveNassaLimit", sqlCon);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@EffectiveDate", SqlDbType.Date).Value = EffectiveDate;
                sql_cmnd.Parameters.AddWithValue("@Amount", SqlDbType.Float).Value = Amount;
                sql_cmnd.ExecuteNonQuery();
                sqlCon.Close();

            }
        }
        public void SaveNecRate(DateTime EffectiveDate, double Amount)
        {

            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            SqlConnection sqlCon = null;
            using (sqlCon = new SqlConnection(constr))
            {
                sqlCon.Open();
                SqlCommand sql_cmnd = new SqlCommand("sp_SaveNecRate", sqlCon);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@EffectiveDate", SqlDbType.Date).Value = EffectiveDate;
                sql_cmnd.Parameters.AddWithValue("@Amount", SqlDbType.Float).Value = Amount;
                sql_cmnd.ExecuteNonQuery();
                sqlCon.Close();

            }
        }
        public void SaveTaxTables(string Currency, DateTime EffectiveDate,double BandRate,double StartRate,double EndRate, double Cummulative)
        {

            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            SqlConnection sqlCon = null;
            using (sqlCon = new SqlConnection(constr))
            {
                sqlCon.Open();
                SqlCommand sql_cmnd = new SqlCommand("sp_SaveTaxTables", sqlCon);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@Currency", SqlDbType.NVarChar).Value = Currency;
                sql_cmnd.Parameters.AddWithValue("@EffectiveDate", SqlDbType.Date).Value = EffectiveDate;
                sql_cmnd.Parameters.AddWithValue("@BandRate", SqlDbType.Float).Value = BandRate;
                sql_cmnd.Parameters.AddWithValue("@StartRate", SqlDbType.Float).Value = StartRate;
                sql_cmnd.Parameters.AddWithValue("@EndRate", SqlDbType.Float).Value = EndRate;
                sql_cmnd.Parameters.AddWithValue("@Cummulative", SqlDbType.Float).Value = Cummulative;
                sql_cmnd.ExecuteNonQuery();
                sqlCon.Close();

            }
        }
        public void SavePensionRate(DateTime EffectiveDate, double Amount)
        {

            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            SqlConnection sqlCon = null;
            using (sqlCon = new SqlConnection(constr))
            {
                sqlCon.Open();
                SqlCommand sql_cmnd = new SqlCommand("sp_SavePensionRate", sqlCon);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@EffectiveDate", SqlDbType.Date).Value = EffectiveDate;
                sql_cmnd.Parameters.AddWithValue("@Amount", SqlDbType.Float).Value = Amount;
                sql_cmnd.ExecuteNonQuery();
                sqlCon.Close();

            }
        }

        public void SaveAIDSLevy(DateTime EffectiveDate, double Amount)
        {

            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            SqlConnection sqlCon = null;
            using (sqlCon = new SqlConnection(constr))
            {
                sqlCon.Open();
                SqlCommand sql_cmnd = new SqlCommand("sp_SaveAidsLevy", sqlCon);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@EffectiveDate", SqlDbType.Date).Value = EffectiveDate;
                sql_cmnd.Parameters.AddWithValue("@Amount", SqlDbType.Float).Value = Amount;
                sql_cmnd.ExecuteNonQuery();
                sqlCon.Close();

            }
        }

        public void SaveNassaRate(DateTime EffectiveDate, double Amount)
        {

            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            SqlConnection sqlCon = null;
            using (sqlCon = new SqlConnection(constr))
            {
                sqlCon.Open();
                SqlCommand sql_cmnd = new SqlCommand("sp_SaveNassaRate", sqlCon);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@EffectiveDate", SqlDbType.Date).Value = EffectiveDate;
                sql_cmnd.Parameters.AddWithValue("@Amount", SqlDbType.Float).Value = Amount;
                sql_cmnd.ExecuteNonQuery();
                sqlCon.Close();

            }
        }
        public DataSet TaxTables(double CumSalary, string Currency, DateTime PayDate)
        {
            try
            {
                string sql = "TaxTables_BySalary";
                System.Data.Common.DbCommand cmd = db.GetStoredProcCommand(sql);

                db.AddInParameter(cmd, "@Amount", DbType.Double, CumSalary);
                db.AddInParameter(cmd, "@Currency", DbType.String, Currency);
                db.AddInParameter(cmd, "@PaydateDate", DbType.Date, PayDate);
                DataSet ds = db.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return ds;

                }
                else
                {
                    return null;
                }
            }

            catch (Exception ex)
            {
                mMsgFlg = ex.Message;
                return null;
            }
        }

        public DataSet getRate()
        {
            try
            {
                string sql = "sp_getRate";
                System.Data.Common.DbCommand cmd = db.GetStoredProcCommand(sql);

                //db.AddInParameter(cmd, "@Amount", DbType.Double, CumSalary);
                //db.AddInParameter(cmd, "@Currency", DbType.String, Currency);
                //db.AddInParameter(cmd, "@PaydateDate", DbType.Date, PayDate);
                DataSet ds = db.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return ds;

                }
                else
                {
                    return null;
                }
            }

            catch (Exception ex)
            {
                mMsgFlg = ex.Message;
                return null;
            }
        }
        public DataSet getNassaRate()
        {
            try
            {
                string sql = "sp_getNassaRate";
                System.Data.Common.DbCommand cmd = db.GetStoredProcCommand(sql);


                DataSet ds = db.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return ds;

                }
                else
                {
                    return null;
                }
            }

            catch (Exception ex)
            {
                mMsgFlg = ex.Message;
                return null;
            }
        }
        public DataSet getLevy()
        {
            try
            {
                string sql = "sp_getAidsLevy";
                System.Data.Common.DbCommand cmd = db.GetStoredProcCommand(sql);


                DataSet ds = db.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return ds;

                }
                else
                {
                    return null;
                }
            }

            catch (Exception ex)
            {
                mMsgFlg = ex.Message;
                return null;
            }
        }
        public DataSet getPensionRate()
        {
            try
            {
                string sql = "sp_getPensionRate";
                System.Data.Common.DbCommand cmd = db.GetStoredProcCommand(sql);


                DataSet ds = db.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return ds;

                }
                else
                {
                    return null;
                }
            }

            catch (Exception ex)
            {
                mMsgFlg = ex.Message;
                return null;
            }
        }
        public DataSet getNecRate()
        {
            try
            {
                string sql = "sp_getNecRate";
                System.Data.Common.DbCommand cmd = db.GetStoredProcCommand(sql);


                DataSet ds = db.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return ds;

                }
                else
                {
                    return null;
                }
            }

            catch (Exception ex)
            {
                mMsgFlg = ex.Message;
                return null;
            }
        }
        public DataSet getLimit()
        {
            try
            {
                string sql = "sp_getNassaLimit";
                System.Data.Common.DbCommand cmd = db.GetStoredProcCommand(sql);

               
                DataSet ds = db.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return ds;

                }
                else
                {
                    return null;
                }
            }

            catch (Exception ex)
            {
                mMsgFlg = ex.Message;
                return null;
            }
        }

        protected DataSet ReturnDs(string str)
        {
            try
            {
                DataSet ds = db.ExecuteDataSet(CommandType.Text, str);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return ds;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                mMsgFlg = ex.Message;
                return null;
            }
        }
    }
}