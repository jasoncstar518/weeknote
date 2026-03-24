<!--
 * @Descripttion: (项目配置表/projectconfig)
 * @version: (1.0)
 * @Author: (admin)
 * @Date: (2025-12-16)
 * @LastEditors: (admin)
 * @LastEditTime: (2025-12-16)
-->
<template>
  <div class="app-container">
    <!-- :model属性用于表单验证使用 比如下面的el-form-item 的 prop属性用于对表单值进行验证操作 -->
    <el-form :model="queryParams" size="small" label-position="right" inline ref="queryForm" :label-width="labelWidth" v-show="showSearch" 
      @submit.native.prevent>
      
      <el-form-item>
        <el-button type="primary" icon="el-icon-search" size="mini" @click="handleQuery">搜索</el-button>
        <el-button icon="el-icon-refresh" size="mini" @click="resetQuery">重置</el-button>
      </el-form-item>
    </el-form>
    <!-- 工具区域 -->
    <el-row :gutter="10" class="mb8">
      <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['business:projectconfig:add']" plain icon="el-icon-plus" size="mini" @click="handleAdd">新增</el-button>
      </el-col>
      <el-col :span="1.5">
        <el-button type="success" :disabled="single" v-hasPermi="['business:projectconfig:edit']" plain icon="el-icon-edit" size="mini" @click="handleUpdate">修改</el-button>
      </el-col>
      <el-col :span="1.5">
        <el-button type="danger" :disabled="multiple" v-hasPermi="['business:projectconfig:delete']" plain icon="el-icon-delete" size="mini" @click="handleDelete">删除</el-button>
      </el-col>
      <right-toolbar :showSearch.sync="showSearch" @queryTable="getList"></right-toolbar>
    </el-row>

    <!-- 数据区域 -->
    <el-table :data="dataList" v-loading="loading" ref="table" border highlight-current-row @sort-change="sortChange" @selection-change="handleSelectionChange">
      <el-table-column type="selection" width="50" align="center"/>
     
      <el-table-column prop="code" label="项目编码" align="center" :show-overflow-tooltip="true" width="120" />
       <el-table-column prop="hospitalName" label="所属医院" align="center" :show-overflow-tooltip="true" width="200" />
      <el-table-column prop="name" label="项目名称" align="center" :show-overflow-tooltip="true" width="200" />
     
       <el-table-column prop="projectType" label="项目类型" align="center">
        <template slot-scope="scope">
          <dict-tag :options="projectTypeOptions" :value="scope.row.projectType" />
        </template>
      </el-table-column>
      <el-table-column prop="projectStatus" label="项目状态" align="center">
        <template slot-scope="scope">
          <dict-tag :options="projectStatusOptions" :value="scope.row.projectStatus" />
        </template>
      </el-table-column>
      <el-table-column prop="department" label="建设科室" align="center" :show-overflow-tooltip="true" />
      <el-table-column prop="signingDate" label="签订时间" align="center" :show-overflow-tooltip="true" />
     
      <el-table-column prop="projectLeader" label="项目负责人" align="center" :show-overflow-tooltip="true" />
      <el-table-column prop="businessLeader" label="商务负责人" align="center" :show-overflow-tooltip="true" />
      <el-table-column prop="presalesLeader" label="售前负责人" align="center" :show-overflow-tooltip="true" />
      <el-table-column prop="totalControlFee" label="控费总金额(万)" align="center" />
      <el-table-column prop="developmentControlFee" label="控费开发金额(万)" align="center" />
      <el-table-column prop="projectControlFee" label="控费项目金额(万)" align="center" />
      <el-table-column prop="maintenanceControlFee" label="控费运维金额(万)" align="center" />
      <el-table-column prop="totalWorkHours" label="控费总工时(人天)" align="center" />
      <el-table-column prop="developmentWorkHours" label="控费开发工时(人天)" align="center" />
      <el-table-column prop="projectWorkHours" label="控费项目工时(人天)" align="center" />
      <el-table-column prop="maintenanceWorkHours" label="控费运维工时(人天)" align="center" />
      <el-table-column prop="milestoneCompletion" label="里程碑完成节点情况" align="center" :show-overflow-tooltip="true" />
      <el-table-column prop="remarks" label="备注" align="center" :show-overflow-tooltip="true" width="300"/>
      <el-table-column prop="riskAnalysis" label="控费风险分析说明" align="center">
        <template slot-scope="scope">
          <dict-tag :options="riskAnalysisOptions" :value="scope.row.riskAnalysis" />
        </template>
      </el-table-column>
      <el-table-column prop="contractStatus" label="合同状态" align="center">
        <template slot-scope="scope">
          <dict-tag :options="contractStatusOptions" :value="scope.row.contractStatus" />
        </template>
      </el-table-column>
      <el-table-column prop="documentStatus" label="资料状态" align="center">
        <template slot-scope="scope">
          <dict-tag :options="documentStatusOptions" :value="scope.row.documentStatus" />
        </template>
      </el-table-column>
      <el-table-column prop="acceptanceReportStatus" label="验收报告状态" align="center">
        <template slot-scope="scope">
          <dict-tag :options="acceptanceReportStatusOptions" :value="scope.row.acceptanceReportStatus" />
        </template>
      </el-table-column>
      <el-table-column prop="inspectionQ1" label="一季度巡检报告" align="center" :show-overflow-tooltip="true" />
      <el-table-column prop="inspectionQ2" label="二季度巡检报告" align="center" :show-overflow-tooltip="true" />
      <el-table-column prop="inspectionQ3" label="三季度巡检报告" align="center" :show-overflow-tooltip="true" />
      <el-table-column prop="inspectionQ4" label="四季度巡检报告" align="center" :show-overflow-tooltip="true" />
      <el-table-column prop="maintenanceStartDate" label="维保开始时间" align="center" :show-overflow-tooltip="true" />
      <el-table-column prop="maintenanceEndDate" label="维保结束时间" align="center" :show-overflow-tooltip="true" />
      <el-table-column prop="maintenanceTerminationDate" label="中止运维时间" align="center" :show-overflow-tooltip="true" />
      

      <el-table-column label="操作" align="center" width="140" fixed="right">
        <template slot-scope="scope">
          <el-button size="mini" v-hasPermi="['business:projectconfig:edit']" type="success" icon="el-icon-edit" title="编辑" 
            @click="handleUpdate(scope.row)"></el-button>      
          <el-button size="mini" v-hasPermi="['business:projectconfig:delete']" type="danger" icon="el-icon-delete" title="删除" 
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination class="mt10" background :total="total" :page.sync="queryParams.pageNum" :limit.sync="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改项目配置表对话框 -->
    <el-dialog :title="title" :lock-scroll="false" :visible.sync="open" width="70%">
      <el-form ref="form" :model="form" :rules="rules" :label-width="formLabelWidth">
        <!-- 基本信息区域 -->
        <el-row class="mb10">
          <el-card class="box-card" shadow="never">
            <div slot="header" class="clearfix">
              <span>基本信息</span>
            </div>
            <el-row :gutter="20">
              <el-col :lg="6" v-if="opertype == 2">
                <el-form-item label="编号">{{form.id}}</el-form-item>
              </el-col>

              <el-col :lg="6">
                <el-form-item label="项目编码" prop="code">
                  <el-input v-model="form.code" placeholder="请输入项目编码" />
                </el-form-item>
              </el-col>

              <el-col :lg="6">
                <el-form-item label="项目名称" prop="name">
                  <el-input v-model="form.name" placeholder="请输入项目名称" />
                </el-form-item>
              </el-col>

              <el-col :lg="6">
                <el-form-item label="所属医院" prop="hospitalName">
                  <el-input v-model="form.hospitalName" placeholder="请输入所属医院" />
                </el-form-item>
              </el-col>

              <el-col :lg="6">
                <el-form-item label="建设科室" prop="department">
                  <el-input v-model="form.department" placeholder="请输入建设科室" />
                </el-form-item>
              </el-col>

              <el-col :lg="6">
                <el-form-item label="签订时间" prop="signingDate">
                  <el-date-picker v-model="form.signingDate" type="datetime" placeholder="选择日期时间" style="width: 100%;"></el-date-picker>
                </el-form-item>
              </el-col>

              <el-col :lg="6">
                <el-form-item label="项目类型" prop="projectType">
                  <el-select v-model="form.projectType" placeholder="请选择项目类型">
                    <el-option v-for="item in projectTypeOptions" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
                  </el-select>
                </el-form-item>
              </el-col>

              <el-col :lg="6">
                <el-form-item label="项目状态" prop="projectStatus">
                  <el-select v-model="form.projectStatus" placeholder="请选择项目状态">
                    <el-option v-for="item in projectStatusOptions" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
                  </el-select>
                </el-form-item>
              </el-col>

              <el-col :lg="6">
                <el-form-item label="项目负责人" prop="projectLeader">
                  <el-input v-model="form.projectLeader" placeholder="请输入项目负责人" />
                </el-form-item>
              </el-col>

              <el-col :lg="6">
                <el-form-item label="商务负责人" prop="businessLeader">
                  <el-input v-model="form.businessLeader" placeholder="请输入商务负责人" />
                </el-form-item>
              </el-col>

              <el-col :lg="6">
                <el-form-item label="售前负责人" prop="presalesLeader">
                  <el-input v-model="form.presalesLeader" placeholder="请输入售前负责人" />
                </el-form-item>
              </el-col>

              <el-col :lg="6">
                <el-form-item label="合同状态" prop="contractStatus">
                  <el-select v-model="form.contractStatus" placeholder="请选择合同状态">
                    <el-option v-for="item in contractStatusOptions" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
                  </el-select>
                </el-form-item>
              </el-col>

              <el-col :lg="6">
                <el-form-item label="资料状态" prop="documentStatus">
                  <el-select v-model="form.documentStatus" placeholder="请选择资料状态">
                    <el-option v-for="item in documentStatusOptions" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
                  </el-select>
                </el-form-item>
              </el-col>

              <el-col :lg="6">
                <el-form-item label="验收报告状态" prop="acceptanceReportStatus">
                  <el-select v-model="form.acceptanceReportStatus" placeholder="请选择验收报告状态">
                    <el-option v-for="item in acceptanceReportStatusOptions" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
                  </el-select>
                </el-form-item>
              </el-col>

              <!-- 单独占一行的字段 -->
              <el-col :lg="12">
                <el-form-item label="备注" prop="remarks">
                  <el-input v-model="form.remarks" placeholder="请输入备注" />
                </el-form-item>
              </el-col>

              <el-col :lg="12">
                <el-form-item label="控费风险分析说明" prop="riskAnalysis">
                  <el-input v-model="form.riskAnalysis" placeholder="请输入控费风险分析说明" />
                </el-form-item>
              </el-col>
            </el-row>
          </el-card>
        </el-row>

        <!-- 控费信息区域 -->
        <el-row class="mb10">
          <el-card class="box-card" shadow="never">
            <div slot="header" class="clearfix">
              <span>控费信息</span>
            </div>
            <el-row :gutter="20">
              <el-col :lg="6">
                <el-form-item label="控费总金额(万)" prop="totalControlFee">
                  <el-input v-model="form.totalControlFee" placeholder="请输入控费总金额(万)" />
                </el-form-item>
              </el-col>

              <el-col :lg="6">
                <el-form-item label="控费开发金额(万)" prop="developmentControlFee">
                  <el-input v-model="form.developmentControlFee" placeholder="请输入控费开发金额(万)" />
                </el-form-item>
              </el-col>

              <el-col :lg="6">
                <el-form-item label="控费项目金额(万)" prop="projectControlFee">
                  <el-input v-model="form.projectControlFee" placeholder="请输入控费项目金额(万)" />
                </el-form-item>
              </el-col>

              <el-col :lg="6">
                <el-form-item label="控费运维金额(万)" prop="maintenanceControlFee">
                  <el-input v-model="form.maintenanceControlFee" placeholder="请输入控费运维金额(万)" />
                </el-form-item>
              </el-col>

              <el-col :lg="6">
                <el-form-item label="控费总工时(人天)" prop="totalWorkHours">
                  <el-input v-model="form.totalWorkHours" placeholder="请输入控费总工时(人天)" />
                </el-form-item>
              </el-col>

              <el-col :lg="6">
                <el-form-item label="控费开发工时(人天)" prop="developmentWorkHours">
                  <el-input v-model="form.developmentWorkHours" placeholder="请输入控费开发工时(人天)" />
                </el-form-item>
              </el-col>

              <el-col :lg="6">
                <el-form-item label="控费项目工时(人天)" prop="projectWorkHours">
                  <el-input v-model="form.projectWorkHours" placeholder="请输入控费项目工时(人天)" />
                </el-form-item>
              </el-col>

              <el-col :lg="6">
                <el-form-item label="控费运维工时(人天)" prop="maintenanceWorkHours">
                  <el-input v-model="form.maintenanceWorkHours" placeholder="请输入控费运维工时(人天)" />
                </el-form-item>
              </el-col>
            </el-row>
          </el-card>
        </el-row>

        <!-- 运维信息区域 -->
        <el-row class="mb10">
          <el-card class="box-card" shadow="never">
            <div slot="header" class="clearfix">
              <span>运维信息</span>
            </div>
            <el-row :gutter="20">
              <el-col :lg="6">
                <el-form-item label="里程碑完成节点情况" prop="milestoneCompletion">
                  <el-input v-model="form.milestoneCompletion" placeholder="请输入里程碑完成节点情况" />
                </el-form-item>
              </el-col>

              <el-col :lg="6">
                <el-form-item label="一季度巡检报告" prop="inspectionQ1">
                  <el-input v-model="form.inspectionQ1" placeholder="请输入一季度巡检报告" />
                </el-form-item>
              </el-col>

              <el-col :lg="6">
                <el-form-item label="二季度巡检报告" prop="inspectionQ2">
                  <el-input v-model="form.inspectionQ2" placeholder="请输入二季度巡检报告" />
                </el-form-item>
              </el-col>

              <el-col :lg="6">
                <el-form-item label="三季度巡检报告" prop="inspectionQ3">
                  <el-input v-model="form.inspectionQ3" placeholder="请输入三季度巡检报告" />
                </el-form-item>
              </el-col>

              <el-col :lg="6">
                <el-form-item label="四季度巡检报告" prop="inspectionQ4">
                  <el-input v-model="form.inspectionQ4" placeholder="请输入四季度巡检报告" />
                </el-form-item>
              </el-col>

              <el-col :lg="6">
                <el-form-item label="维保开始时间" prop="maintenanceStartDate">
                  <el-date-picker v-model="form.maintenanceStartDate" type="datetime" placeholder="选择日期时间" style="width: 100%;"></el-date-picker>
                </el-form-item>
              </el-col>

              <el-col :lg="6">
                <el-form-item label="维保结束时间" prop="maintenanceEndDate">
                  <el-date-picker v-model="form.maintenanceEndDate" type="datetime" placeholder="选择日期时间" style="width: 100%;"></el-date-picker>
                </el-form-item>
              </el-col>

              <el-col :lg="6">
                <el-form-item label="中止运维时间" prop="maintenanceTerminationDate">
                  <el-date-picker v-model="form.maintenanceTerminationDate" type="datetime" placeholder="选择日期时间" style="width: 100%;"></el-date-picker>
                </el-form-item>
              </el-col>
            </el-row>
          </el-card>
        </el-row>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button type="text" @click="cancel">取 消</el-button>
        <el-button type="primary" @click="submitForm">确 定</el-button>
      </div>
    </el-dialog>

  </div>
</template>
<script>
import { 
  listProjectconfig,
  addProjectconfig,
  delProjectconfig,
  updateProjectconfig,
  getProjectconfig,
} from '@/api/business/projectconfig.js';

export default {
  name: "projectconfig",
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
        { index: 0, key: 'id', label: `自增主键`, checked:  true  },
        { index: 1, key: 'code', label: `项目编码`, checked:  true  },
        { index: 2, key: 'name', label: `项目名称`, checked:  true  },
        { index: 3, key: 'hospitalName', label: `所属医院`, checked:  true  },
        { index: 4, key: 'department', label: `建设科室`, checked:  true  },
        { index: 5, key: 'signingDate', label: `签订时间`, checked:  true  },
        { index: 6, key: 'projectType', label: `项目类型`, checked:  true  },
        { index: 7, key: 'projectStatus', label: `项目状态`, checked:  true  },
        { index: 8, key: 'projectLeader', label: `项目负责人`, checked:  true  },
        { index: 9, key: 'businessLeader', label: `商务负责人`, checked:  false  },
        { index: 10, key: 'presalesLeader', label: `售前负责人`, checked:  false  },
        { index: 11, key: 'totalControlFee', label: `控费总金额(万)`, checked:  false  },
        { index: 12, key: 'developmentControlFee', label: `控费开发金额(万)`, checked:  false  },
        { index: 13, key: 'projectControlFee', label: `控费项目金额(万)`, checked:  false  },
        { index: 14, key: 'maintenanceControlFee', label: `控费运维金额(万)`, checked:  false  },
        { index: 15, key: 'totalWorkHours', label: `控费总工时(人天)`, checked:  false  },
        { index: 16, key: 'developmentWorkHours', label: `控费开发工时(人天)`, checked:  false  },
        { index: 17, key: 'projectWorkHours', label: `控费项目工时(人天)`, checked:  false  },
        { index: 18, key: 'maintenanceWorkHours', label: `控费运维工时(人天)`, checked:  false  },
        { index: 19, key: 'milestoneCompletion', label: `里程碑完成节点情况`, checked:  false  },
        { index: 20, key: 'remarks', label: `备注`, checked:  false  },
        { index: 21, key: 'riskAnalysis', label: `控费风险分析说明`, checked:  false  },
        { index: 22, key: 'contractStatus', label: `合同状态`, checked:  false  },
        { index: 23, key: 'documentStatus', label: `资料状态`, checked:  false  },
        { index: 24, key: 'acceptanceReportStatus', label: `验收报告状态`, checked:  false  },
        { index: 25, key: 'inspectionQ1', label: `一季度巡检报告`, checked:  false  },
        { index: 26, key: 'inspectionQ2', label: `二季度巡检报告`, checked:  false  },
        { index: 27, key: 'inspectionQ3', label: `三季度巡检报告`, checked:  false  },
        { index: 28, key: 'inspectionQ4', label: `四季度巡检报告`, checked:  false  },
        { index: 29, key: 'maintenanceStartDate', label: `维保开始时间`, checked:  false  },
        { index: 30, key: 'maintenanceEndDate', label: `维保结束时间`, checked:  false  },
        { index: 31, key: 'maintenanceTerminationDate', label: `中止运维时间`, checked:  false  },
        { index: 32, key: 'updateTime', label: `修改时间`, checked:  false  },
       
      ],
      // 项目类型（可选值：全新建设、运维）选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
      projectTypeOptions: [{ dictLabel: '全新建设', dictValue: '全新建设'},{ dictLabel: '运维', dictValue: '运维'}],
      // 项目状态（可选值：备案、在建、运维、已结束）选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
      projectStatusOptions: [{ dictLabel: '备案', dictValue: '备案'},{ dictLabel: '在建', dictValue: '在建'},{ dictLabel: '运维', dictValue: '运维'},{ dictLabel: '已结束', dictValue: '已结束'}],
      // 控费风险分析说明选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
      riskAnalysisOptions: [],
      // 合同状态（可选值：有、待签、已签、待招标、售前、合同中、已结束、中止运维）选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
      contractStatusOptions: [{ dictLabel: '有', dictValue: '有'},{ dictLabel: '待签', dictValue: '待签'},{ dictLabel: '已签', dictValue: '已签'},{ dictLabel: '待招标', dictValue: '待招标'},{ dictLabel: '售前', dictValue: '售前'},{ dictLabel: '合同中', dictValue: '合同中'},{ dictLabel: '已结束', dictValue: '已结束'},{ dictLabel: '中止运维', dictValue: '中止运维'}],
      // 资料状态（可选值：挂起、无、公司挂起）选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
      documentStatusOptions: [{ dictLabel: '挂起', dictValue: '挂起'},{ dictLabel: '无', dictValue: '无'},{ dictLabel: '公司挂起', dictValue: '公司挂起'}],
      // 验收报告状态（可选值：有、无）选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
      acceptanceReportStatusOptions: [{ dictLabel: '有', dictValue: '有'},{ dictLabel: '无', dictValue: '无'}],
      // 是否删除(0否1是)选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
      isDeletedOptions: [{ dictLabel: '否', dictValue: '0'},{ dictLabel: '是', dictValue: '1'}],
      // 数据列表
      dataList: [],
      // 总记录数
      total: 0,
      // 提交按钮是否显示
      btnSubmitVisible: true,
      // 表单校验
      rules: {
        
      },
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
      this.loading = true;
      listProjectconfig(this.queryParams).then(res => {
         if (res.code == 200) {
           this.dataList = res.data.result;
           this.total = res.data.totalNum;
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
        code: undefined,
        name: undefined,
        hospitalName: undefined,
        department: undefined,
        signingDate: undefined,
        projectType: undefined,
        projectStatus: undefined,
        projectLeader: undefined,
        businessLeader: undefined,
        presalesLeader: undefined,
        totalControlFee: undefined,
        developmentControlFee: undefined,
        projectControlFee: undefined,
        maintenanceControlFee: undefined,
        totalWorkHours: undefined,
        developmentWorkHours: undefined,
        projectWorkHours: undefined,
        maintenanceWorkHours: undefined,
        milestoneCompletion: undefined,
        remarks: undefined,
        riskAnalysis: undefined,
        contractStatus: undefined,
        documentStatus: undefined,
        acceptanceReportStatus: undefined,
        inspectionQ1: undefined,
        inspectionQ2: undefined,
        inspectionQ3: undefined,
        inspectionQ4: undefined,
        maintenanceStartDate: undefined,
        maintenanceEndDate: undefined,
        maintenanceTerminationDate: undefined,
        createdBy: undefined,
        updatedBy: undefined,
        creationTime: undefined,
        updateTime: undefined,
        isDeleted: undefined,
      };
      this.resetForm("form");
    },
    // 重置查询操作
    resetQuery() {
      this.timeRange = [];
      this.resetForm("queryForm");
      this.handleQuery();
    },
    // 多选框选中数据
    handleSelectionChange(selection) {
      this.ids = selection.map((item) => item.id);
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
      this.reset();
      this.open = true;
      this.title = "添加";
      this.opertype = 1;
    },
    /** 删除按钮操作 */
    handleDelete(row) {
      const Ids = row.id || this.ids;

      this.$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？')
        .then(function () {
          return delProjectconfig(Ids);
        })
        .then(() => {
          this.handleQuery();
          this.msgSuccess("删除成功");
        })
        .catch(() => {});
    },
    /** 修改按钮操作 */
    handleUpdate(row) {
      this.reset();
      const id = row.id || this.ids;
      getProjectconfig(id).then((res) => {
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
    // 项目类型（可选值：全新建设、运维）字典翻译
    projectTypeFormat(row, column) {
      return this.selectDictLabel(this.projectTypeOptions, row.projectType);
    },
    // 项目状态（可选值：备案、在建、运维、已结束）字典翻译
    projectStatusFormat(row, column) {
      return this.selectDictLabel(this.projectStatusOptions, row.projectStatus);
    },
    // 控费风险分析说明字典翻译
    riskAnalysisFormat(row, column) {
      return this.selectDictLabel(this.riskAnalysisOptions, row.riskAnalysis);
    },
    // 合同状态（可选值：有、待签、已签、待招标、售前、合同中、已结束、中止运维）字典翻译
    contractStatusFormat(row, column) {
      return this.selectDictLabel(this.contractStatusOptions, row.contractStatus);
    },
    // 资料状态（可选值：挂起、无、公司挂起）字典翻译
    documentStatusFormat(row, column) {
      return this.selectDictLabel(this.documentStatusOptions, row.documentStatus);
    },
    // 验收报告状态（可选值：有、无）字典翻译
    acceptanceReportStatusFormat(row, column) {
      return this.selectDictLabel(this.acceptanceReportStatusOptions, row.acceptanceReportStatus);
    },
    // 是否删除(0否1是)字典翻译
    isDeletedFormat(row, column) {
      return this.selectDictLabel(this.isDeletedOptions, row.isDeleted);
    },
    /** 提交按钮 */
    submitForm: function () {
      this.$refs["form"].validate((valid) => {
        if (valid) {
          console.log(JSON.stringify(this.form));
          
          if (this.form.id != undefined && this.opertype === 2) {
            updateProjectconfig(this.form)
              .then((res) => {
                this.msgSuccess("修改成功");
                this.open = false;
                this.getList();
            })
            .catch((err) => {
                //TODO 错误逻辑
              });
          } else {
            addProjectconfig(this.form)
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

<style scoped>
.mb10 {
  margin-bottom: 10px;
}

.box-card {
  width: 100%;
}

.clearfix:before,
.clearfix:after {
  display: table;
  content: "";
}

.clearfix:after {
  clear: both;
}

.el-card__header {
  padding: 10px 20px;
  background-color: #409eff;
  border-bottom: 1px solid #ebeef5;
  color: white;
  font-weight: bold;
}
</style>







