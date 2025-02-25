//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : index.vue  
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 通用分页组件
//===================================================================

<template>
  <div class="hbt-pagination" :class="[Align, { 'hbt-pagination-mini': Small }]">
    <a-pagination
      v-model:current="currentPage"
      v-model:pageSize="pageSize"
      :total="Total"
      :show-total="ShowTotal"
      :show-size-changer="ShowSizeChanger"
      :show-quick-jumper="ShowQuickJumper"
      :disabled="Disabled"
      :page-size-options="PageSizeOptions"
      :size="Size"
      :simple="Simple"
      :responsive="Responsive"
      :hide-on-single-page="HideOnSinglePage"
      :show-less-items="ShowLessItems"
      :item-render="ItemRender"
      :default-current="DefaultCurrent"
      :default-page-size="DefaultPageSize"
      @change="HandleChange"
      @showSizeChange="HandleSizeChange"
      @jumpTo="HandleJumpTo"
      role="navigation"
      aria-label="分页导航"
    >
      <template #itemRender="{ page, type, originalElement }" v-if="$slots.itemRender">
        <slot name="itemRender" :page="page" :type="type" :original="originalElement" />
      </template>
      <template #buildOptionText="{ value }" v-if="$slots.buildOptionText">
        <slot name="buildOptionText" :value="value" />
      </template>
      <template #total="range" v-if="$slots.total">
        <slot name="total" v-bind="range" />
      </template>
    </a-pagination>
  </div>
</template>

<script setup lang="ts">
import { ref, watch, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import type { VNode } from 'vue'

/**
 * 分页组件属性接口
 */
interface IHbtPaginationProps {
  /** 当前页码 */
  Current?: number;
  /** 每页条数 */
  PageSize?: number;
  /** 数据总数 */
  Total: number;
  /** 是否显示快速跳转 */
  ShowQuickJumper?: boolean;
  /** 是否显示每页条数选择器 */
  ShowSizeChanger?: boolean;
  /** 是否显示总数 */
  ShowTotal?: (total: number, range: [number, number]) => VNode;
  /** 是否禁用 */
  Disabled?: boolean;
  /** 每页条数选项 */
  PageSizeOptions?: string[];
  /** 组件大小 */
  Size?: 'small' | 'default';
  /** 是否使用简单模式 */
  Simple?: boolean;
  /** 对齐方式 */
  Align?: 'left' | 'center' | 'right';
  /** 是否显示较少页面内容 */
  Small?: boolean;
  /** 用于自定义页码的结构 */
  ItemRender?: (opt: { page: number; type: 'page' | 'prev' | 'next' | 'jump-prev' | 'jump-next'; originalElement: any }) => any;
  /** 当添加该属性时，显示为简单分页 */
  Responsive?: boolean;
  /** 指定每页可以显示多少条 */
  DefaultPageSize?: number;
  /** 指定默认的当前页数 */
  DefaultCurrent?: number;
  /** 只有一页时是否隐藏分页器 */
  HideOnSinglePage?: boolean;
  /** 当为「small」时，是否显示较少页面内容 */
  ShowLessItems?: boolean;
  /** 主题模式 */
  Theme?: 'light' | 'dark';
}

const props = withDefaults(defineProps<IHbtPaginationProps>(), {
  Current: 1,
  PageSize: 10,
  ShowQuickJumper: true,
  ShowSizeChanger: true,
  ShowTotal: undefined,
  Disabled: false,
  PageSizeOptions: () => ['10', '20', '50', '100'],
  Size: 'default',
  Simple: false,
  Align: 'right',
  Small: false,
  ItemRender: undefined,
  Responsive: false,
  DefaultPageSize: 10,
  DefaultCurrent: 1,
  HideOnSinglePage: false,
  ShowLessItems: false,
  Theme: 'light'
})

/**
 * 组件事件
 */
const emit = defineEmits<{
  /**
   * 更新当前页码事件
   * @param page 新的页码
   */
  (e: 'update:Current', page: number): void;
  /**
   * 更新每页条数事件
   * @param size 新的每页条数
   */
  (e: 'update:PageSize', size: number): void;
  /**
   * 分页变更事件
   * @param page 新的页码
   * @param pageSize 新的每页条数
   */
  (e: 'change', page: number, pageSize: number): void;
  /**
   * 页码改变的回调
   * @param page 改变后的页码
   * @param pageSize 每页条数
   */
  (e: 'PageChange', page: number, pageSize: number): void;
  /**
   * 每页条数改变的回调
   * @param size 改变后的每页条数
   * @param current 当前页码
   */
  (e: 'SizeChange', size: number, current: number): void;
  /**
   * 快速跳转时的回调
   * @param page 跳转的页码
   */
  (e: 'JumpTo', page: number): void;
}>()

// 使用vue-i18n
const { t } = useI18n()

// 当前页码
const currentPage = ref(props.Current)
const pageSize = ref(props.PageSize)

// 计算主题类名
const themeClass = computed(() => props.Theme === 'dark' ? 'dark' : '')

// 监听props变化
watch(() => props.Current, (val) => {
  currentPage.value = val
})

watch(() => props.PageSize, (val) => {
  pageSize.value = val
})

/**
 * 页码改变事件处理
 * @param page 新的页码
 * @param size 新的每页条数
 */
const HandleChange = (page: number, size: number) => {
  emit('update:Current', page)
  emit('update:PageSize', size)
  emit('change', page, size)
  emit('PageChange', page, size)
}

/**
 * 每页条数改变事件处理
 * @param current 当前页码
 * @param size 新的每页条数
 */
const HandleSizeChange = (current: number, size: number) => {
  emit('update:PageSize', size)
  emit('change', current, size)
  emit('SizeChange', size, current)
}

/**
 * 快速跳转事件处理
 * @param page 跳转的页码
 */
const HandleJumpTo = (page: number) => {
  emit('JumpTo', page)
}
</script>

<style lang="less" scoped>
.hbt-pagination {
  margin: 16px 0;
  
  &-mini {
    .ant-pagination-prev,
    .ant-pagination-next {
      min-width: 24px;
      height: 24px;
      line-height: 24px;
    }
    
    .ant-pagination-item {
      min-width: 24px;
      height: 24px;
      line-height: 24px;
    }
  }
  
  &.left {
    text-align: left;
  }
  
  &.center {
    text-align: center;
  }
  
  &.right {
    text-align: right;
  }
  
  :deep(.ant-pagination-options) {
    margin-left: 16px;
  }
  
  :deep(.ant-select-single:not(.ant-select-customize-input) .ant-select-selector) {
    height: 32px;
    line-height: 32px;
  }
  
  &.dark {
    // 暗色主题样式
    :deep(.ant-pagination-item) {
      background: transparent;
      border-color: #434343;
      a {
        color: rgba(255, 255, 255, 0.85);
      }
      &:hover {
        border-color: #177ddc;
        a {
          color: #177ddc;
        }
      }
      &-active {
        background: #177ddc;
        border-color: #177ddc;
        a {
          color: #fff;
        }
      }
    }
  }

  // 响应式布局
  @media screen and (max-width: 576px) {
    :deep(.ant-pagination-options) {
      display: none;
    }
  }
}
</style> 