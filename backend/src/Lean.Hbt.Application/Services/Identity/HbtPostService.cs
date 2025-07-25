//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtPostService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 岗位服务实现 - 使用仓储工厂模式
//===================================================================

using System.Linq.Expressions;
using Lean.Hbt.Domain.Repositories;
using Microsoft.AspNetCore.Http;

namespace Lean.Hbt.Application.Services.Identity
{
    /// <summary>
    /// 岗位服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// 更新: 2024-12-01 - 使用仓储工厂模式支持多库
    /// </remarks>
    public class HbtPostService : HbtBaseService, IHbtPostService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly IHbtRepositoryFactory _repositoryFactory;

        private IHbtRepository<HbtPost> PostRepository => _repositoryFactory.GetAuthRepository<HbtPost>();
        private IHbtRepository<HbtUserPost> UserPostRepository => _repositoryFactory.GetAuthRepository<HbtUserPost>();

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtPostService(
            IHbtLogger logger,
            IHbtRepositoryFactory repositoryFactory,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <summary>
        /// 获取岗位分页列表
        /// </summary>
        public async Task<HbtPagedResult<HbtPostDto>> GetListAsync(HbtPostQueryDto query)
        {
            var result = await PostRepository.GetPagedListAsync(
                QueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.OrderNum,
                OrderByType.Asc);

            return new HbtPagedResult<HbtPostDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<HbtPostDto>>()
            };
        }

        /// <summary>
        /// 获取岗位详情
        /// </summary>
        public async Task<HbtPostDto> GetByIdAsync(long id)
        {
            var post = await PostRepository.GetByIdAsync(id);
            if (post == null)
                throw new HbtException(L("Identity.Post.NotFound"));

            return post.Adapt<HbtPostDto>();
        }

        /// <summary>
        /// 创建岗位
        /// </summary>
        public async Task<long> CreateAsync(HbtPostCreateDto input)
        {
            
            // 验证字段是否已存在
            await HbtValidateUtils.ValidateFieldExistsAsync(PostRepository, "PostCode", input.PostCode);
            await HbtValidateUtils.ValidateFieldExistsAsync(PostRepository, "PostName", input.PostName);

            var post = input.Adapt<HbtPost>();
            var result = await PostRepository.CreateAsync(post);
            if (result > 0)
                _logger.Info(L("Common.AddSuccess"));

            return post.Id;
        }

        /// <summary>
        /// 更新岗位
        /// </summary>
        public async Task<bool> UpdateAsync(HbtPostUpdateDto input)
        {
            var post = await PostRepository.GetByIdAsync(input.PostId)
                ?? throw new HbtException(L("Identity.Post.NotFound"));

            // 验证字段是否已存在
            if (post.PostCode != input.PostCode)
                await HbtValidateUtils.ValidateFieldExistsAsync(PostRepository, "PostCode", input.PostCode, input.PostId);
            if (post.PostName != input.PostName)
                await HbtValidateUtils.ValidateFieldExistsAsync(PostRepository, "PostName", input.PostName, input.PostId);

            input.Adapt(post);
            return await PostRepository.UpdateAsync(post) > 0;
        }

        /// <summary>
        /// 删除岗位
        /// </summary>
        public async Task<bool> DeleteAsync(long id)
        {
            var userPostRepository = _repositoryFactory.GetAuthRepository<HbtUserPost>();
            
            var post = await PostRepository.GetByIdAsync(id)
                ?? throw new HbtException(L("Identity.Post.NotFound"));

            // 检查是否有用户关联
            if (await UserPostRepository.AsQueryable().AnyAsync(x => x.PostId == id))
                throw new HbtException(L("Identity.Post.DeleteFailed"));

            return await PostRepository.DeleteAsync(post) > 0;
        }

        /// <summary>
        /// 批量删除岗位
        /// </summary>
        public async Task<bool> BatchDeleteAsync(long[] postIds)
        {
            if (postIds == null || postIds.Length == 0)
                throw new HbtException(L("Identity.Post.SelectRequired"));

            var userPostRepository = _repositoryFactory.GetAuthRepository<HbtUserPost>();

            // 检查是否有用户关联
            if (await UserPostRepository.AsQueryable().AnyAsync(up => postIds.Contains(up.PostId)))
                throw new HbtException(L("Identity.Post.HasUsers"));

            return await PostRepository.DeleteRangeAsync(postIds.Cast<object>().ToList()) > 0;
        }

        /// <summary>
        /// 获取岗位选项列表
        /// </summary>
        /// <returns>岗位选项列表</returns>
        public async Task<List<HbtSelectOption>> GetOptionsAsync()
        {
            var posts = await PostRepository.AsQueryable()
                .Where(p => p.Status == 0)  // 只获取正常状态的岗位
                .OrderBy(p => p.OrderNum)
                .Select(p => new HbtSelectOption
                {
                    Label = p.PostName,
                    Value = p.Id,

                })
                .ToListAsync();
            return posts;
        }

        /// <summary>
        /// 导入岗位数据
        /// </summary>
        public async Task<List<HbtPostImportDto>> ImportAsync(Stream fileStream, string sheetName = "Sheet1")
        {
            try
            {
                var postRepository = _repositoryFactory.GetAuthRepository<HbtPost>();
                var posts = await HbtExcelHelper.ImportAsync<HbtPostImportDto>(fileStream, sheetName);
                if (!posts.Any())
                    return new List<HbtPostImportDto>();

                foreach (var post in posts)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(post.PostCode) || string.IsNullOrEmpty(post.PostName))
                        {
                            _logger.Warn(L("Identity.Post.Log.ImportEmptyFields"));
                            continue;
                        }

                        await HbtValidateUtils.ValidateFieldExistsAsync(PostRepository, "PostCode", post.PostCode);
                        await HbtValidateUtils.ValidateFieldExistsAsync(PostRepository, "PostName", post.PostName);

                        var entity = post.Adapt<HbtPost>();
                        entity.CreateTime = DateTime.Now;
                        entity.CreateBy = "system"; // TODO: 从当前用户上下文获取

                        await PostRepository.CreateAsync(entity);
                    }
                    catch (Exception ex)
                    {
                        _logger.Warn(L("Identity.Post.Log.ImportFailed", ex.Message));
                    }
                }

                return posts;
            }
            catch (Exception ex)
            {
                _logger.Error(L("Identity.Post.Log.ImportDataFailed"), ex);
                throw new HbtException(L("Identity.Post.ImportFailed"));
            }
        }

        /// <summary>
        /// 生成岗位导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件字节数组</returns>
        public async Task<byte[]> GenerateTemplateAsync(string sheetName = "Sheet1")
        {
            var (_, content) = await HbtExcelHelper.GenerateTemplateAsync<HbtPostTemplateDto>(sheetName);
            return content;
        }

        /// <summary>
        /// 导出岗位数据
        /// </summary>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtPostQueryDto query, string sheetName = "Sheet1")
        {
            try
            {
                var postRepository = _repositoryFactory.GetAuthRepository<HbtPost>();
                var list = await postRepository.GetListAsync(QueryExpression(query));
                var exportList = list.Adapt<List<HbtPostExportDto>>();
                return await HbtExcelHelper.ExportAsync(exportList, sheetName, "岗位数据");
            }
            catch (Exception ex)
            {
                _logger.Error(L("Identity.Post.Log.ExportDataFailed"), ex);
                throw new HbtException(L("Identity.Post.ExportFailed"));
            }
        }

        /// <summary>
        /// 更新岗位状态
        /// </summary>
        public async Task<bool> UpdateStatusAsync(HbtPostStatusDto input)
        {
            var post = await PostRepository.GetByIdAsync(input.PostId);
            if (post == null)
                throw new HbtException(L("Identity.Post.NotFound"));

            post.Status = input.Status;
            var result = await PostRepository.UpdateAsync(post);
            return result > 0;
        }

        /// <summary>
        /// 获取岗位用户列表
        /// </summary>
        /// <param name="postId">岗位ID</param>
        /// <returns>岗位用户列表</returns>
        public async Task<HbtUserPostDto> GetPostUsersAsync(long postId)
        {
            var userRepository = _repositoryFactory.GetAuthRepository<HbtUser>();
            
            var post = await PostRepository.GetByIdAsync(postId)
                ?? throw new HbtException(L("Identity.Post.NotFound"));

            // 获取已分配的用户
            var assignedUsers = await UserPostRepository.AsQueryable()
                .LeftJoin<HbtUser>((up, u) => up.UserId == u.Id)
                .Where(up => up.PostId == postId)
                .Select((up, u) => new HbtUserDto
                {
                    UserId = u.Id,
                    UserName = u.UserName,
                    NickName = u.NickName,
                    UserType = u.UserType,
                    Status = u.Status
                })
                .ToListAsync();

            // 获取所有可选用户（未分配的用户）
            var optionalUsers = await UserPostRepository.AsQueryable()
                .RightJoin<HbtUser>((up, u) => up.UserId == u.Id)
                .Where((up, u) => up.PostId != postId || up.PostId == null)
                .Select((up, u) => new HbtUserDto
                {
                    UserId = u.Id,
                    UserName = u.UserName,
                    NickName = u.NickName,
                    UserType = u.UserType,
                    Status = u.Status
                })
                .ToListAsync();

            return new HbtUserPostDto
            {
                PostId = postId,
                AssignedUsers = assignedUsers,
                OptionalUsers = optionalUsers
            };
        }

        /// <summary>
        /// 分配岗位用户
        /// </summary>
        /// <param name="postId">岗位ID</param>
        /// <param name="userIds">用户ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> AssignPostUsersAsync(long postId, long[] userIds)
        {
            var userPostRepository = _repositoryFactory.GetAuthRepository<HbtUserPost>();
            
            var post = await PostRepository.GetByIdAsync(postId)
                ?? throw new HbtException(L("Identity.Post.NotFound"));

            // 获取已分配的用户
            var existingUserPosts = await userPostRepository.AsQueryable()
                .Where(up => up.PostId == postId)
                .ToListAsync();

            // 过滤出需要新增的用户
            var newUserIds = userIds.Except(existingUserPosts.Select(up => up.UserId)).ToList();
            if (!newUserIds.Any())
                return true;

            // 批量创建用户岗位关联
            var userPosts = newUserIds.Select(userId => new HbtUserPost
            {
                PostId = postId,
                UserId = userId,
                CreateTime = DateTime.Now,
                CreateBy = _currentUser.UserName
            }).ToList();

            return await userPostRepository.CreateRangeAsync(userPosts) > 0;
        }

        /// <summary>
        /// 移除岗位用户
        /// </summary>
        /// <param name="postId">岗位ID</param>
        /// <param name="userIds">用户ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> RemovePostUsersAsync(long postId, long[] userIds)
        {
            var userPostRepository = _repositoryFactory.GetAuthRepository<HbtUserPost>();
            
            var post = await PostRepository.GetByIdAsync(postId)
                ?? throw new HbtException(L("Identity.Post.NotFound"));

            // 删除用户岗位关联
            return await userPostRepository.DeleteAsync((HbtUserPost up) =>
                up.PostId == postId && userIds.Contains(up.UserId)) > 0;
        }

        /// <summary>
        /// 构建岗位查询条件
        /// </summary>
        private Expression<Func<HbtPost, bool>> QueryExpression(HbtPostQueryDto query)
        {
            var exp = Expressionable.Create<HbtPost>();

            if (!string.IsNullOrEmpty(query.PostName))
                exp.And(x => x.PostName.Contains(query.PostName));

            if (!string.IsNullOrEmpty(query.PostCode))
                exp.And(x => x.PostCode.Contains(query.PostCode));

            if (query.Status.HasValue && query.Status.Value != -1)
                exp.And(x => x.Status == query.Status.Value);

            return exp.ToExpression();
        }    
    
    }
} 