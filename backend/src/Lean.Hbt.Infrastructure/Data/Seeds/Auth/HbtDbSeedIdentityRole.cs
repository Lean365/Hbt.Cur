//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedRole.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 角色数据初始化类 - 使用仓储工厂模式
//===================================================================

using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.Repositories;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 角色数据初始化类
/// </summary>
public class HbtDbSeedIdentityRole
{
    private readonly IHbtRepositoryFactory _repositoryFactory;
    private readonly IHbtLogger _logger;

    private IHbtRepository<HbtRole> RoleRepository => _repositoryFactory.GetAuthRepository<HbtRole>();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repositoryFactory">仓储工厂</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedIdentityRole(IHbtRepositoryFactory repositoryFactory, IHbtLogger logger)
    {
        _repositoryFactory = repositoryFactory;
        _logger = logger;
    }

    /// <summary>
    /// 初始化角色数据
    /// </summary> 
    public async Task<(int, int)> InitializeRoleAsync()
    {
        int insertCount = 0;
        int updateCount = 0;
        long nextId = 1;

        var defaultRoles = new List<HbtRole>
        {
            // 系统管理角色
            new HbtRole
            {
                Id = nextId++,
                RoleKey = "system_admin",
                RoleName = "系统管理员",
                OrderNum = 1,
                Status = 0,

                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now,
                Remark = "System Administrator;システム管理者"
            },
            new HbtRole
            {
                Id = nextId++,
                RoleKey = "security_admin",
                RoleName = "安全管理员",
                OrderNum = 2,
                Status = 0,

                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now,
                Remark = "Security Administrator;セキュリティ管理者"
            },

            // 业务管理角色
            new HbtRole
            {
                Id = nextId++,
                RoleKey = "biz_manager",
                RoleName = "业务管理员",
                OrderNum = 3,
                Status = 0,

                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now,
                Remark = "Business Manager;業務管理者"
            },
            new HbtRole
            {
                Id = nextId++,
                RoleKey = "hr_manager",
                RoleName = "人事管理员",
                OrderNum = 4,
                Status = 0,

                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now,
                Remark = "HR Manager;人事管理者"
            },
            new HbtRole
            {
                Id = nextId++,
                RoleKey = "fin_manager",
                RoleName = "财务管理员",
                OrderNum = 5,
                Status = 0,

                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now,
                Remark = "Finance Manager;財務管理者"
            },

            // 生产管理角色
            new HbtRole
            {
                Id = nextId++,
                RoleKey = "prod_manager",
                RoleName = "生产管理员",
                OrderNum = 6,
                Status = 0,

                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now,
                Remark = "Production Manager;生産管理者"
            },
            new HbtRole
            {
                Id = nextId++,
                RoleKey = "qc_manager",
                RoleName = "品质管理员",
                OrderNum = 7,
                Status = 0,

                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now,
                Remark = "Quality Control Manager;品質管理者"
            },

            // 库存管理角色
            new HbtRole
            {
                Id = nextId++,
                RoleKey = "wh_manager",
                RoleName = "仓库管理员",
                OrderNum = 8,
                Status = 0,

                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now,
                Remark = "Warehouse Manager;倉庫管理者"
            },
            new HbtRole
            {
                Id = nextId++,
                RoleKey = "inv_manager",
                RoleName = "库存管理员",
                OrderNum = 9,
                Status = 0,

                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now,
                Remark = "Inventory Manager;在庫管理者"
            },

            // 采购管理角色
            new HbtRole
            {
                Id = nextId++,
                RoleKey = "purchase_manager",
                RoleName = "采购管理员",
                OrderNum = 10,
                Status = 0,

                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now,
                Remark = "Purchase Manager;購買管理者"
            },
            new HbtRole
            {
                Id = nextId++,
                RoleKey = "supplier_manager",
                RoleName = "供应商管理员",
                OrderNum = 11,
                Status = 0,

                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now,
                Remark = "Supplier Manager;仕入先管理者"
            },

            // 销售管理角色
            new HbtRole
            {
                Id = nextId++,
                RoleKey = "sales_manager",
                RoleName = "销售管理员",
                OrderNum = 12,
                Status = 0,

                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now,
                Remark = "Sales Manager;営業管理者"
            },
            new HbtRole
            {
                Id = nextId++,
                RoleKey = "customer_manager",
                RoleName = "客户管理员",
                OrderNum = 13,
                Status = 0,

                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now,
                Remark = "Customer Manager;顧客管理者"
            },

            // 基础数据角色
            new HbtRole
            {
                Id = nextId++,
                RoleKey = "master_manager",
                RoleName = "主数据管理员",
                OrderNum = 14,
                Status = 0,

                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now,
                Remark = "Master Data Manager;マスタ管理者"
            },
            new HbtRole
            {
                Id = nextId++,
                RoleKey = "code_manager",
                RoleName = "编码管理员",
                OrderNum = 15,
                Status = 0,

                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now,
                Remark = "Code Manager;コード管理者"
            },

            // 一般用户角色
            new HbtRole
            {
                Id = nextId++,
                RoleKey = "general_user",
                RoleName = "一般用户",
                OrderNum = 16,
                Status = 0,

                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now,
                Remark = "General User;一般ユーザー"
            },
            new HbtRole
            {
                Id = nextId++,
                RoleKey = "guest",
                RoleName = "访客",
                OrderNum = 17,
                Status = 0,

                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now,
                Remark = "Guest User;ゲストユーザー"
            }
        };

        foreach (var role in defaultRoles)
        {
            var existingRole = await RoleRepository.GetFirstAsync(r => r.RoleKey == role.RoleKey);
            if (existingRole == null)
            {
                role.CreateBy = "Hbt365";
                role.CreateTime = DateTime.Now;
                role.UpdateBy = "Hbt365";
                role.UpdateTime = DateTime.Now;
                await RoleRepository.CreateAsync(role);
                insertCount++;
                _logger.Info($"[创建] 角色 '{role.RoleName}' 创建成功");
            }
            else
            {
                existingRole.RoleName = role.RoleName;
                existingRole.OrderNum = role.OrderNum;
                existingRole.Status = role.Status;
                existingRole.Remark = role.Remark;
                existingRole.UpdateBy = "Hbt365";
                existingRole.UpdateTime = DateTime.Now;

                await RoleRepository.UpdateAsync(existingRole);
                updateCount++;
                _logger.Info($"[更新] 角色 '{existingRole.RoleName}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }
}