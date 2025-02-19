// 工作流节点查询参数
export interface WorkflowNodeQuery {
  pageNum?: number;
  pageSize?: number;
  workflowDefinitionId?: number;
  nodeName?: string;
  nodeType?: string;
  status?: string;
  beginTime?: string;
  endTime?: string;
}

// 工作流节点对象
export interface WorkflowNode {
  id: number;
  workflowDefinitionId: number;
  nodeName: string;
  nodeType: string;
  nodeConfig: string;
  parentNodeId: number;
  sort: number;
  status: string;
  createBy: string;
  createTime: string;
  updateBy: string;
  updateTime: string;
  remark: string;
}

// 创建工作流节点参数
export interface WorkflowNodeCreate {
  workflowDefinitionId: number;
  nodeName: string;
  nodeType: string;
  nodeConfig: string;
  parentNodeId?: number;
  sort?: number;
  status: string;
  remark?: string;
}

// 更新工作流节点参数
export interface WorkflowNodeUpdate extends WorkflowNodeCreate {
  id: number;
}

// 工作流节点状态更新参数
export interface WorkflowNodeStatus {
  nodeId: string;
  status: string;
} 