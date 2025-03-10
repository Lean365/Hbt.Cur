//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedDept.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 部门数据初始化类
//===================================================================

using Lean.Hbt.Common.Enums;
using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.Entities.Admin;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.IServices;
using Lean.Hbt.Infrastructure.Data.Contexts;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 部门数据初始化类
/// </summary>
public class HbtDbSeedDept
{
    private readonly IHbtRepository<HbtDept> _deptRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="deptRepository">部门仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedDept(IHbtRepository<HbtDept> deptRepository, IHbtLogger logger)
    {
        _deptRepository = deptRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化部门数据
    /// </summary>
    public async Task<(int, int)> InitializeDeptAsync()
    {
        int insertCount = 0;
        int updateCount = 0;
        long nextId = 1;

        // 创建部门列表
        var defaultDepts = new List<HbtDept>();

        // 总公司
        var headquarters = new HbtDept
        {
            Id = nextId++,
            DeptName = "总公司",
            ParentId = 0,
            OrderNum = 1,
            Leader = "董事长",
            Phone = "13800138000",
            Email = "chairman@lean365.com",
            Status = 0,
            TenantId = 0,
            CreateBy = "system",
            CreateTime = DateTime.Now,
            UpdateBy = "system",
            UpdateTime = DateTime.Now,
            Remark = "Headquarters;本社"
        };
        defaultDepts.Add(headquarters);

        // 分公司
        var branch = new HbtDept
        {
            Id = nextId++,
            DeptName = "分公司",
            ParentId = headquarters.Id,
            OrderNum = 1,
            Leader = "分公司社长",
            Phone = "13800138001",
            Email = "president@lean365.com",
            Status = 0,
            TenantId = 0,
            CreateBy = "system",
            CreateTime = DateTime.Now,
            UpdateBy = "system",
            UpdateTime = DateTime.Now,
            Remark = "Branch Company;支社"
        };
        defaultDepts.Add(branch);

        // 总经办
        var gmOffice = new HbtDept
        {
            Id = nextId++,
            DeptName = "总经办",
            ParentId = branch.Id,
            OrderNum = 1,
            Leader = "总经理",
            Phone = "13800138002",
            Email = "gm@lean365.com",
            Status = 0,
            TenantId = 0,
            CreateBy = "system",
            CreateTime = DateTime.Now,
            UpdateBy = "system",
            UpdateTime = DateTime.Now,
            Remark = "General Manager Office;社長室"
        };
        defaultDepts.Add(gmOffice);

        // 总务部
        var gaOffice = new HbtDept
        {
            Id = nextId++,
            DeptName = "总务部",
            ParentId = gmOffice.Id,
            OrderNum = 1,
            Leader = "总务部长",
            Phone = "13800138003",
            Email = "ga@lean365.com",
            Status = 0,
            TenantId = 0,
            CreateBy = "system",
            CreateTime = DateTime.Now,
            UpdateBy = "system",
            UpdateTime = DateTime.Now,
            Remark = "General Affairs Department;総務部"
        };
        defaultDepts.Add(gaOffice);

        // 财务部
        var financeOffice = new HbtDept
        {
            Id = nextId++,
            DeptName = "财务部",
            ParentId = gmOffice.Id,
            OrderNum = 2,
            Leader = "财务部长",
            Phone = "13800138004",
            Email = "finance@lean365.com",
            Status = 0,
            TenantId = 0,
            CreateBy = "system",
            CreateTime = DateTime.Now,
            UpdateBy = "system",
            UpdateTime = DateTime.Now,
            Remark = "Finance Department;財務部"
        };
        defaultDepts.Add(financeOffice);

        // IT部
        var itOffice = new HbtDept
        {
            Id = nextId++,
            DeptName = "IT部",
            ParentId = gmOffice.Id,
            OrderNum = 3,
            Leader = "IT部长",
            Phone = "13800138005",
            Email = "it@lean365.com",
            Status = 0,
            TenantId = 0,
            CreateBy = "system",
            CreateTime = DateTime.Now,
            UpdateBy = "system",
            UpdateTime = DateTime.Now,
            Remark = "IT Department;IT部"
        };
        defaultDepts.Add(itOffice);

        // 事业推进本部
        var businessDivision = new HbtDept
        {
            Id = nextId++,
            DeptName = "事业推进本部",
            ParentId = gmOffice.Id,
            OrderNum = 4,
            Leader = "事业推进本部长",
            Phone = "13800138006",
            Email = "business@lean365.com",
            Status = 0,
            TenantId = 0,
            CreateBy = "system",
            CreateTime = DateTime.Now,
            UpdateBy = "system",
            UpdateTime = DateTime.Now,
            Remark = "Business Promotion Division;事業推進本部"
        };
        defaultDepts.Add(businessDivision);

        // 管理部
        var adminDept = new HbtDept
        {
            Id = nextId++,
            DeptName = "管理部",
            ParentId = businessDivision.Id,
            OrderNum = 1,
            Leader = "管理部长",
            Phone = "13800138007",
            Email = "admin@lean365.com",
            Status = 0,
            TenantId = 0,
            CreateBy = "system",
            CreateTime = DateTime.Now,
            UpdateBy = "system",
            UpdateTime = DateTime.Now,
            Remark = "Administration Department;管理部"
        };
        defaultDepts.Add(adminDept);

        // 生管课
        var ppcSection = new HbtDept
        {
            Id = nextId++,
            DeptName = "生管课",
            ParentId = adminDept.Id,
            OrderNum = 1,
            Leader = "生管课长",
            Phone = "13800138008",
            Email = "ppc@lean365.com",
            Status = 0,
            TenantId = 0,
            CreateBy = "system",
            CreateTime = DateTime.Now,
            UpdateBy = "system",
            UpdateTime = DateTime.Now,
            Remark = "Production Control Section;生産管理課"
        };
        defaultDepts.Add(ppcSection);

        // 部管课
        var deptMgrSection = new HbtDept
        {
            Id = nextId++,
            DeptName = "部管课",
            ParentId = adminDept.Id,
            OrderNum = 2,
            Leader = "部管课长",
            Phone = "13800138009",
            Email = "deptmgr@lean365.com",
            Status = 0,
            TenantId = 0,
            CreateBy = "system",
            CreateTime = DateTime.Now,
            UpdateBy = "system",
            UpdateTime = DateTime.Now,
            Remark = "Department Management Section;部門管理課"
        };
        defaultDepts.Add(deptMgrSection);

        // 报关课
        var customsSection = new HbtDept
        {
            Id = nextId++,
            DeptName = "报关课",
            ParentId = adminDept.Id,
            OrderNum = 3,
            Leader = "报关课长",
            Phone = "13800138010",
            Email = "customs@lean365.com",
            Status = 0,
            TenantId = 0,
            CreateBy = "system",
            CreateTime = DateTime.Now,
            UpdateBy = "system",
            UpdateTime = DateTime.Now,
            Remark = "Customs Declaration Section;通関課"
        };
        defaultDepts.Add(customsSection);

        // 资材部
        var materialDept = new HbtDept
        {
            Id = nextId++,
            DeptName = "资材部",
            ParentId = businessDivision.Id,
            OrderNum = 2,
            Leader = "资材部长",
            Phone = "13800138011",
            Email = "material@lean365.com",
            Status = 0,
            TenantId = 0,
            CreateBy = "system",
            CreateTime = DateTime.Now,
            UpdateBy = "system",
            UpdateTime = DateTime.Now,
            Remark = "Material Department;資材部"
        };
        defaultDepts.Add(materialDept);

        // 采购课
        var purchaseSection = new HbtDept
        {
            Id = nextId++,
            DeptName = "采购课",
            ParentId = materialDept.Id,
            OrderNum = 1,
            Leader = "采购课长",
            Phone = "13800138012",
            Email = "purchase@lean365.com",
            Status = 0,
            TenantId = 0,
            CreateBy = "system",
            CreateTime = DateTime.Now,
            UpdateBy = "system",
            UpdateTime = DateTime.Now,
            Remark = "Purchasing Section;購買課"
        };
        defaultDepts.Add(purchaseSection);

        // 生产改善推进本部
        var productionDivision = new HbtDept
        {
            Id = nextId++,
            DeptName = "生产改善推进本部",
            ParentId = gmOffice.Id,
            OrderNum = 5,
            Leader = "生产改善推进本部长",
            Phone = "13800138013",
            Email = "production@lean365.com",
            Status = 0,
            TenantId = 0,
            CreateBy = "system",
            CreateTime = DateTime.Now,
            UpdateBy = "system",
            UpdateTime = DateTime.Now,
            Remark = "Production Improvement Division;生産改善推進本部"
        };
        defaultDepts.Add(productionDivision);

        // 技术部
        var techDept = new HbtDept
        {
            Id = nextId++,
            DeptName = "技术部",
            ParentId = productionDivision.Id,
            OrderNum = 1,
            Leader = "技术部长",
            Phone = "13800138014",
            Email = "tech@lean365.com",
            Status = 0,
            TenantId = 0,
            CreateBy = "system",
            CreateTime = DateTime.Now,
            UpdateBy = "system",
            UpdateTime = DateTime.Now,
            Remark = "Technical Department;技術部"
        };
        defaultDepts.Add(techDept);

        // 技术课
        var techSection = new HbtDept
        {
            Id = nextId++,
            DeptName = "技术课",
            ParentId = techDept.Id,
            OrderNum = 1,
            Leader = "技术课长",
            Phone = "13800138015",
            Email = "engineering@lean365.com",
            Status = 0,
            TenantId = 0,
            CreateBy = "system",
            CreateTime = DateTime.Now,
            UpdateBy = "system",
            UpdateTime = DateTime.Now,
            Remark = "Engineering Section;技術課"
        };
        defaultDepts.Add(techSection);

        // 制造部
        var manufacturingDept = new HbtDept
        {
            Id = nextId++,
            DeptName = "制造部",
            ParentId = productionDivision.Id,
            OrderNum = 2,
            Leader = "制造部长",
            Phone = "13800138016",
            Email = "manufacturing@lean365.com",
            Status = 0,
            TenantId = 0,
            CreateBy = "system",
            CreateTime = DateTime.Now,
            UpdateBy = "system",
            UpdateTime = DateTime.Now,
            Remark = "Manufacturing Department;製造部"
        };
        defaultDepts.Add(manufacturingDept);

        // 制一课
        var mfg1Section = new HbtDept
        {
            Id = nextId++,
            DeptName = "制一课",
            ParentId = manufacturingDept.Id,
            OrderNum = 1,
            Leader = "制一课长",
            Phone = "13800138017",
            Email = "mfg1@lean365.com",
            Status = 0,
            TenantId = 0,
            CreateBy = "system",
            CreateTime = DateTime.Now,
            UpdateBy = "system",
            UpdateTime = DateTime.Now,
            Remark = "Manufacturing Section 1;製造1課"
        };
        defaultDepts.Add(mfg1Section);

        // 制二课
        var mfg2Section = new HbtDept
        {
            Id = nextId++,
            DeptName = "制二课",
            ParentId = manufacturingDept.Id,
            OrderNum = 2,
            Leader = "制二课长",
            Phone = "13800138018",
            Email = "mfg2@lean365.com",
            Status = 0,
            TenantId = 0,
            CreateBy = "system",
            CreateTime = DateTime.Now,
            UpdateBy = "system",
            UpdateTime = DateTime.Now,
            Remark = "Manufacturing Section 2;製造2課"
        };
        defaultDepts.Add(mfg2Section);

        // 制技课
        var mfgTechSection = new HbtDept
        {
            Id = nextId++,
            DeptName = "制技课",
            ParentId = manufacturingDept.Id,
            OrderNum = 3,
            Leader = "制技课长",
            Phone = "13800138019",
            Email = "mfgtech@lean365.com",
            Status = 0,
            TenantId = 0,
            CreateBy = "system",
            CreateTime = DateTime.Now,
            UpdateBy = "system",
            UpdateTime = DateTime.Now,
            Remark = "Manufacturing Technology Section;製造技術課"
        };
        defaultDepts.Add(mfgTechSection);

        // 品管部
        var qaDept = new HbtDept
        {
            Id = nextId++,
            DeptName = "品管部",
            ParentId = productionDivision.Id,
            OrderNum = 3,
            Leader = "品管部长",
            Phone = "13800138020",
            Email = "qa@lean365.com",
            Status = 0,
            TenantId = 0,
            CreateBy = "system",
            CreateTime = DateTime.Now,
            UpdateBy = "system",
            UpdateTime = DateTime.Now,
            Remark = "Quality Assurance Department;品質管理部"
        };
        defaultDepts.Add(qaDept);

        // 受检课
        var inspectionSection = new HbtDept
        {
            Id = nextId++,
            DeptName = "受检课",
            ParentId = qaDept.Id,
            OrderNum = 1,
            Leader = "受检课长",
            Phone = "13800138021",
            Email = "inspection@lean365.com",
            Status = 0,
            TenantId = 0,
            CreateBy = "system",
            CreateTime = DateTime.Now,
            UpdateBy = "system",
            UpdateTime = DateTime.Now,
            Remark = "Inspection Section;検査課"
        };
        defaultDepts.Add(inspectionSection);

        // 品管课
        var qcSection = new HbtDept
        {
            Id = nextId++,
            DeptName = "品管课",
            ParentId = qaDept.Id,
            OrderNum = 2,
            Leader = "品管课长",
            Phone = "13800138022",
            Email = "qc@lean365.com",
            Status = 0,
            TenantId = 0,
            CreateBy = "system",
            CreateTime = DateTime.Now,
            UpdateBy = "system",
            UpdateTime = DateTime.Now,
            Remark = "Quality Control Section;品質管理課"
        };
        defaultDepts.Add(qcSection);

        foreach (var dept in defaultDepts)
        {
            var existingDept = await _deptRepository.FirstOrDefaultAsync(d => d.Id == dept.Id);
            if (existingDept == null)
            {
                await _deptRepository.InsertAsync(dept);
                insertCount++;
                _logger.Info($"[创建] 部门 '{dept.DeptName}' 创建成功");
            }
            else
            {
                existingDept.DeptName = dept.DeptName;
                existingDept.ParentId = dept.ParentId;
                existingDept.OrderNum = dept.OrderNum;
                existingDept.Leader = dept.Leader;
                existingDept.Phone = dept.Phone;
                existingDept.Email = dept.Email;
                existingDept.Status = dept.Status;
                existingDept.TenantId = dept.TenantId;
                existingDept.UpdateBy = "system";
                existingDept.UpdateTime = DateTime.Now;

                await _deptRepository.UpdateAsync(existingDept);
                updateCount++;
                _logger.Info($"[更新] 部门 '{existingDept.DeptName}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }
} 