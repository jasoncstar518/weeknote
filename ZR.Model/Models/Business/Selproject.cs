using System;
using System.Collections.Generic;
using SqlSugar;
using OfficeOpenXml.Attributes;

namespace ZR.Model.Models
{
    /// <summary>
    /// 项目配置表，数据实体对象
    ///
    /// @author admin
    /// @date 2026-04-30
    /// </summary>
    [SugarTable("selproject")]
    public class Selproject
    {
        /// <summary>
        /// 描述 :自增主键 
        /// 空值 : false  
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long Id { get; set; }

        /// <summary>
        /// 描述 :项目编码 
        /// 空值 : true  
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 描述 :项目名称 
        /// 空值 : true  
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述 :所属医院 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "hospital_name")]
        public string HospitalName { get; set; }

        /// <summary>
        /// 描述 :备注 
        /// 空值 : true  
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 描述 :创建用户ID 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "created_by")]
        public long? CreatedBy { get; set; }

        /// <summary>
        /// 描述 :修改人用户ID 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "updated_by")]
        public long? UpdatedBy { get; set; }

        /// <summary>
        /// 描述 :创建时间 
        /// 空值 : false  
        /// </summary>
        [SugarColumn(ColumnName = "creation_time")]
        public DateTime? CreationTime { get; set; }

        /// <summary>
        /// 描述 :修改时间 
        /// 空值 : false  
        /// </summary>
        [SugarColumn(ColumnName = "update_time")]
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 描述 :是否删除(0否1是) 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "is_deleted")]
        public byte IsDeleted { get; set; }



    }
}