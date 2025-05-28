<template>
  <div class="database-connection">
    <a-form-item :label="t('identity.tenant.database.fields.dbType')" required>
      <a-select
        v-model:value="formData.dbType"
        :options="dbTypeOptions"
        :placeholder="t('identity.tenant.database.fields.typePlaceholder')"
        @change="handleDbTypeChange"
      />
    </a-form-item>

    <template v-if="formData.dbType">
      <a-form-item :label="t('identity.tenant.database.fields.host')" required>
        <a-input
          v-model:value="formData.host"
          :placeholder="t('identity.tenant.database.fields.hostPlaceholder')"
        />
      </a-form-item>

      <a-form-item :label="t('identity.tenant.database.fields.port')" required>
        <a-input-number
          v-model:value="formData.port"
          :min="1"
          :max="65535"
          :placeholder="t('identity.tenant.database.fields.portPlaceholder')"
        />
      </a-form-item>

      <a-form-item :label="t('identity.tenant.database.fields.database')" required>
        <a-input
          v-model:value="formData.database"
          :placeholder="t('identity.tenant.database.fields.databasePlaceholder')"
        />
      </a-form-item>

      <a-form-item :label="t('identity.tenant.database.fields.username')" required>
        <a-input
          v-model:value="formData.username"
          :placeholder="t('identity.tenant.database.fields.usernamePlaceholder')"
        />
      </a-form-item>

      <a-form-item :label="t('identity.tenant.database.fields.password')" required>
        <a-input-password
          v-model:value="formData.password"
          :placeholder="t('identity.tenant.database.fields.passwordPlaceholder')"
        />
      </a-form-item>

      <a-form-item :label="t('identity.tenant.database.fields.options')">
        <a-textarea
          v-model:value="formData.options"
          :placeholder="t('identity.tenant.database.fields.optionsPlaceholder')"
          :rows="4"
        />
      </a-form-item>

      <a-form-item>
        <a-button type="primary" @click="handleTestConnection">
          {{ t('identity.tenant.database.fields.testConnection') }}
        </a-button>
      </a-form-item>
    </template>
  </div>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { SelectValue } from 'ant-design-vue/es/select'
import { testDbConnection } from '@/api/identity/tenant'

const { t } = useI18n()

const props = defineProps<{
  modelValue?: string
}>()

const emit = defineEmits<{
  (e: 'update:modelValue', value: string): void
}>()

// 数据库类型选项
const dbTypeOptions = [
  { label: 'MySQL', value: 'mysql' },
  { label: 'MariaDB', value: 'mariadb' },
  { label: 'SQL Server', value: 'sqlserver' },
  { label: 'PostgreSQL', value: 'postgresql' },
  { label: 'Oracle', value: 'oracle' },
  { label: 'SQLite', value: 'sqlite' },
  { label: 'MongoDB', value: 'mongodb' },
  { label: 'Redis', value: 'redis' },
  { label: 'ClickHouse', value: 'clickhouse' },
  { label: 'TiDB', value: 'tidb' },
  { label: 'OceanBase', value: 'oceanbase' },
  { label: 'GaussDB', value: 'gaussdb' },
  { label: 'OpenGauss', value: 'opengauss' },
  { label: 'DM', value: 'dm' },
  { label: 'KingbaseES', value: 'kingbasees' }
]

// 表单数据
const formData = ref({
  dbType: '',
  host: '',
  port: 3306,
  database: '',
  username: '',
  password: '',
  options: ''
})

// 监听连接字符串变化
watch(
  () => props.modelValue,
  (newVal) => {
    if (newVal) {
      try {
        const connectionString = JSON.parse(newVal)
        formData.value = { ...connectionString }
      } catch (error) {
        console.error('解析连接字符串失败:', error)
      }
    }
  },
  { immediate: true }
)

// 监听表单数据变化
watch(
  formData,
  (newVal) => {
    emit('update:modelValue', JSON.stringify(newVal))
  },
  { deep: true }
)

// 处理数据库类型变化
const handleDbTypeChange = (value: SelectValue) => {
  if (typeof value !== 'string') return
  
  // 根据数据库类型设置默认端口
  const defaultPorts: Record<string, number> = {
    mysql: 3306,
    mariadb: 3306,
    sqlserver: 1433,
    postgresql: 5432,
    oracle: 1521,
    sqlite: 0,
    mongodb: 27017,
    redis: 6379,
    clickhouse: 9000,
    tidb: 4000,
    oceanbase: 2881,
    gaussdb: 5432,
    opengauss: 5432,
    dm: 5236,
    kingbasees: 54321
  }
  formData.value.port = defaultPorts[value] || 3306
}

// 测试连接
const handleTestConnection = async () => {
  try {
    // 验证必填字段
    if (!formData.value.dbType) {
      message.warning(t('identity.tenant.database.fields.typeRequired'))
      return
    }
    if (!formData.value.host) {
      message.warning(t('identity.tenant.database.fields.hostRequired'))
      return
    }
    if (!formData.value.port) {
      message.warning(t('identity.tenant.database.fields.portRequired'))
      return
    }
    if (!formData.value.database) {
      message.warning(t('identity.tenant.database.fields.databaseRequired'))
      return
    }
    if (!formData.value.username) {
      message.warning(t('identity.tenant.database.fields.usernameRequired'))
      return
    }
    if (!formData.value.password) {
      message.warning(t('identity.tenant.database.fields.passwordRequired'))
      return
    }

    // 调用测试连接API
    const res = await testDbConnection(formData.value)
    if (res.data.code === 200) {
      message.success(t('identity.tenant.database.fields.testSuccess'))
    } else {
      message.error(res.data.msg || t('identity.tenant.database.fields.testFailed'))
    }
  } catch (error: any) {
    console.error('测试数据库连接失败:', error)
    message.error(error.message || t('identity.tenant.database.fields.testFailed'))
  }
}
</script>

<style scoped>
.database-connection {
  padding: 16px;
}
</style> 