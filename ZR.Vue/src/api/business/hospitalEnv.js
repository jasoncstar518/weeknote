import request from '@/utils/request'

/**
 * 获取医院环境版本列表
 * @param query
 */
export function listHospitalEnv(query) {
  return request({
    url: 'business/hospitalEnv/list',
    method: 'get',
    params: query
  })
}

/**
 * 获取医院环境版本详情
 * @param id
 */
export function getHospitalEnv(id) {
  return request({
    url: 'business/hospitalEnv/' + id,
    method: 'get'
  })
}

/**
 * 新增医院环境版本
 * @param data
 */
export function addHospitalEnv(data) {
  return request({
    url: 'business/hospitalEnv',
    method: 'post',
    data: data
  })
}

/**
 * 更新医院环境版本
 * @param data
 */
export function updateHospitalEnv(data) {
  return request({
    url: 'business/hospitalEnv',
    method: 'put',
    data: data
  })
}

/**
 * 删除医院环境版本
 * @param id
 */
export function delHospitalEnv(id) {
  return request({
    url: 'business/hospitalEnv/' + id,
    method: 'delete'
  })
}

/**
 * 获取所有可用版本列表
 */
export function listAllVersions() {
  return request({
    url: 'business/hospitalEnv/versions',
    method: 'get'
  })
}

/**
 * 批量保存环境版本
 * @param data
 */
export function batchSaveEnvVersion(data) {
  return request({
    url: 'business/hospitalEnv/batchSave',
    method: 'post',
    data: data
  })
}