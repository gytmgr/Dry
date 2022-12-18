global using Dry.Admin.Application.Contracts.Dtos;
global using Dry.Admin.Application.Contracts.Services;
global using Dry.Admin.Application.RESTFul.Api;
global using Dry.Admin.Domain;
global using Dry.Admin.EF.Sqlite;
global using Dry.Application.RESTFul.Api;
global using Dry.AutoMapper;
global using Dry.Core.Json;
global using Dry.Dependency;
global using Dry.EF.Extensions;
global using Dry.Mvc.Infrastructure;
global using Dry.Serilog.Extensions;
global using Dry.Swagger;
global using MediatR;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); }).UseDrySerilog().Build();
IDependency.RootServiceProvider = host.Services;
using var scope = host.Services.CreateScope();
var context = scope.ServiceProvider.GetService<IAdminContext>() as DbContext;
await context.Database.MigrateAsync();
await host.RunAsync();