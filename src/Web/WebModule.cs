using ApplicationCore;
using ApplicationCore.Services;
using Autofac;
using Common;
using Common.Logger;
using Microsoft.Extensions.Logging;
using Web.Controllers;
using Web.Converter;
using NLog.Extensions.Logging;
using Web.Models;
using ApplicationCore.Exporter;

namespace Web
{
    public class WebModule : Module
    {
        private readonly string pathLogger;
        private readonly string UsedNLog;

        public WebModule(string pathLogger, string UsedNLog)
        {
            this.pathLogger = pathLogger;
            this.UsedNLog = UsedNLog;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EmployeesConverter>().As<IConverter<EmployeeBLO, EmployeeModel>>();
            builder.RegisterType<ProjectsConverter>().As<IConverter<ProjectBLO, ProjectModel>>();
            builder.RegisterType<TasksConverter>().As<IConverter<TaskBLO, TaskModel>>();
            builder.RegisterType<StatusesConverter>().As<IConverter<StatusBLO, StatusModel>>();
            builder.RegisterType<PositionsConverter>().As<IConverter<PositionBLO, PositionModel>>();


            builder.RegisterType<EmployeeService>().As<IService<EmployeeBLO>>();
            builder.RegisterType<ProjectService>().As<IService<ProjectBLO>>();
            builder.RegisterType<TaskService>().As<IService<TaskBLO>>();
            builder.RegisterType<StatusService>().As<ISafeOperations<StatusBLO>>();
            builder.RegisterType<PositionService>().As<ISafeOperations<PositionBLO>>();

            builder.RegisterType<EmployeeExporter>().As<IExporter<EmployeeBLO>>();
            builder.RegisterType<ProjectExporter>().As<IExporter<ProjectBLO>>();
            builder.RegisterType<TaskExporter>().As<IExporter<TaskBLO>>();

            if (UsedNLog != "true")
            {
                builder.RegisterType<FileLogger<EmployeeController>>().As<ILogger<EmployeeController>>().WithParameter(new TypedParameter(typeof(string), pathLogger));
                builder.RegisterType<FileLogger<ProjectController>>().As<ILogger<ProjectController>>().WithParameter(new TypedParameter(typeof(string), pathLogger));
                builder.RegisterType<FileLogger<TaskController>>().As<ILogger<TaskController>>().WithParameter(new TypedParameter(typeof(string), pathLogger));
            }
        }
    }
}
