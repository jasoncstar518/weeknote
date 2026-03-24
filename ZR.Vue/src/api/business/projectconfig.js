import request from '@/utils/request'

/**
* 项目配置表分页查询
* @param {查询条件} data
*/
export function listProjectconfig(query) {
  return request({
    url: 'business/Projectconfig/list',
    method: 'get',
    params: query,
  })
}


/**
* 新增项目配置表
* @param data
*/
export function addProjectconfig(data) {
  return request({
    url: 'business/Projectconfig',
    method: 'post',
    data: data,
  })
}

/**
* 修改项目配置表
* @param data
*/
export function updateProjectconfig(data) {
  return request({
    url: 'business/Projectconfig',
    method: 'PUT',
    data: data,
  })
}

/**
* 获取项目配置表详情
* @param {Id}
*/
export function getProjectconfig(id) {
  return request({
    url: 'business/Projectconfig/' + id,
    method: 'get'
  })
}

/**
* 删除项目配置表
* @param {主键} pid
*/
export function delProjectconfig(pid) {
  return request({
    url: 'business/Projectconfig/' + pid,
    method: 'delete'
  })
}

// 导出项目配置表
export function exportProjectconfig(query) {
  return request({
    url: 'business/Projectconfig/export',
    method: 'get',
    params: query
  })
}

