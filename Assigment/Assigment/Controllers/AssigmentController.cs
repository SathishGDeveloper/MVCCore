using Assigment.Models;
using Assigment.Repositories;
using Assigment.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NetCoreCRUDESQL.Constants;
using Serilog;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Assigment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssigmentController : ControllerBase
    {
        Type myType = typeof(AssigmentController);
        private readonly IConfiguration _config;
        private readonly IAssigmentRepository _crudeRepository;


        public AssigmentController(IAssigmentRepository crudeRepository, IConfiguration config)
        {
            _config = config;
            _crudeRepository = crudeRepository;
        }

        [HttpGet("ConversionUnit/{ConversionType}/{ConversionValue}")]
        public async Task<IActionResult> ConversionUnit(string ConversionType, int ConversionValue)
        {
            ExceptionLogger exceptionLogger = new ExceptionLogger("Loging Start");
            ResponseModel resp = new ResponseModel();
            try
            {
                Log.Information(AppConstants.StartingLog, myType.Namespace, MethodBase.GetCurrentMethod().Name);
                Log.Information(AppConstants.Repositories, myType.Namespace, MethodBase.GetCurrentMethod().Name);
                if (!string.IsNullOrEmpty(ConversionType) && Convert.ToInt32(ConversionValue)!= 0)
                {
                    resp = await _crudeRepository.ConversionUnit(ConversionType, ConversionValue);
                }
                else
                {
                    resp.userMsg = "Unauthorize user";
                    resp.sysMsg = "Unauthorize";
                    resp.statusCode = StatusCodes.Status401Unauthorized;
                }
            }
            catch (Exception ex)
            {
                resp.sysMsg = ex.Message;
                resp.statusCode = StatusCodes.Status400BadRequest;
                Log.Error(ex.ToString());
            }
            return Ok(resp);
        }
    }
}