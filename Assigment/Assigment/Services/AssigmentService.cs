using Assigment.Utilities;
using NetCoreCRUDESQL.Constants;
using Serilog;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading.Tasks;

namespace Assigment.Services
{
    public class AssigmentService
    {
        Type myType = typeof(AssigmentService);

        public async Task<DataTable> ConversionUnit(string uName, int ConversionValue)
        {
            Log.Information(AppConstants.StartingLog, myType.Namespace, MethodBase.GetCurrentMethod().Name);
            DataHelper dh = new DataHelper();
            Log.Information(AppConstants.Service, myType.Namespace, MethodBase.GetCurrentMethod().Name);
            SqlCommand _cmd = new SqlCommand();
            try
            {
                _cmd = dh.BuildCommand("GetConvertionValueByType");
                _cmd.Parameters.Add("@ConvertionType", SqlDbType.VarChar, 50).Value = uName.Trim();
                _cmd.Parameters.Add("@Value", SqlDbType.Int).Value = ConversionValue;
                return await dh.ExecuteDataTable(_cmd);
            }
            catch (Exception ex)
            {
                Utility.RetainExceptionStackTrace(ex);
                Log.Error(ex.ToString());
                throw;
            }
            finally
            {
                _cmd.Dispose();
                _cmd.Connection.Close();
                Log.Information(AppConstants.EndingLog, Assembly.GetExecutingAssembly().EntryPoint.DeclaringType.Namespace, MethodBase.GetCurrentMethod().Name);
                Log.CloseAndFlush();

            }
        }
    }
}
