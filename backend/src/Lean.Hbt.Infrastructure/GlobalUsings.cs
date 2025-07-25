//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : GlobalUsings.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 09:30
// 版本号 : V0.0.1
// 描述    : 基础设施层全局 Using 声明
//===================================================================

// System 命名空间
global using System;
global using System.Collections.Generic;
global using System.Data;
global using System.Threading.Tasks;

// 项目依赖
global using Lean.Hbt.Domain.Entities;
global using Lean.Hbt.Domain.IServices.Extensions;
global using Lean.Hbt.Domain.Repositories;
global using Lean.Hbt.Domain.Interfaces;
global using Lean.Hbt.Infrastructure.Repositories;

global using Lean.Hbt.Application.Services.Routine.Contract;
global using Lean.Hbt.Application.Services.Routine.Metting;
global using Lean.Hbt.Application.Services.Routine.Project;
global using Lean.Hbt.Application.Services.Routine.Schedule;
global using Lean.Hbt.Application.Services.Routine.Vehicle;
global using Lean.Hbt.Application.Services.Routine.Quartz;
global using Lean.Hbt.Application.Services.Routine.Document;
global using Lean.Hbt.Application.Services.Routine.Notice;
global using Lean.Hbt.Application.Services.Routine.Email;
global using Lean.Hbt.Application.Services.Routine.News;

// Microsoft 扩展
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;

// 第三方库
global using SqlSugar;
global using StackExchange.Redis;