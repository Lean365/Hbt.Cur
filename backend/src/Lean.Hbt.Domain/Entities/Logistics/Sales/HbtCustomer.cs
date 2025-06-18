#nullable enable

using SqlSugar;
using Lean.Hbt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtCustomer.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V1.0.0
// 描述    : 客户实体类
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Lean.Hbt.Domain.Entities.Logistics.Sales
{
    /// <summary>
    /// 客户实体类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// </remarks>
    [SugarTable("hbt_logistics_sales_customer", "客户表")]
    [SugarIndex("ix_customer_code", nameof(Kunnr), OrderByType.Asc, true)]
    public class HbtCustomer : HbtBaseEntity
    {
        /// <summary>集团</summary>
        [SugarColumn(ColumnName = "mandt", ColumnDescription = "集团", Length = 6, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Mandt { get; set; }

        /// <summary>客户编号</summary>
        [SugarColumn(ColumnName = "kunnr", ColumnDescription = "客户编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Kunnr { get; set; }

        /// <summary>国家代码</summary>
        [SugarColumn(ColumnName = "land1", ColumnDescription = "国家代码", Length = 6, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Land1 { get; set; }

        /// <summary>名称1</summary>
        [SugarColumn(ColumnName = "name1", ColumnDescription = "名称 1", Length = 70, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Name1 { get; set; }

        /// <summary>名称2</summary>
        [SugarColumn(ColumnName = "name2", ColumnDescription = "名称 2", Length = 70, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Name2 { get; set; }

        /// <summary>城市</summary>
        [SugarColumn(ColumnName = "ort01", ColumnDescription = "城市", Length = 70, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Ort01 { get; set; }

        /// <summary>邮政编码</summary>
        [SugarColumn(ColumnName = "pstlz", ColumnDescription = "邮政编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Pstlz { get; set; }

        /// <summary>地区</summary>
        [SugarColumn(ColumnName = "regio", ColumnDescription = "地区（省/自治区/直辖市、市、县）", Length = 6, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Regio { get; set; }

        /// <summary>排序字段</summary>
        [SugarColumn(ColumnName = "sortl", ColumnDescription = "排序字段", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Sortl { get; set; }

        /// <summary>住宅号及街道</summary>
        [SugarColumn(ColumnName = "stras", ColumnDescription = "住宅号及街道", Length = 70, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Stras { get; set; }

        /// <summary>第一个电话号</summary>
        [SugarColumn(ColumnName = "telf1", ColumnDescription = "第一个电话号", Length = 32, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Telf1 { get; set; }

        /// <summary>传真号</summary>
        [SugarColumn(ColumnName = "telfx", ColumnDescription = "传真号", Length = 62, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Telfx { get; set; }

        /// <summary>指示符:科目是一次性科目吗?</summary>
        [SugarColumn(ColumnName = "xcpdk", ColumnDescription = "指示符:科目是一次性科目吗?", Length = 2, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Xcpdk { get; set; }

        /// <summary>地址</summary>
        [SugarColumn(ColumnName = "adrnr", ColumnDescription = "地址", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Adrnr { get; set; }

        /// <summary>检索词1</summary>
        [SugarColumn(ColumnName = "mcod1", ColumnDescription = "检索词1", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Mcod1 { get; set; }

        /// <summary>检索词2</summary>
        [SugarColumn(ColumnName = "mcod2", ColumnDescription = "检索词2", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Mcod2 { get; set; }

        /// <summary>检索词3</summary>
        [SugarColumn(ColumnName = "mcod3", ColumnDescription = "检索词3", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Mcod3 { get; set; }

        /// <summary>标题</summary>
        [SugarColumn(ColumnName = "anred", ColumnDescription = "标题", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Anred { get; set; }

        /// <summary>客户主要订单块</summary>
        [SugarColumn(ColumnName = "aufsd", ColumnDescription = "客户主要订单块", Length = 4, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Aufsd { get; set; }

        /// <summary>高速列车站</summary>
        [SugarColumn(ColumnName = "bahne", ColumnDescription = "高速列车站", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Bahne { get; set; }

        /// <summary>列车站</summary>
        [SugarColumn(ColumnName = "bahns", ColumnDescription = "列车站", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Bahns { get; set; }

        /// <summary>标准公司号</summary>
        [SugarColumn(ColumnName = "bbbnr", ColumnDescription = "标准公司号", Length = 14, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Bbbnr { get; set; }

        /// <summary>国际区位号（部分 2）</summary>
        [SugarColumn(ColumnName = "bbsnr", ColumnDescription = "国际区位号（部分 2）", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Bbsnr { get; set; }

        /// <summary>权限组</summary>
        [SugarColumn(ColumnName = "begru", ColumnDescription = "权限组", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Begru { get; set; }

        /// <summary>行业代码</summary>
        [SugarColumn(ColumnName = "brsch", ColumnDescription = "行业代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Brsch { get; set; }

        /// <summary>国际区位码的校验位</summary>
        [SugarColumn(ColumnName = "bubkz", ColumnDescription = "国际区位码的校验位", Length = 2, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Bubkz { get; set; }

        /// <summary>数据通讯线路号码</summary>
        [SugarColumn(ColumnName = "datlt", ColumnDescription = "数据通讯线路号码", Length = 28, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Datlt { get; set; }

        /// <summary>记录创建日期</summary>
        [SugarColumn(ColumnName = "erdat", ColumnDescription = "记录创建日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? Erdat { get; set; }

        /// <summary>创建对象的人员名称</summary>
        [SugarColumn(ColumnName = "ernam", ColumnDescription = "创建对象的人员名称", Length = 24, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Ernam { get; set; }

        /// <summary>指示符：卸货点存在</summary>
        [SugarColumn(ColumnName = "exabl", ColumnDescription = "指示符：卸货点存在", Length = 2, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Exabl { get; set; }

        /// <summary>客户集中出具发票块</summary>
        [SugarColumn(ColumnName = "faksd", ColumnDescription = "客户集中出具发票块", Length = 4, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Faksd { get; set; }

        /// <summary>带有财务地址的主记录帐号</summary>
        [SugarColumn(ColumnName = "fiskn", ColumnDescription = "带有财务地址的主记录帐号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Fiskn { get; set; }

        /// <summary>工作时间日历</summary>
        [SugarColumn(ColumnName = "knazk", ColumnDescription = "工作时间日历", Length = 4, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Knazk { get; set; }

        /// <summary>一个备选付款人的帐号</summary>
        [SugarColumn(ColumnName = "knrza", ColumnDescription = "一个备选付款人的帐号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Knrza { get; set; }

        /// <summary>组代码</summary>
        [SugarColumn(ColumnName = "konzs", ColumnDescription = "组代码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Konzs { get; set; }

        /// <summary>客户帐户组</summary>
        [SugarColumn(ColumnName = "ktokd", ColumnDescription = "客户帐户组", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Ktokd { get; set; }

        /// <summary>客户分类</summary>
        [SugarColumn(ColumnName = "kukla", ColumnDescription = "客户分类", Length = 4, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Kukla { get; set; }

        /// <summary>供应商或债权人的帐号</summary>
        [SugarColumn(ColumnName = "lifnr", ColumnDescription = "供应商或债权人的帐号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Lifnr { get; set; }

        /// <summary>客户主要交货块</summary>
        [SugarColumn(ColumnName = "lifsd", ColumnDescription = "客户主要交货块", Length = 4, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Lifsd { get; set; }

        /// <summary>城市协调</summary>
        [SugarColumn(ColumnName = "locco", ColumnDescription = "城市协调", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Locco { get; set; }

        /// <summary>主记录的集中删除标志</summary>
        [SugarColumn(ColumnName = "loevm", ColumnDescription = "主记录的集中删除标志", Length = 2, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Loevm { get; set; }

        /// <summary>名称3</summary>
        [SugarColumn(ColumnName = "name3", ColumnDescription = "名称3", Length = 70, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Name3 { get; set; }

        /// <summary>名称4</summary>
        [SugarColumn(ColumnName = "name4", ColumnDescription = "名称4", Length = 70, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Name4 { get; set; }

        /// <summary>Nielsen 标识</summary>
        [SugarColumn(ColumnName = "niels", ColumnDescription = "Nielsen 标识", Length = 4, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Niels { get; set; }

        /// <summary>地区</summary>
        [SugarColumn(ColumnName = "ort02", ColumnDescription = "地区", Length = 70, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Ort02 { get; set; }

        /// <summary>邮政信箱</summary>
        [SugarColumn(ColumnName = "pfach", ColumnDescription = "邮政信箱", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Pfach { get; set; }

        /// <summary>邮箱的邮编</summary>
        [SugarColumn(ColumnName = "pstl2", ColumnDescription = "邮箱的邮编", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Pstl2 { get; set; }

        /// <summary>县代码</summary>
        [SugarColumn(ColumnName = "counc", ColumnDescription = "县代码", Length = 6, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Counc { get; set; }

        /// <summary>城市代码</summary>
        [SugarColumn(ColumnName = "cityc", ColumnDescription = "城市代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Cityc { get; set; }

        /// <summary>地区市场</summary>
        [SugarColumn(ColumnName = "rpmkr", ColumnDescription = "地区市场", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Rpmkr { get; set; }

        /// <summary>中心记帐冻结</summary>
        [SugarColumn(ColumnName = "sperr", ColumnDescription = "中心记帐冻结", Length = 2, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Sperr { get; set; }

        /// <summary>语言代码</summary>
        [SugarColumn(ColumnName = "spras", ColumnDescription = "语言代码", Length = 2, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Spras { get; set; }
    }
} 