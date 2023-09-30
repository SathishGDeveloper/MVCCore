using System;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Assigment.Models;
using Assigment.Services;
using NetCoreCRUDESQL.Constants;
using Serilog;

namespace Assigment.Repositories
{
    public class AssigmentRepository : IAssigmentRepository
    {
        Type myType = typeof(AssigmentRepository);
        private readonly AssigmentService _AssigmentService;

        public AssigmentRepository(AssigmentService AssigmentServices)
        {
            _AssigmentService = AssigmentServices;
        }

        public async Task<ResponseModel> ConversionUnit(string uName, int ConversionValue)
        {
            Log.Information(AppConstants.StartingLog, myType.Namespace, MethodBase.GetCurrentMethod().Name);
            ResponseModel resp = new ResponseModel();
            try
            {
                DataTable dtResp = await _AssigmentService.ConversionUnit(uName,  ConversionValue);
                if (dtResp == null || (dtResp != null && dtResp.Rows.Count == 0))
                {
                    resp.userMsg = "Data not match";
                    resp.statusCode = StatusCodes.Status404NotFound;
                }
                else
                {
                    resp.data = dtResp;
                    resp.userMsg = "Data Found";
                }
            }
            catch (Exception ex)
            {
                resp.sysMsg = ex.Message;
                resp.statusCode = StatusCodes.Status400BadRequest;
                Log.Error(ex.ToString());
            }
            Log.Information(AppConstants.EndingLog, Assembly.GetExecutingAssembly().EntryPoint.DeclaringType.Namespace, MethodBase.GetCurrentMethod().Name);
            Log.CloseAndFlush();
            return resp;
        }
    }
}
