//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : GlobalUsings.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 09:30
// 版本号 : V1.0.0
// 描述    : 基础设施层全局 Using 声明
//===================================================================

// System 命名空间
global using System;
global using System.Collections.Generic;
global using System.Data;
global using System.Threading.Tasks;

// 项目依赖
global using Lean.Hbt.Domain.Entities;
global using Lean.Hbt.Domain.IServices.Identity;
global using Lean.Hbt.Domain.Repositories;
global using Lean.Hbt.Infrastructure.Repositories;

// Microsoft 扩展
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;

// 第三方库
global using SqlSugar;
global using StackExchange.Redis;