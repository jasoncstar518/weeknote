<template>
  <div>
    <div class="navbar">
      <!-- 展开折叠左边菜单栏 -->
      <hamburger id="hamburger-container" class="hamburger-container" :is-active="sidebar.opened" @toggleClick="toggleSideBar" />

      <!-- 面包屑导航 -->
      <breadcrumb id="breadcrumb-container" class="breadcrumb-container" v-if="!topNav" />
      <top-nav id="topmenu-container" class="topmenu-container" v-if="topNav" />

      <div class="right-menu">
        <template v-if="device!=='mobile'">
          <search id="header-search" class="right-menu-item" />
          <screenfull id="screenfull" class="right-menu-item hover-effect" />
          <!-- <el-tooltip content="布局大小" effect="dark" placement="bottom"> -->
          <size-select id="size-select" class="right-menu-item hover-effect" />
          <!-- </el-tooltip> -->
          <el-tooltip content="源码地址" effect="dark" placement="bottom">
            <zr-git class="right-menu-item hover-effect" />
          </el-tooltip>
          <el-tooltip content="医院远程信息" effect="dark" placement="bottom">
            <zr-doc class="right-menu-item hover-effect" />
          </el-tooltip>
          <el-tooltip content="Git提交规范" effect="dark" placement="bottom">
            <i class="el-icon-document right-menu-item hover-effect" @click="showCommitGuidelines" style="cursor: pointer;" />
          </el-tooltip>
        </template>
        <!-- 通知 -->
        <div class="right-menu-item">
          <el-popover placement="bottom" trigger="click" v-model="isShowUserNewsPopover" width="300" popper-class="el-popover-pupop-user-news">
            <el-badge @click.stop="isShowUserNewsPopover = !isShowUserNewsPopover" :is-dot="true" slot="reference">
              <i class="el-icon-bell" title="通知"></i>
            </el-badge>
            <Notice v-show="isShowUserNewsPopover" />
          </el-popover>
        </div>

        <el-dropdown class="avatar-container right-menu-item hover-effect" trigger="click">
          <div class="avatar-wrapper">
            <img :src="avatar" class="user-avatar">
            <i class="el-icon-caret-bottom" />
          </div>
          <el-dropdown-menu slot="dropdown">
            <router-link to="/user/profile">
              <el-dropdown-item>个人中心</el-dropdown-item>
            </router-link>
            <el-dropdown-item @click.native="setting = true">
              <span>布局设置</span>
            </el-dropdown-item>
            <el-dropdown-item divided @click.native="logout">
              <span>退出登录</span>
            </el-dropdown-item>
          </el-dropdown-menu>
        </el-dropdown>
      </div>
    </div>
    
    <!-- Git提交规范模态框 -->
    <el-dialog title="代码提交规范" :visible.sync="commitGuidelinesVisible" width="600px" :modal="false">
      <div style="line-height: 1; max-height: 800px; overflow-y: auto;">
        <h3>类型枚举值：</h3>
        <ul>
          <li><strong>add</strong>（新增功能）</li>
          <li><strong>bugfix</strong>（修复缺陷）</li>
          <li><strong>update</strong>（优化改进）</li>
        </ul>
        <h3>格式模板：</h3>
        <p><strong>类型(模块>接口): 简要说明</strong></p>
        <p><strong>注意事项：</strong>（数据库/配置变更时填写）</p>
        <p><strong>禅道链接：</strong>（需求/bug链接）</p>
        <h3>示例：</h3>
        <div class="commit-example">
          <el-button type="primary" size="mini" @click="copyCommitExample(commitExamples[0])" style="margin-bottom: 10px;">复制示例1</el-button>
          <div style="background: #f5f7fa; padding: 10px; border-radius: 4px; margin-bottom: 15px;">
            <pre style="margin: 0; white-space: pre-wrap;">{{ commitExamples[0] }}</pre>
          </div>
        </div>
        
        <div class="commit-example">
          <el-button type="primary" size="mini" @click="copyCommitExample(commitExamples[1])" style="margin-bottom: 10px;">复制示例2</el-button>
          <div style="background: #f5f7fa; padding: 10px; border-radius: 4px; margin-bottom: 15px;">
            <pre style="margin: 0; white-space: pre-wrap;">{{ commitExamples[1] }}</pre>
          </div>
        </div>
        
        <div class="commit-example">
          <el-button type="primary" size="mini" @click="copyCommitExample(commitExamples[2])" style="margin-bottom: 10px;">复制示例3</el-button>
          <div style="background: #f5f7fa; padding: 10px; border-radius: 4px;">
            <pre style="margin: 0; white-space: pre-wrap;">{{ commitExamples[2] }}</pre>
          </div>
        </div>
      </div>
      <span slot="footer" class="dialog-footer">
        <el-button @click="commitGuidelinesVisible = false">关闭</el-button>
      </span>
    </el-dialog>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import Breadcrumb from "@/components/Breadcrumb";
import TopNav from "@/components/TopNav";
import Hamburger from "@/components/Hamburger";
import Screenfull from "@/components/Screenfull";
import SizeSelect from "@/components/SizeSelect";
import Search from "@/components/HeaderSearch";
import ZrGit from "@/components/Zr/Git";
import ZrDoc from "@/components/Zr/Doc";
import Notice from "@/components/Notice/Index.vue";

export default {
  components: {
    Breadcrumb,
    TopNav,
    Hamburger,
    Screenfull,
    SizeSelect,
    Search,
    ZrGit,
    ZrDoc,
    Notice,
  },
  data() {
    return {
      isShowUserNewsPopover: false,
      commitGuidelinesVisible: false,
      commitExamples: [
        "add(PC > 合理用药 > 获取药品说明书): 新增接口：获取药品说明书。\n注意事项：需要配置全局参数XXXX。\n禅道链接：http://zentao.szxys.cn/zentao/task-view-11324.html",
        "bugfix(患者小程序 > 处方 > 获取咨询量): 无法获取咨询量。原因是后台接口查询字段错误。\n注意事项：XXXX表XX字段 改成 XXXX表YY字段。\n禅道链接：http://zentao.szxys.cn/zentao/task-view-11324.html",
        "update(医生小程序 > 诊断 > 诊断搜索): 优化诊断搜索接口执行效率。优化方法：加入本地缓存机制。\n禅道链接：http://zentao.szxys.cn/zentao/task-view-11324.html"
      ]
    };
  },
  computed: {
    ...mapGetters(["sidebar", "avatar", "device"]),
    setting: {
      get() {
        return this.$store.state.settings.showSettings;
      },
      set(val) {
        this.$store.dispatch("settings/changeSetting", {
          key: "showSettings",
          value: val,
        });
      },
    },
    topNav: {
      get() {
        return this.$store.state.settings.topNav;
      },
    },
  },
  methods: {
    toggleSideBar() {
      this.$store.dispatch("app/toggleSideBar");
    },
    async logout() {
      this.$confirm("确定注销并退出系统吗？", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      }).then(() => {
        this.$store.dispatch("LogOut").then(() => {
          location.href = process.env.VUE_APP_ROUTER_PREFIX + "index";
        });
      });
    },
    showCommitGuidelines() {
      this.commitGuidelinesVisible = true;
    },
    copyCommitExample(text) {
      const textarea = document.createElement('textarea');
      textarea.value = text;
      document.body.appendChild(textarea);
      textarea.select();
      document.execCommand('copy');
      document.body.removeChild(textarea);
      
      this.$message({
        message: '复制成功',
        type: 'success',
        duration: 1000
      });
    }
  },
};
</script>

<style lang="scss" scoped>
.el-menu {
  display: inline-table;
  line-height: 46px !important;
  .el-menu-item {
    vertical-align: center;
  }
}

.navbar {
  height: 50px;
  overflow: hidden;
  position: relative;
  background: #fff;
  box-shadow: 0 1px 4px rgba(0, 21, 41, 0.08);

  .hamburger-container {
    line-height: 46px;
    height: 100%;
    float: left;
    cursor: pointer;
    transition: background 0.3s;
    -webkit-tap-highlight-color: transparent;

    &:hover {
      background: rgba(0, 0, 0, 0.025);
    }
  }

  .breadcrumb-container {
    float: left;
  }

  .topmenu-container {
    position: absolute;
    left: 50px;
  }

  .errLog-container {
    display: inline-block;
    vertical-align: top;
  }

  .right-menu {
    display: flex;
    align-items: center;
    justify-content: flex-end;

    &:focus {
      outline: none;
    }

    .right-menu-item {
      display: inline-block;
      padding: 0 8px;
      height: 100%;
      font-size: 18px;
      color: #5a5e66;
      vertical-align: text-bottom;

      &.hover-effect {
        cursor: pointer;
        transition: background 0.3s;

        &:hover {
          background: rgba(0, 0, 0, 0.025);
        }
      }
    }

    .avatar-container {
      margin-right: 30px;

      .avatar-wrapper {
        margin-top: 5px;
        position: relative;

        .user-avatar {
          cursor: pointer;
          width: 40px;
          height: 40px;
          border-radius: 10px;
        }

        .el-icon-caret-bottom {
          cursor: pointer;
          position: absolute;
          right: -20px;
          top: 25px;
          font-size: 12px;
        }
      }
    }
  }
}

.commit-example {
  margin-bottom: 20px;
  
  pre {
    font-family: 'Courier New', monospace;
    font-size: 12px;
    line-height: 1.5;
  }
}
</style>