import request from '@/utils/request'

/**
* 日报记录表分页查询
* @param {查询条件} data
*/
export function listDailyreport(query) {
  return request({
    url: 'business/Dailyreport/list',
    method: 'get',
    params: query,
  })
}


/**
* 新增日报记录表
* @param data
*/
export function addDailyreport(data) {
  return request({
    url: 'business/Dailyreport',
    method: 'post',
    data: data,
  })
}

/**
* 修改日报记录表
* @param data
*/
export function updateDailyreport(data) {
  return request({
    url: 'business/Dailyreport',
    method: 'PUT',
    data: data,
  })
}

/**
* 获取日报记录表详情
* @param {Id}
*/
export function getDailyreport(id) {
  return request({
    url: 'business/Dailyreport/' + id,
    method: 'get'
  })
}

/**
* 删除日报记录表
* @param {主键} pid
*/
export function delDailyreport(pid) {
  return request({
    url: 'business/Dailyreport/' + pid,
    method: 'delete'
  })
}

// 导出日报记录表
export function exportDailyreport(query) {
  return request({
    url: 'business/Dailyreport/export',
    method: 'get',
    params: query
  })
}

