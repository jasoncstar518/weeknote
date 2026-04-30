<!--
 * @Descripttion: (项目配置表/selproject)
 * @version: (1.0)
 * @Author: (admin)
 * @Date: (2026-04-30)
 * @LastEditors: (admin)
 * @LastEditTime: (2026-04-30)
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
        <el-button type="primary" v-hasPermi="['business:selproject:add']" plain icon="el-icon-plus" size="mini" @click="handleAdd">新增</el-button>
      </el-col>
      <el-col :span="1.5">
        <el-button type="success" :disabled="single" v-hasPermi="['business:selproject:edit']" plain icon="el-icon-edit" size="mini" @click="handleUpdate">修改</el-button>
      </el-col>
      <el-col :span="1.5">
        <el-button type="danger" :disabled="multiple" v-hasPermi="['business:selproject:delete']" plain icon="el-icon-delete" size="mini" @click="handleDelete">删除</el-button>
      </el-col>
      <right-toolbar :showSearch.sync="showSearch" @queryTable="getList"></right-toolbar>
    </el-row>

    <!-- 数据区域 -->
    <el-table :data="dataList" v-loading="loading" ref="table" border highlight-current-row @sort-change="sortChange" @selection-change="handleSelectionChange">
      <el-table-column type="selection" width="50" align="center"/>
      <el-table-column prop="id" label="自增主键" align="center" />
      <el-table-column prop="code" label="项目编码" align="center" :show-overflow-tooltip="true" />
      <el-table-column prop="name" label="项目名称" align="center" :show-overflow-tooltip="true" />
      <el-table-column prop="hospitalName" label="所属医院" align="center" :show-overflow-tooltip="true" />
      <el-table-column prop="remarks" label="备注" align="center" :show-overflow-tooltip="true" />
      <el-table-column prop="createdBy" label="创建用户ID" align="center" />
      <el-table-column prop="updatedBy" label="修改人用户ID" align="center" />
      <el-table-column prop="creationTime" label="创建时间" align="center" :show-overflow-tooltip="true" />
      <el-table-column prop="isDeleted" label="是否删除(0否1是)" align="center">
        <template slot-scope="scope">
          <dict-tag :options="isDeletedOptions" :value="scope.row.isDeleted" />
        </template>
      </el-table-column>

      <el-table-column label="操作" align="center" width="140">
        <template slot-scope="scope">
          <el-button size="mini" v-hasPermi="['business:selproject:edit']" type="success" icon="el-icon-edit" title="编辑" 
            @click="handleUpdate(scope.row)"></el-button>      
          <el-button size="mini" v-hasPermi="['business:selproject:delete']" type="danger" icon="el-icon-delete" title="删除" 
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination class="mt10" background :total="total" :page.sync="queryParams.pageNum" :limit.sync="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改项目配置表对话框 -->
    <el-dialog :title="title" :lock-scroll="false" :visible.sync="open" >
      <el-form ref="form" :model="form" :rules="rules" :label-width="formLabelWidth">
        <el-row :gutter="20">
            
          <el-col :lg="12" v-if="opertype == 2">
            <el-form-item label="自增主键">{{form.id}}</el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="项目编码" prop="code">
              <el-input v-model="form.code" placeholder="请输入项目编码" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="项目名称" prop="name">
              <el-input v-model="form.name" placeholder="请输入项目名称" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="所属医院" prop="hospitalName">
              <el-input v-model="form.hospitalName" placeholder="请输入所属医院" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="备注" prop="remarks">
              <el-input v-model="form.remarks" placeholder="请输入备注" />
            </el-form-item>
          </el-col>
    
          <el-col :lg="12">
            <el-form-item label="创建用户ID" prop="createdBy">
              <el-input v-model="form.createdBy" placeholder="请输入创建用户ID" />
            </el-form-item>
          </el-col>
    
          <el-col :lg="12">
            <el-form-item label="修改人用户ID" prop="updatedBy">
              <el-input v-model="form.updatedBy" placeholder="请输入修改人用户ID" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="创建时间" prop="creationTime">
              <el-date-picker v-model="form.creationTime" type="datetime" placeholder="选择日期时间"></el-date-picker>
            </el-form-item>
          </el-col>


          <el-col :lg="12">
            <el-form-item label="是否删除(0否1是)" prop="isDeleted">
              <el-radio-group v-model="form.isDeleted">
                <el-radio v-for="item in isDeletedOptions" :key="item.dictValue" :label="item.dictValue">{{item.dictLabel}}</el-radio>
              </el-radio-group>
            </el-form-item>
          </el-col>

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
  listSelproject,
  addSelproject,
  delSelproject,
  updateSelproject,
  getSelproject,
} from '@/api/business/selproject.js';

export default {
  name: "selproject",
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
        { index: 4, key: 'remarks', label: `备注`, checked:  true  },
        { index: 5, key: 'createdBy', label: `创建用户ID`, checked:  true  },
        { index: 6, key: 'updatedBy', label: `修改人用户ID`, checked:  true  },
        { index: 7, key: 'creationTime', label: `创建时间`, checked:  true  },
        { index: 8, key: 'updateTime', label: `修改时间`, checked:  true  },
        { index: 9, key: 'isDeleted', label: `是否删除(0否1是)`, checked:  false  },
      ],
      // 是否删除(0否1是)选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
      isDeletedOptions: [],
      // 数据列表
      dataList: [],
      // 总记录数
      total: 0,
      // 提交按钮是否显示
      btnSubmitVisible: true,
      // 表单校验
      rules: {
        creationTime: [
          { required: true, message: "创建时间不能为空", trigger: "blur" }
        ],
        updateTime: [
          { required: true, message: "修改时间不能为空", trigger: "blur" }
        ],
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
      listSelproject(this.queryParams).then(res => {
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
        remarks: undefined,
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
          return delSelproject(Ids);
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
      getSelproject(id).then((res) => {
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
            updateSelproject(this.form)
              .then((res) => {
                this.msgSuccess("修改成功");
                this.open = false;
                this.getList();
            })
            .catch((err) => {
                //TODO 错误逻辑
              });
          } else {
            addSelproject(this.form)
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