// 工作流变量查询参数
export interface WorkflowVariableQuery {
  pageNum?: number;
  pageSize?: number;
  workflowInstanceId?: number;
  nodeId?: number;
  variableName?: string;
  variableType?: string;
  beginTime?: string;
  endTime?: string;
}

// 工作流变量对象
export interface WorkflowVariable {
  id: number;
  workflowInstanceId: number;
  nodeId: number;
  variableName: string;
  variableType: string;
  variableValue: string;
  scope: string;
  createBy: string;
  createTime: string;
  updateBy: string;
  updateTime: string;
  remark: string;
}

// 创建工作流变量参数
export interface WorkflowVariableCreate {
  workflowInstanceId: number;
  nodeId: number;
  variableName: string;
  variableType: string;
  variableValue: string;
  scope: string;
  remark?: string;
}

// 更新工作流变量参数
export interface WorkflowVariableUpdate extends WorkflowVariableCreate {
  id: number;
}

// 批量更新工作流变量参数
export interface WorkflowVariableBatchUpdate {
  instanceId: string;
  variables: {
    variableName: string;
    variableValue: string;
  }[];
} 