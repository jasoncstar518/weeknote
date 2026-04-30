<!--
 * @Descripttion: (日报记录表/dailyreport)
 * @version: (1.0)
 * @Author: (admin)
 * @Date: (2025-11-10)
 * @LastEditors: (admin)
 * @LastEditTime: (2025-11-10)
-->
<template>
  <div class="app-container">
    <!-- :model属性用于表单验证使用 比如下面的el-form-item 的 prop属性用于对表单值进行验证操作 -->
    <el-form :model="queryParams" size="small" label-position="right" inline ref="queryForm" :label-width="labelWidth" v-show="showSearch" 
      @submit.native.prevent>
      
      <el-form-item label="开始日期">
        <el-date-picker
          v-model="queryParams.StartTime"
          type="date"
          placeholder="选择开始日期"
          value-format="yyyy-MM-dd"
          style="width: 180px;">
        </el-date-picker>
      </el-form-item>
      
      <el-form-item label="结束日期">
        <el-date-picker
          v-model="queryParams.EndTime"
          type="date"
          placeholder="选择结束日期"
          value-format="yyyy-MM-dd"
          style="width: 180px;">
        </el-date-picker>
      </el-form-item>
      
      <el-form-item>
        <el-button type="primary" icon="el-icon-search" size="mini" @click="handleQuery">搜索</el-button>
        <el-button icon="el-icon-refresh" size="mini" @click="resetQuery">重置</el-button>
      </el-form-item>
    </el-form>
    <!-- 工具区域 -->
    <el-row :gutter="10" class="mb8">
     
      <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['business:dailyreport:add']" plain icon="el-icon-plus" size="mini" @click="handleAdd">新增</el-button>
      </el-col>
     
      <el-col :span="1.5">
        <el-button type="danger" :disabled="multiple" v-hasPermi="['business:dailyreport:delete']" plain icon="el-icon-delete" size="mini" @click="handleDelete">删除</el-button>
      </el-col>
     
      <el-col :span="1.5">
        <el-button type="warning" plain icon="el-icon-download" size="mini" @click="handleExportReport">导出日报</el-button>
      </el-col>
      <el-col :span="1.5">
        <el-button type="success" plain icon="el-icon-s-grid" size="mini" @click="handleExportWeekReport">导出周报</el-button>
      </el-col>
      <el-col :span="1.5">
        <el-button type="primary" plain icon="el-icon-document-copy" size="mini" @click="handleCopyToDingtalk">复制到钉钉</el-button>
      </el-col>
      <el-col :span="1.5">
        <el-checkbox 
          v-model="autoSaveEnabled" 
          @change="handleAutoSaveToggle"
          style="margin-right: 10px;">
          自动保存
        </el-checkbox>
        <el-select 
          v-model="autoSaveInterval" 
          size="mini" 
          @change="handleAutoSaveIntervalChange"
          :disabled="!autoSaveEnabled"
          style="width: 100px;">
          <el-option label="30秒" :value="30"></el-option>
          <el-option label="60秒" :value="60"></el-option>
          <el-option label="120秒" :value="120"></el-option>
          <el-option label="180秒" :value="180"></el-option>
        </el-select>
      </el-col>
      <right-toolbar :showSearch.sync="showSearch" @queryTable="getList"></right-toolbar>
    </el-row>

    <!-- 数据区域 -->
    <el-table :data="dataList" v-loading="loading" ref="table" border highlight-current-row @sort-change="sortChange" @selection-change="handleSelectionChange">
      <el-table-column type="selection" width="50" align="center"/>
      
      <el-table-column prop="reportdate" label="日期" align="center" width="160">
        <template slot-scope="scope">
          <el-date-picker 
            v-model="scope.row.reportdate" 
            type="date" 
            size="small"
            placeholder="选择日期"
            value-format="yyyy-MM-dd"
            style="width: 130px;">
          </el-date-picker>
        </template>
      </el-table-column>
      
      <el-table-column prop="hospitalname" label="医院名称" align="center" width="200">
        <template slot-scope="scope">
          <el-select 
            v-model="scope.row.hospitalname" 
            size="small" 
            placeholder="请选择医院"
            filterable
            clearable
            @change="handleHospitalChange(scope.row)"
            style="width: 100%;">
            <el-option
              v-for="hospital in hospitalList"
              :key="hospital"
              :label="hospital"
              :value="hospital">
            </el-option>
          </el-select>
        </template>
      </el-table-column>
      
      <el-table-column prop="projectid" label="项目编号" align="center" width="140">
        <template slot-scope="scope">
          <el-select 
            v-model="scope.row.projectid" 
            size="small" 
            placeholder="请选择项目"
            filterable
            clearable
            :disabled="!scope.row.hospitalname"
            :filter-method="(query) => filterProject(query, scope.row.hospitalname)"
            @change="handleProjectChange(scope.row, scope.row.projectid)"
            @focus="currentFilterHospital = scope.row.hospitalname"
            style="width: 100%;">
            <el-option
              v-for="project in getFilteredProjectList(scope.row.hospitalname)"
              :key="project.projectId"
              :label="project.projectId"
              :value="project.projectId">
              <span style="float: left">{{ project.projectId }}</span>
              <span style="float: right; color: #8492a6; font-size: 12px">{{ project.projectName }}</span>
            </el-option>
          </el-select>
        </template>
      </el-table-column>
      
      <el-table-column prop="projectname" label="项目名称" align="center" width="250">
        <template slot-scope="scope">
          <el-input v-model="scope.row.projectname" size="small" placeholder="项目名称"></el-input>
        </template>
      </el-table-column>
      
      <el-table-column prop="worklocation" label="工作地点" align="center" width="120">
        <template slot-scope="scope">
          <el-input v-model="scope.row.worklocation" size="small" placeholder="工作地点"></el-input>
        </template>
      </el-table-column>
      
      <el-table-column prop="intransithours" label="在途工时(h)" align="center" width="100">
        <template slot-scope="scope">
          <el-input v-model.number="scope.row.intransithours" size="small" type="number" placeholder="0"></el-input>
        </template>
      </el-table-column>
      
      <el-table-column prop="actualhours" label="实际工时(h)" align="center" width="100">
        <template slot-scope="scope">
          <el-input v-model.number="scope.row.actualhours" size="small" type="number" placeholder="0"></el-input>
        </template>
      </el-table-column>
      
      <el-table-column prop="workSummary" label="工作内容（摘要）" align="center" min-width="300">
        <template slot-scope="scope">
          <el-input 
            v-model="scope.row.workSummary" 
            type="textarea" 
            :autosize="{ minRows: 1, maxRows: 4 }"
            size="small" 
            placeholder="请输入工作内容">
          </el-input>
        </template>
      </el-table-column>
      
      <el-table-column prop="remarks" label="备注" align="center" width="200">
        <template slot-scope="scope">
          <el-input 
            v-model="scope.row.remarks" 
            type="textarea" 
            :autosize="{ minRows: 1, maxRows: 4 }"
            size="small" 
            placeholder="备注">
          </el-input>
        </template>
      </el-table-column>

      <el-table-column label="操作" align="center" width="150" fixed="right">
        <template slot-scope="scope">
          <el-button 
            size="mini" 
            v-hasPermi="['business:dailyreport:edit']" 
            type="text" 
            icon="el-icon-copy-document" 
            @click="handleCopyRow(scope.row)">
            复制
          </el-button>
          <el-button 
            size="mini" 
            v-hasPermi="['business:dailyreport:edit']" 
            type="text" 
            icon="el-icon-delete" 
            @click="handleDeleteRow(scope.row, scope.$index)">
            删除
          </el-button>
        </template>
      </el-table-column>
    </el-table>
    
    <!-- 批量保存按钮 -->
    <div style="text-align: center; margin: 20px 0;">
      <el-button 
        type="success" 
        v-hasPermi="['business:dailyreport:edit']" 
        icon="el-icon-check" 
        size="medium" 
        @click="handleBatchSave(false)"
        style="font-weight: bold; padding: 12px 40px;">
        批量保存
      </el-button>
    </div>
    
    <pagination class="mt10" background :total="total" :page.sync="queryParams.pageNum" :limit.sync="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改日报记录表对话框 -->
    <el-dialog :title="title" :lock-scroll="false" :visible.sync="open" >
      <el-form ref="form" :model="form" :rules="rules" :label-width="formLabelWidth">
        <el-row :gutter="20">
            
          <el-col :lg="12" v-if="opertype == 2">
            <el-form-item label="id">{{form.id}}</el-form-item>
          </el-col>
    
          <el-col :lg="12">
            <el-form-item label="用户ID" prop="userid">
              <el-input v-model="form.userid" placeholder="请输入用户ID" />
            </el-form-item>
          </el-col>


          <el-col :lg="12">
            <el-form-item label="是否删除(0否1是)" prop="isdeleted">
              <el-radio-group v-model="form.isdeleted">
                <el-radio v-for="item in isdeletedOptions" :key="item.dictValue" :label="item.dictValue">{{item.dictLabel}}</el-radio>
              </el-radio-group>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="日期" prop="reportdate">
              <el-date-picker v-model="form.reportdate" type="datetime" placeholder="选择日期时间"></el-date-picker>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="项目编号" prop="projectid">
              <el-input v-model="form.projectid" placeholder="请输入项目编号" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="医院名称" prop="hospitalname">
              <el-input v-model="form.hospitalname" placeholder="请输入医院名称" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="项目名称" prop="projectname">
              <el-input v-model="form.projectname" placeholder="请输入项目名称" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="工作地点" prop="worklocation">
              <el-input v-model="form.worklocation" placeholder="请输入工作地点" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="在途工时(h)" prop="intransithours">
              <el-input v-model="form.intransithours" placeholder="请输入在途工时(h)" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="实际工时(h)" prop="actualhours">
              <el-input v-model="form.actualhours" placeholder="请输入实际工时(h)" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="工作内容" prop="workSummary">
              <el-input v-model="form.workSummary" placeholder="请输入工作内容" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="备注" prop="remarks">
              <el-input v-model="form.remarks" placeholder="请输入备注" />
            </el-form-item>
          </el-col>

        </el-row>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button type="text" @click="cancel">取 消</el-button>
        <el-button type="primary" @click="submitForm">确 定</el-button>
      </div>
    </el-dialog>

    <!-- 导出日报对话框 -->
    <el-dialog 
      title="日报内容" 
      :visible.sync="reportDialogVisible" 
      width="70%"
      :close-on-click-modal="false"
      :close-on-press-escape="false"
      :show-close="false">
      
      <el-tabs type="border-card">
        <!-- 格式一：原格式 -->
        <el-tab-pane label="格式一：【医院名称】工作内容备注">
          <el-input 
            type="textarea" 
            :rows="15" 
            v-model="reportContent"
            placeholder="日报内容将显示在这里">
          </el-input>
          <div style="margin-top: 10px; text-align: right;">
            <el-button type="primary" size="small" @click="handleCopyReport(1)">复制格式一</el-button>
          </div>
        </el-tab-pane>
        
        <!-- 格式二：序号）医院名称 工作内容 -->
        <el-tab-pane label="格式二：序号）医院名称 工作内容">
          <el-input 
            type="textarea" 
            :rows="15" 
            v-model="reportContent2"
            placeholder="日报内容将显示在这里">
          </el-input>
          <div style="margin-top: 10px; text-align: right;">
            <el-button type="primary" size="small" @click="handleCopyReport(2)">复制格式二</el-button>
          </div>
        </el-tab-pane>
      </el-tabs>
      
      <div slot="footer" class="dialog-footer">
        <el-button @click="reportDialogVisible = false">关 闭</el-button>
      </div>
    </el-dialog>

  </div>
</template>
<script>
import { 
  listDailyreport,
  addDailyreport,
  delDailyreport,
  updateDailyreport,
  getDailyreport,
} from '@/api/business/dailyreport.js';
import { listSelproject } from '@/api/business/selproject.js';
import * as XLSX from 'xlsx';

export default {
  name: "dailyreport",
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
        pageSize: 50,
        sort: undefined,
        sortType: undefined,
        StartTime: undefined,
        EndTime: undefined,
      },
      // 弹出层标题
      title: "",
      // 操作类型 1、add 2、edit
      opertype: 0,
      // 是否显示弹出层
      open: false,
      // 表单参数
      form: {},
      columns: [
        { index: 0, key: 'id', label: ``, checked:  true  },
        { index: 1, key: 'userid', label: `用户ID`, checked:  true  },
        { index: 2, key: 'createtime', label: `创建时间`, checked:  true  },
        { index: 3, key: 'isdeleted', label: `是否删除(0否1是)`, checked:  true  },
        { index: 4, key: 'reportdate', label: `日期`, checked:  true  },
        { index: 5, key: 'projectid', label: `项目编号`, checked:  true  },
        { index: 6, key: 'hospitalname', label: `医院名称`, checked:  true  },
        { index: 7, key: 'projectname', label: `项目名称`, checked:  true  },
        { index: 8, key: 'worklocation', label: `工作地点`, checked:  true  },
        { index: 9, key: 'intransithours', label: `在途工时(h)`, checked:  false  },
        { index: 10, key: 'actualhours', label: `实际工时(h)`, checked:  false  },
        { index: 11, key: 'workSummary', label: `工作内容`, checked:  false  },
        { index: 12, key: 'remarks', label: `备注`, checked:  false  },
      ],
      // 是否删除(0否1是)选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
      isdeletedOptions: [],
      // 数据列表
      dataList: [],
      // 总记录数
      total: 0,
      // 提交按钮是否显示
      btnSubmitVisible: true,
      // 表单校验
      rules: {
       
      },
      // 项目数据源（从数据库加载）
      projectDataSource: [],
      // 医院列表（去重后）
      hospitalList: [],
      // 项目映射表（医院->项目列表）
      hospitalProjectMap: {},
      // 项目数据加载状态
      excelLoading: false,
      // 日报导出对话框
      reportDialogVisible: false,
      reportContent: '',
      reportContent2: '', // 第二种格式的日报内容
      // 当前搜索的医院
      currentFilterHospital: '',
      // 过滤后的项目列表
      filteredProjects: [],
      // 当前选中的行数据
      selectedRows: [],
      // 自动保存配置
      autoSaveEnabled: false,  // 默认开启
      autoSaveInterval: 30,   // 默认30秒
      autoSaveTimer: null,    // 定时器
    };
  },
  created() {    
    // 设置默认查询时间为本周一到周日
    this.setDefaultWeekRange();
    // 从数据库加载项目数据
    this.loadProjectData();
    // 列表数据查询
    this.getList();

    var dictParams = [
    ];
  },
  mounted() {
    // 初始化时添加一行空数据
    this.addEmptyRow();
    // 启动自动保存定时器
    this.startAutoSave();
  },
  beforeDestroy() {
    // 组件销毁前清除定时器
    this.stopAutoSave();
  },
  methods: {
    // 设置默认查询时间为本周一到周日
    setDefaultWeekRange() {
      const now = new Date();
      const dayOfWeek = now.getDay(); // 0是周日，1是周一，...6是周六
      
      // 计算本周一的日期
      const monday = new Date(now);
      const diff = dayOfWeek === 0 ? -6 : 1 - dayOfWeek; // 如果是周日，往前推6天；否则往前推到周一
      monday.setDate(now.getDate() + diff);
      
      // 计算本周日的日期
      const sunday = new Date(monday);
      sunday.setDate(monday.getDate() + 7);
      
      // 格式化为 yyyy-MM-dd
      this.queryParams.StartTime = this.formatDate(monday);
      this.queryParams.EndTime = this.formatDate(sunday);
    },
    // 格式化日期为 yyyy-MM-dd
    formatDate(date) {
      const year = date.getFullYear();
      const month = String(date.getMonth() + 1).padStart(2, '0');
      const day = String(date.getDate()).padStart(2, '0');
      return `${year}-${month}-${day}`;
    },
    // 查询数据
    getList() {
      this.loading = true;
      listDailyreport(this.queryParams).then(res => {
         if (res.code == 200) {
           this.dataList = res.data.result;
           this.total = res.data.totalNum;
           this.loading = false;
           // 查询完成后添加一行空数据用于新增
           this.addEmptyRow();
         }
       })
    },
    // 从数据库加载项目数据
    async loadProjectData() {
      try {
        this.excelLoading = true;
        const res = await listSelproject({ pageNum: 1, pageSize: 9999 });
        if (res.code == 200 && res.data && res.data.result) {
          this.projectDataSource = res.data.result
            .filter(item => item.hospitalName && item.code && item.name)
            .map(item => ({
              hospital: item.hospitalName.trim(),
              projectId: item.code.trim(),
              projectName: item.name.trim()
            }));
          this.buildHospitalProjectMap();
          console.log('项目数据加载成功，共加载', this.projectDataSource.length, '条');
        }
      } catch (error) {
        console.error('加载项目数据失败:', error);
        this.msgError('加载项目数据失败: ' + error.message);
      } finally {
        this.excelLoading = false;
      }
    },
    // 构建医院-项目映射关系
    buildHospitalProjectMap() {
      const hospitalSet = new Set();
      const projectMap = {};

      this.projectDataSource.forEach(item => {
        hospitalSet.add(item.hospital);
        if (!projectMap[item.hospital]) {
          projectMap[item.hospital] = [];
        }
        projectMap[item.hospital].push({
          projectId: item.projectId,
          projectName: item.projectName
        });
      });

      this.hospitalList = Array.from(hospitalSet).sort();
      this.hospitalProjectMap = projectMap;
    },
    // 医院选择改变时
    handleHospitalChange(row) {
      // 清空项目相关字段
      row.projectid = undefined;
      row.projectname = undefined;
      
      // 初始化该医院的项目列表（调用搜索）
      if (row.hospitalname) {
        this.currentFilterHospital = row.hospitalname;
        this.filterProject('', row.hospitalname);
      } else {
        this.currentFilterHospital = '';
        this.filteredProjects = [];
      }
    },
    // 项目选择改变时
    handleProjectChange(row, projectId) {
      // 根据项目编号找到对应的项目名称
      const projects = this.hospitalProjectMap[row.hospitalname] || [];
      const project = projects.find(p => p.projectId === projectId);
      if (project) {
        row.projectname = project.projectName;
      }
    },
    // 获取当前行的项目列表
    getProjectList(hospitalname) {
      return this.hospitalProjectMap[hospitalname] || [];
    },
    // 自定义过滤方法：支持搜索项目编号或项目名称
    filterProject(query, hospitalname) {
      if (!hospitalname) {
        this.filteredProjects = [];
        return;
      }
      
      const allProjects = this.hospitalProjectMap[hospitalname] || [];
      
      if (!query) {
        this.filteredProjects = allProjects;
        return;
      }
      
      // 转换为小写进行模糊搜索
      const searchText = query.toLowerCase();
      
      this.filteredProjects = allProjects.filter(project => {
        const projectId = (project.projectId || '').toLowerCase();
        const projectName = (project.projectName || '').toLowerCase();
        // 支持搜索项目编号或项目名称
        return projectId.includes(searchText) || projectName.includes(searchText);
      });
    },
    // 获取过滤后的项目列表
    getFilteredProjectList(hospitalname) {
      // 如果没有过滤结果，返回全部
      if (this.filteredProjects.length === 0 && hospitalname === this.currentFilterHospital) {
        return [];
      }
      if (this.filteredProjects.length > 0 && hospitalname === this.currentFilterHospital) {
        return this.filteredProjects;
      }
      // 默认返回全部
      return this.hospitalProjectMap[hospitalname] || [];
    },
    // 添加一行空数据
    addEmptyRow() {
      const emptyRow = {
        id: undefined,
        userid: undefined,
        createtime: undefined,
        isdeleted: 0,
        reportdate: this.parseTime(new Date(), '{y}-{m}-{d}'),
        projectid: undefined,
        hospitalname: undefined,
        projectname: undefined,
        worklocation: '公司',
        intransithours: 0,
        actualhours: 0,
        workSummary: undefined,
        remarks: undefined,
        _isNew: true // 标记为新增行
      };
      this.dataList.push(emptyRow);
      
      // 滚动到最后一行
      this.$nextTick(() => {
        const table = this.$refs.table;
        if (table && table.bodyWrapper) {
          table.bodyWrapper.scrollTop = table.bodyWrapper.scrollHeight;
        }
      });
    },
    // 保存行数据
    handleSaveRow(row, index) {
      // 验证必填项
      if (!row.reportdate) {
        this.msgError('第' + (index + 1) + '行：请选择日期');
        return false;
      }
      if (!row.hospitalname) {
        this.msgError('第' + (index + 1) + '行：请选择医院名称');
        return false;
      }
      if (!row.projectid) {
        this.msgError('第' + (index + 1) + '行：请选择项目编号');
        return false;
      }
      if (!row.projectname) {
        this.msgError('第' + (index + 1) + '行：请选择项目名称');
        return false;
      }

      // 构建保存数据
      const saveData = {
        userid: row.userid,
        isdeleted: row.isdeleted || 0,
        reportdate: row.reportdate,
        projectid: row.projectid,
        hospitalname: row.hospitalname,
        projectname: row.projectname,
        worklocation: row.worklocation || '公司',
        intransithours: row.intransithours || 0,
        actualhours: row.actualhours || 0,
        workSummary: row.workSummary,
        remarks: row.remarks,
        createtime: new Date().toISOString().replace('T', ' ').substring(0, 19)
      };

      // 判断是新增还是修改
      if (row.id && !row._isNew) {
        // 修改
        saveData.id = row.id;
        return updateDailyreport(saveData);
      } else {
        // 新增
        return addDailyreport(saveData);
      }
    },
    /** 启动自动保存定时器 */
    startAutoSave() {
      if (this.autoSaveEnabled && this.autoSaveInterval > 0) {
        this.stopAutoSave(); // 先清除旧定时器
        this.autoSaveTimer = setInterval(() => {
          this.handleBatchSave(true); // 传入true表示静默保存
        }, this.autoSaveInterval * 1000);
      }
    },
    /** 停止自动保存定时器 */
    stopAutoSave() {
      if (this.autoSaveTimer) {
        clearInterval(this.autoSaveTimer);
        this.autoSaveTimer = null;
      }
    },
    /** 自动保存开关切换 */
    handleAutoSaveToggle(enabled) {
      if (enabled) {
        this.startAutoSave();
      } else {
        this.stopAutoSave();
      }
    },
    /** 自动保存间隔时间改变 */
    handleAutoSaveIntervalChange(interval) {
      if (this.autoSaveEnabled) {
        this.startAutoSave(); // 重新启动定时器
      }
    },
    /** 批量保存当前页所有数据 */
    handleBatchSave(isSilent) {
      // 过滤掉空白行（没有医院名称的行）
      const validRows = this.dataList.filter(row => row.hospitalname);
      
      if (validRows.length === 0) {
        if (!isSilent) {
          this.msgError('没有可保存的数据');
        }
        return;
      }

      // 验证所有行的必填项
      for (let i = 0; i < this.dataList.length; i++) {
        const row = this.dataList[i];
        // 跳过空白行
        if (!row.hospitalname) continue;
        
        if (!row.reportdate) {
          if (!isSilent) {
            this.msgError('第' + (i + 1) + '行：请选择日期');
          }
          return;
        }
        if (!row.projectid) {
          if (!isSilent) {
            this.msgError('第' + (i + 1) + '行：请选择项目编号');
          }
          return;
        }
        if (!row.projectname) {
          if (!isSilent) {
            this.msgError('第' + (i + 1) + '行：项目名称不能为空');
          }
          return;
        }
      }

      // 静默保存模式（自动保存）
      if (isSilent) {
        // 批量保存
        const savePromises = [];
        validRows.forEach((row) => {
          const promise = this.handleSaveRow(row, this.dataList.indexOf(row));
          if (promise) {
            savePromises.push(promise);
          }
        });

        // 等待所有保存完成（静默保存不提示）
        Promise.all(savePromises)
          .then(() => {
            this.getList();
          })
          .catch((err) => {
            console.error('自动保存错误:', err);
          });
      } else {
        // 手动保存模式（弹出确认，保存成功后提示）
        this.$confirm('是否确认保存当前页的 ' + validRows.length + ' 条数据？', '提示', {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning'
        }).then(() => {
          // 批量保存
          const savePromises = [];
          validRows.forEach((row) => {
            const promise = this.handleSaveRow(row, this.dataList.indexOf(row));
            if (promise) {
              savePromises.push(promise);
            }
          });

          // 等待所有保存完成后提示成功
          return Promise.all(savePromises);
        }).then(() => {
          this.msgSuccess('批量保存成功');
          this.getList();
        }).catch((err) => {
          // 区分是取消操作还是保存失败
          if (err !== 'cancel') {
            this.msgError('部分数据保存失败，请检查后重试');
            console.error('批量保存错误:', err);
          }
        });
      }
    },
    /** 复制行数据 */
    handleCopyRow(row) {
      // 创建新行数据，复制医院名称、项目编号、项目名称
      const newRow = {
        id: undefined,
        userid: undefined,
        createtime: undefined,
        isdeleted: 0,
        reportdate: this.parseTime(new Date(), '{y}-{m}-{d}'),
        projectid: row.projectid,
        hospitalname: row.hospitalname,
        projectname: row.projectname,
        worklocation: row.worklocation || '公司',
        intransithours: 0,
        actualhours: 0,
        workSummary: undefined,
        remarks: undefined,
        _isNew: true // 标记为新增行
      };
      
      // 添加到数据列表末尾
      this.dataList.push(newRow);
      
      // 滚动到最后一行
      this.$nextTick(() => {
        const table = this.$refs.table;
        if (table && table.bodyWrapper) {
          table.bodyWrapper.scrollTop = table.bodyWrapper.scrollHeight;
        }
      });
      
      this.msgSuccess('已复制行数据');
    },
    
    /** 删除单行数据 */
    handleDeleteRow(row, index) {
      // 如果是未保存的新行，直接从列表中移除
      if (!row.id || row._isNew) {
        this.dataList.splice(index, 1);
        this.msgSuccess('删除成功');
        // 确保至少有一行空白行
        if (this.dataList.length === 0 || !this.dataList.some(item => item._isNew)) {
          this.addEmptyRow();
        }
        return;
      }

      // 如果是已保存的数据，调用删除接口
      this.$confirm('是否确认删除该条数据？', '警告', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        return delDailyreport(row.id);
      }).then(() => {
        this.msgSuccess('删除成功');
        this.getList();
      }).catch(() => {});
    },
    // 取消按钮
    cancel() {
      this.open = false;
      this.reset();
    },
    // 重置数据表单
    reset() {
      this.form = {
        userid: undefined,
        createtime: undefined,
        isdeleted: undefined,
        reportdate: undefined,
        projectid: undefined,
        hospitalname: undefined,
        projectname: undefined,
        worklocation: undefined,
        intransithours: undefined,
        actualhours: undefined,
        workSummary: undefined,
        remarks: undefined,
      };
      this.resetForm("form");
    },
    // 重置查询操作
    resetQuery() {
      // 重置为本周一到周日
      this.setDefaultWeekRange();
      this.resetForm("queryForm");
      this.handleQuery();
    },
    // 多选框选中数据
    handleSelectionChange(selection) {
      this.ids = selection.map((item) => item.id);
      this.selectedRows = selection; // 保存选中的完整行数据
      this.single = selection.length != 1
      this.multiple = !selection.length;
    },
     // 自定义排序
    sortChange(column) {
      if (column.prop == null || column.order == null) {
        this.queryParams.sort = undefined;
        this.queryParams.sortType = undefined;
      } else {
        this.queryParams.sort = column.prop;
        this.queryParams.sortType = column.order;
      }

      this.handleQuery();
    },
    /** 搜索按钮操作 */
    handleQuery() {
      this.queryParams.pageNum = 1;
      this.getList();
    },
    /** 新增按钮操作 */
    handleAdd() {
      // 直接添加一行空白记录
      this.addEmptyRow();
    },
    /** 删除按钮操作 */
    handleDelete(row) {
      // 如果是从行内删除按钮触发
      if (row && row.id) {
        this.$confirm('是否确认删除该条数据？', '警告', {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning'
        }).then(() => {
          return delDailyreport(row.id);
        }).then(() => {
          this.msgSuccess("删除成功");
          this.getList();
        }).catch(() => {});
      } else {
        // 批量删除勾选的数据
        if (this.ids.length === 0) {
          this.msgError('请先勾选要删除的数据');
          return;
        }
        
        this.$confirm('是否确认删除选中的' + this.ids.length + '条数据？', '警告', {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning'
        }).then(() => {
          // 过滤出已保存的数据ID（排除新增但未保存的行）
          const savedIds = this.ids.filter(id => id !== undefined && id !== null && id !== '');
          
          if (savedIds.length === 0) {
            // 只是删除未保存的新行，直接从列表中移除
            this.dataList = this.dataList.filter(item => {
              return !this.ids.includes(item.id) || (item.id && !item._isNew);
            });
            this.msgSuccess("删除成功");
            // 确保至少有一行空白行
            if (this.dataList.length === 0 || !this.dataList.some(item => item._isNew)) {
              this.addEmptyRow();
            }
            return Promise.resolve();
          }
          
          // 调用后端删除接口
          return delDailyreport(savedIds.join(','));
        }).then(() => {
          this.msgSuccess("删除成功");
          this.getList();
        }).catch(() => {});
      }
    },
    /** 修改按钮操作 */
    handleUpdate(row) {
      this.reset();
      const id = row.id || this.ids;
      getDailyreport(id).then((res) => {
        const { code, data } = res;
        if (code == 200) {
          this.open = true;
          this.title = "修改数据";
          this.opertype = 2;

          this.form = {
            ...data,
          };
        }
      });
    },
    /** 导出日报 */
    handleExportReport() {
      // 检查是否有勾选数据
      if (!this.selectedRows || this.selectedRows.length === 0) {
        this.msgError('请先勾选要导出的数据');
        return;
      }

      // 生成日报内容 - 格式一：【医院名称】工作内容备注
      const reportLines1 = [];
      this.selectedRows.forEach(row => {
        // 过滤掉未保存的新行(没有医院名称的)
        if (!row.hospitalname) {
          return;
        }

        // 拼接格式:【医院名称】工作内容正文备注内容正文
        let line = `【${row.hospitalname}】`;
        
        // 添加工作内容
        if (row.workSummary) {
          line += row.workSummary;
        }
        
        // 添加备注
        if (row.remarks) {
          line += row.remarks;
        }
        
        reportLines1.push(line);
      });

      // 生成日报内容 - 格式二：序号）医院名称 工作内容
      const reportLines2 = [];
      // 按医院分组
      const hospitalGroups = {};
      this.selectedRows.forEach(row => {
        if (!row.hospitalname) return;
        
        if (!hospitalGroups[row.hospitalname]) {
          hospitalGroups[row.hospitalname] = [];
        }
        
        // 合并工作内容和备注
        let content = '';
        if (row.workSummary) {
          content += row.workSummary;
        }
        if (row.remarks) {
          content += row.remarks;
        }
        
        if (content) {
          hospitalGroups[row.hospitalname].push(content);
        }
      });

      // 格式化输出
      let index = 1;
      Object.keys(hospitalGroups).forEach(hospital => {
        const contents = hospitalGroups[hospital];
        if (contents.length > 0) {
          // 医院名称行
          reportLines2.push(`${index}）${hospital}`);
          
          // 工作内容行（可能有多条）
          contents.forEach((content, idx) => {
            reportLines2.push(`      ${idx + 1}、${content}`);
          });
          
          // 空行分隔
          reportLines2.push('');
          index++;
        }
      });

      if (reportLines1.length === 0) {
        this.msgError('选中的数据中没有有效的日报内容');
        return;
      }

      // 用换行符连接每一行
      this.reportContent = reportLines1.join('\n');
      this.reportContent2 = reportLines2.join('\n');
      
      // 显示对话框
      this.reportDialogVisible = true;
    },
    /** 复制日报内容 */
    handleCopyReport(format) {
      // 根据格式选择要复制的内容
      const content = format === 1 ? this.reportContent : this.reportContent2;
      
      // 创建一个临时的 textarea 元素
      const textarea = document.createElement('textarea');
      textarea.value = content;
      textarea.style.position = 'fixed';
      textarea.style.opacity = '0';
      document.body.appendChild(textarea);
      
      // 选中并复制
      textarea.select();
      try {
        document.execCommand('copy');
        this.msgSuccess(`格式${format}复制成功，可以粘贴到其他地方了`);
      } catch (err) {
        this.msgError('复制失败，请手动复制');
      }
      
      // 移除临时元素
      document.body.removeChild(textarea);
    },
    /** 导出周报Excel */
    handleExportWeekReport() {
      // 检查是否有勾选数据
      if (!this.selectedRows || this.selectedRows.length === 0) {
        this.msgError('请先勾选要导出的数据');
        return;
      }

      // 过滤有效数据
      const validRows = this.selectedRows.filter(row => row.hospitalname);
      
      if (validRows.length === 0) {
        this.msgError('选中的数据中没有有效的周报内容');
        return;
      }

      // 准备Excel数据
      const excelData = [];
      
      // 表头
      const headers = [
        '日期',
        '项目编号',
        '医院名称',
        '项目名称',
        '工作地点',
        '在途工时（h）',
        '实际工时（h）',
        '工作内容（摘要）',
        '备注'
      ];
      excelData.push(headers);

      // 数据行
      validRows.forEach(row => {
        // 处理日期格式，只保留年月日
        let dateStr = row.reportdate || '';
        if (dateStr) {
          // 如果日期包含时间部分，只取日期部分
          dateStr = dateStr.split(' ')[0];
          // 如果是完整的日期时间格式，提取年月日
          if (dateStr.includes('T')) {
            dateStr = dateStr.split('T')[0];
          }
        }
        
        // 处理工作内容和备注，去掉换行符
        const workSummary = (row.workSummary || '').replace(/[\r\n]+/g, '');
        const remarks = (row.remarks || '').replace(/[\r\n]+/g, '');
        
        const rowData = [
          dateStr,
          row.projectid || '',
          row.hospitalname || '',
          row.projectname || '',
          row.worklocation || '',
          row.intransithours || 0,
          row.actualhours || 0,
          workSummary,
          remarks
        ];
        excelData.push(rowData);
      });

      // 创建工作簿
      const ws = XLSX.utils.aoa_to_sheet(excelData);
      
      // 设置列宽
      const colWidths = [
        { wch: 12 },  // 日期
        { wch: 15 },  // 项目编号
        { wch: 25 },  // 医院名称
        { wch: 35 },  // 项目名称
        { wch: 12 },  // 工作地点
        { wch: 15 },  // 在途工时
        { wch: 15 },  // 实际工时
        { wch: 50 },  // 工作内容
        { wch: 30 }   // 备注
      ];
      ws['!cols'] = colWidths;

      // 设置行高（让内容自动换行）
      const rowHeights = [];
      for (let i = 0; i < excelData.length; i++) {
        rowHeights.push({ hpt: 20 }); // 默认行高20
      }
      ws['!rows'] = rowHeights;

      // 设置单元格样式（文本换行）
      const range = XLSX.utils.decode_range(ws['!ref']);
      for (let R = range.s.r; R <= range.e.r; ++R) {
        for (let C = range.s.c; C <= range.e.c; ++C) {
          const cellAddress = XLSX.utils.encode_cell({ r: R, c: C });
          if (!ws[cellAddress]) continue;
          
          // 设置文本样式
          if (!ws[cellAddress].s) ws[cellAddress].s = {};
          ws[cellAddress].s.alignment = {
            wrapText: true,  // 自动换行
            vertical: 'top',
            horizontal: 'left'
          };
          
          // 设置表头样式
          if (R === 0) {
            ws[cellAddress].s = {
              font: { bold: true },
              alignment: { horizontal: 'center', vertical: 'center' },
              fill: { fgColor: { rgb: 'F0F0F0' } }
            };
          }
        }
      }

      // 创建工作簿集
      const wb = XLSX.utils.book_new();
      XLSX.utils.book_append_sheet(wb, ws, '周报');

      // 生成文件名（使用当前日期）
      const now = new Date();
      const fileName = `周报_${this.formatDate(now)}.xlsx`;

      // 导出Excel文件
      XLSX.writeFile(wb, fileName);
      
      this.msgSuccess('导出成功');
    },
    /** 复制到钉钉 */
    handleCopyToDingtalk() {
      // 检查是否有勾选数据
      if (!this.selectedRows || this.selectedRows.length === 0) {
        this.msgError('请先勾选要复制的数据');
        return;
      }

      // 过滤有效数据
      const validRows = this.selectedRows.filter(row => row.hospitalname);
      
      if (validRows.length === 0) {
        this.msgError('选中的数据中没有有效的内容');
        return;
      }

      // 构造CSV格式数据（适用于钉钉表格粘贴），不包含表头
      let csvContent = '';

      // 数据行
      validRows.forEach(row => {
        // 处理日期格式，只保留年月日
        let dateStr = row.reportdate || '';
        if (dateStr) {
          // 如果日期包含时间部分，只取日期部分
          dateStr = dateStr.split(' ')[0];
          // 如果是完整的日期时间格式，提取年月日
          if (dateStr.includes('T')) {
            dateStr = dateStr.split('T')[0];
          }
        }
        
        // 处理工作内容和备注，去掉换行符并用空格替换
        const workSummary = (row.workSummary || '').replace(/[\r\n]+/g, ' ');
        const remarks = (row.remarks || '').replace(/[\r\n]+/g, ' ');
        
        const rowData = [
          dateStr,
          row.projectid || '',
          row.hospitalname || '',
          row.projectname || '',
          row.worklocation || '',
          row.intransithours || 0,
          row.actualhours || 0,
          workSummary,
          remarks
        ];
        
        csvContent += rowData.join('\t') + '\n';
      });

      // 复制到剪贴板
      this.copyToClipboard(csvContent);
      
      this.msgSuccess('已复制到剪贴板，可粘贴到钉钉表格');
    },
    /** 复制内容到剪贴板 */
    copyToClipboard(text) {
      // 创建一个临时的 textarea 元素
      const textarea = document.createElement('textarea');
      textarea.value = text;
      textarea.style.position = 'fixed';
      textarea.style.opacity = '0';
      document.body.appendChild(textarea);
      
      // 选中并复制
      textarea.select();
      try {
        document.execCommand('copy');
      } catch (err) {
        this.msgError('复制失败，请手动复制');
      }
      
      // 移除临时元素
      document.body.removeChild(textarea);
    },
    // 是否删除(0否1是)字典翻译
    isdeletedFormat(row, column) {
      return this.selectDictLabel(this.isdeletedOptions, row.isdeleted);
    },
    /** 提交按钮 */
    submitForm: function () {
      this.$refs["form"].validate((valid) => {
        if (valid) {
          console.log(JSON.stringify(this.form));
          
          if (this.form.id != undefined && this.opertype === 2) {
            updateDailyreport(this.form)
              .then((res) => {
                this.msgSuccess("修改成功");
                this.open = false;
                this.getList();
            })
            .catch((err) => {
                //TODO 错误逻辑
              });
          } else {
            addDailyreport(this.form)
              .then((res) => {
                this.msgSuccess("新增成功");
                this.open = false;
                this.getList();
            })
            .catch((err) => {
                //TODO 错误逻辑
              });
          }
        }
      });
    },
  },
};
</script>