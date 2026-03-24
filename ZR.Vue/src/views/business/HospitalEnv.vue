<template>
  <div class="app-container">
    <!-- 搜索区域 -->
    <el-form :model="queryParams" inline ref="queryForm" v-show="showSearch" label-width="80px">
      <el-form-item label="医院名称" prop="hospitalName">
        <el-input v-model="queryParams.hospitalName" placeholder="请输入医院名称" clearable size="small" style="width: 200px" @keyup.enter.native="handleQuery" />
      </el-form-item>
      <el-form-item>
        <el-button type="primary" icon="el-icon-search" size="mini" @click="handleQuery">搜索</el-button>
        <el-button icon="el-icon-refresh" size="mini" @click="resetQuery">重置</el-button>
      </el-form-item>
    </el-form>

    <!-- 工具区域 -->
    <el-row :gutter="10" class="mb8">
      <el-col :span="1.5">
        <el-button type="primary" plain icon="el-icon-plus" size="mini" @click="handleAdd" v-hasPermi="['business:hospitalenv:add']">新增</el-button>
      </el-col>
     
    
      <right-toolbar :showSearch.sync="showSearch" @queryTable="getList"></right-toolbar>
    </el-row>

    <!-- 卡片列表 -->
    <el-row :gutter="20" v-loading="loading">
      <el-col :span="6" v-for="item in dataList" :key="item.id" class="card-col">
        <el-card class="hospital-card" shadow="hover">
          <div slot="header" class="card-header">
            <span class="hospital-name">{{ item.hospitalName }}</span>
            <el-tag size="mini" :type="item.status === '1' ? 'success' : 'info'">{{ item.status === '1' ? '启用' : '禁用' }}</el-tag>
          </div>
          <div class="card-body">
            <div class="info-item">
              <span class="label label-lg">医院编码：</span>
              <span class="value">{{ item.hospitalCode || '-' }}</span>
            </div>
            <div class="info-item version-item">
              <span class="label label-lg">正式环境版本：</span>
              <el-input v-model="item.prodVersion" placeholder="请输入版本" size="small" style="width: 320px"  />
            </div>
            <div class="info-item version-item">
              <span class="label label-lg">测试环境版本：</span>
              <el-input v-model="item.testVersion" placeholder="请输入版本" size="small" style="width: 320px"  />
            </div>
            <div class="info-item remark-item" v-if="item.remark">
              <span class="label">备注：</span>
              <span class="value remark-text" @click="showRemarkDialog(item)">{{ item.remark }}</span>
            </div>
            <div class="info-item" v-else>
              <span class="label">备注：</span>
              <span class="value text-gray">-</span>
            </div>
          </div>
          <div class="card-footer">
            <el-button size="mini" type="text" icon="el-icon-edit" @click="handleUpdate(item)" v-hasPermi="['business:hospitalenv:edit']">编辑</el-button>
          </div>
        </el-card>
      </el-col>
    </el-row>

    <!-- 无数据提示 -->
    <el-empty v-if="!loading && dataList.length === 0" description="暂无数据"></el-empty>

    <pagination background :total="total" :page.sync="queryParams.pageNum" :limit.sync="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改对话框 -->
    <el-dialog :title="title" :visible.sync="open" width="500px" append-to-body>
      <el-form ref="form" :model="form" :rules="rules" label-width="110px">
        <el-form-item label="医院名称" prop="hospitalName">
          <el-input v-model="form.hospitalName" placeholder="请输入医院名称" />
        </el-form-item>
        <el-form-item label="医院编码" prop="hospitalCode">
          <el-input v-model="form.hospitalCode" placeholder="请输入医院编码" />
        </el-form-item>
        <el-form-item label="正式环境版本" prop="prodVersion">
          <el-input v-model="form.prodVersion" placeholder="请输入正式环境版本" />
        </el-form-item>
        <el-form-item label="测试环境版本" prop="testVersion">
          <el-input v-model="form.testVersion" placeholder="请输入测试环境版本" />
        </el-form-item>
        <el-form-item label="状态" prop="status">
          <el-radio-group v-model="form.status">
            <el-radio label="1">启用</el-radio>
            <el-radio label="0">禁用</el-radio>
          </el-radio-group>
        </el-form-item>
        <el-form-item label="备注" prop="remark">
          <el-input v-model="form.remark" type="textarea" :rows="3" placeholder="请输入备注" />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button type="primary" @click="submitForm">确 定</el-button>
        <el-button @click="cancel">取 消</el-button>
      </div>
    </el-dialog>

    <!-- 备注查看弹窗 -->
    <el-dialog title="备注详情" :visible.sync="remarkDialogVisible" width="600px" append-to-body>
      <div class="remark-content">{{ currentRemark }}</div>
      <div slot="footer" class="dialog-footer">
        <el-button @click="remarkDialogVisible = false">关 闭</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import {
  listHospitalEnv,
  getHospitalEnv,
  addHospitalEnv,
  updateHospitalEnv,
  delHospitalEnv
} from "@/api/business/hospitalEnv";

export default {
  name: "HospitalEnv",
  data() {
    return {
      loading: true,
      ids: [],
      single: true,
      multiple: true,
      showSearch: true,
      total: 0,
      dataList: [],
      title: "",
      open: false,
      remarkDialogVisible: false,
      currentRemark: "",
      queryParams: {
        pageNum: 1,
        pageSize: 20,
        hospitalName: undefined
      },
      form: {},
      rules: {
        hospitalName: [
          { required: true, message: "医院名称不能为空", trigger: "blur" }
        ],
        hospitalCode: [
          { required: true, message: "医院编码不能为空", trigger: "blur" }
        ]
      }
    };
  },
  created() {
    this.getList();
  },
  methods: {
    getList() {
      this.loading = true;
      listHospitalEnv(this.queryParams).then(response => {
        if (response.code == 200) {
          this.dataList = response.data.result;
          this.total = response.data.totalNum;
        }
        this.loading = false;
      });
    },
    handleVersionChange(row) {
      const data = {
        id: row.id,
        prodVersion: row.prodVersion,
        testVersion: row.testVersion
      };
      updateHospitalEnv(data).then(response => {
        if (response.code == 200) {
          this.msgSuccess("版本更新成功");
        }
      });
    },
    showRemarkDialog(item) {
      this.currentRemark = item.remark;
      this.remarkDialogVisible = true;
    },
    cancel() {
      this.open = false;
      this.reset();
    },
    reset() {
      this.form = {
        id: undefined,
        hospitalName: undefined,
        hospitalCode: undefined,
        prodVersion: undefined,
        testVersion: undefined,
        status: "1",
        remark: undefined
      };
      this.resetForm("form");
    },
    handleQuery() {
      this.queryParams.pageNum = 1;
      this.getList();
    },
    resetQuery() {
      this.resetForm("queryForm");
      this.handleQuery();
    },
    handleSelectionChange(selection) {
      this.ids = selection.map(item => item.id);
      this.single = selection.length !== 1;
      this.multiple = !selection.length;
    },
    handleAdd() {
      this.reset();
      this.open = true;
      this.title = "添加医院环境";
    },
    handleUpdate(row) {
      this.reset();
      const id = row.id || this.ids;
      getHospitalEnv(id).then(response => {
        if (response.code == 200) {
          this.form = response.data;
          this.open = true;
          this.title = "修改医院环境";
        }
      });
    },
    submitForm: function() {
      this.$refs["form"].validate(valid => {
        if (valid) {
          if (this.form.id !== undefined) {
            updateHospitalEnv(this.form).then(response => {
              if (response.code == 200) {
                this.msgSuccess("修改成功");
                this.open = false;
                this.getList();
              }
            });
          } else {
            addHospitalEnv(this.form).then(response => {
              if (response.code == 200) {
                this.msgSuccess("新增成功");
                this.open = false;
                this.getList();
              }
            });
          }
        }
      });
    },
    handleDelete(row) {
      const ids = row.id || this.ids;
      this.$confirm('是否确认删除医院环境编号为"' + ids + '"的数据项?')
        .then(function() {
          return delHospitalEnv(ids);
        })
        .then(() => {
          this.getList();
          this.msgSuccess("删除成功");
        })
        .catch(() => {});
    }
  }
};
</script>

<style scoped>
.mb8 {
  margin-bottom: 8px;
}
.card-col {
  margin-bottom: 20px;
}
.hospital-card {
  height: 100%;
}
.card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}
.hospital-name {
  font-weight: bold;
  font-size: 28px;
}
.card-body {
  padding: 10px 0;
}
.info-item {
  display: flex;
  align-items: center;
  margin-bottom: 14px;
}
.info-item .label {
  color: #666;
  font-size: 14px;
  flex-shrink: 0;
}
.info-item .label-lg {
  font-size: 15px;
  font-weight: 500;
}
.info-item .value {
  font-size: 13px;
}
.remark-text {
  color: #409eff;
  cursor: pointer;
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
  max-width: 180px;
}
.remark-text:hover {
  text-decoration: underline;
}
.text-gray {
  color: #999;
}
.card-footer {
  border-top: 1px solid #eee;
  padding-top: 10px;
  text-align: right;
}
.text-danger {
  color: #f56c6c;
}
.remark-content {
  padding: 10px;
  line-height: 1.8;
  white-space: pre-wrap;
  word-break: break-all;
}
</style>