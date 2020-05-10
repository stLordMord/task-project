using ApplicationCore;
using ApplicationCore.Converter;
using Autofac;
using Infrastructure;
using Infrastructure.Repositories;
using Common;
using Microsoft.Extensions.Logging;
using Common.Logger;
using Infrastructure.Repositories.Converter;

namespace ApplicationCore
{
    public class ServiceModule : Module
    {
        private readonly string conString;
        private readonly string UsedEF;
        private readonly string pathLogger;
        private readonly string UsedNLog;


        public ServiceModule(string conString, string UsedEF, string pathLogger, string UsedNLog)
        {
            this.conString = conString;
            this.UsedEF = UsedEF;
            this.pathLogger = pathLogger;
            this.UsedNLog = UsedNLog;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EmployeeConverter>().As<IConverter<EmployeeDTO, EmployeeBLO>>();
            builder.RegisterType<ProjectConverter>().As<IConverter<ProjectDTO, ProjectBLO>>();
            builder.RegisterType<TaskConverter>().As<IConverter<TaskDTO, TaskBLO>>();
            builder.RegisterType<StatusConverter>().As<IConverter<StatusDTO, StatusBLO>>();
            builder.RegisterType<PositionConverter>().As<IConverter<PositionDTO, PositionBLO>>();

            if (UsedEF == "true")
            {
                builder.RegisterType<EmployeeDBRepository>().As<IRepository<EmployeeDTO>>();
                builder.RegisterType<ProjectDBRepository>().As<IRepository<ProjectDTO>>();
                builder.RegisterType<TaskDBRepository>().As<IRepository<TaskDTO>>();
                builder.RegisterType<StatusDBRepository>().As<ISafeRepository<StatusDTO>>();
                builder.RegisterType<PositionDBRepository>().As<ISafeRepository<PositionDTO>>();
            }
            else
            {
                builder.RegisterType<EmployeeRepository>().As<IRepository<EmployeeDTO>>().WithParameter(new TypedParameter(typeof(string), conString));
                builder.RegisterType<ProjectRepository>().As<IRepository<ProjectDTO>>().WithParameter(new TypedParameter(typeof(string), conString));
                builder.RegisterType<TaskRepository>().As<IRepository<TaskDTO>>().WithParameter(new TypedParameter(typeof(string), conString));
                builder.RegisterType<StatusRepository>().As<ISafeRepository<StatusDTO>>().WithParameter(new TypedParameter(typeof(string), conString));
                builder.RegisterType<PositionRepository>().As<ISafeRepository<PositionDTO>>().WithParameter(new TypedParameter(typeof(string), conString));

                builder.RegisterType<EmployeeReaderConverter>().As<IReaderConverter<EmployeeDTO>>();
                builder.RegisterType<PositionReaderConverter>().As<IReaderConverter<PositionDTO>>();
                builder.RegisterType<ProjectReaderConverter>().As<IReaderConverter<ProjectDTO>>();
                builder.RegisterType<StatusReaderConverter>().As<IReaderConverter<StatusDTO>>();
                builder.RegisterType<TaskReaderConverter>().As<IReaderConverter<TaskDTO>>();
            }

            if (UsedNLog != "true")
            {
                builder.RegisterType<FileLogger<IRepository<EmployeeDTO>>>().As<ILogger<IRepository<EmployeeDTO>>>().WithParameter(new TypedParameter(typeof(string), pathLogger));
                builder.RegisterType<FileLogger<IRepository<ProjectDTO>>>().As<ILogger<IRepository<ProjectDTO>>>().WithParameter(new TypedParameter(typeof(string), pathLogger));
                builder.RegisterType<FileLogger<IRepository<TaskDTO>>>().As<ILogger<IRepository<TaskDTO>>>().WithParameter(new TypedParameter(typeof(string), pathLogger));
                builder.RegisterType<FileLogger<ISafeRepository<StatusDTO>>>().As<ILogger<ISafeRepository<StatusDTO>>>().WithParameter(new TypedParameter(typeof(string), pathLogger));
                builder.RegisterType<FileLogger<ISafeRepository<PositionDTO>>>().As<ILogger<ISafeRepository<PositionDTO>>>().WithParameter(new TypedParameter(typeof(string), pathLogger));
               
                builder.RegisterType<FileLogger<BaseRepository<EmployeeDTO>>>().As<ILogger<BaseRepository<EmployeeDTO>>>().WithParameter(new TypedParameter(typeof(string), pathLogger));
                builder.RegisterType<FileLogger<BaseRepository<ProjectDTO>>>().As<ILogger<BaseRepository<ProjectDTO>>>().WithParameter(new TypedParameter(typeof(string), pathLogger));
                builder.RegisterType<FileLogger<BaseRepository<TaskDTO>>>().As<ILogger<BaseRepository<TaskDTO>>>().WithParameter(new TypedParameter(typeof(string), pathLogger));
                builder.RegisterType<FileLogger<BaseRepository<StatusDTO>>>().As<ILogger<BaseRepository<StatusDTO>>>().WithParameter(new TypedParameter(typeof(string), pathLogger));
                builder.RegisterType<FileLogger<BaseRepository<PositionDTO>>>().As<ILogger<BaseRepository<PositionDTO>>>().WithParameter(new TypedParameter(typeof(string), pathLogger));

                builder.RegisterType<FileLogger<EmployeeConverter>>().As<ILogger<EmployeeConverter>>().WithParameter(new TypedParameter(typeof(string), pathLogger));
                builder.RegisterType<FileLogger<StatusConverter>>().As<ILogger<StatusConverter>>().WithParameter(new TypedParameter(typeof(string), pathLogger));
                builder.RegisterType<FileLogger<ProjectConverter>>().As<ILogger<ProjectConverter>>().WithParameter(new TypedParameter(typeof(string), pathLogger));
                builder.RegisterType<FileLogger<PositionConverter>>().As<ILogger<PositionConverter>>().WithParameter(new TypedParameter(typeof(string), pathLogger));
                builder.RegisterType<FileLogger<TaskConverter>>().As<ILogger<TaskConverter>>().WithParameter(new TypedParameter(typeof(string), pathLogger));
            }
        }
    }
}
