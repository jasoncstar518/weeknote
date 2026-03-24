using Infrastructure;
using Infrastructure.Attribute;
using Infrastructure.Enums;
using Infrastructure.Model;
using JinianNet.JNTemplate;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System;
using ZR.Admin.WebApi.Extensions;
using ZR.Admin.WebApi.Filters;
using ZR.Admin.WebApi.Framework;
using ZR.Common;
using ZR.Model.Dto;
using ZR.Model.Models;
using ZR.Model.System;
using ZR.Service.Business.IBusinessService;

namespace ZR.Admin.WebApi.Controllers
{
    /// <summary>
    /// 日报记录表Controller
    /// 
    /// @tableName dailyreport
    /// @author admin
    /// @date 2025-11-10
    /// </summary>
    [Verify]
    [Route("business/Dailyreport")]
    public class DailyreportController : BaseController
    {
        /// <summary>
        /// 日报记录表接口
        /// </summary>
        private readonly IDailyreportService _DailyreportService;

        public DailyreportController(IDailyreportService DailyreportService)
        {
            _DailyreportService = DailyreportService;
        }

        /// <summary>
        /// 查询日报记录表列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "business:dailyreport:list")]
        public IActionResult QueryDailyreport([FromQuery] DailyreportQueryDto parm)
        {
            long userId = HttpContext.GetUId();
            parm.Userid = userId;
            parm.StartTime = DateTimeHelper.GetBeginTime(parm.StartTime, -1);
            parm.EndTime = DateTimeHelper.GetBeginTime(parm.EndTime, 1);
            parm.Sort = "Reportdate";
            var response = _DailyreportService.GetList(parm);
            return SUCCESS(response);
        }


        /// <summary>
        /// 查询日报记录表详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        [ActionPermissionFilter(Permission = "business:dailyreport:query")]
        public IActionResult GetDailyreport(long Id)
        {
            var response = _DailyreportService.GetFirst(x => x.Id == Id);
            
            return SUCCESS(response);
        }

        /// <summary>
        /// 添加日报记录表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "business:dailyreport:add")]
        [Log(Title = "日报记录表", BusinessType = BusinessType.INSERT)]
        public IActionResult AddDailyreport([FromBody] DailyreportDto parm)
        {
            if (parm == null)
            {
                throw new CustomException("请求参数错误");
            }
            long userId = HttpContext.GetUId();
            parm.Userid = userId;
            parm.Createtime = DateTimeHelper.GetBeginTime(DateTime.Now);
            //从 Dto 映射到 实体
            var modal = parm.Adapt<Dailyreport>().ToCreate(HttpContext);
           
            var response = _DailyreportService.AddDailyreport(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 更新日报记录表
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "business:dailyreport:edit")]
        [Log(Title = "日报记录表", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateDailyreport([FromBody] DailyreportDto parm)
        {
            if (parm == null)
            {
                throw new CustomException("请求实体不能为空");
            }
            parm.Createtime = DateTimeHelper.GetBeginTime(DateTime.Now);
            //从 Dto 映射到 实体
            var modal = parm.Adapt<Dailyreport>().ToUpdate(HttpContext);

            var response = _DailyreportService.Update(w => w.Id == modal.Id, it => new Dailyreport()
            {
                //Update 字段映射
                Createtime = modal.Createtime,
                Isdeleted = modal.Isdeleted,
                Reportdate = modal.Reportdate,
                Projectid = modal.Projectid,
                Hospitalname = modal.Hospitalname,
                Projectname = modal.Projectname,
                Worklocation = modal.Worklocation,
                Intransithours = modal.Intransithours,
                Actualhours = modal.Actualhours,
                WorkSummary = modal.WorkSummary,
                Remarks = modal.Remarks,
            });

            return ToResponse(response);
        }

        /// <summary>
        /// 删除日报记录表
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{ids}")]
        [ActionPermissionFilter(Permission = "business:dailyreport:delete")]
        [Log(Title = "日报记录表", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteDailyreport(string ids)
        {
            int[] idsArr = Tools.SpitIntArrary(ids);
            if (idsArr.Length <= 0) { return ToResponse(ApiResult.Error($"删除失败Id 不能为空")); }

            var response = _DailyreportService.Delete(idsArr);

            return ToResponse(response);
        }


    }
}