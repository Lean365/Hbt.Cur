//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtPostService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 岗位服务实现
//===================================================================

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using Lean.Hbt.Application.Dtos.Identity;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Common.Helpers;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.IServices;
using Lean.Hbt.Domain.Repositories;
using Lean.Hbt.Domain.Utils;
using Mapster;
using Microsoft.Extensions.Logging;
using SqlSugar;
using Lean.Hbt.Infrastructure.Services;
using Lean.Hbt.Domain.Services;
using Lean.Hbt.Domain.IServices.Admin;

namespace Lean.Hbt.Application.Services.Identity
{
    /// <summary>
    /// 岗位服务实现
    /// </summary>
    public class HbtPostService : IHbtPostService
    {
        private readonly IHbtRepository<HbtPost> _postRepository;
        private readonly IHbtRepository<HbtUserPost> _userPostRepository;
        private readonly IHbtLogger _logger;
        private readonly IHbtLocalizationService _localizationService;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtPostService(
            IHbtRepository<HbtPost> postRepository,
            IHbtRepository<HbtUserPost> userPostRepository,
            IHbtLocalizationService localizationService,
            IHbtLogger logger)
        {
            _postRepository = postRepository;
            _userPostRepository = userPostRepository;
            _localizationService = localizationService;
            _logger = logger;
        }

        /// <summary>
        /// 获取岗位分页列表
        /// </summary>
        public async Task<HbtPagedResult<HbtPostDto>> GetPagedListAsync(HbtPostQueryDto query)
        {
            var predicate = Expressionable.Create<HbtPost>();

            // 构建查询条件
            if (!string.IsNullOrEmpty(query.PostCode))
                predicate.And(p => p.PostCode.Contains(query.PostCode));
            if (!string.IsNullOrEmpty(query.PostName))
                predicate.And(p => p.PostName.Contains(query.PostName));
            if (query.Status.HasValue)
                predicate.And(p => p.Status == query.Status.Value);

            // 查询数据
            var posts = await _postRepository.AsQueryable()
                .Where(predicate.ToExpression())
                .OrderBy(p => p.OrderNum)
                .ToPageListAsync(query.PageIndex, query.PageSize);

            // 转换为DTO
            var result = posts.Adapt<HbtPagedResult<HbtPostDto>>();
            return result;
        }

        /// <summary>
        /// 获取岗位详情
        /// </summary>
        public async Task<HbtPostDto> GetAsync(long id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null)
                throw new HbtException(_localizationService.L("Common.NotExists"));

            return post.Adapt<HbtPostDto>();
        }

        /// <summary>
        /// 创建岗位
        /// </summary>
        public async Task<long> InsertAsync(HbtPostCreateDto input)
        {
            // 验证字段是否已存在
            if (await _postRepository.AsQueryable().AnyAsync(x => x.PostCode == input.PostCode))
                throw new HbtException(_localizationService.L("Common.CodeExists"));
            
            if (await _postRepository.AsQueryable().AnyAsync(x => x.PostName == input.PostName))
                throw new HbtException(_localizationService.L("Common.NameExists"));

            var post = input.Adapt<HbtPost>();
            var result = await _postRepository.InsertAsync(post);
            if (result > 0)
                _logger.Info(_localizationService.L("Common.AddSuccess"));

            return post.Id;
        }

        /// <summary>
        /// 更新岗位
        /// </summary>
        public async Task<bool> UpdateAsync(HbtPostUpdateDto input)
        {
            var post = await _postRepository.GetByIdAsync(input.Id);
            if (post == null)
                throw new HbtException(_localizationService.L("Common.NotExists"));

            // 验证字段是否已存在
            if (await _postRepository.AsQueryable().AnyAsync(x => x.PostCode == input.PostCode && x.Id != input.Id))
                throw new HbtException(_localizationService.L("Common.CodeExists"));
            
            if (await _postRepository.AsQueryable().AnyAsync(x => x.PostName == input.PostName && x.Id != input.Id))
                throw new HbtException(_localizationService.L("Common.NameExists"));

            input.Adapt(post);
            var result = await _postRepository.UpdateAsync(post);
            if (result > 0)
                _logger.Info(_localizationService.L("Common.UpdateSuccess"));

            return result > 0;
        }

        /// <summary>
        /// 删除岗位
        /// </summary>
        public async Task<bool> DeleteAsync(long id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null)
                throw new HbtException(_localizationService.L("Common.NotExists"));

            // 检查是否有用户关联
            if (await _userPostRepository.AsQueryable().AnyAsync(x => x.PostId == id))
                throw new HbtException(_localizationService.L("Common.DeleteFailed"));

            var result = await _postRepository.DeleteAsync(post);
            if (result > 0)
                _logger.Info(_localizationService.L("Common.DeleteSuccess"));

            return result > 0;
        }

        /// <summary>
        /// 批量删除岗位
        /// </summary>
        public async Task<bool> BatchDeleteAsync(long[] ids)
        {
            if (ids == null || ids.Length == 0)
                throw new HbtException("请选择要删除的岗位");

            // 检查是否有用户关联
            if (await _userPostRepository.AsQueryable().AnyAsync(up => ids.Contains(up.PostId)))
                throw new HbtException("选中的岗位中已有岗位分配,不能删除");

            Expression<Func<HbtPost, bool>> condition = p => ids.Contains(p.Id);
            var result = await _postRepository.DeleteAsync(condition);
            return result > 0;
        }

        /// <summary>
        /// 导入岗位数据
        /// </summary>
        public async Task<string> ImportAsync(Stream fileStream)
        {
            var posts = await HbtExcelHelper.ImportAsync<HbtPostImportDto>(fileStream);
            if (posts == null || !posts.Any())
                throw new HbtException("导入数据为空");

            int success = 0, fail = 0;
            foreach (var post in posts)
            {
                try
                {
                    // 验证字段是否已存在
                    try
                    {
                        if (string.IsNullOrEmpty(post.PostCode) || string.IsNullOrEmpty(post.PostName))
                        {
                            _logger.Warn("导入岗位失败: 岗位编码或岗位名称不能为空");
                            fail++;
                            continue;
                        }

                        await HbtValidateUtils.ValidateFieldExistsAsync(_postRepository, "PostCode", post.PostCode);
                        await HbtValidateUtils.ValidateFieldExistsAsync(_postRepository, "PostName", post.PostName);
                    }
                    catch (HbtException ex)
                    {
                        _logger.Warn($"导入岗位失败: {ex.Message}");
                        fail++;
                        continue;
                    }

                    // 创建岗位
                    var newPost = new HbtPost
                    {
                        PostCode = post.PostCode,
                        PostName = post.PostName,
                        OrderNum = post.OrderNum,
                        Status = post.Status == "正常" ? HbtStatus.Normal : HbtStatus.Disabled,
                        Remark = post.Remark ?? string.Empty,
                        CreateTime = DateTime.Now,
                        CreateBy = "system" // TODO: 从当前用户上下文获取
                    };

                    var result = await _postRepository.InsertAsync(newPost);
                    if (result > 0)
                        success++;
                    else
                        fail++;
                }
                catch (Exception ex)
                {
                    _logger.Error($"导入岗位失败: {post.PostCode}", ex);
                    fail++;
                }
            }

            return $"导入成功 {success} 条，失败 {fail} 条";
        }

        /// <summary>
        /// 导出岗位数据
        /// </summary>
        public async Task<byte[]> ExportAsync(HbtPostQueryDto query)
        {
            var predicate = Expressionable.Create<HbtPost>();

            // 构建查询条件
            if (!string.IsNullOrEmpty(query.PostCode))
                predicate.And(p => p.PostCode.Contains(query.PostCode));
            if (!string.IsNullOrEmpty(query.PostName))
                predicate.And(p => p.PostName.Contains(query.PostName));
            if (query.Status.HasValue)
                predicate.And(p => p.Status == query.Status.Value);

            // 查询数据
            var posts = await _postRepository.AsQueryable()
                .Where(predicate.ToExpression())
                .OrderBy(p => p.OrderNum)
                .ToListAsync();

            // 转换为导出DTO
            var exportData = posts.Adapt<List<HbtPostExportDto>>();

            // 导出Excel
            return await HbtExcelHelper.ExportAsync(exportData);
        }

        /// <summary>
        /// 获取岗位导入模板
        /// </summary>
        public async Task<byte[]> GetImportTemplateAsync()
        {
            return await HbtExcelHelper.GenerateTemplateAsync<HbtPostTemplateDto>();
        }

        /// <summary>
        /// 更新岗位状态
        /// </summary>
        public async Task<bool> UpdateStatusAsync(HbtPostStatusDto input)
        {
            var post = await _postRepository.GetByIdAsync(input.PostId);
            if (post == null)
                throw new HbtException($"岗位不存在: {input.PostId}");

            post.Status = input.Status;
            var result = await _postRepository.UpdateAsync(post);
            return result > 0;
        }
    }
} 