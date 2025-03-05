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
using Lean.Hbt.Domain.IServices.Admin;
using Lean.Hbt.Domain.Repositories;
using Lean.Hbt.Domain.Utils;
using Mapster;
using SqlSugar;

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
            var post = await _postRepository.GetByIdAsync(input.PostId);
            if (post == null)
            {
                throw new HbtException("岗位不存在");
            }

            post.PostCode = input.PostCode;
            post.PostName = input.PostName;
            post.OrderNum = input.OrderNum;
            post.Status = input.Status;
            post.Remark = input.Remark;

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
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导入的岗位数据集合</returns>
        public async Task<List<HbtPostImportDto>> ImportAsync(Stream fileStream, string sheetName = "Sheet1")
        {
            // 1.从Excel导入数据
            var posts = await HbtExcelHelper.ImportAsync<HbtPostImportDto>(fileStream, sheetName);
            if (!posts.Any())
                return new List<HbtPostImportDto>();

            // 2.检查字段是否已存在
            foreach (var post in posts)
            {
                try
                {
                    if (string.IsNullOrEmpty(post.PostCode) || string.IsNullOrEmpty(post.PostName))
                    {
                        _logger.Warn("导入岗位失败: 岗位编码或岗位名称不能为空");
                        continue;
                    }

                    await HbtValidateUtils.ValidateFieldExistsAsync(_postRepository, "PostCode", post.PostCode);
                    await HbtValidateUtils.ValidateFieldExistsAsync(_postRepository, "PostName", post.PostName);
                }
                catch (HbtException ex)
                {
                    _logger.Warn($"导入岗位失败: {ex.Message}");
                    continue;
                }
            }

            // 3.转换为实体并批量插入
            var entities = posts.Select(post => new HbtPost
            {
                PostCode = post.PostCode,
                PostName = post.PostName,
                OrderNum = post.OrderNum,
                Status = post.Status == 0 ? 0 : 1,
                Remark = post.Remark ?? string.Empty,
                CreateTime = DateTime.Now,
                CreateBy = "system" // TODO: 从当前用户上下文获取
            }).ToList();

            await _postRepository.InsertRangeAsync(entities);
            return posts;
        }

        /// <summary>
        /// 导出岗位数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导出的Excel文件字节数组</returns>
        public async Task<byte[]> ExportAsync(HbtPostQueryDto query, string sheetName = "Sheet1")
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
            return await HbtExcelHelper.ExportAsync(exportData, sheetName);
        }

        /// <summary>
        /// 生成岗位导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导入模板Excel文件字节数组</returns>
        public async Task<byte[]> GenerateTemplateAsync(string sheetName = "Sheet1")
        {
            return await HbtExcelHelper.GenerateTemplateAsync<HbtPostTemplateDto>(sheetName);
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