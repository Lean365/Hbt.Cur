//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtPostService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 岗位服务实现
//===================================================================

using System.Linq.Expressions;
using Lean.Hbt.Application.Dtos.Identity;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Common.Helpers;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.IServices.Extensions;
using Lean.Hbt.Domain.IServices.Extensions;
using Lean.Hbt.Domain.Repositories;
using Lean.Hbt.Domain.Utils;
using Mapster;
using SqlSugar;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Http;

namespace Lean.Hbt.Application.Services.Identity
{
    /// <summary>
    /// 岗位服务实现
    /// </summary>
    public class HbtPostService : HbtBaseService, IHbtPostService
    {
        private readonly IHbtRepository<HbtPost> _postRepository;
        private readonly IHbtRepository<HbtUserPost> _userPostRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtPostService(
            IHbtRepository<HbtPost> postRepository,
            IHbtRepository<HbtUserPost> userPostRepository,
            IHbtLogger logger,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _postRepository = postRepository;
            _userPostRepository = userPostRepository;
        }

        /// <summary>
        /// 构建岗位查询条件
        /// </summary>
        private Expression<Func<HbtPost, bool>> HbtPostQueryExpression(HbtPostQueryDto query)
        {
            var exp = Expressionable.Create<HbtPost>();

            if (!string.IsNullOrEmpty(query.PostName))
                exp.And(x => x.PostName.Contains(query.PostName));

            if (!string.IsNullOrEmpty(query.PostCode))
                exp.And(x => x.PostCode.Contains(query.PostCode));

            if (query.Status.HasValue)
                exp.And(x => x.Status == query.Status.Value);

            return exp.ToExpression();
        }

        /// <summary>
        /// 获取岗位分页列表
        /// </summary>
        public async Task<HbtPagedResult<HbtPostDto>> GetListAsync(HbtPostQueryDto query)
        {
            var result = await _postRepository.GetPagedListAsync(
                HbtPostQueryExpression(query),
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
            var post = await _postRepository.GetByIdAsync(id);
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
            await HbtValidateUtils.ValidateFieldExistsAsync(_postRepository, "PostCode", input.PostCode);
            await HbtValidateUtils.ValidateFieldExistsAsync(_postRepository, "PostName", input.PostName);

            var post = input.Adapt<HbtPost>();
            var result = await _postRepository.CreateAsync(post);
            if (result > 0)
                _logger.Info(L("Common.AddSuccess"));

            return post.Id;
        }

        /// <summary>
        /// 更新岗位
        /// </summary>
        public async Task<bool> UpdateAsync(HbtPostUpdateDto input)
        {
            var post = await _postRepository.GetByIdAsync(input.PostId)
                ?? throw new HbtException(L("Identity.Post.NotFound"));

            // 验证字段是否已存在
            if (post.PostCode != input.PostCode)
                await HbtValidateUtils.ValidateFieldExistsAsync(_postRepository, "PostCode", input.PostCode, input.PostId);
            if (post.PostName != input.PostName)
                await HbtValidateUtils.ValidateFieldExistsAsync(_postRepository, "PostName", input.PostName, input.PostId);

            input.Adapt(post);
            return await _postRepository.UpdateAsync(post) > 0;
        }

        /// <summary>
        /// 删除岗位
        /// </summary>
        public async Task<bool> DeleteAsync(long id)
        {
            var post = await _postRepository.GetByIdAsync(id)
                ?? throw new HbtException(L("Identity.Post.NotFound"));

            // 检查是否有用户关联
            if (await _userPostRepository.AsQueryable().AnyAsync(x => x.PostId == id))
                throw new HbtException(L("Identity.Post.DeleteFailed"));

            return await _postRepository.DeleteAsync(post) > 0;
        }

        /// <summary>
        /// 批量删除岗位
        /// </summary>
        public async Task<bool> BatchDeleteAsync(long[] postIds)
        {
            if (postIds == null || postIds.Length == 0)
                throw new HbtException(L("Identity.Post.SelectRequired"));

            // 检查是否有用户关联
            if (await _userPostRepository.AsQueryable().AnyAsync(up => postIds.Contains(up.PostId)))
                throw new HbtException(L("Identity.Post.HasUsers"));

            return await _postRepository.DeleteRangeAsync(postIds.Cast<object>().ToList()) > 0;
        }

        /// <summary>
        /// 获取岗位选项列表
        /// </summary>
        /// <returns>岗位选项列表</returns>
        public async Task<List<HbtSelectOption>> GetOptionsAsync()
        {
            var posts = await _postRepository.AsQueryable()
                .Where(p => p.Status == 0)  // 只获取正常状态的岗位
                .OrderBy(p => p.OrderNum)
                .Select(p => new HbtSelectOption
                {
                    Label = p.PostName,
                    Value = p.Id,
                    Disabled = false
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

                        await HbtValidateUtils.ValidateFieldExistsAsync(_postRepository, "PostCode", post.PostCode);
                        await HbtValidateUtils.ValidateFieldExistsAsync(_postRepository, "PostName", post.PostName);

                        var entity = post.Adapt<HbtPost>();
                        entity.CreateTime = DateTime.Now;
                        entity.CreateBy = "system"; // TODO: 从当前用户上下文获取

                        await _postRepository.CreateAsync(entity);
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
                var list = await _postRepository.GetListAsync(HbtPostQueryExpression(query));
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
            var post = await _postRepository.GetByIdAsync(input.PostId);
            if (post == null)
                throw new HbtException(L("Identity.Post.NotFound"));

            post.Status = input.Status;
            var result = await _postRepository.UpdateAsync(post);
            return result > 0;
        }
    }
}