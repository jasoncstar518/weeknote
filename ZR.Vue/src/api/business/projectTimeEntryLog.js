import request from '@/utils/request'

/**
* 钉钉日志记录分页查询
* @param {查询条件} data
*/
export function listProjectTimeEntryLog(query) {
  return request({
    url: 'business/ProjectTimeEntryLog/list',
    method: 'get',
    params: query,
  })
}


/**
* 新增钉钉日志记录
* @param data
*/
export function addProjectTimeEntryLog(data) {
  return request({
    url: 'business/ProjectTimeEntryLog',
    method: 'post',
    data: data,
  })
}

/**
* 修改钉钉日志记录
* @param data
*/
export function updateProjectTimeEntryLog(data) {
  return request({
    url: 'business/ProjectTimeEntryLog',
    method: 'PUT',
    data: data,
  })
}

/**
* 获取钉钉日志记录详情
* @param {Id}
*/
export function getProjectTimeEntryLog(id) {
  return request({
    url: 'business/ProjectTimeEntryLog/' + id,
    method: 'get'
  })
}

/**
* 删除钉钉日志记录
* @param {主键} pid
*/
export function delProjectTimeEntryLog(pid) {
  return request({
    url: 'business/ProjectTimeEntryLog/' + pid,
    method: 'delete'
  })
}

// 导出钉钉日志记录
export function exportProjectTimeEntryLog(query) {
  return request({
    url: 'business/ProjectTimeEntryLog/export',
    method: 'get',
    params: query
  })
}

