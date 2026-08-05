using Infrastructure;
using Infrastructure.Attribute;
using Infrastructure.Enums;
using Infrastructure.Model;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using ZR.Model.Dto;
using ZR.Model.Models;
using ZR.Service.Business.IBusinessService;
using ZR.Admin.WebApi.Extensions;
using ZR.Admin.WebApi.Filters;
using ZR.Common;

namespace ZR.Admin.WebApi.Controllers
{
    /// <summary>
    /// 钉钉日志记录Controller
    /// 
    /// @tableName project_time_entry_log
    /// @author admin
    /// @date 2026-07-29
    /// </summary>
    [Verify]
    [Route("business/ProjectTimeEntryLog")]
    public class ProjectTimeEntryLogController : BaseController
    {
        /// <summary>
        /// 钉钉日志记录接口
        /// </summary>
        private readonly IProjectTimeEntryLogService _ProjectTimeEntryLogService;

        public ProjectTimeEntryLogController(IProjectTimeEntryLogService ProjectTimeEntryLogService)
        {
            _ProjectTimeEntryLogService = ProjectTimeEntryLogService;
        }

        /// <summary>
        /// 查询钉钉日志记录列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "business:projecttimeentrylog:list")]
        public IActionResult QueryProjectTimeEntryLog([FromQuery] ProjectTimeEntryLogQueryDto parm)
        {
            var response = _ProjectTimeEntryLogService.GetList(parm);
            return SUCCESS(response);
        }


        /// <summary>
        /// 查询钉钉日志记录详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        [ActionPermissionFilter(Permission = "business:projecttimeentrylog:query")]
        public IActionResult GetProjectTimeEntryLog(int Id)
        {
            var response = _ProjectTimeEntryLogService.GetFirst(x => x.Id == Id);
            
            return SUCCESS(response);
        }




        /// <summary>
        /// 导出钉钉日志记录
        /// </summary>
        /// <returns></returns>
        [Log(Title = "钉钉日志记录", BusinessType = BusinessType.EXPORT, IsSaveResponseData = false)]
        [HttpGet("export")]
        [ActionPermissionFilter(Permission = "business:projecttimeentrylog:export")]
        public IActionResult Export([FromQuery] ProjectTimeEntryLogQueryDto parm)
        {
            parm.PageSize = 10000;
            var list = _ProjectTimeEntryLogService.GetList(parm).Result;

            string sFileName = ExportExcel(list, "ProjectTimeEntryLog", "钉钉日志记录");
            return SUCCESS(new { path = "/export/" + sFileName, fileName = sFileName });
        }

    }
}