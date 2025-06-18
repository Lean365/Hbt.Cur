#nullable enable

using SqlSugar;
using Lean.Hbt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtPlant.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V1.0.0
// 描述    : 工厂实体类
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Lean.Hbt.Domain.Entities.Logistics.Material
{
    /// <summary>
    /// 工厂实体类（字段参照SAP标准简写）
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// </remarks>
    [SugarTable("hbt_logistics_plant", "工厂表")]
    [SugarIndex("ix_werks", nameof(Werks), OrderByType.Asc, true)]
    public class HbtPlant : HbtBaseEntity
    {
        /// <summary>集团</summary>
        [SugarColumn(ColumnName = "mandt", ColumnDescription = "集团", Length = 3, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Mandt { get; set; }

        /// <summary>工厂</summary>
        [SugarColumn(ColumnName = "werks", ColumnDescription = "工厂", Length = 4, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Werks { get; set; }

        /// <summary>名称</summary>
        [SugarColumn(ColumnName = "name1", ColumnDescription = "名称", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Name1 { get; set; }

        /// <summary>评估范围</summary>
        [SugarColumn(ColumnName = "bwkey", ColumnDescription = "评估范围", Length = 4, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Bwkey { get; set; }

        /// <summary>工厂的客户号</summary>
        [SugarColumn(ColumnName = "kunnr", ColumnDescription = "工厂的客户号", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Kunnr { get; set; }

        /// <summary>工厂供应商号码</summary>
        [SugarColumn(ColumnName = "lifnr", ColumnDescription = "工厂供应商号码", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Lifnr { get; set; }

        /// <summary>工厂日历码</summary>
        [SugarColumn(ColumnName = "fabkl", ColumnDescription = "工厂日历码", Length = 2, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Fabkl { get; set; }

        /// <summary>名称2</summary>
        [SugarColumn(ColumnName = "name2", ColumnDescription = "名称2", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Name2 { get; set; }

        /// <summary>住宅号及街道</summary>
        [SugarColumn(ColumnName = "stras", ColumnDescription = "住宅号及街道", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Stras { get; set; }

        /// <summary>邮政信箱</summary>
        [SugarColumn(ColumnName = "pfach", ColumnDescription = "邮政信箱", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Pfach { get; set; }

        /// <summary>邮政编码</summary>
        [SugarColumn(ColumnName = "pstlz", ColumnDescription = "邮政编码", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Pstlz { get; set; }

        /// <summary>城市</summary>
        [SugarColumn(ColumnName = "ort01", ColumnDescription = "城市", Length = 25, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Ort01 { get; set; }

        /// <summary>采购组织</summary>
        [SugarColumn(ColumnName = "ekorg", ColumnDescription = "采购组织", Length = 4, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Ekorg { get; set; }

        /// <summary>用于公司间出具发票的销售组织</summary>
        [SugarColumn(ColumnName = "vkorg", ColumnDescription = "用于公司间出具发票的销售组织", Length = 4, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Vkorg { get; set; }

        /// <summary>标识: 批量状态管理激活</summary>
        [SugarColumn(ColumnName = "chazv", ColumnDescription = "标识: 批量状态管理激活", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Chazv { get; set; }

        /// <summary>标识: 工厂级别的条件</summary>
        [SugarColumn(ColumnName = "kkowk", ColumnDescription = "标识: 工厂级别的条件", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Kkowk { get; set; }

        /// <summary>标识: 源清单要求</summary>
        [SugarColumn(ColumnName = "kordb", ColumnDescription = "标识: 源清单要求", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Kordb { get; set; }

        /// <summary>激活需求计划</summary>
        [SugarColumn(ColumnName = "bedpl", ColumnDescription = "激活需求计划", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Bedpl { get; set; }

        /// <summary>国家键值</summary>
        [SugarColumn(ColumnName = "land1", ColumnDescription = "国家键值", Length = 3, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Land1 { get; set; }

        /// <summary>地区</summary>
        [SugarColumn(ColumnName = "regio", ColumnDescription = "地区", Length = 3, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Regio { get; set; }

        /// <summary>县代码</summary>
        [SugarColumn(ColumnName = "counc", ColumnDescription = "县代码", Length = 3, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Counc { get; set; }

        /// <summary>城市代码</summary>
        [SugarColumn(ColumnName = "cityc", ColumnDescription = "城市代码", Length = 4, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Cityc { get; set; }

        /// <summary>地址</summary>
        [SugarColumn(ColumnName = "adrnr", ColumnDescription = "地址", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Adrnr { get; set; }

        /// <summary>维护计划工厂</summary>
        [SugarColumn(ColumnName = "iwerk", ColumnDescription = "维护计划工厂", Length = 4, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Iwerk { get; set; }

        /// <summary>地区税务代码</summary>
        [SugarColumn(ColumnName = "txjcd", ColumnDescription = "地区税务代码", Length = 15, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Txjcd { get; set; }

        /// <summary>公司内出具发票分销渠道</summary>
        [SugarColumn(ColumnName = "vtweg", ColumnDescription = "公司内出具发票分销渠道", Length = 2, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Vtweg { get; set; }

        /// <summary>为公司内部出具发票的产品组</summary>
        [SugarColumn(ColumnName = "spart", ColumnDescription = "为公司内部出具发票的产品组", Length = 2, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Spart { get; set; }

        /// <summary>语言代码</summary>
        [SugarColumn(ColumnName = "spras", ColumnDescription = "语言代码", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Spras { get; set; }

        /// <summary>SOP工厂</summary>
        [SugarColumn(ColumnName = "wksop", ColumnDescription = "SOP工厂", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Wksop { get; set; }

        /// <summary>差异码</summary>
        [SugarColumn(ColumnName = "awsls", ColumnDescription = "差异码", Length = 6, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Awsls { get; set; }

        /// <summary>工厂种类</summary>
        [SugarColumn(ColumnName = "vlfkz", ColumnDescription = "工厂种类", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Vlfkz { get; set; }

        /// <summary>销售地区</summary>
        [SugarColumn(ColumnName = "bzirk", ColumnDescription = "销售地区", Length = 6, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Bzirk { get; set; }

        /// <summary>供应地区(被供应的地区)</summary>
        [SugarColumn(ColumnName = "zone1", ColumnDescription = "供应地区(被供应的地区)", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Zone1 { get; set; }

        /// <summary>税收标识：工厂（采购）</summary>
        [SugarColumn(ColumnName = "taxiw", ColumnDescription = "税收标识：工厂（采购）", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Taxiw { get; set; }

        /// <summary>将常规供应商考虑进去</summary>
        [SugarColumn(ColumnName = "bzqhl", ColumnDescription = "将常规供应商考虑进去", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Bzqhl { get; set; }

        /// <summary>第一封催询单天数</summary>
        [SugarColumn(ColumnName = "let01", ColumnDescription = "第一封催询单天数", ColumnDataType = "decimal(18,2)", IsNullable = true)]
        public decimal? Let01 { get; set; }

        /// <summary>第二封催询单的天数</summary>
        [SugarColumn(ColumnName = "let02", ColumnDescription = "第二封催询单的天数", ColumnDataType = "decimal(18,2)", IsNullable = true)]
        public decimal? Let02 { get; set; }

        /// <summary>第三封催询单的天数</summary>
        [SugarColumn(ColumnName = "let03", ColumnDescription = "第三封催询单的天数", ColumnDataType = "decimal(18,2)", IsNullable = true)]
        public decimal? Let03 { get; set; }

        /// <summary>采购订单容差的天数－压缩信息记录－SU</summary>
        [SugarColumn(ColumnName = "betol", ColumnDescription = "采购订单容差的天数－压缩信息记录－SU", Length = 3, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Betol { get; set; }

        /// <summary>关于为库存转储确定销售范围的规则</summary>
        [SugarColumn(ColumnName = "vtbfi", ColumnDescription = "关于为库存转储确定销售范围的规则", Length = 2, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Vtbfi { get; set; }

        /// <summary>工厂层的分配参数文件</summary>
        [SugarColumn(ColumnName = "fprfw", ColumnDescription = "工厂层的分配参数文件", Length = 3, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Fprfw { get; set; }

        /// <summary>关于主文挡记录中心归档标记</summary>
        [SugarColumn(ColumnName = "achvm", ColumnDescription = "关于主文挡记录中心归档标记", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Achvm { get; set; }

        /// <summary>批次记录: 使用DMS的类型</summary>
        [SugarColumn(ColumnName = "dvsart", ColumnDescription = "批次记录: 使用DMS的类型", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Dvsart { get; set; }

        /// <summary>节点类型:替代链网络</summary>
        [SugarColumn(ColumnName = "nodetype", ColumnDescription = "节点类型:替代链网络", Length = 3, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Nodetype { get; set; }

        /// <summary>名称组成结构</summary>
        [SugarColumn(ColumnName = "nschema", ColumnDescription = "名称组成结构", Length = 4, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Nschema { get; set; }

        /// <summary>成本对象控制联接激活</summary>
        [SugarColumn(ColumnName = "pkosa", ColumnDescription = "成本对象控制联接激活", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Pkosa { get; set; }

        /// <summary>混合成本核算中更新激活</summary>
        [SugarColumn(ColumnName = "misch", ColumnDescription = "混合成本核算中更新激活", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Misch { get; set; }

        /// <summary>实际成本核算中更新激活</summary>
        [SugarColumn(ColumnName = "mgvupd", ColumnDescription = "实际成本核算中更新激活", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Mgvupd { get; set; }

        /// <summary>装运点/接收点</summary>
        [SugarColumn(ColumnName = "vstel", ColumnDescription = "装运点/接收点", Length = 4, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Vstel { get; set; }

        /// <summary>更新数量结构中的作业消耗</summary>
        [SugarColumn(ColumnName = "mgvlaupd", ColumnDescription = "更新数量结构中的作业消耗", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Mgvlaupd { get; set; }

        /// <summary>成本中心的信用控制</summary>
        [SugarColumn(ColumnName = "mgvlareval", ColumnDescription = "成本中心的信用控制", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Mgvlareval { get; set; }

        /// <summary>通过 ATP 调用附加功能货源确定</summary>
        [SugarColumn(ColumnName = "sourcing", ColumnDescription = "通过 ATP 调用附加功能货源确定", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Sourcing { get; set; }

        /// <summary>交易评估标识</summary>
        [SugarColumn(ColumnName = "oilival", ColumnDescription = "交易评估标识", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Oilival { get; set; }

        /// <summary>供应商类型 (炼油厂/工厂/其它) (巴西)</summary>
        [SugarColumn(ColumnName = "oihvtype", ColumnDescription = "供应商类型 (炼油厂/工厂/其它) (巴西)", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Oihvtype { get; set; }

        /// <summary>允许的 IPI 信贷</summary>
        [SugarColumn(ColumnName = "oihcredipi", ColumnDescription = "允许的 IPI 信贷", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Oihcredipi { get; set; }

        /// <summary>区分商店、百货公司、车间的存储类别</summary>
        [SugarColumn(ColumnName = "storetype", ColumnDescription = "区分商店、百货公司、车间的存储类别", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Storetype { get; set; }
    }
} 