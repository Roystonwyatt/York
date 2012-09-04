using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace YorkNet
{
    class DBStuff
    {
        public const SqlDbType myInt = SqlDbType.Int;
        public const SqlDbType myVarChar = SqlDbType.VarChar;
        public const SqlDbType myDateTime = SqlDbType.DateTime;
        public const SqlDbType myFloat = SqlDbType.Float;
        public const SqlDbType myBit = SqlDbType.Bit;
        public const SqlDbType myBinary = SqlDbType.Binary;
        public const SqlDbType myDecimal = SqlDbType.Decimal;
        public const SqlDbType myText = SqlDbType.Text;

        public bool m_bExecSQLDBActionSPErr;
        public bool m_bFillDataTableFromSPErr;
        public string m_sExceptionMessage;
        public bool m_bQuerySystemSPErr;

        int CommandTimeout = 7200;

        public SqlConnection DBConn()
        {
            string Connection = ConfigurationManager.ConnectionStrings["YorkNetConnString"].ConnectionString;
            SqlConnection Conn = new SqlConnection(Connection);
            return Conn;
        }

        public Int32 OutputParameterSize(string ParameterName, string SpName)
        {
            //Queries a Db to get the size of the given Output Parameter for the given ParameterName 
            // and Stored Proc name, this query should only ever return 1 row and only 1 column.

            Int32 ParSize = 0;
            string QueryString;
            SqlDataReader drData;

            QueryString = "SELECT CAST(ISNULL(character_maximum_length, numeric_scale) AS INT) AS prec ";
            QueryString = QueryString + " FROM INFORMATION_SCHEMA.PARAMETERS ";
            QueryString = QueryString + " WHERE specific_name = '" + SpName + "' AND parameter_name = '" + ParameterName + "' ";
            QueryString = QueryString + " AND parameter_mode = 'INOUT'";

            drData = QuerySystemDB(QueryString);

            if (drData.HasRows == true)
            {
                while (drData.Read())
                {
                    ParSize = drData.GetInt32(0);
                }
                drData.Close();
            }

            return ParSize;
        }

        public SqlDataReader QuerySystemDB(string Query)
        {
            //Queries a database by string query and returns a datareader containing the results

            SqlConnection conn;
            SqlCommand cmd;
            SqlDataReader drData;

            conn = DBConn();
            conn.Open();

            cmd = new SqlCommand(Query, conn);
            drData = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            return drData;
        }

        public SqlDataReader QuerySystemSP(string SPName)
        {
            //Queries a database by StoredProcedure and returns a datareader containing the results

            SqlConnection conn;
            SqlCommand cmd;
            SqlDataReader drData;

            m_bQuerySystemSPErr = false;

            try
            {
                
                cmd = new SqlCommand();
                conn = DBConn();
                cmd.Connection = conn;
                cmd.CommandText = SPName;
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                drData = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                return drData;
            }
            catch(Exception)
            {
                m_bQuerySystemSPErr = true;
                return null;
            }
        }

        public SqlDataReader QuerySystemSP(ArrayList ParameterArrayList, ArrayList ValueArrayList, ArrayList TypeArrayList, string SPName)
        {
            SqlConnection conn;
            SqlCommand cmd;
            SqlParameter myPar;
            SqlDataReader drData;

            m_bQuerySystemSPErr = false;

            try
            {
                cmd = new SqlCommand();
                conn = DBConn();
                cmd.Connection = conn;
                cmd.CommandText = SPName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 7200;

                int i;
                SqlDbType ParType = new SqlDbType();

                for (i = 0; i < ParameterArrayList.Count; i++)
                {
                    switch(Convert.ToString(TypeArrayList[i]).ToLower())
                    {
                        case "myvarchar":
                            ParType = myVarChar;
                            break;
                        case "myint":
                            ParType = myInt;
                            break;
                        case "mydatetime":
                            ParType = myDateTime;
                            break;
                        case "myfloat":
                            ParType = myFloat;
                            break;
                        case "mybit":
                            ParType = myBit;
                            break;
                        case "mybinary":
                            ParType = myBinary;
                            break;
                        case "mydecimal":
                            ParType = myDecimal;
                            break;
                        case "mytext":
                            ParType = myText;
                            break;
                    }

                    myPar = cmd.CreateParameter();
                    myPar.ParameterName = Convert.ToString(ParameterArrayList[i]);
                    myPar.Direction = ParameterDirection.Input;
                    myPar.SqlDbType = ParType;
                    myPar.Value = ValueArrayList[i];
                    cmd.Parameters.Add(myPar);
                }

                conn.Open();
                drData = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return drData;
            }
            catch(Exception ex)
            {
                m_bQuerySystemSPErr = true;
                m_sExceptionMessage = ex.ToString();
                drData = null;
                return drData;
            }
        }

        public string ExecSQLDBAction(string Query)
        {
            //Executes an action on the database from a string query

            SqlConnection conn;
            SqlCommand cmd;
            string strReturn;

            m_bExecSQLDBActionSPErr = false;
            strReturn = "OK";

            try
            {
                if (Query.StartsWith("select") == false)
                {
                    conn = DBConn();
                    conn.Open();
                    cmd = new SqlCommand(Query, conn);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    conn.Close();
                    conn.Dispose();
                }
                else
                {
                    m_bExecSQLDBActionSPErr = true;
                    strReturn = "ExecuteNonQuery cannot start with 'SELECT'";
                }
            }
            catch (Exception ex)
            {
                m_bExecSQLDBActionSPErr = true;
                strReturn = ex.ToString();
            }

            return strReturn;
        }

        public string ExecSQLDBActionSP(ArrayList ParameterArrayList, ArrayList ValueArrayList, ArrayList TypeArrayList, string SPName)
        {
            //Executes an action on the database from a StoredProcedure with parameters

            SqlConnection conn;
            SqlCommand cmd;
            SqlParameter myPar;
            string strReturn;

            m_bExecSQLDBActionSPErr = false;
            strReturn = "OK";

            try
            {
                cmd = new SqlCommand();
                conn = DBConn();
                cmd.Connection = conn;
                cmd.CommandText = SPName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = CommandTimeout;

                int i;
                SqlDbType ParType = new SqlDbType();

                for (i = 0; i < ParameterArrayList.Count; i++)
                {
                    switch(Convert.ToString(TypeArrayList[i]).ToLower())
                    {
                        case "myvarchar":
                            ParType = myVarChar;
                            break;
                        case "myint":
                            ParType = myInt;
                            break;
                        case "mydatetime":
                            ParType = myDateTime;
                            break;
                        case "myfloat":
                            ParType = myFloat;
                            break;
                        case "mybit":
                            ParType = myBit;
                            break;
                        case "mybinary":
                            ParType = myBinary;
                            break;
                        case "mydecimal":
                            ParType = myDecimal;
                            break;
                        case "mytext":
                            ParType = myText;
                            break;
                    }

                    myPar = cmd.CreateParameter();
                    myPar.ParameterName = Convert.ToString(ParameterArrayList[i]);
                    myPar.Direction = ParameterDirection.Input;
                    myPar.SqlDbType = ParType;
                    myPar.Value = ValueArrayList[i];
                    cmd.Parameters.Add(myPar);
                }

                conn.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
            catch(Exception ex)
            {
                m_bExecSQLDBActionSPErr = true;
                strReturn = "INFOERR: Stored Procedure: " + SPName + " " + Convert.ToString(ex) + "\r\nSECTION: ExecSQLDBActionSP, DB.ExecSQLDBActionSP.";
                m_sExceptionMessage = strReturn;
            }

            return strReturn;
        }

        public DataTable FillDataTableFromSP(ArrayList ParameterArrayList, ArrayList ValueArrayList, ArrayList TypeArrayList, string SPName)
        {
            //Returns a DataTable from a SQL stored procedure

            SqlConnection conn;
            SqlCommand cmd;
            SqlParameter myPar;
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dtData = new DataTable();

            m_bFillDataTableFromSPErr = false;
            
            try
            {
                cmd = new SqlCommand();
                conn = DBConn();
                cmd.Connection = conn;
                cmd.CommandText = SPName;
                cmd.CommandType = CommandType.StoredProcedure;

                int i;
                SqlDbType ParType = new SqlDbType();

                for (i = 0; i < ParameterArrayList.Count; i++)
                {
                    switch(Convert.ToString(TypeArrayList[i]).ToLower())
                    {
                        case "myvarchar":
                            ParType = myVarChar;
                            break;
                        case "myint":
                            ParType = myInt;
                            break;
                        case "mydatetime":
                            ParType = myDateTime;
                            break;
                        case "myfloat":
                            ParType = myFloat;
                            break;
                        case "mybit":
                            ParType = myBit;
                            break;
                        case "mybinary":
                            ParType = myBinary;
                            break;
                        case "mydecimal":
                            ParType = myDecimal;
                            break;
                        case "mytext":
                            ParType = myText;
                            break;
                    }

                    myPar = cmd.CreateParameter();
                    myPar.ParameterName = Convert.ToString(ParameterArrayList[i]);
                    myPar.Direction = ParameterDirection.Input;
                    myPar.SqlDbType = ParType;
                    myPar.Value = ValueArrayList[i];
                    cmd.Parameters.Add(myPar);
                }

                conn.Open();
                da.SelectCommand = cmd;
                da.Fill(dtData);
                conn.Close();
            }

            catch(Exception ex)
            {
                m_bFillDataTableFromSPErr = true;
                m_sExceptionMessage = ex.ToString();
            }

            return dtData;
        }

        public ArrayList QuerySystemSP_Output(ArrayList ParameterArrayList, ArrayList ValueArrayList, ArrayList TypeArrayList, ArrayList OutputParameterArrayList, ArrayList OutputTypeArrayList, string SPName)
        {
            SqlConnection conn;
            SqlCommand cmd;
            SqlParameter myPar;
            SqlDataReader drData;
            SqlDbType ParType = SqlDbType.VarChar;
            ArrayList alData = new ArrayList();

            m_bQuerySystemSPErr = false;

            try
            {
                cmd = new SqlCommand();
                conn = DBConn();
                cmd.Connection = conn;
                cmd.CommandText = SPName;
                cmd.CommandType = CommandType.StoredProcedure;

                for (int i = 0; i <= ParameterArrayList.Count - 1; i++)
                {
                    switch (Convert.ToString(TypeArrayList[i]).ToLower())
                    {
                        case "myvarchar":
                            ParType = myVarChar;
                            break;
                        case "myint":
                            ParType = myInt;
                            break;
                        case "mydatetime":
                            ParType = myDateTime;
                            break;
                        case "myfloat":
                            ParType = myFloat;
                            break;
                        case "mybit":
                            ParType = myBit;
                            break;
                        case "mybinary":
                            ParType = myBinary;
                            break;
                        case "mydecimal":
                            ParType = myDecimal;
                            break;
                        case "mytext":
                            ParType = myText;
                            break;
                    }

                    myPar = cmd.CreateParameter();
                    myPar.ParameterName = Convert.ToString(ParameterArrayList[i]);
                    myPar.Direction = ParameterDirection.Input;
                    myPar.SqlDbType = ParType;
                    myPar.Value = ValueArrayList[i];
                    cmd.Parameters.Add(myPar);
                }

                for (int i = 0; i <= OutputParameterArrayList.Count - 1; i++)
                {
                    switch (Convert.ToString(OutputTypeArrayList[i]).ToLower())
                    {
                        case "myvarchar":
                            ParType = myVarChar;
                            break;
                        case "myint":
                            ParType = myInt;
                            break;
                        case "mydatetime":
                            ParType = myDateTime;
                            break;
                        case "myfloat":
                            ParType = myFloat;
                            break;
                        case "mybit":
                            ParType = myBit;
                            break;
                        case "mybinary":
                            ParType = myBinary;
                            break;
                        case "mydecimal":
                            ParType = myDecimal;
                            break;
                        case "mytext":
                            ParType = myText;
                            break;
                    }

                    myPar = cmd.CreateParameter();
                    myPar.ParameterName = (string)OutputParameterArrayList[i];
                    myPar.Direction = ParameterDirection.Output;
                    myPar.SqlDbType = ParType;
                    if (ParType == myVarChar)
                    {
                        myPar.Size = OutputParameterSize(myPar.ParameterName, SPName);
                    }
                    cmd.Parameters.Add(myPar);
                }

                conn.Open();
                drData = cmd.ExecuteReader(CommandBehavior.Default);
                for (int i = 0; i <= cmd.Parameters.Count - 1; i++)
                {
                    if (cmd.Parameters[i].Direction == ParameterDirection.Output)
                    {
                        alData.Add(cmd.Parameters[i].Value);
                    }
                }

                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
            catch (SqlException SqlEx)
            {
                m_bQuerySystemSPErr = true;
                m_sExceptionMessage = SqlEx.ToString();
            }
            catch (FormatException FormEx)
            {
                m_bQuerySystemSPErr = true;
                m_sExceptionMessage = FormEx.ToString();
            }
            catch (Exception ex)
            {
                m_bQuerySystemSPErr = true;
                m_sExceptionMessage = ex.ToString();
            }

            return alData;
        }

        public string InsertIntoSQLTableFromDataTable(string TableName, DataTable RawTable, string DataBase, string Server, string UserName, string Password)
        {
            //Takes the given data from a Memory resident datatable and inserts it into the given table.
            //Used specifically for pumping data from Raw input and putput files into Database
            SqlConnection conn;
            SqlCommand cmd;
            SqlDataAdapter adapt = new SqlDataAdapter();
            DataTable dtData = new DataTable();
            string strSelectList = "";

            for (int i = 0; i < RawTable.Columns.Count; i++)
            {
                if (i == 0)
                {
                    strSelectList = strSelectList + RawTable.Columns[i].ColumnName.ToString();
                }
                else
                {
                    strSelectList = strSelectList + "," + RawTable.Columns[i].ColumnName.ToString();
                }
            }

            bool m_bQuerySystemSPErr = false;
            string m_sExceptionMessage = null;

            try
            {
                cmd = new SqlCommand();
                conn = DBConn();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT " + strSelectList + " FROM " + TableName.ToString() + " WHERE 1 = 2";
                cmd.CommandType = CommandType.Text;
                SqlCommandBuilder builder = new SqlCommandBuilder(adapt);
                builder.QuotePrefix = "[";
                builder.QuoteSuffix = "]";

                conn.Open();
                adapt.SelectCommand = cmd;
                adapt.Fill(dtData);
                adapt.Update(RawTable);
                conn.Close();
                RawTable.Dispose();
                return "OK";
            }
            catch (Exception ex)
            {
                m_bQuerySystemSPErr = true;
                m_sExceptionMessage = ex.ToString();
                return null;
            }
        }
 

    }
}
