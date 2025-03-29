// 工作流定义查询参数
export interface WorkflowDefinitionQuery {
  pageNum?: number;
  pageSize?: number;
  workflowName?: string;
  workflowCategory?: string;
  workflowVersion?: string;
  status?: string;
  beginTime?: string;
  endTime?: string;
}

// 工作流定义对象
export interface WorkflowDefinition {
  id: number;
  workflowName: string;
  workflowCategory: string;
  workflowVersion: string;
  description: string;
  status: string;
  createBy: string;
  createTime: string;
  updateBy: string;
  updateTime: string;
  remark: string;
}

// 创建工作流定义参数
export interface WorkflowDefinitionCreate {
  workflowName: string;
  workflowCategory: string;
  workflowVersion: string;
  description?: string;
  status: string;
  remark?: string;
}

// 更新工作流定义参数
export interface WorkflowDefinitionUpdate extends WorkflowDefinitionCreate {
  id: number;
}

// 工作流定义状态更新参数
export interface WorkflowDefinitionStatus {
  workflowDefinitionId: number;
  status: string;
}

// 工作流定义导入参数
export interface WorkflowDefinitionImport {
  workflowName: string;
  workflowCategory: string;
  workflowVersion: string;
  description?: string;
  status: string;
  remark?: string;
}

// 工作流定义导出参数
export interface WorkflowDefinitionExport extends WorkflowDefinitionQuery {
  orderByColumn?: string;
  isAsc?: string;
}

// 工作流定义模板参数
export interface WorkflowDefinitionTemplate {
  workflowName: string;
  workflowCategory: string;
  workflowVersion: string;
  description: string;
  status: string;
  remark: string;
} 