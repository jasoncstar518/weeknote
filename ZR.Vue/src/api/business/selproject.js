import request from '@/utils/request'

/**
* 项目配置表分页查询
* @param {查询条件} data
*/
export function listSelproject(query) {
  return request({
    url: 'business/Selproject/list',
    method: 'get',
    params: query,
  })
}


/**
* 新增项目配置表
* @param data
*/
export function addSelproject(data) {
  return request({
    url: 'business/Selproject',
    method: 'post',
    data: data,
  })
}

/**
* 修改项目配置表
* @param data
*/
export function updateSelproject(data) {
  return request({
    url: 'business/Selproject',
    method: 'PUT',
    data: data,
  })
}

/**
* 获取项目配置表详情
* @param {Id}
*/
export function getSelproject(id) {
  return request({
    url: 'business/Selproject/' + id,
    method: 'get'
  })
}

/**
* 删除项目配置表
* @param {主键} pid
*/
export function delSelproject(pid) {
  return request({
    url: 'business/Selproject/' + pid,
    method: 'delete'
  })
}

// 导出项目配置表
export function exportSelproject(query) {
  return request({
    url: 'business/Selproject/export',
    method: 'get',
    params: query
  })
}

