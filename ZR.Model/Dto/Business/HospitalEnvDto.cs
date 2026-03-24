using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ZR.Model.Dto;
using ZR.Model.Models;

namespace ZR.Model.Dto
{
    /// <summary>
    /// 医院环境版本表输入对象
    /// </summary>
    public class HospitalEnvDto
    {
        [Required(ErrorMessage = "主键ID不能为空")]
        public long Id { get; set; }
        [Required(ErrorMessage = "医院名称不能为空")]
        public string HospitalName { get; set; }
        [Required(ErrorMessage = "医院编码不能为空")]
        public string HospitalCode { get; set; }
        public string ProdVersion { get; set; }
        public string TestVersion { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string DelFlag { get; set; }
    }

    /// <summary>
    /// 医院环境版本表查询对象
    /// </summary>
    public class HospitalEnvQueryDto : PagerInfo 
    {
    }
}
