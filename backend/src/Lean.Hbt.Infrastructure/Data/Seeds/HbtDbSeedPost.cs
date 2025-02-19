//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedPost.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 岗位数据初始化类
//===================================================================

using Lean.Hbt.Common.Enums;
using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.Entities.Admin;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.IServices;
using Lean.Hbt.Infrastructure.Data.Contexts;

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

        var defaultPosts = new List<HbtPost>
        {
            new HbtPost
            {
                PostName = "总经理",
                PostCode = "GM",
                OrderNum = 1,
                Status = HbtStatus.Normal,
                TenantId = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtPost
            {
                PostName = "项目经理",
                PostCode = "PM",
                OrderNum = 2,
                Status = HbtStatus.Normal,
                TenantId = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtPost
            {
                PostName = "开发工程师",
                PostCode = "DEV",
                OrderNum = 3,
                Status = HbtStatus.Normal,
                TenantId = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            }
        };

        foreach (var post in defaultPosts)
        {
            var existingPost = await _postRepository.FirstOrDefaultAsync(p => p.PostCode == post.PostCode);
            if (existingPost == null)
            {
                await _postRepository.InsertAsync(post);
                insertCount++;
                _logger.Info($"[创建] 岗位 '{post.PostName}' 创建成功");
            }
            else
            {
                existingPost.PostCode = post.PostCode;
                existingPost.PostName = post.PostName;
                existingPost.OrderNum = post.OrderNum;
                existingPost.Status = post.Status;
                existingPost.TenantId = post.TenantId;
                existingPost.Remark = post.Remark;
                existingPost.CreateBy = post.CreateBy;
                existingPost.CreateTime = post.CreateTime;
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