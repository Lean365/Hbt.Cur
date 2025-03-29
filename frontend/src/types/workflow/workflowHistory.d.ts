// 工作流历史查询参数
export interface WorkflowHistoryQuery {
  pageNum?: number;
  pageSize?: number;
  workflowInstanceId?: number;
  nodeId?: number;
  operatorId?: number;
  operationType?: string;
  beginTime?: string;
  endTime?: string;
}

// 工作流历史对象
export interface WorkflowHistory {
  id: number;
  workflowInstanceId: number;
  nodeId: number;
  operatorId: number;
  operatorName: string;
  operationType: string;
  operationTime: string;
  operationResult: string;
  operationComment: string;
  createBy: string;
  createTime: string;
  updateBy: string;
  updateTime: string;
  remark: string;
}

// 创建工作流历史参数
export interface WorkflowHistoryCreate {
  workflowInstanceId: number;
  nodeId: number;
  operatorId: number;
  operationType: string;
  operationResult: string;
  operationComment?: string;
  remark?: string;
}

// 更新工作流历史参数
export interface WorkflowHistoryUpdate extends WorkflowHistoryCreate {
  id: number;
}

// 工作流历史导出参数
export interface WorkflowHistoryExport extends WorkflowHistoryQuery {
  orderByColumn?: string;
  isAsc?: string;
} 