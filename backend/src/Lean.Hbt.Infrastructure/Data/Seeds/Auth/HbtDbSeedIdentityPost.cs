//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedPost.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 岗位数据初始化类 - 使用仓储工厂模式
//===================================================================

using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.Repositories;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 岗位数据初始化类
/// </summary>
public class HbtDbSeedIdentityPost
{
    private readonly IHbtRepositoryFactory _repositoryFactory;
    private readonly IHbtLogger _logger;

    private IHbtRepository<HbtPost> PostRepository => _repositoryFactory.GetAuthRepository<HbtPost>();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repositoryFactory">仓储工厂</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedIdentityPost(IHbtRepositoryFactory repositoryFactory, IHbtLogger logger)
    {
        _repositoryFactory = repositoryFactory;
        _logger = logger;
    }

    /// <summary>
    /// 初始化岗位数据
    /// </summary>
    public async Task<(int, int)> InitializePostAsync()
    {
        int insertCount = 0;
        int updateCount = 0;
        int nextOrderNum = 1;

        var defaultPosts = new List<HbtPost>
        {
            // 董事会成员
            new HbtPost
            {
                PostCode = "CHAIRMAN",
                PostName = "董事长",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Chairman;会長"
            },
            new HbtPost
            {
                PostCode = "VICE_CHAIRMAN",
                PostName = "副董事长",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Vice Chairman;副会長"
            },
            new HbtPost
            {
                PostCode = "EXECUTIVE_DIRECTOR",
                PostName = "执行董事",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Executive Director;執行取締役"
            },
            new HbtPost
            {
                PostCode = "NON_EXECUTIVE_DIRECTOR",
                PostName = "非执行董事",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Non-Executive Director;非執行取締役"
            },
            new HbtPost
            {
                PostCode = "MANAGING_DIRECTOR",
                PostName = "常务董事",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Managing Director;常務取締役"
            },
            new HbtPost
            {
                PostCode = "INDEPENDENT_DIRECTOR",
                PostName = "独立董事",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Independent Director;独立取締役"
            },

            // 监事会成员
            new HbtPost
            {
                PostCode = "SUPERVISOR_CHAIRMAN",
                PostName = "监事会主席",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Chairman of Supervisory Board;監査役会議長"
            },
            new HbtPost
            {
                PostCode = "SUPERVISOR_VICE_CHAIRMAN",
                PostName = "监事会副主席",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Vice Chairman of Supervisory Board;監査役会副議長"
            },
            new HbtPost
            {
                PostCode = "MANAGING_SUPERVISOR",
                PostName = "常务监事",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Managing Supervisor;常勤監査役"
            },
            new HbtPost
            {
                PostCode = "SUPERVISOR",
                PostName = "监事",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Supervisor;監査役"
            },

            // 高管层级
            new HbtPost
            {
                PostCode = "CEO",
                PostName = "首席执行官",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Chief Executive Officer;最高経営責任者"
            },
            new HbtPost
            {
                PostCode = "PRESIDENT",
                PostName = "总裁",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "President;社長"
            },
            new HbtPost
            {
                PostCode = "SENIOR_EXECUTIVE_VP",
                PostName = "高级执行副总裁",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Senior Executive Vice President;上級執行副社長"
            },
            new HbtPost
            {
                PostCode = "EXECUTIVE_VP",
                PostName = "执行副总裁",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Executive Vice President;執行副社長"
            },
            new HbtPost
            {
                PostCode = "SENIOR_VP",
                PostName = "高级副总裁",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Senior Vice President;上級副社長"
            },
            new HbtPost
            {
                PostCode = "VP",
                PostName = "副总裁",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Vice President;副社長"
            },

            // 首席官职级
            new HbtPost
            {
                PostCode = "COO",
                PostName = "首席运营官",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Chief Operating Officer;最高執行責任者"
            },
            new HbtPost
            {
                PostCode = "CFO",
                PostName = "首席财务官",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Chief Financial Officer;最高財務責任者"
            },
            new HbtPost
            {
                PostCode = "CTO",
                PostName = "首席技术官",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Chief Technology Officer;最高技術責任者"
            },
            new HbtPost
            {
                PostCode = "CIO",
                PostName = "首席信息官",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Chief Information Officer;最高情報責任者"
            },
            new HbtPost
            {
                PostCode = "CMO",
                PostName = "首席市场官",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Chief Marketing Officer;最高マーケティング責任者"
            },
            new HbtPost
            {
                PostCode = "CLO",
                PostName = "首席法务官",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Chief Legal Officer;最高法務責任者"
            },
            new HbtPost
            {
                PostCode = "CSO",
                PostName = "首席战略官",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Chief Strategy Officer;最高戦略責任者"
            },
            new HbtPost
            {
                PostCode = "CQO",
                PostName = "首席质量官",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Chief Quality Officer;最高品質責任者"
            },

            // 总经理层级
            new HbtPost
            {
                PostCode = "GM",
                PostName = "总经理",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "General Manager;総経理"
            },
            new HbtPost
            {
                PostCode = "DEPUTY_GM",
                PostName = "副总经理",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Deputy General Manager;副総経理"
            },
            new HbtPost
            {
                PostCode = "FACTORY_DIRECTOR",
                PostName = "厂长",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Factory Director;工場長"
            },
            new HbtPost
            {
                PostCode = "EXECUTIVE_ASSISTANT",
                PostName = "特助",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Executive Assistant;スペシャルアシスタント"
            },

            // 本部长层级
            new HbtPost
            {
                PostCode = "DIVISION_DIRECTOR",
                PostName = "本部长",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Division Director;本部長"
            },
            new HbtPost
            {
                PostCode = "DEPUTY_DIVISION_DIRECTOR",
                PostName = "副本部长",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Deputy Division Director;副本部長"
            },

            // 总监层级
            new HbtPost
            {
                PostCode = "DIRECTOR",
                PostName = "总监",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Director;ディレクター"
            },
            new HbtPost
            {
                PostCode = "DEPUTY_DIRECTOR",
                PostName = "副总监",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Deputy Director;副ディレクター"
            },

            // 部长层级
            new HbtPost
            {
                PostCode = "DEPT_DIRECTOR",
                PostName = "部长",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Department Director;部長"
            },
            new HbtPost
            {
                PostCode = "DEPUTY_DEPT_DIRECTOR",
                PostName = "副部长",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Deputy Department Director;副部長"
            },
            new HbtPost
            {
                PostCode = "ASSOCIATE_DIRECTOR",
                PostName = "次长",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Associate Director;次長"
            },

            // 经理层级
            new HbtPost
            {
                PostCode = "SENIOR_MANAGER",
                PostName = "高级经理",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Senior Manager;上級マネージャー"
            },
            new HbtPost
            {
                PostCode = "MANAGER",
                PostName = "经理",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Manager;マネージャー"
            },
            new HbtPost
            {
                PostCode = "DEPUTY_MANAGER",
                PostName = "副经理",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Deputy Manager;副マネージャー"
            },

            // 科长层级
            new HbtPost
            {
                PostCode = "DIVISION_CHIEF",
                PostName = "科长",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Division Chief;科長"
            },
            new HbtPost
            {
                PostCode = "DEPUTY_DIVISION_CHIEF",
                PostName = "副科长",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Deputy Division Chief;副科長"
            },

            // 课长层级
            new HbtPost
            {
                PostCode = "SECTION_CHIEF",
                PostName = "课长",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Section Chief;課長"
            },
            new HbtPost
            {
                PostCode = "DEPUTY_SECTION_CHIEF",
                PostName = "副课长",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Deputy Section Chief;副課長"
            },

            // 主管层级
            new HbtPost
            {
                PostCode = "SUPERVISOR",
                PostName = "主管",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Supervisor;主任"
            },
            new HbtPost
            {
                PostCode = "DEPUTY_SUPERVISOR",
                PostName = "副主管",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Deputy Supervisor;副主任"
            },

            // 股长层级
            new HbtPost
            {
                PostCode = "GROUP_LEADER",
                PostName = "股长",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Group Leader;係長"
            },
            new HbtPost
            {
                PostCode = "DEPUTY_GROUP_LEADER",
                PostName = "副股长",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Deputy Group Leader;副係長"
            },

            // 班组长层级
            new HbtPost
            {
                PostCode = "TEAM_LEADER",
                PostName = "班组长",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Team Leader;班長"
            },
            new HbtPost
            {
                PostCode = "DEPUTY_TEAM_LEADER",
                PostName = "副班组长",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Deputy Team Leader;副班長"
            },

            // 专业技术职级
            new HbtPost
            {
                PostCode = "SENIOR_EXPERT",
                PostName = "资深专家",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Senior Expert;上級専門家"
            },
            new HbtPost
            {
                PostCode = "SENIOR_ENGINEER",
                PostName = "正高级工程师",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Senior Engineer;上級エンジニア"
            },
            new HbtPost
            {
                PostCode = "SENIOR_ACCOUNTANT",
                PostName = "正高级会计师",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Senior Accountant;上級会計士"
            },
            new HbtPost
            {
                PostCode = "SENIOR_CONSULTANT",
                PostName = "正高级顾问",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Senior Consultant;上級コンサルタント"
            },
            new HbtPost
            {
                PostCode = "HIGH_ENGINEER",
                PostName = "高级工程师",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "High Engineer;高級エンジニア"
            },
            new HbtPost
            {
                PostCode = "HIGH_ACCOUNTANT",
                PostName = "高级会计师",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "High Accountant;高級会計士"
            },
            new HbtPost
            {
                PostCode = "HIGH_CONSULTANT",
                PostName = "高级顾问",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "High Consultant;高級コンサルタント"
            },
            new HbtPost
            {
                PostCode = "MID_ENGINEER",
                PostName = "中级工程师",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Mid Engineer;中級エンジニア"
            },
            new HbtPost
            {
                PostCode = "MID_ACCOUNTANT",
                PostName = "中级会计师",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Mid Accountant;中級会計士"
            },
            new HbtPost
            {
                PostCode = "MID_CONSULTANT",
                PostName = "中级顾问",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Mid Consultant;中級コンサルタント"
            },
            new HbtPost
            {
                PostCode = "ASSISTANT_ENGINEER",
                PostName = "助理工程师",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Assistant Engineer;助理エンジニア"
            },
            new HbtPost
            {
                PostCode = "ASSISTANT_ACCOUNTANT",
                PostName = "助理会计师",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Assistant Accountant;助理会計士"
            },
            new HbtPost
            {
                PostCode = "ASSISTANT_CONSULTANT",
                PostName = "助理顾问",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Assistant Consultant;助理コンサルタント"
            },

            // 基层职级
            new HbtPost
            {
                PostCode = "TECHNICIAN",
                PostName = "技术员",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Technician;技術員"
            },
            new HbtPost
            {
                PostCode = "ACCOUNTANT",
                PostName = "会计员",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Accountant;会計員"
            },
            new HbtPost
            {
                PostCode = "CLERK",
                PostName = "事务员",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Clerk;事務員"
            },
            new HbtPost
            {
                PostCode = "MULTI_SKILLED_OPERATOR",
                PostName = "多能工",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Multi-skilled Operator;多能工"
            },
            new HbtPost
            {
                PostCode = "INSPECTOR",
                PostName = "检查员",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Inspector;検査員"
            },
            new HbtPost
            {
                PostCode = "WAREHOUSE_KEEPER",
                PostName = "库管员",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Warehouse Keeper;倉庫管理員"
            },
            new HbtPost
            {
                PostCode = "CLEANER",
                PostName = "保洁员",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Cleaner;清掃員"
            },
            new HbtPost
            {
                PostCode = "SECURITY",
                PostName = "安保员",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Security Guard;警備員"
            },
            new HbtPost
            {
                PostCode = "COOK",
                PostName = "厨师",
                OrderNum = nextOrderNum++,
                Status = 0,
                Remark = "Cook;調理師"
            }
        };

        foreach (var post in defaultPosts)
        {
            var existingPost = await PostRepository.GetFirstAsync(p => p.PostCode == post.PostCode);
            if (existingPost == null)
            {
                // 统一处理租户和审计字段

                post.CreateBy = "Hbt365";
                post.CreateTime = DateTime.Now;
                post.UpdateBy = "Hbt365";
                post.UpdateTime = DateTime.Now;

                await PostRepository.CreateAsync(post);
                insertCount++;
                _logger.Info($"[创建] 岗位 '{post.PostName}' 创建成功");
            }
            else
            {
                existingPost.PostName = post.PostName;
                existingPost.OrderNum = post.OrderNum;
                existingPost.Status = post.Status;
                existingPost.Remark = post.Remark;

                // 统一处理租户和审计字段

                existingPost.UpdateBy = "Hbt365";
                existingPost.UpdateTime = DateTime.Now;

                await PostRepository.UpdateAsync(existingPost);
                updateCount++;
                _logger.Info($"[更新] 岗位 '{existingPost.PostName}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }
}