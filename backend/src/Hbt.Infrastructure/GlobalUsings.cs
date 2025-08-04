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
global using Hbt.Cur.Domain.Entities;
global using Hbt.Cur.Domain.IServices.Extensions;
global using Hbt.Cur.Domain.Repositories;
global using Hbt.Cur.Domain.Interfaces;
global using Hbt.Cur.Infrastructure.Repositories;

global using Hbt.Cur.Application.Services.Routine.Contract;
global using Hbt.Cur.Application.Services.Routine.Metting;
global using Hbt.Cur.Application.Services.Routine.Project;
global using Hbt.Cur.Application.Services.Routine.Schedule;
global using Hbt.Cur.Application.Services.Routine.Vehicle;
global using Hbt.Cur.Application.Services.Routine.Quartz;
global using Hbt.Cur.Application.Services.Routine.Document;
global using Hbt.Cur.Application.Services.Routine.Notice;
global using Hbt.Cur.Application.Services.Routine.Email;
global using Hbt.Cur.Application.Services.Routine.News;

// Microsoft 扩展
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;

// 第三方库
global using SqlSugar;
global using StackExchange.Redis;