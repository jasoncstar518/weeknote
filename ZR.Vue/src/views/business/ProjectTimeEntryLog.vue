<!--
 * @Descripttion: (钉钉日志记录/project_time_entry_log)
 * @version: (1.0)
 * @Author: (admin)
 * @Date: (2026-07-29)
 * @LastEditors: (admin)
 * @LastEditTime: (2026-07-29)
-->
<template>
  <div class="app-container">
    <!-- :model属性用于表单验证使用 比如下面的el-form-item 的 prop属性用于对表单值进行验证操作 -->
    <el-form :model="queryParams" size="small" label-position="right" inline ref="queryForm" :label-width="labelWidth" v-show="showSearch" 
      @submit.native.prevent>
          				    
      <el-form-item label="创建人姓名" prop="creator">
        <el-input v-model="queryParams.creator" placeholder="请输入创建人姓名" />
      </el-form-item>
    				    
      <el-form-item label="创建人所属部门" prop="creatorDepartment">
        <el-input v-model="queryParams.creatorDepartment" placeholder="请输入创建人所属部门" />
      </el-form-item>
    				    
      <el-form-item label="填报日期">
        <el-date-picker v-model="dateRangeEntryDate" style="width: 240px" value-format="yyyy-MM-dd" type="daterange" range-separator="-"
          start-placeholder="开始日期" end-placeholder="结束日期" placeholder="请选择填报日期" :picker-options="{ firstDayOfWeek: 1}"></el-date-picker>
      </el-form-item>
    				    
      <el-form-item label="项目唯一编号" prop="projectCode">
        <el-input v-model="queryParams.projectCode" placeholder="请输入项目唯一编号" />
      </el-form-item>
    				    
      <el-form-item label="医院完整名称" prop="hospitalFullName">
        <el-input v-model="queryParams.hospitalFullName" placeholder="请输入医院完整名称" />
      </el-form-item>
    				    
      <el-form-item label="项目名称" prop="projectName">
        <el-input v-model="queryParams.projectName" placeholder="请输入项目名称" />
      </el-form-item>
    				    
      <el-form-item label="工作内容简要描述" prop="workSummary">
        <el-input v-model="queryParams.workSummary" placeholder="请输入工作内容简要描述" />
      </el-form-item>

      <el-form-item>
        <el-button type="primary" icon="el-icon-search" size="mini" @click="handleQuery">搜索</el-button>
        <el-button icon="el-icon-refresh" size="mini" @click="resetQuery">重置</el-button>
      </el-form-item>
    </el-form>
    <!-- 工具区域 -->
    <el-row :gutter="10" class="mb8">
      <el-col :span="1.5">
        <el-button type="warning" plain icon="el-icon-download" size="mini" @click="handleExport" v-hasPermi="['business:projecttimeentrylog:export']">导出</el-button>
      </el-col>
      <right-toolbar :showSearch.sync="showSearch" @queryTable="getList"></right-toolbar>
    </el-row>

    <!-- 数据区域 -->
    <el-table :data="dataList" v-loading="loading" ref="table" border highlight-current-row show-summary :summary-method="getSummaries" @selection-change="handleSelectionChange">
      <el-table-column type="selection" width="50" align="center"/>
      <el-table-column prop="creator" label="创建人姓名" align="center" :show-overflow-tooltip="true" />
      <el-table-column prop="creatorDepartment" label="创建人所属部门" align="center" :show-overflow-tooltip="true" />
      <el-table-column prop="entryDate" label="填报日期" align="center" :show-overflow-tooltip="true" />
      <el-table-column prop="projectCode" label="项目唯一编号" align="center" :show-overflow-tooltip="true" />
      <el-table-column prop="hospitalFullName" label="医院完整名称" align="center" :show-overflow-tooltip="true" />
      <el-table-column prop="projectName" label="项目名称" align="center" :show-overflow-tooltip="true" />
      <el-table-column prop="workLocation" label="工作地点" align="center" :show-overflow-tooltip="true" />
      <el-table-column prop="transitHours" label="在途工时（小时）" align="center" />
      <el-table-column prop="actualHours" label="实际工时（小时）" align="center" />
      <el-table-column prop="workSummary" label="工作内容简要描述" align="center" :show-overflow-tooltip="true" />
      <el-table-column prop="remarks" label="备注信息" align="center" :show-overflow-tooltip="true" />
      <el-table-column prop="logType" label="日志类型（如：正常/加班/调休等）" align="center">
        <template slot-scope="scope">
          <dict-tag :options="logTypeOptions" :value="scope.row.logType" />
        </template>
      </el-table-column>
      <el-table-column prop="importTime" label="数据导入时间" align="center" :show-overflow-tooltip="true" />

      <el-table-column label="操作" align="center" width="140">
        <template slot-scope="scope">
          <el-button size="mini" type="success" icon="el-icon-view" title="查看" 
            @click="handleView(scope.row)"></el-button>      
        </template>
      </el-table-column>
    </el-table>
    <pagination class="mt10" background :total="total" :page.sync="queryParams.pageNum" :limit.sync="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改钉钉日志记录对话框 -->
    <el-dialog :lock-scroll="false" :visible.sync="open" >
      <el-form ref="form" :model="form" label-position="left" :label-width="formLabelWidth">
        <el-form-item label="主键ID">
          {{form.id}}
        </el-form-item>
        <el-form-item label="创建人姓名">
          {{form.creator}}
        </el-form-item>
        <el-form-item label="创建人所属部门">
          {{form.creatorDepartment}}
        </el-form-item>
        <el-form-item label="填报日期">
          {{form.entryDate}}
        </el-form-item>
        <el-form-item label="项目唯一编号">
          {{form.projectCode}}
        </el-form-item>
        <el-form-item label="医院完整名称">
          {{form.hospitalFullName}}
        </el-form-item>
        <el-form-item label="项目名称">
          {{form.projectName}}
        </el-form-item>
        <el-form-item label="工作地点">
          {{form.workLocation}}
        </el-form-item>
        <el-form-item label="在途工时（小时）">
          {{form.transitHours}}
        </el-form-item>
        <el-form-item label="实际工时（小时）">
          {{form.actualHours}}
        </el-form-item>
        <el-form-item label="工作内容简要描述">
          {{form.workSummary}}
        </el-form-item>
        <el-form-item label="备注信息">
          {{form.remarks}}
        </el-form-item>
        <el-form-item label="日志类型（如：正常/加班/调休等）">
          <dict-tag :options="logTypeOptions" :value="form.logType" />
        </el-form-item>
        <el-form-item label="数据导入时间">
          {{form.importTime}}
        </el-form-item>
        <el-form-item label="创建者">
          {{form.createBy}}
        </el-form-item>
        <el-form-item label="创建时间">
          {{form.createTime}}
        </el-form-item>
        <el-form-item label="更新者">
          {{form.updateBy}}
        </el-form-item>
        <el-form-item label="更新时间">
          {{form.updateTime}}
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button type="text" @click="cancel">取 消</el-button>
      </div>
    </el-dialog>
  </div>
</template>
<script>
import { 
  listProjectTimeEntryLog,
  getProjectTimeEntryLog,
  exportProjectTimeEntryLog,
} from '@/api/business/projectTimeEntryLog.js';

export default {
  name: "projecttimeentrylog",
  data() {
    return {
      labelWidth: "100px",
      formLabelWidth:"100px",
      // 选中id数组
      ids: [],
      // 非单个禁用
      single: true,
      // 非多个禁用
      multiple: true,
      // 遮罩层
      loading: false,
      // 显示搜索条件
      showSearch: true,
      // 查询参数
      queryParams: {
        pageNum: 1,
        pageSize: 10,
        sort: undefined,
        sortType: undefined,
        creator: undefined,
        creatorDepartment: undefined,
        entryDate: undefined,
        projectCode: undefined,
        hospitalFullName: undefined,
        projectName: undefined,
        workSummary: undefined,
      },
      // 弹出层标题
      title: "",
      // 是否显示弹出层
      open: false,
      // 表单参数
      form: {},
      columns: [
        { index: 0, key: 'id', label: `主键ID`, checked:  true  },
        { index: 1, key: 'creator', label: `创建人姓名`, checked:  true  },
        { index: 2, key: 'creatorDepartment', label: `创建人所属部门`, checked:  true  },
        { index: 3, key: 'entryDate', label: `填报日期`, checked:  true  },
        { index: 4, key: 'projectCode', label: `项目唯一编号`, checked:  true  },
        { index: 5, key: 'hospitalFullName', label: `医院完整名称`, checked:  true  },
        { index: 6, key: 'projectName', label: `项目名称`, checked:  true  },
        { index: 7, key: 'workLocation', label: `工作地点`, checked:  true  },
        { index: 8, key: 'transitHours', label: `在途工时（小时）`, checked:  true  },
        { index: 9, key: 'actualHours', label: `实际工时（小时）`, checked:  false  },
        { index: 10, key: 'workSummary', label: `工作内容简要描述`, checked:  false  },
        { index: 11, key: 'remarks', label: `备注信息`, checked:  false  },
        { index: 12, key: 'logType', label: `日志类型（如：正常/加班/调休等）`, checked:  false  },
        { index: 13, key: 'importTime', label: `数据导入时间`, checked:  false  },
        { index: 14, key: 'createBy', label: `创建者`, checked:  false  },
        { index: 15, key: 'createTime', label: `创建时间`, checked:  false  },
        { index: 16, key: 'updateBy', label: `更新者`, checked:  false  },
        { index: 17, key: 'updateTime', label: `更新时间`, checked:  false  },
      ],
      //填报日期时间范围
      dateRangeEntryDate: [],
      // 日志类型（如：正常/加班/调休等）选项列表
      logTypeOptions: [],
      // 数据列表
      dataList: [],
      // 总记录数
      total: 0,
      // 提交按钮是否显示
      btnSubmitVisible: true,
      // 合计数据
      summaries: {
        transitHoursSum: 0,
        actualHoursSum: 0
      }
    };
  },
  created() {
    // 列表数据查询
    this.getList();

    var dictParams = [
    ];
  },
  methods: {
    // 查询数据
    getList() {
      this.addDateRange(this.queryParams, this.dateRangeEntryDate, 'EntryDate');
      this.loading = true;
      listProjectTimeEntryLog(this.queryParams).then(res => {
         if (res.code == 200) {
           this.dataList = res.data.result;
           this.total = res.data.totalNum;
           // 从后端获取合计数据
           this.summaries.transitHoursSum = res.data.extra?.transitHoursSum || 0;
           this.summaries.actualHoursSum = res.data.extra?.actualHoursSum || 0;
           this.loading = false;
         }
       })
    },
    // 取消按钮
    cancel() {
      this.open = false;
      this.reset();
    },
    // 重置数据表单
    reset() {
      this.form = {
        creator: undefined,
        creatorDepartment: undefined,
        entryDate: undefined,
        projectCode: undefined,
        hospitalFullName: undefined,
        projectName: undefined,
        workLocation: undefined,
        transitHours: undefined,
        actualHours: undefined,
        workSummary: undefined,
        remarks: undefined,
        logType: undefined,
        importTime: undefined,
        createBy: undefined,
        createTime: undefined,
        updateBy: undefined,
        updateTime: undefined,
      };
      this.resetForm("form");
    },
    // 重置查询操作
    resetQuery() {
      this.timeRange = [];
      //填报日期时间范围
      this.dateRangeEntryDate= [];
      this.resetForm("queryForm");
      this.handleQuery();
    },
    // 多选框选中数据
    handleSelectionChange(selection) {
      this.ids = selection.map((item) => item.id);
      this.single = selection.length != 1
      this.multiple = !selection.length;
    },
    /** 搜索按钮操作 */
    handleQuery() {
      this.queryParams.pageNum = 1;
      this.getList();
    },
    /** 删除按钮操作 */
    handleDelete(row) {
      const Ids = row.id || this.ids;

      this.$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？')
        .then(function () {
          return delProjectTimeEntryLog(Ids);
        })
        .then(() => {
          this.handleQuery();
          this.msgSuccess("删除成功");
        })
        .catch(() => {});
    },
    /** 预览按钮操作 */
    handleView(row) {
      this.reset();
      const id = row.id || this.ids;
      getProjectTimeEntryLog(id).then((res) => {
        const { code, data } = res;
        if (code == 200) {
          this.open = true;
          this.title = "修改数据";

          this.form = {
            ...data,
          };
        }
      });
    },
    /** 导出按钮操作 */
    handleExport() {
      const queryParams = this.queryParams;
      this.$confirm("是否确认导出所有钉钉日志记录数据项?", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      })
        .then(function () {
          return exportProjectTimeEntryLog(queryParams);
        })
        .then((response) => {
          this.download(response.data.path);
        });
    },
    /** 表格合计行 */
    getSummaries(param) {
      const { columns } = param;
      const sums = [];
      columns.forEach((column, index) => {
        if (index === 0) {
          sums[index] = '合计';
          return;
        }
        if (column.property === 'transitHours') {
          sums[index] = this.summaries.transitHoursSum.toFixed(2);
        } else if (column.property === 'actualHours') {
          sums[index] = this.summaries.actualHoursSum.toFixed(2);
        } else {
          sums[index] = '';
        }
      });
      return sums;
    },
  },
};
</script>