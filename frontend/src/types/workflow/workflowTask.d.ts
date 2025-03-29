// 工作流任务查询参数
export interface WorkflowTaskQuery {
  pageNum?: number;
  pageSize?: number;
  workflowInstanceId?: number;
  nodeId?: number;
  taskTitle?: string;
  taskType?: string;
  assigneeId?: number;
  status?: string;
  beginTime?: string;
  endTime?: string;
}

// 工作流任务对象
export interface WorkflowTask {
  id: number;
  workflowInstanceId: number;
  nodeId: number;
  taskTitle: string;
  taskType: string;
  assigneeId: number;
  assigneeName: string;
  dueDate: string;
  priority: number;
  status: string;
  result: string;
  comment: string;
  startTime: string;
  endTime: string;
  duration: number;
  createBy: string;
  createTime: string;
  updateBy: string;
  updateTime: string;
  remark: string;
}

// 创建工作流任务参数
export interface WorkflowTaskCreate {
  workflowInstanceId: number;
  nodeId: number;
  taskTitle: string;
  taskType: string;
  assigneeId: number;
  dueDate?: string;
  priority?: number;
  status: string;
  remark?: string;
}

// 更新工作流任务参数
export interface WorkflowTaskUpdate extends WorkflowTaskCreate {
  id: number;
}

// 工作流任务状态更新参数
export interface WorkflowTaskStatus {
  taskId: number;
  status: string;
  result?: string;
  comment?: string;
}

// 工作流任务导入参数
export interface WorkflowTaskImport {
  workflowInstanceId: number;
  nodeId: number;
  taskTitle: string;
  taskType: string;
  assigneeId: number;
  dueDate?: string;
  priority?: number;
  status: string;
  remark?: string;
}

// 工作流任务导出参数
export interface WorkflowTaskExport extends WorkflowTaskQuery {
  orderByColumn?: string;
  isAsc?: string;
}

// 工作流任务模板参数
export interface WorkflowTaskTemplate {
  workflowInstanceId: number;
  nodeId: number;
  taskTitle: string;
  taskType: string;
  assigneeId: number;
  dueDate: string;
  priority: number;
  status: string;
  remark: string;
}

// 工作流任务认领参数
export interface WorkflowTaskClaim {
  taskId: string;
  userId: string;
}

// 工作流任务完成参数
export interface WorkflowTaskComplete {
  taskId: string;
  result: string;
  comment?: string;
} 