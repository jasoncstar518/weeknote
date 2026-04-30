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
    /// 项目配置表Controller
    /// 
    /// @tableName selproject
    /// @author admin
    /// @date 2026-04-30
    /// </summary>
    [Verify]
    [Route("business/Selproject")]
    public class SelprojectController : BaseController
    {
        /// <summary>
        /// 项目配置表接口
        /// </summary>
        private readonly ISelprojectService _SelprojectService;

        public SelprojectController(ISelprojectService SelprojectService)
        {
            _SelprojectService = SelprojectService;
        }

        /// <summary>
        /// 查询项目配置表列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "business:selproject:list")]
        public IActionResult QuerySelproject([FromQuery] SelprojectQueryDto parm)
        {
            var response = _SelprojectService.GetList(parm);
            return SUCCESS(response);
        }


        /// <summary>
        /// 查询项目配置表详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        [ActionPermissionFilter(Permission = "business:selproject:query")]
        public IActionResult GetSelproject(long Id)
        {
            var response = _SelprojectService.GetFirst(x => x.Id == Id);
            
            return SUCCESS(response);
        }

        /// <summary>
        /// 添加项目配置表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "business:selproject:add")]
        [Log(Title = "项目配置表", BusinessType = BusinessType.INSERT)]
        public IActionResult AddSelproject([FromBody] SelprojectDto parm)
        {
            if (parm == null)
            {
                throw new CustomException("请求参数错误");
            }
            //从 Dto 映射到 实体
            var modal = parm.Adapt<Selproject>().ToCreate(HttpContext);

            var response = _SelprojectService.AddSelproject(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 更新项目配置表
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "business:selproject:edit")]
        [Log(Title = "项目配置表", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateSelproject([FromBody] SelprojectDto parm)
        {
            if (parm == null)
            {
                throw new CustomException("请求实体不能为空");
            }
            //从 Dto 映射到 实体
            var modal = parm.Adapt<Selproject>().ToUpdate(HttpContext);

            var response = _SelprojectService.Update(w => w.Id == modal.Id, it => new Selproject()
            {
                //Update 字段映射
                Code = modal.Code,
                Name = modal.Name,
                HospitalName = modal.HospitalName,
                Remarks = modal.Remarks,
                CreatedBy = modal.CreatedBy,
                UpdatedBy = modal.UpdatedBy,
                CreationTime = modal.CreationTime,
                UpdateTime = modal.UpdateTime,
                IsDeleted = modal.IsDeleted,
            });

            return ToResponse(response);
        }

        /// <summary>
        /// 删除项目配置表
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{ids}")]
        [ActionPermissionFilter(Permission = "business:selproject:delete")]
        [Log(Title = "项目配置表", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteSelproject(string ids)
        {
            int[] idsArr = Tools.SpitIntArrary(ids);
            if (idsArr.Length <= 0) { return ToResponse(ApiResult.Error($"删除失败Id 不能为空")); }

            var response = _SelprojectService.Delete(idsArr);

            return ToResponse(response);
        }


    }
}