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
    /// 医院环境版本表Controller
    /// 
    /// @tableName hospital_env
    /// @author admin
    /// @date 2026-03-24
    /// </summary>
    [Verify]
    [Route("business/HospitalEnv")]
    public class HospitalEnvController : BaseController
    {
        /// <summary>
        /// 医院环境版本表接口
        /// </summary>
        private readonly IHospitalEnvService _HospitalEnvService;

        public HospitalEnvController(IHospitalEnvService HospitalEnvService)
        {
            _HospitalEnvService = HospitalEnvService;
        }

        /// <summary>
        /// 查询医院环境版本表列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "business:hospitalenv:list")]
        public IActionResult QueryHospitalEnv([FromQuery] HospitalEnvQueryDto parm)
        {
            var response = _HospitalEnvService.GetList(parm);
            return SUCCESS(response);
        }


        /// <summary>
        /// 查询医院环境版本表详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        [ActionPermissionFilter(Permission = "business:hospitalenv:query")]
        public IActionResult GetHospitalEnv(long Id)
        {
            var response = _HospitalEnvService.GetFirst(x => x.Id == Id);
            
            return SUCCESS(response);
        }

        /// <summary>
        /// 添加医院环境版本表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "business:hospitalenv:add")]
        [Log(Title = "医院环境版本表", BusinessType = BusinessType.INSERT)]
        public IActionResult AddHospitalEnv([FromBody] HospitalEnvDto parm)
        {
            if (parm == null)
            {
                throw new CustomException("请求参数错误");
            }
            //从 Dto 映射到 实体
            var modal = parm.Adapt<HospitalEnv>().ToCreate(HttpContext);

            var response = _HospitalEnvService.AddHospitalEnv(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 更新医院环境版本表
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "business:hospitalenv:edit")]
        [Log(Title = "医院环境版本表", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateHospitalEnv([FromBody] HospitalEnvDto parm)
        {
            if (parm == null)
            {
                throw new CustomException("请求实体不能为空");
            }
            //从 Dto 映射到 实体
            var modal = parm.Adapt<HospitalEnv>().ToUpdate(HttpContext);

            var response = _HospitalEnvService.Update(w => w.Id == modal.Id, it => new HospitalEnv()
            {
                //Update 字段映射
                HospitalName = modal.HospitalName,
                HospitalCode = modal.HospitalCode,
                ProdVersion = modal.ProdVersion,
                TestVersion = modal.TestVersion,
                Status = modal.Status,
                Remark = modal.Remark,
                UpdateBy = modal.UpdateBy,
                UpdateTime = modal.UpdateTime,
                DelFlag = modal.DelFlag,
            });

            return ToResponse(response);
        }

        /// <summary>
        /// 删除医院环境版本表
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{ids}")]
        [ActionPermissionFilter(Permission = "business:hospitalenv:delete")]
        [Log(Title = "医院环境版本表", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteHospitalEnv(string ids)
        {
            int[] idsArr = Tools.SpitIntArrary(ids);
            if (idsArr.Length <= 0) { return ToResponse(ApiResult.Error($"删除失败Id 不能为空")); }

            var response = _HospitalEnvService.Delete(idsArr);

            return ToResponse(response);
        }


    }
}