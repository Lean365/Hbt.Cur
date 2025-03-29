//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedPost.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 岗位数据初始化类
//===================================================================

using Lean.Hbt.Common.Enums;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.IServices;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 岗位数据初始化类
/// </summary>
public class HbtDbSeedPost
{
    private readonly IHbtRepository<HbtPost> _postRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="postRepository">岗位仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedPost(IHbtRepository<HbtPost> postRepository, IHbtLogger logger)
    {
        _postRepository = postRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化岗位数据
    /// </summary>
    public async Task<(int, int)> InitializePostAsync()
    {
        int insertCount = 0;
        int updateCount = 0;
        long nextId = 1;

        var defaultPosts = new List<HbtPost>
        {
            // 董事会成员
            new HbtPost
            {
                Id = nextId++,
                PostCode = "CHAIRMAN",
                PostName = "董事长",
                OrderNum = 1,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Chairman;会長"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "VICE_CHAIRMAN",
                PostName = "副董事长",
                OrderNum = 2,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Vice Chairman;副会長"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "EXECUTIVE_DIRECTOR",
                PostName = "执行董事",
                OrderNum = 3,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Executive Director;執行取締役"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "NON_EXECUTIVE_DIRECTOR",
                PostName = "非执行董事",
                OrderNum = 4,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Non-Executive Director;非執行取締役"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "MANAGING_DIRECTOR",
                PostName = "常务董事",
                OrderNum = 5,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Managing Director;常務取締役"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "INDEPENDENT_DIRECTOR",
                PostName = "独立董事",
                OrderNum = 6,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Independent Director;独立取締役"
            },

            // 监事会成员
            new HbtPost
            {
                Id = nextId++,
                PostCode = "SUPERVISOR_CHAIRMAN",
                PostName = "监事会主席",
                OrderNum = 7,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Chairman of Supervisory Board;監査役会議長"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "SUPERVISOR_VICE_CHAIRMAN",
                PostName = "监事会副主席",
                OrderNum = 8,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Vice Chairman of Supervisory Board;監査役会副議長"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "MANAGING_SUPERVISOR",
                PostName = "常务监事",
                OrderNum = 9,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Managing Supervisor;常勤監査役"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "SUPERVISOR",
                PostName = "监事",
                OrderNum = 10,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Supervisor;監査役"
            },

            // 高管层级
            new HbtPost
            {
                Id = nextId++,
                PostCode = "CEO",
                PostName = "首席执行官",
                OrderNum = 11,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Chief Executive Officer;最高経営責任者"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "PRESIDENT",
                PostName = "总裁",
                OrderNum = 12,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "President;社長"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "SENIOR_EXECUTIVE_VP",
                PostName = "高级执行副总裁",
                OrderNum = 13,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Senior Executive Vice President;上級執行副社長"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "EXECUTIVE_VP",
                PostName = "执行副总裁",
                OrderNum = 14,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Executive Vice President;執行副社長"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "SENIOR_VP",
                PostName = "高级副总裁",
                OrderNum = 15,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Senior Vice President;上級副社長"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "VP",
                PostName = "副总裁",
                OrderNum = 16,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Vice President;副社長"
            },

            // 首席官职级
            new HbtPost
            {
                Id = nextId++,
                PostCode = "COO",
                PostName = "首席运营官",
                OrderNum = 17,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Chief Operating Officer;最高執行責任者"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "CFO",
                PostName = "首席财务官",
                OrderNum = 18,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Chief Financial Officer;最高財務責任者"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "CTO",
                PostName = "首席技术官",
                OrderNum = 19,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Chief Technology Officer;最高技術責任者"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "CIO",
                PostName = "首席信息官",
                OrderNum = 20,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Chief Information Officer;最高情報責任者"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "CMO",
                PostName = "首席市场官",
                OrderNum = 21,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Chief Marketing Officer;最高マーケティング責任者"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "CLO",
                PostName = "首席法务官",
                OrderNum = 22,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Chief Legal Officer;最高法務責任者"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "CSO",
                PostName = "首席战略官",
                OrderNum = 23,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Chief Strategy Officer;最高戦略責任者"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "CQO",
                PostName = "首席质量官",
                OrderNum = 24,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Chief Quality Officer;最高品質責任者"
            },

            // 总经理层级
            new HbtPost
            {
                Id = nextId++,
                PostCode = "GM",
                PostName = "总经理",
                OrderNum = 25,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "General Manager;総経理"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "DEPUTY_GM",
                PostName = "副总经理",
                OrderNum = 26,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Deputy General Manager;副総経理"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "FACTORY_DIRECTOR",
                PostName = "厂长",
                OrderNum = 27,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Factory Director;工場長"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "EXECUTIVE_ASSISTANT",
                PostName = "特助",
                OrderNum = 28,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Executive Assistant;スペシャルアシスタント"
            },

            // 本部长层级
            new HbtPost
            {
                Id = nextId++,
                PostCode = "DIVISION_DIRECTOR",
                PostName = "本部长",
                OrderNum = 29,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Division Director;本部長"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "DEPUTY_DIVISION_DIRECTOR",
                PostName = "副本部长",
                OrderNum = 30,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Deputy Division Director;副本部長"
            },

            // 总监层级
            new HbtPost
            {
                Id = nextId++,
                PostCode = "DIRECTOR",
                PostName = "总监",
                OrderNum = 31,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Director;ディレクター"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "DEPUTY_DIRECTOR",
                PostName = "副总监",
                OrderNum = 32,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Deputy Director;副ディレクター"
            },

            // 部长层级
            new HbtPost
            {
                Id = nextId++,
                PostCode = "DEPT_DIRECTOR",
                PostName = "部长",
                OrderNum = 33,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Department Director;部長"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "DEPUTY_DEPT_DIRECTOR",
                PostName = "副部长",
                OrderNum = 34,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Deputy Department Director;副部長"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "ASSOCIATE_DIRECTOR",
                PostName = "次长",
                OrderNum = 35,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Associate Director;次長"
            },

            // 经理层级
            new HbtPost
            {
                Id = nextId++,
                PostCode = "SENIOR_MANAGER",
                PostName = "高级经理",
                OrderNum = 36,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Senior Manager;上級マネージャー"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "MANAGER",
                PostName = "经理",
                OrderNum = 37,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Manager;マネージャー"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "DEPUTY_MANAGER",
                PostName = "副经理",
                OrderNum = 38,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Deputy Manager;副マネージャー"
            },

            // 科长层级
            new HbtPost
            {
                Id = nextId++,
                PostCode = "DIVISION_CHIEF",
                PostName = "科长",
                OrderNum = 39,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Division Chief;科長"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "DEPUTY_DIVISION_CHIEF",
                PostName = "副科长",
                OrderNum = 40,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Deputy Division Chief;副科長"
            },

            // 课长层级
            new HbtPost
            {
                Id = nextId++,
                PostCode = "SECTION_CHIEF",
                PostName = "课长",
                OrderNum = 41,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Section Chief;課長"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "DEPUTY_SECTION_CHIEF",
                PostName = "副课长",
                OrderNum = 42,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Deputy Section Chief;副課長"
            },

            // 主管层级
            new HbtPost
            {
                Id = nextId++,
                PostCode = "SUPERVISOR",
                PostName = "主管",
                OrderNum = 43,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Supervisor;主任"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "DEPUTY_SUPERVISOR",
                PostName = "副主管",
                OrderNum = 44,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Deputy Supervisor;副主任"
            },

            // 股长层级
            new HbtPost
            {
                Id = nextId++,
                PostCode = "GROUP_LEADER",
                PostName = "股长",
                OrderNum = 45,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Group Leader;係長"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "DEPUTY_GROUP_LEADER",
                PostName = "副股长",
                OrderNum = 46,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Deputy Group Leader;副係長"
            },

            // 班组长层级
            new HbtPost
            {
                Id = nextId++,
                PostCode = "TEAM_LEADER",
                PostName = "班组长",
                OrderNum = 47,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Team Leader;班長"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "DEPUTY_TEAM_LEADER",
                PostName = "副班组长",
                OrderNum = 48,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Deputy Team Leader;副班長"
            },

            // 专业技术职级
            new HbtPost
            {
                Id = nextId++,
                PostCode = "SENIOR_EXPERT",
                PostName = "资深专家",
                OrderNum = 49,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Senior Expert;上級専門家"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "SENIOR_ENGINEER",
                PostName = "正高级工程师",
                OrderNum = 50,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Senior Engineer;上級エンジニア"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "SENIOR_ACCOUNTANT",
                PostName = "正高级会计师",
                OrderNum = 51,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Senior Accountant;上級会計士"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "SENIOR_CONSULTANT",
                PostName = "正高级顾问",
                OrderNum = 52,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Senior Consultant;上級コンサルタント"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "HIGH_ENGINEER",
                PostName = "高级工程师",
                OrderNum = 53,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "High Engineer;高級エンジニア"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "HIGH_ACCOUNTANT",
                PostName = "高级会计师",
                OrderNum = 54,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "High Accountant;高級会計士"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "HIGH_CONSULTANT",
                PostName = "高级顾问",
                OrderNum = 55,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "High Consultant;高級コンサルタント"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "MID_ENGINEER",
                PostName = "中级工程师",
                OrderNum = 56,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Mid Engineer;中級エンジニア"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "MID_ACCOUNTANT",
                PostName = "中级会计师",
                OrderNum = 57,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Mid Accountant;中級会計士"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "MID_CONSULTANT",
                PostName = "中级顾问",
                OrderNum = 58,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Mid Consultant;中級コンサルタント"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "ASSISTANT_ENGINEER",
                PostName = "助理工程师",
                OrderNum = 59,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Assistant Engineer;助理エンジニア"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "ASSISTANT_ACCOUNTANT",
                PostName = "助理会计师",
                OrderNum = 60,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Assistant Accountant;助理会計士"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "ASSISTANT_CONSULTANT",
                PostName = "助理顾问",
                OrderNum = 61,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Assistant Consultant;助理コンサルタント"
            },

            // 基层职级
            new HbtPost
            {
                Id = nextId++,
                PostCode = "TECHNICIAN",
                PostName = "技术员",
                OrderNum = 62,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Technician;技術員"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "ACCOUNTANT",
                PostName = "会计员",
                OrderNum = 63,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Accountant;会計員"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "CLERK",
                PostName = "事务员",
                OrderNum = 64,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Clerk;事務員"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "MULTI_SKILLED_OPERATOR",
                PostName = "多能工",
                OrderNum = 65,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Multi-skilled Operator;多能工"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "INSPECTOR",
                PostName = "检查员",
                OrderNum = 66,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Inspector;検査員"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "WAREHOUSE_KEEPER",
                PostName = "库管员",
                OrderNum = 67,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Warehouse Keeper;倉庫管理員"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "CLEANER",
                PostName = "保洁员",
                OrderNum = 68,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Cleaner;清掃員"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "SECURITY",
                PostName = "安保员",
                OrderNum = 69,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Security Guard;警備員"
            },
            new HbtPost
            {
                Id = nextId++,
                PostCode = "COOK",
                PostName = "厨师",
                OrderNum = 70,
                Status = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "Cook;調理師"
            }
        };

        foreach (var post in defaultPosts)
        {
            var existingPost = await _postRepository.GetInfoAsync(p => p.PostCode == post.PostCode);
            if (existingPost == null)
            {
                await _postRepository.CreateAsync(post);
                insertCount++;
                _logger.Info($"[创建] 岗位 '{post.PostName}' 创建成功");
            }
            else
            {
                existingPost.PostName = post.PostName;
                existingPost.OrderNum = post.OrderNum;
                existingPost.Status = post.Status;
                existingPost.Remark = post.Remark;
                existingPost.UpdateBy = "system";
                existingPost.UpdateTime = DateTime.Now;

                await _postRepository.UpdateAsync(existingPost);
                updateCount++;
                _logger.Info($"[更新] 岗位 '{existingPost.PostName}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }
} 