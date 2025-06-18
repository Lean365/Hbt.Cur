//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedForm.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 工作流表单种子数据
//===================================================================

using Lean.Hbt.Domain.Entities.Workflow;
using Lean.Hbt.Domain.Repositories;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 工作流表单种子数据
/// </summary>
public class HbtDbSeedForm
{
    private readonly IHbtRepository<HbtForm> _formRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="formRepository">表单仓储</param>
    /// <param name="logger">日志服务</param>
    public HbtDbSeedForm(IHbtRepository<HbtForm> formRepository, IHbtLogger logger)
    {
        _formRepository = formRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化表单数据
    /// </summary>
    /// <returns>(新增数量, 更新数量)</returns>
    public async Task<(int insertCount, int updateCount)> InitializeFormAsync()
    {
        int insertCount = 0;
        int updateCount = 0;


        var forms = new List<HbtForm>
        {
            new HbtForm
            {
                FormName = "请假申请表单",
                FormDesc = "员工请假申请表单",
                FormConfig = "{\"rule\":[{\"type\":\"input\",\"field\":\"reason\",\"title\":\"请假原因\",\"props\":{\"type\":\"text\",\"placeholder\":\"请输入请假原因\"}},{\"type\":\"datePicker\",\"field\":\"startTime\",\"title\":\"开始时间\",\"props\":{\"type\":\"datetime\",\"placeholder\":\"请选择开始时间\"}},{\"type\":\"datePicker\",\"field\":\"endTime\",\"title\":\"结束时间\",\"props\":{\"type\":\"datetime\",\"placeholder\":\"请选择结束时间\"}},{\"type\":\"select\",\"field\":\"type\",\"title\":\"请假类型\",\"props\":{\"placeholder\":\"请选择请假类型\"},\"options\":[{\"label\":\"事假\",\"value\":1},{\"label\":\"病假\",\"value\":2},{\"label\":\"年假\",\"value\":3},{\"label\":\"调休\",\"value\":4}]}]}",
                

            },
            new HbtForm
            {
                FormName = "报销申请表单",
                FormDesc = "员工报销申请表单",
                FormConfig = "{\"rule\":[{\"type\":\"input\",\"field\":\"title\",\"title\":\"报销标题\",\"props\":{\"type\":\"text\",\"placeholder\":\"请输入报销标题\"}},{\"type\":\"input\",\"field\":\"amount\",\"title\":\"报销金额\",\"props\":{\"type\":\"number\",\"placeholder\":\"请输入报销金额\"}},{\"type\":\"select\",\"field\":\"type\",\"title\":\"报销类型\",\"props\":{\"placeholder\":\"请选择报销类型\"},\"options\":[{\"label\":\"差旅费\",\"value\":1},{\"label\":\"办公用品\",\"value\":2},{\"label\":\"业务招待\",\"value\":3},{\"label\":\"其他\",\"value\":4}]},{\"type\":\"textarea\",\"field\":\"remark\",\"title\":\"备注说明\",\"props\":{\"placeholder\":\"请输入备注说明\"}}]}",
                

            },
            // 会议申请表单
            new HbtForm
            {
                FormName = "会议申请表单",
                FormDesc = "会议室/会议活动申请表单",
                FormConfig = "{\"rule\":[{\"type\":\"input\",\"field\":\"subject\",\"title\":\"会议主题\",\"props\":{\"placeholder\":\"请输入会议主题\"}},{\"type\":\"datePicker\",\"field\":\"meetingTime\",\"title\":\"会议时间\",\"props\":{\"type\":\"datetime\",\"placeholder\":\"请选择会议时间\"}},{\"type\":\"input\",\"field\":\"location\",\"title\":\"会议地点\",\"props\":{\"placeholder\":\"请输入会议地点\"}},{\"type\":\"input\",\"field\":\"attendees\",\"title\":\"参会人员\",\"props\":{\"placeholder\":\"请输入参会人员\"}},{\"type\":\"textarea\",\"field\":\"content\",\"title\":\"会议内容\",\"props\":{\"placeholder\":\"请输入会议内容\"}}]}",
                
            },
            // 公告通知表单
            new HbtForm
            {
                FormName = "公告通知表单",
                FormDesc = "公司公告、通知发布表单",
                FormConfig = "{\"rule\":[{\"type\":\"input\",\"field\":\"title\",\"title\":\"公告标题\",\"props\":{\"placeholder\":\"请输入公告标题\"}},{\"type\":\"textarea\",\"field\":\"content\",\"title\":\"公告内容\",\"props\":{\"placeholder\":\"请输入公告内容\"}},{\"type\":\"input\",\"field\":\"publisher\",\"title\":\"发布人\",\"props\":{\"placeholder\":\"请输入发布人\"}},{\"type\":\"datePicker\",\"field\":\"publishTime\",\"title\":\"发布时间\",\"props\":{\"type\":\"datetime\",\"placeholder\":\"请选择发布时间\"}}]}",
                
            },
            // 用章申请表单
            new HbtForm
            {
                FormName = "用章申请表单",
                FormDesc = "公司用章申请表单",
                FormConfig = "{\"rule\":[{\"type\":\"input\",\"field\":\"reason\",\"title\":\"用章事由\",\"props\":{\"placeholder\":\"请输入用章事由\"}},{\"type\":\"select\",\"field\":\"sealType\",\"title\":\"用章类型\",\"props\":{\"placeholder\":\"请选择用章类型\"},\"options\":[{\"label\":\"公章\",\"value\":1},{\"label\":\"合同章\",\"value\":2},{\"label\":\"财务章\",\"value\":3}]},{\"type\":\"datePicker\",\"field\":\"sealTime\",\"title\":\"用章时间\",\"props\":{\"type\":\"datetime\",\"placeholder\":\"请选择用章时间\"}},{\"type\":\"input\",\"field\":\"applicant\",\"title\":\"申请人\",\"props\":{\"placeholder\":\"请输入申请人\"}}]}",
                
            },
            // 物品领用申请表单
            new HbtForm
            {
                FormName = "物品领用申请表单",
                FormDesc = "办公物品领用申请表单",
                FormConfig = "{\"rule\":[{\"type\":\"input\",\"field\":\"itemName\",\"title\":\"物品名称\",\"props\":{\"placeholder\":\"请输入物品名称\"}},{\"type\":\"input\",\"field\":\"quantity\",\"title\":\"领用数量\",\"props\":{\"type\":\"number\",\"placeholder\":\"请输入领用数量\"}},{\"type\":\"input\",\"field\":\"receiver\",\"title\":\"领用人\",\"props\":{\"placeholder\":\"请输入领用人\"}},{\"type\":\"datePicker\",\"field\":\"receiveTime\",\"title\":\"领用时间\",\"props\":{\"type\":\"datetime\",\"placeholder\":\"请选择领用时间\"}},{\"type\":\"textarea\",\"field\":\"remark\",\"title\":\"备注\",\"props\":{\"placeholder\":\"请输入备注\"}}]}",
                
            },
            // 设计变更流程表单
            new HbtForm
            {
                FormName = "设计变更流程表单",
                FormDesc = "设计变更申请流程表单",
                FormConfig = "{\"rule\":[{\"type\":\"input\",\"field\":\"changeTitle\",\"title\":\"变更标题\",\"props\":{\"placeholder\":\"请输入变更标题\"}},{\"type\":\"textarea\",\"field\":\"changeContent\",\"title\":\"变更内容\",\"props\":{\"placeholder\":\"请输入变更内容\"}},{\"type\":\"textarea\",\"field\":\"changeReason\",\"title\":\"变更原因\",\"props\":{\"placeholder\":\"请输入变更原因\"}},{\"type\":\"input\",\"field\":\"applicant\",\"title\":\"申请人\",\"props\":{\"placeholder\":\"请输入申请人\"}},{\"type\":\"datePicker\",\"field\":\"applyTime\",\"title\":\"申请时间\",\"props\":{\"type\":\"datetime\",\"placeholder\":\"请选择申请时间\"}}]}",
                
            },
            // 采购申请流程表单
            new HbtForm
            {
                FormName = "采购申请流程表单",
                FormDesc = "采购物品申请流程表单",
                FormConfig = "{\"rule\":[{\"type\":\"input\",\"field\":\"itemName\",\"title\":\"采购物品名称\",\"props\":{\"placeholder\":\"请输入物品名称\"}},{\"type\":\"input\",\"field\":\"quantity\",\"title\":\"采购数量\",\"props\":{\"type\":\"number\",\"placeholder\":\"请输入采购数量\"}},{\"type\":\"input\",\"field\":\"amount\",\"title\":\"采购金额\",\"props\":{\"type\":\"number\",\"placeholder\":\"请输入采购金额\"}},{\"type\":\"textarea\",\"field\":\"reason\",\"title\":\"采购原因\",\"props\":{\"placeholder\":\"请输入采购原因\"}},{\"type\":\"input\",\"field\":\"applicant\",\"title\":\"申请人\",\"props\":{\"placeholder\":\"请输入申请人\"}},{\"type\":\"datePicker\",\"field\":\"applyTime\",\"title\":\"申请时间\",\"props\":{\"type\":\"datetime\",\"placeholder\":\"请选择申请时间\"}}]}",
                
            },
            // 外出申请表单
            new HbtForm
            {
                FormName = "外出申请表单",
                FormDesc = "员工外出申请表单",
                FormConfig = "{\"rule\":[{\"type\":\"input\",\"field\":\"reason\",\"title\":\"外出事由\",\"props\":{\"placeholder\":\"请输入外出事由\"}},{\"type\":\"input\",\"field\":\"location\",\"title\":\"外出地点\",\"props\":{\"placeholder\":\"请输入外出地点\"}},{\"type\":\"datePicker\",\"field\":\"outTime\",\"title\":\"外出时间\",\"props\":{\"type\":\"datetime\",\"placeholder\":\"请选择外出时间\"}},{\"type\":\"datePicker\",\"field\":\"returnTime\",\"title\":\"返回时间\",\"props\":{\"type\":\"datetime\",\"placeholder\":\"请选择返回时间\"}},{\"type\":\"select\",\"field\":\"needCar\",\"title\":\"是否用车\",\"props\":{\"placeholder\":\"请选择\"},\"options\":[{\"label\":\"是\",\"value\":1},{\"label\":\"否\",\"value\":0}]},{\"type\":\"select\",\"field\":\"needBanquet\",\"title\":\"是否宴请\",\"props\":{\"placeholder\":\"请选择\"},\"options\":[{\"label\":\"是\",\"value\":1},{\"label\":\"否\",\"value\":0}]},{\"type\":\"textarea\",\"field\":\"remark\",\"title\":\"备注\",\"props\":{\"placeholder\":\"请输入备注（可选）\"}},{\"type\":\"input\",\"field\":\"applicant\",\"title\":\"申请人\",\"props\":{\"placeholder\":\"请输入申请人\"}}]}" ,
                
            },
            // 用车申请表单
            new HbtForm
            {
                FormName = "用车申请表单",
                FormDesc = "公司用车申请表单",
                FormConfig = "{\"rule\":[{\"type\":\"input\",\"field\":\"reason\",\"title\":\"用车事由\",\"props\":{\"placeholder\":\"请输入用车事由\"}},{\"type\":\"datePicker\",\"field\":\"carTime\",\"title\":\"用车时间\",\"props\":{\"type\":\"datetime\",\"placeholder\":\"请选择用车时间\"}},{\"type\":\"datePicker\",\"field\":\"returnTime\",\"title\":\"预计返回时间\",\"props\":{\"type\":\"datetime\",\"placeholder\":\"请选择预计返回时间\"}},{\"type\":\"input\",\"field\":\"from\",\"title\":\"出发地点\",\"props\":{\"placeholder\":\"请输入出发地点\"}},{\"type\":\"input\",\"field\":\"to\",\"title\":\"目的地\",\"props\":{\"placeholder\":\"请输入目的地\"}},{\"type\":\"input\",\"field\":\"applicant\",\"title\":\"申请人\",\"props\":{\"placeholder\":\"请输入申请人\"}}]}",
                
            },
            // 出差申请表单
            new HbtForm
            {
                FormName = "出差申请表单",
                FormDesc = "员工出差申请表单",
                FormConfig = "{\"rule\":[{\"type\":\"input\",\"field\":\"reason\",\"title\":\"出差事由\",\"props\":{\"placeholder\":\"请输入出差事由\"}},{\"type\":\"input\",\"field\":\"location\",\"title\":\"出差地点\",\"props\":{\"placeholder\":\"请输入出差地点\"}},{\"type\":\"datePicker\",\"field\":\"startTime\",\"title\":\"出差开始时间\",\"props\":{\"type\":\"datetime\",\"placeholder\":\"请选择开始时间\"}},{\"type\":\"datePicker\",\"field\":\"endTime\",\"title\":\"出差结束时间\",\"props\":{\"type\":\"datetime\",\"placeholder\":\"请选择结束时间\"}},{\"type\":\"select\",\"field\":\"vehicle\",\"title\":\"交通工具\",\"props\":{\"placeholder\":\"请选择\"},\"options\":[{\"label\":\"高铁\",\"value\":1},{\"label\":\"飞机\",\"value\":2},{\"label\":\"自驾\",\"value\":3},{\"label\":\"其他\",\"value\":4}]},{\"type\":\"input\",\"field\":\"budget\",\"title\":\"预计费用\",\"props\":{\"type\":\"number\",\"placeholder\":\"请输入预计费用\"}},{\"type\":\"input\",\"field\":\"companions\",\"title\":\"同行人员\",\"props\":{\"placeholder\":\"请输入同行人员（可选）\"}},{\"type\":\"textarea\",\"field\":\"remark\",\"title\":\"备注\",\"props\":{\"placeholder\":\"请输入备注（可选）\"}},{\"type\":\"input\",\"field\":\"applicant\",\"title\":\"申请人\",\"props\":{\"placeholder\":\"请输入申请人\"}}]}" ,
                
            },
            // 出差费用报销表单
            new HbtForm
            {
                FormName = "出差费用报销表单",
                FormDesc = "员工出差费用报销表单",
                FormConfig = "{\"rule\":[{\"type\":\"input\",\"field\":\"title\",\"title\":\"报销标题\",\"props\":{\"placeholder\":\"请输入报销标题\"}},{\"type\":\"input\",\"field\":\"location\",\"title\":\"出差地点\",\"props\":{\"placeholder\":\"请输入出差地点\"}},{\"type\":\"datePicker\",\"field\":\"startTime\",\"title\":\"出差开始时间\",\"props\":{\"type\":\"datetime\",\"placeholder\":\"请选择开始时间\"}},{\"type\":\"datePicker\",\"field\":\"endTime\",\"title\":\"出差结束时间\",\"props\":{\"type\":\"datetime\",\"placeholder\":\"请选择结束时间\"}},{\"type\":\"select\",\"field\":\"vehicle\",\"title\":\"交通工具\",\"props\":{\"placeholder\":\"请选择\"},\"options\":[{\"label\":\"高铁\",\"value\":1},{\"label\":\"飞机\",\"value\":2},{\"label\":\"自驾\",\"value\":3},{\"label\":\"其他\",\"value\":4}]},{\"type\":\"input\",\"field\":\"trafficFee\",\"title\":\"交通费\",\"props\":{\"type\":\"number\",\"placeholder\":\"请输入交通费\"}},{\"type\":\"input\",\"field\":\"hotelFee\",\"title\":\"住宿费\",\"props\":{\"type\":\"number\",\"placeholder\":\"请输入住宿费\"}},{\"type\":\"input\",\"field\":\"mealFee\",\"title\":\"餐饮费\",\"props\":{\"type\":\"number\",\"placeholder\":\"请输入餐饮费\"}},{\"type\":\"input\",\"field\":\"otherFee\",\"title\":\"其他费用\",\"props\":{\"type\":\"number\",\"placeholder\":\"请输入其他费用（可选）\"}},{\"type\":\"input\",\"field\":\"totalFee\",\"title\":\"总金额\",\"props\":{\"type\":\"number\",\"placeholder\":\"请输入总金额\"}},{\"type\":\"textarea\",\"field\":\"remark\",\"title\":\"备注\",\"props\":{\"placeholder\":\"请输入备注（可选）\"}},{\"type\":\"input\",\"field\":\"applicant\",\"title\":\"申请人\",\"props\":{\"placeholder\":\"请输入申请人\"}}]}" ,
                
            },
            // 培训费用申请表单
            new HbtForm
            {
                FormName = "培训费用申请表单",
                FormDesc = "员工培训费用申请表单",
                FormConfig = "{\"rule\":[{\"type\":\"input\",\"field\":\"title\",\"title\":\"培训主题\",\"props\":{\"placeholder\":\"请输入培训主题\"}},{\"type\":\"textarea\",\"field\":\"content\",\"title\":\"培训内容\",\"props\":{\"placeholder\":\"请输入培训内容\"}},{\"type\":\"datePicker\",\"field\":\"trainTime\",\"title\":\"培训时间\",\"props\":{\"type\":\"datetime\",\"placeholder\":\"请选择培训时间\"}},{\"type\":\"input\",\"field\":\"location\",\"title\":\"培训地点\",\"props\":{\"placeholder\":\"请输入培训地点\"}},{\"type\":\"input\",\"field\":\"teacher\",\"title\":\"培训讲师\",\"props\":{\"placeholder\":\"请输入培训讲师\"}},{\"type\":\"input\",\"field\":\"trainees\",\"title\":\"参训人员\",\"props\":{\"placeholder\":\"请输入参训人员\"}},{\"type\":\"input\",\"field\":\"trainFee\",\"title\":\"培训费\",\"props\":{\"type\":\"number\",\"placeholder\":\"请输入培训费\"}},{\"type\":\"input\",\"field\":\"trafficFee\",\"title\":\"交通费\",\"props\":{\"type\":\"number\",\"placeholder\":\"请输入交通费\"}},{\"type\":\"input\",\"field\":\"hotelFee\",\"title\":\"住宿费\",\"props\":{\"type\":\"number\",\"placeholder\":\"请输入住宿费\"}},{\"type\":\"input\",\"field\":\"mealFee\",\"title\":\"餐饮费\",\"props\":{\"type\":\"number\",\"placeholder\":\"请输入餐饮费\"}},{\"type\":\"input\",\"field\":\"otherFee\",\"title\":\"其他费用\",\"props\":{\"type\":\"number\",\"placeholder\":\"请输入其他费用（可选）\"}},{\"type\":\"input\",\"field\":\"totalFee\",\"title\":\"总金额\",\"props\":{\"type\":\"number\",\"placeholder\":\"请输入总金额\"}},{\"type\":\"textarea\",\"field\":\"remark\",\"title\":\"备注\",\"props\":{\"placeholder\":\"请输入备注（可选）\"}},{\"type\":\"input\",\"field\":\"applicant\",\"title\":\"申请人\",\"props\":{\"placeholder\":\"请输入申请人\"}}]}" ,
                
            },
            // 设备维护申请表单
            new HbtForm
            {
                FormName = "设备维护申请表单",
                FormDesc = "设备维护及相关费用申请表单",
                FormConfig = "{\"rule\":[{\"type\":\"input\",\"field\":\"deviceName\",\"title\":\"设备名称\",\"props\":{\"placeholder\":\"请输入设备名称\"}},{\"type\":\"input\",\"field\":\"deviceCode\",\"title\":\"设备编号\",\"props\":{\"placeholder\":\"请输入设备编号\"}},{\"type\":\"select\",\"field\":\"maintainType\",\"title\":\"维护类型\",\"props\":{\"placeholder\":\"请选择维护类型\"},\"options\":[{\"label\":\"保养\",\"value\":1},{\"label\":\"维修\",\"value\":2},{\"label\":\"更换\",\"value\":3},{\"label\":\"检验\",\"value\":4},{\"label\":\"其他\",\"value\":5}]},{\"type\":\"textarea\",\"field\":\"content\",\"title\":\"维护内容\",\"props\":{\"placeholder\":\"请输入维护内容\"}},{\"type\":\"datePicker\",\"field\":\"maintainTime\",\"title\":\"维护时间\",\"props\":{\"type\":\"datetime\",\"placeholder\":\"请选择维护时间\"}},{\"type\":\"input\",\"field\":\"keepFee\",\"title\":\"保养费用\",\"props\":{\"type\":\"number\",\"placeholder\":\"请输入保养费用\"}},{\"type\":\"input\",\"field\":\"repairFee\",\"title\":\"维修费用\",\"props\":{\"type\":\"number\",\"placeholder\":\"请输入维修费用\"}},{\"type\":\"input\",\"field\":\"replaceFee\",\"title\":\"更换费用\",\"props\":{\"type\":\"number\",\"placeholder\":\"请输入更换费用\"}},{\"type\":\"input\",\"field\":\"checkFee\",\"title\":\"检验费用\",\"props\":{\"type\":\"number\",\"placeholder\":\"请输入检验费用\"}},{\"type\":\"input\",\"field\":\"otherFee\",\"title\":\"其他费用\",\"props\":{\"type\":\"number\",\"placeholder\":\"请输入其他费用（可选）\"}},{\"type\":\"input\",\"field\":\"totalFee\",\"title\":\"总金额\",\"props\":{\"type\":\"number\",\"placeholder\":\"请输入总金额\"}},{\"type\":\"textarea\",\"field\":\"remark\",\"title\":\"备注\",\"props\":{\"placeholder\":\"请输入备注（可选）\"}},{\"type\":\"input\",\"field\":\"applicant\",\"title\":\"申请人\",\"props\":{\"placeholder\":\"请输入申请人\"}}]}" ,
                
            },
            // 固定资产购买申请表单
            new HbtForm
            {
                FormName = "固定资产购买申请表单",
                FormDesc = "固定资产采购申请表单",
                FormConfig = "{\"rule\":[{\"type\":\"input\",\"field\":\"assetName\",\"title\":\"资产名称\",\"props\":{\"placeholder\":\"请输入资产名称\"}},{\"type\":\"input\",\"field\":\"assetType\",\"title\":\"资产类别\",\"props\":{\"placeholder\":\"请输入资产类别\"}},{\"type\":\"input\",\"field\":\"model\",\"title\":\"规格型号\",\"props\":{\"placeholder\":\"请输入规格型号\"}},{\"type\":\"input\",\"field\":\"quantity\",\"title\":\"数量\",\"props\":{\"type\":\"number\",\"placeholder\":\"请输入数量\"}},{\"type\":\"input\",\"field\":\"price\",\"title\":\"单价\",\"props\":{\"type\":\"number\",\"placeholder\":\"请输入单价\"}},{\"type\":\"textarea\",\"field\":\"reason\",\"title\":\"采购原因\",\"props\":{\"placeholder\":\"请输入采购原因\"}},{\"type\":\"input\",\"field\":\"applicant\",\"title\":\"申请人\",\"props\":{\"placeholder\":\"请输入申请人\"}},{\"type\":\"datePicker\",\"field\":\"applyTime\",\"title\":\"申请时间\",\"props\":{\"type\":\"datetime\",\"placeholder\":\"请选择申请时间\"}},{\"type\":\"textarea\",\"field\":\"remark\",\"title\":\"备注\",\"props\":{\"placeholder\":\"请输入备注（可选）\"}}]}" ,
                
            },
            // 固定资产报废申请表单
            new HbtForm
            {
                FormName = "固定资产报废申请表单",
                FormDesc = "固定资产报废处理申请表单",
                FormConfig = "{\"rule\":[{\"type\":\"input\",\"field\":\"assetName\",\"title\":\"资产名称\",\"props\":{\"placeholder\":\"请输入资产名称\"}},{\"type\":\"input\",\"field\":\"assetCode\",\"title\":\"资产编号\",\"props\":{\"placeholder\":\"请输入资产编号\"}},{\"type\":\"input\",\"field\":\"assetType\",\"title\":\"资产类别\",\"props\":{\"placeholder\":\"请输入资产类别\"}},{\"type\":\"input\",\"field\":\"model\",\"title\":\"规格型号\",\"props\":{\"placeholder\":\"请输入规格型号\"}},{\"type\":\"textarea\",\"field\":\"reason\",\"title\":\"报废原因\",\"props\":{\"placeholder\":\"请输入报废原因\"}},{\"type\":\"input\",\"field\":\"applicant\",\"title\":\"申请人\",\"props\":{\"placeholder\":\"请输入申请人\"}},{\"type\":\"datePicker\",\"field\":\"applyTime\",\"title\":\"申请时间\",\"props\":{\"type\":\"datetime\",\"placeholder\":\"请选择申请时间\"}},{\"type\":\"textarea\",\"field\":\"remark\",\"title\":\"备注\",\"props\":{\"placeholder\":\"请输入备注（可选）\"}}]}" ,
                
            },
            // 固定资产移管申请表单
            new HbtForm
            {
                FormName = "固定资产移管申请表单",
                FormDesc = "固定资产移管处理申请表单",
                FormConfig = "{\"rule\":[{\"type\":\"input\",\"field\":\"assetName\",\"title\":\"资产名称\",\"props\":{\"placeholder\":\"请输入资产名称\"}},{\"type\":\"input\",\"field\":\"assetCode\",\"title\":\"资产编号\",\"props\":{\"placeholder\":\"请输入资产编号\"}},{\"type\":\"input\",\"field\":\"assetType\",\"title\":\"资产类别\",\"props\":{\"placeholder\":\"请输入资产类别\"}},{\"type\":\"input\",\"field\":\"model\",\"title\":\"规格型号\",\"props\":{\"placeholder\":\"请输入规格型号\"}},{\"type\":\"input\",\"field\":\"oldDept\",\"title\":\"原使用部门\",\"props\":{\"placeholder\":\"请输入原使用部门\"}},{\"type\":\"input\",\"field\":\"oldUser\",\"title\":\"原使用人\",\"props\":{\"placeholder\":\"请输入原使用人\"}},{\"type\":\"input\",\"field\":\"newDept\",\"title\":\"新使用部门\",\"props\":{\"placeholder\":\"请输入新使用部门\"}},{\"type\":\"input\",\"field\":\"newUser\",\"title\":\"新使用人\",\"props\":{\"placeholder\":\"请输入新使用人\"}},{\"type\":\"textarea\",\"field\":\"reason\",\"title\":\"移管原因\",\"props\":{\"placeholder\":\"请输入移管原因\"}},{\"type\":\"input\",\"field\":\"applicant\",\"title\":\"申请人\",\"props\":{\"placeholder\":\"请输入申请人\"}},{\"type\":\"datePicker\",\"field\":\"applyTime\",\"title\":\"申请时间\",\"props\":{\"type\":\"datetime\",\"placeholder\":\"请选择申请时间\"}},{\"type\":\"textarea\",\"field\":\"remark\",\"title\":\"备注\",\"props\":{\"placeholder\":\"请输入备注（可选）\"}}]}" ,
                
            },
            // 绩效考核表单
            new HbtForm
            {
                FormName = "绩效考核表单",
                FormDesc = "员工绩效考核表单",
                FormConfig = "{\"rule\":[{\"type\":\"input\",\"field\":\"userName\",\"title\":\"员工姓名\",\"props\":{\"placeholder\":\"请输入员工姓名\"}},{\"type\":\"input\",\"field\":\"dept\",\"title\":\"部门\",\"props\":{\"placeholder\":\"请输入部门\"}},{\"type\":\"input\",\"field\":\"post\",\"title\":\"岗位\",\"props\":{\"placeholder\":\"请输入岗位\"}},{\"type\":\"input\",\"field\":\"period\",\"title\":\"考核周期\",\"props\":{\"placeholder\":\"请输入考核周期\"}},{\"type\":\"textarea\",\"field\":\"workContent\",\"title\":\"主要工作内容\",\"props\":{\"placeholder\":\"请输入主要工作内容\"}},{\"type\":\"textarea\",\"field\":\"achievement\",\"title\":\"工作成绩\",\"props\":{\"placeholder\":\"请输入工作成绩\"}},{\"type\":\"textarea\",\"field\":\"problem\",\"title\":\"存在问题\",\"props\":{\"placeholder\":\"请输入存在问题\"}},{\"type\":\"textarea\",\"field\":\"suggestion\",\"title\":\"改进建议\",\"props\":{\"placeholder\":\"请输入改进建议\"}},{\"type\":\"input\",\"field\":\"score\",\"title\":\"考核评分\",\"props\":{\"type\":\"number\",\"placeholder\":\"请输入考核评分\"}},{\"type\":\"input\",\"field\":\"assessor\",\"title\":\"考核人\",\"props\":{\"placeholder\":\"请输入考核人\"}},{\"type\":\"datePicker\",\"field\":\"assessTime\",\"title\":\"考核时间\",\"props\":{\"type\":\"datetime\",\"placeholder\":\"请选择考核时间\"}},{\"type\":\"textarea\",\"field\":\"remark\",\"title\":\"备注\",\"props\":{\"placeholder\":\"请输入备注（可选）\"}}]}" ,
                
            },
            // 离职交接表单
            new HbtForm
            {
                FormName = "离职交接表单",
                FormDesc = "员工离职交接表单",
                FormConfig = "{\"rule\":[{\"type\":\"input\",\"field\":\"userName\",\"title\":\"员工姓名\",\"props\":{\"placeholder\":\"请输入员工姓名\"}},{\"type\":\"input\",\"field\":\"dept\",\"title\":\"部门\",\"props\":{\"placeholder\":\"请输入部门\"}},{\"type\":\"input\",\"field\":\"post\",\"title\":\"岗位\",\"props\":{\"placeholder\":\"请输入岗位\"}},{\"type\":\"textarea\",\"field\":\"reason\",\"title\":\"离职原因\",\"props\":{\"placeholder\":\"请输入离职原因\"}},{\"type\":\"datePicker\",\"field\":\"leaveDate\",\"title\":\"离职日期\",\"props\":{\"type\":\"datetime\",\"placeholder\":\"请选择离职日期\"}},{\"type\":\"textarea\",\"field\":\"handover\",\"title\":\"交接事项\",\"props\":{\"placeholder\":\"请输入交接事项\"}},{\"type\":\"input\",\"field\":\"handoverUser\",\"title\":\"交接人\",\"props\":{\"placeholder\":\"请输入交接人\"}},{\"type\":\"input\",\"field\":\"receiver\",\"title\":\"接收人\",\"props\":{\"placeholder\":\"请输入接收人\"}},{\"type\":\"datePicker\",\"field\":\"handoverTime\",\"title\":\"交接完成时间\",\"props\":{\"type\":\"datetime\",\"placeholder\":\"请选择交接完成时间\"}},{\"type\":\"textarea\",\"field\":\"remark\",\"title\":\"备注\",\"props\":{\"placeholder\":\"请输入备注（可选）\"}}]}" ,
                
            },
            // 调休申请表单
            new HbtForm
            {
                FormName = "调休申请表单",
                FormDesc = "员工调休申请表单",
                FormConfig = "{\"rule\":[{\"type\":\"input\",\"field\":\"userName\",\"title\":\"员工姓名\",\"props\":{\"placeholder\":\"请输入员工姓名\"}},{\"type\":\"input\",\"field\":\"dept\",\"title\":\"部门\",\"props\":{\"placeholder\":\"请输入部门\"}},{\"type\":\"datePicker\",\"field\":\"offDate\",\"title\":\"调休日期\",\"props\":{\"type\":\"datetime\",\"placeholder\":\"请选择调休日期\"}},{\"type\":\"input\",\"field\":\"offHours\",\"title\":\"调休时长(小时)\",\"props\":{\"type\":\"number\",\"placeholder\":\"请输入调休时长\"}},{\"type\":\"textarea\",\"field\":\"reason\",\"title\":\"调休原因\",\"props\":{\"placeholder\":\"请输入调休原因\"}},{\"type\":\"input\",\"field\":\"applicant\",\"title\":\"申请人\",\"props\":{\"placeholder\":\"请输入申请人\"}},{\"type\":\"datePicker\",\"field\":\"applyTime\",\"title\":\"申请时间\",\"props\":{\"type\":\"datetime\",\"placeholder\":\"请选择申请时间\"}},{\"type\":\"textarea\",\"field\":\"remark\",\"title\":\"备注\",\"props\":{\"placeholder\":\"请输入备注（可选）\"}}]}" ,
                
            },
            // IT相关帐号申请表单
            new HbtForm
            {
                FormName = "IT相关帐号申请表单",
                FormDesc = "IT系统帐号开通申请表单",
                FormConfig = "{\"rule\":[{\"type\":\"input\",\"field\":\"userName\",\"title\":\"申请人姓名\",\"props\":{\"placeholder\":\"请输入申请人姓名\"}},{\"type\":\"input\",\"field\":\"dept\",\"title\":\"部门\",\"props\":{\"placeholder\":\"请输入部门\"}},{\"type\":\"input\",\"field\":\"post\",\"title\":\"岗位\",\"props\":{\"placeholder\":\"请输入岗位\"}},{\"type\":\"select\",\"field\":\"accountType\",\"title\":\"申请帐号类型\",\"props\":{\"placeholder\":\"请选择帐号类型\"},\"options\":[{\"label\":\"邮箱\",\"value\":1},{\"label\":\"ERP\",\"value\":2},{\"label\":\"CRM\",\"value\":3},{\"label\":\"开发平台\",\"value\":4},{\"label\":\"其他\",\"value\":5}]},{\"type\":\"textarea\",\"field\":\"reason\",\"title\":\"申请原因\",\"props\":{\"placeholder\":\"请输入申请原因\"}},{\"type\":\"datePicker\",\"field\":\"expectTime\",\"title\":\"期望开通时间\",\"props\":{\"type\":\"datetime\",\"placeholder\":\"请选择期望开通时间\"}},{\"type\":\"textarea\",\"field\":\"remark\",\"title\":\"备注\",\"props\":{\"placeholder\":\"请输入备注（可选）\"}}]}" ,
                
            },
            // 招聘申请表单
            new HbtForm
            {
                FormName = "招聘申请表单",
                FormDesc = "岗位招聘申请表单",
                FormConfig = "{\"rule\":[{\"type\":\"input\",\"field\":\"dept\",\"title\":\"申请部门\",\"props\":{\"placeholder\":\"请输入申请部门\"}},{\"type\":\"input\",\"field\":\"post\",\"title\":\"岗位名称\",\"props\":{\"placeholder\":\"请输入岗位名称\"}},{\"type\":\"input\",\"field\":\"count\",\"title\":\"招聘人数\",\"props\":{\"type\":\"number\",\"placeholder\":\"请输入招聘人数\"}},{\"type\":\"textarea\",\"field\":\"duty\",\"title\":\"岗位职责\",\"props\":{\"placeholder\":\"请输入岗位职责\"}},{\"type\":\"textarea\",\"field\":\"requirement\",\"title\":\"任职要求\",\"props\":{\"placeholder\":\"请输入任职要求\"}},{\"type\":\"input\",\"field\":\"salary\",\"title\":\"薪资范围\",\"props\":{\"placeholder\":\"请输入薪资范围\"}},{\"type\":\"datePicker\",\"field\":\"expectTime\",\"title\":\"预计到岗时间\",\"props\":{\"type\":\"datetime\",\"placeholder\":\"请选择预计到岗时间\"}},{\"type\":\"textarea\",\"field\":\"reason\",\"title\":\"申请原因\",\"props\":{\"placeholder\":\"请输入申请原因\"}},{\"type\":\"input\",\"field\":\"applicant\",\"title\":\"申请人\",\"props\":{\"placeholder\":\"请输入申请人\"}},{\"type\":\"datePicker\",\"field\":\"applyTime\",\"title\":\"申请时间\",\"props\":{\"type\":\"datetime\",\"placeholder\":\"请选择申请时间\"}},{\"type\":\"textarea\",\"field\":\"remark\",\"title\":\"备注\",\"props\":{\"placeholder\":\"请输入备注（可选）\"}}]}" ,
                
            },
            // IT权限申请表单
            new HbtForm
            {
                FormName = "IT权限申请表单",
                FormDesc = "IT系统权限开通申请表单",
                FormConfig = "{\"rule\":[{\"type\":\"input\",\"field\":\"userName\",\"title\":\"申请人姓名\",\"props\":{\"placeholder\":\"请输入申请人姓名\"}},{\"type\":\"input\",\"field\":\"dept\",\"title\":\"部门\",\"props\":{\"placeholder\":\"请输入部门\"}},{\"type\":\"input\",\"field\":\"post\",\"title\":\"岗位\",\"props\":{\"placeholder\":\"请输入岗位\"}},{\"type\":\"input\",\"field\":\"system\",\"title\":\"申请系统/平台\",\"props\":{\"placeholder\":\"请输入系统或平台名称\"}},{\"type\":\"select\",\"field\":\"permissionType\",\"title\":\"申请权限类型\",\"props\":{\"placeholder\":\"请选择权限类型\"},\"options\":[{\"label\":\"只读\",\"value\":1},{\"label\":\"读写\",\"value\":2},{\"label\":\"管理员\",\"value\":3},{\"label\":\"其他\",\"value\":4}]},{\"type\":\"textarea\",\"field\":\"permissionContent\",\"title\":\"申请权限内容\",\"props\":{\"placeholder\":\"请输入具体权限内容\"}},{\"type\":\"textarea\",\"field\":\"reason\",\"title\":\"申请原因\",\"props\":{\"placeholder\":\"请输入申请原因\"}},{\"type\":\"datePicker\",\"field\":\"expectTime\",\"title\":\"期望开通时间\",\"props\":{\"type\":\"datetime\",\"placeholder\":\"请选择期望开通时间\"}},{\"type\":\"input\",\"field\":\"applicant\",\"title\":\"申请人\",\"props\":{\"placeholder\":\"请输入申请人\"}},{\"type\":\"datePicker\",\"field\":\"applyTime\",\"title\":\"申请时间\",\"props\":{\"type\":\"datetime\",\"placeholder\":\"请选择申请时间\"}},{\"type\":\"textarea\",\"field\":\"remark\",\"title\":\"备注\",\"props\":{\"placeholder\":\"请输入备注（可选）\"}}]}" ,
                
            },
            // 文件签收表单
            new HbtForm
            {
                FormName = "文件签收表单",
                FormDesc = "文件签收登记表单",
                FormConfig = "{\"rule\":[{\"type\":\"input\",\"field\":\"fileName\",\"title\":\"文件名称\",\"props\":{\"placeholder\":\"请输入文件名称\"}},{\"type\":\"input\",\"field\":\"fileCode\",\"title\":\"文件编号\",\"props\":{\"placeholder\":\"请输入文件编号\"}},{\"type\":\"input\",\"field\":\"fileType\",\"title\":\"文件类型\",\"props\":{\"placeholder\":\"请输入文件类型\"}},{\"type\":\"input\",\"field\":\"fileCount\",\"title\":\"文件份数\",\"props\":{\"type\":\"number\",\"placeholder\":\"请输入文件份数\"}},{\"type\":\"input\",\"field\":\"signUser\",\"title\":\"签收人姓名\",\"props\":{\"placeholder\":\"请输入签收人姓名\"}},{\"type\":\"input\",\"field\":\"signDept\",\"title\":\"签收部门\",\"props\":{\"placeholder\":\"请输入签收部门\"}},{\"type\":\"datePicker\",\"field\":\"signTime\",\"title\":\"签收时间\",\"props\":{\"type\":\"datetime\",\"placeholder\":\"请选择签收时间\"}},{\"type\":\"input\",\"field\":\"sendUser\",\"title\":\"发送人\",\"props\":{\"placeholder\":\"请输入发送人\"}},{\"type\":\"input\",\"field\":\"sendDept\",\"title\":\"发送部门\",\"props\":{\"placeholder\":\"请输入发送部门\"}},{\"type\":\"datePicker\",\"field\":\"sendTime\",\"title\":\"发送时间\",\"props\":{\"type\":\"datetime\",\"placeholder\":\"请选择发送时间\"}},{\"type\":\"textarea\",\"field\":\"remark\",\"title\":\"备注\",\"props\":{\"placeholder\":\"请输入备注（可选）\"}}]}" ,
                
            },
        };

        foreach (var form in forms)
        {
            var existingForm = await _formRepository.GetFirstAsync(x => x.FormName == form.FormName);
            if (existingForm == null)
            {
                form.FormCategory = 0;
                form.FormVersion = "v1.0.0-draft";
                form.Status = 5;
                form.CreateBy = "Hbt365";
                form.CreateTime = DateTime.Now;
                form.UpdateBy = "Hbt365";
                form.UpdateTime = DateTime.Now;
                await _formRepository.CreateAsync(form);
                insertCount++;
                _logger.Info($"[创建] 表单 '{form.FormName}' 创建成功");
            }
            else
            {
                existingForm.FormCategory = 0;
                existingForm.FormVersion = "v1.0.0-draft";
                existingForm.Status = 5;
                existingForm.FormDesc = form.FormDesc;
                existingForm.FormConfig = form.FormConfig;
                existingForm.UpdateBy = "Hbt365";
                existingForm.UpdateTime = DateTime.Now;
                await _formRepository.UpdateAsync(existingForm);
                updateCount++;
                _logger.Info($"[更新] 表单 '{existingForm.FormName}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }
} 