# {{ table.comment }}表单组件
<template>
  <a-modal
    :title="title"
    :visible="visible"
    @cancel="handleCancel"
    :destroyOnClose="true"
    :confirmLoading="loading"
  >
    <a-form
      ref="formRef"
      :model="form"
      :rules="rules"
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 16 }"
    >
      {{~ for column in table.columns ~}}
      {{~ if not column.is_pk and column.column_name != "create_time" and column.column_name != "update_time" ~}}
      <a-form-item
        :label="$t('{{ table.module_name }}.{{ table.name }}.field.{{ column.column_name }}')"
        name="{{ column.column_name }}"
      >
        {{~ if column.data_type == "string" and column.length > 100 ~}}
        <a-textarea
          v-model:value="form.{{ column.column_name }}"
          :rows="4"
          :maxlength="{{ column.length }}"
          show-count
        />
        {{~ else if column.data_type == "string" ~}}
        <a-input
          v-model:value="form.{{ column.column_name }}"
          :maxlength="{{ column.length }}"
          allow-clear
        />
        {{~ else if column.data_type == "int" or column.data_type == "long" ~}}
        <a-input-number
          v-model:value="form.{{ column.column_name }}"
          style="width: 100%"
        />
        {{~ else if column.data_type == "datetime" ~}}
        <a-date-picker
          v-model:value="form.{{ column.column_name }}"
          show-time
          style="width: 100%"
        />
        {{~ end ~}}
      </a-form-item>
      {{~ end ~}}
      {{~ end ~}}
    </a-form>
    <template #footer>
      <a-button @click="handleCancel">{{ $t('button.cancel') }}</a-button>
      <a-button type="primary" :loading="loading" @click="handleSubmit">
        {{ $t('button.submit') }}
      </a-button>
    </template>
  </a-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, computed } from 'vue';
import { useI18n } from 'vue-i18n';
import { message } from 'ant-design-vue';
import type { FormInstance } from 'ant-design-vue';
import type { {{ pascal_case table.table_name }}Dto } from '@/types/{{ table.module_name }}/{{ table.name }}';
import { create{{ pascal_case table.table_name }}, update{{ pascal_case table.table_name }} } from '@/api/{{ table.module_name }}/{{ table.name }}';

const props = defineProps<{
  visible: boolean;
  data?: {{ pascal_case table.table_name }}Dto;
}>();

const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void;
  (e: 'success'): void;
}>();

const { t } = useI18n();
const formRef = ref<FormInstance>();
const loading = ref(false);

const title = computed(() => {
  return props.data?.id
    ? t('{{ table.module_name }}.{{ table.name }}.button.edit')
    : t('{{ table.module_name }}.{{ table.name }}.button.add');
});

const form = reactive<Partial<{{ pascal_case table.table_name }}Dto>>({
  {{~ for column in table.columns ~}}
  {{~ if not column.is_pk and column.column_name != "create_time" and column.column_name != "update_time" ~}}
  {{ column.column_name }}: undefined,
  {{~ end ~}}
  {{~ end ~}}
});

const rules = {
  {{~ for column in table.columns ~}}
  {{~ if not column.is_nullable and not column.is_pk and column.column_name != "create_time" and column.column_name != "update_time" ~}}
  {{ column.column_name }}: [
    { required: true, message: t('{{ table.module_name }}.{{ table.name }}.field.{{ column.column_name }}.required') },
    {{~ if column.data_type == "string" ~}}
    { max: {{ column.length }}, message: t('{{ table.module_name }}.{{ table.name }}.field.{{ column.column_name }}.max') },
    {{~ end ~}}
  ],
  {{~ end ~}}
  {{~ end ~}}
};

const handleCancel = () => {
  emit('update:visible', false);
};

const handleSubmit = async () => {
  try {
    await formRef.value?.validate();
    loading.value = true;
    
    const submitData = {
      ...form,
      id: props.data?.id,
    };

    const result = props.data?.id
      ? await update{{ pascal_case table.table_name }}(submitData)
      : await create{{ pascal_case table.table_name }}(submitData);

    if (result.success) {
      message.success(
        t(
          props.data?.id
            ? '{{ table.module_name }}.{{ table.name }}.message.success.update'
            : '{{ table.module_name }}.{{ table.name }}.message.success.create'
        )
      );
      emit('success');
      emit('update:visible', false);
    }
  } catch (error) {
    console.error(error);
  } finally {
    loading.value = false;
  }
};

watch(
  () => props.visible,
  (val) => {
    if (val && props.data) {
      Object.assign(form, props.data);
    } else {
      formRef.value?.resetFields();
    }
  }
);
</script> 