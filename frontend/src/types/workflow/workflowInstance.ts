// 工作流实例查询参数
export interface WorkflowInstanceQuery {
  pageNum?: number;
  pageSize?: number;
  workflowDefinitionId?: number;
  workflowTitle?: string;
  initiatorId?: number;
  status?: string;
  beginTime?: string;
  endTime?: string;
}

// 工作流实例对象
export interface WorkflowInstance {
  id: number;
  workflowDefinitionId: number;
  workflowTitle: string;
  initiatorId: number;
  initiatorName: string;
  status: string;
  startTime: string;
  endTime: string;
  duration: number;
  createBy: string;
  createTime: string;
  updateBy: string;
  updateTime: string;
  remark: string;
}

// 创建工作流实例参数
export interface WorkflowInstanceCreate {
  workflowDefinitionId: number;
  workflowTitle: string;
  initiatorId: number;
  status: string;
  remark?: string;
}

// 更新工作流实例参数
export interface WorkflowInstanceUpdate extends WorkflowInstanceCreate {
  id: number;
}

// 工作流实例状态更新参数
export interface WorkflowInstanceStatus {
  workflowInstanceId: number;
  status: string;
  reason?: string;
}

// 工作流实例导入参数
export interface WorkflowInstanceImport {
  workflowDefinitionId: number;
  workflowTitle: string;
  initiatorId: number;
  status: string;
  remark?: string;
}

// 工作流实例导出参数
export interface WorkflowInstanceExport extends WorkflowInstanceQuery {
  orderByColumn?: string;
  isAsc?: string;
}

// 工作流实例模板参数
export interface WorkflowInstanceTemplate {
  workflowDefinitionId: number;
  workflowTitle: string;
  initiatorId: number;
  status: string;
  remark: string;
}

// 工作流实例终止参数
export interface WorkflowInstanceTerminate {
  workflowInstanceId: number;
  reason: string;
} 