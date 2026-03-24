using System;
using System.Collections.Generic;
using SqlSugar;
using OfficeOpenXml.Attributes;

namespace ZR.Model.Models
{
    /// <summary>
    /// 医院环境版本表，数据实体对象
    ///
    /// @author admin
    /// @date 2026-03-24
    /// </summary>
    [SugarTable("hospital_env")]
    public class HospitalEnv
    {
        /// <summary>
        /// 描述 :主键ID 
        /// 空值 : false  
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long Id { get; set; }

        /// <summary>
        /// 描述 :医院名称 
        /// 空值 : false  
        /// </summary>
        [SugarColumn(ColumnName = "hospital_name")]
        public string HospitalName { get; set; }

        /// <summary>
        /// 描述 :医院编码 
        /// 空值 : false  
        /// </summary>
        [SugarColumn(ColumnName = "hospital_code")]
        public string HospitalCode { get; set; }

        /// <summary>
        /// 描述 :正式环境版本 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "prod_version")]
        public string ProdVersion { get; set; }

        /// <summary>
        /// 描述 :测试环境版本 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "test_version")]
        public string TestVersion { get; set; }

        /// <summary>
        /// 描述 :状态 0禁用 1启用 
        /// 空值 : true  
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 描述 :备注 
        /// 空值 : true  
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 描述 :创建者 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "create_by")]
        public string CreateBy { get; set; }

        /// <summary>
        /// 描述 :创建时间 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "create_time")]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 描述 :更新者 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "update_by")]
        public string UpdateBy { get; set; }

        /// <summary>
        /// 描述 :更新时间 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "update_time")]
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 描述 :删除标志 0存在 1删除 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "del_flag")]
        public string DelFlag { get; set; }



    }
}