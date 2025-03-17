import { defineStore } from 'pinia'

interface UserDefault {
  password: string
}

interface ConfigState {
  userDefault: UserDefault
}

export const useConfigStore = defineStore('config', {
  state: (): ConfigState => ({
    userDefault: {
      password: '123456' // 默认密码
    }
  }),
  actions: {
    // 如果需要，这里可以添加修改配置的方法
  }
}) 