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
global using Hbt.Domain.Entities;
global using Hbt.Domain.IServices.Extensions;
global using Hbt.Domain.Repositories;
global using Hbt.Domain.Interfaces;
global using Hbt.Infrastructure.Repositories;

global using Hbt.Application.Services.Routine.Contract;
global using Hbt.Application.Services.Routine.Metting;
global using Hbt.Application.Services.Routine.Project;
global using Hbt.Application.Services.Routine.Schedule;
global using Hbt.Application.Services.Routine.Vehicle;
global using Hbt.Application.Services.Routine.Quartz;
global using Hbt.Application.Services.Routine.Document;
global using Hbt.Application.Services.Routine.Notice;
global using Hbt.Application.Services.Routine.Email;
global using Hbt.Application.Services.Routine.News;

// Microsoft 扩展
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;

// 第三方库
global using SqlSugar;
global using StackExchange.Redis;