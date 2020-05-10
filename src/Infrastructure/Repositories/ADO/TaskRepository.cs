using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Infrastructure.Repositories.Converter;

namespace Infrastructure.Repositories
{
    public class TaskRepository : BaseRepository<TaskDTO>, IRepository<TaskDTO>
    {
        private readonly ILogger<IRepository<TaskDTO>> logger;
        private readonly IRepository<EmployeeDTO> employeeRepository;
        private readonly IRepository<ProjectDTO> projectRepository;
        private readonly ISafeRepository<StatusDTO> statusRepository;

        protected override string ConnectionString { get; }
        public TaskRepository(string connectionString, 
            ILogger<IRepository<TaskDTO>> logger, 
            IRepository<EmployeeDTO> employeeRepository, 
            IRepository<ProjectDTO> projectRepository, 
            ISafeRepository<StatusDTO> statusRepository,
            IReaderConverter<TaskDTO> taskConverter) : base(logger)
        {
            this.logger = logger;
            ConnectionString = connectionString;
            this.employeeRepository = employeeRepository;
            this.projectRepository = projectRepository;
            this.statusRepository = statusRepository;
            convert = taskConverter.converterToDTO;
        }

        private readonly Func<SqlDataReader, List<TaskDTO>> convert;

        public int GetCount(string searchText)
        {
            try
            {
                string query = "GetCountOftasks";
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@searchText", searchText));
                var count = Execute<int>(query, parameters, ScalarConverter.convert);
                return count.FirstOrDefault();
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось получить количество Task на уровне TaskRepository: {ex}");
                throw new Exception("Не удалось получить количество задач");
            }
        }
        public IList<TaskDTO> GetAll(int page, int size, string searchText)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@page", page));
                parameters.Add(new SqlParameter("@size", size));
                parameters.Add(new SqlParameter("@searchText", searchText));
                string query = "GetAllTasks";
                IList<TaskDTO> tasks = Execute(query, parameters, convert);

                foreach (var task in tasks)
                {
                    task.Project = projectRepository.GetById(task.ProjectId);
                    task.Employee = employeeRepository.GetById(task.EmployeeId);
                    task.Status = statusRepository.GetById(task.StatusId);
                }
                return tasks;
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось получить список Task на уровне TaskRepository: {ex}");
                throw new Exception("Не удалось получить список задач в БД");
            }
        }
        public TaskDTO GetById(int id)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Id", id));
                string query = "GetTaskById";
                List<TaskDTO> tasks = Execute(query, parameters, convert);
                TaskDTO task = new TaskDTO();
                task = tasks.FirstOrDefault();
                task.Project = projectRepository.GetById(task.ProjectId);
                task.Employee = employeeRepository.GetById(task.EmployeeId);
                task.Status = statusRepository.GetById(task.StatusId);

                return task;
            }
            catch (Exception ex)
            {
                logger.LogError($"не удалось найти Task на уровне TaskDBRepository: {ex}");
                throw new Exception("Не удалось найти задачу с таким ID в БД");
            }
        }

        private List<SqlParameter> SqlParameters(TaskDTO taskDTO)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ProjectId", taskDTO.ProjectId));
            parameters.Add(new SqlParameter("@Name", taskDTO.Name));
            parameters.Add(new SqlParameter("@Timing", taskDTO.Timing));
            parameters.Add(new SqlParameter("@DateStart", taskDTO.DateStart));
            parameters.Add(new SqlParameter("@DateEnd", taskDTO.DateEnd));
            parameters.Add(new SqlParameter("@StatusId", taskDTO.StatusId));
            parameters.Add(new SqlParameter("@EmployeeId", taskDTO.EmployeeId));
            return parameters;
        }

        public int Insert(TaskDTO taskDTO)
        {
            try
            {
                List<SqlParameter> parameters = SqlParameters(taskDTO);
                string query = "InsertTask";
                var taskId = Execute(query, parameters, ScalarConverter.convert);
                return taskId.FirstOrDefault();
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось создать Task на уровне TaskRepository: {ex}");
                throw new Exception("Не удалось добавить задачу в БД");
            }

        }
        public void Update(TaskDTO taskDTO)
        {
            try
            {
                List<SqlParameter> parameters = SqlParameters(taskDTO);
                parameters.Add(new SqlParameter("@Id", taskDTO.Id));
                string query = "UpdateTask";
                ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось обновить Task на уровне TaskRepository: {ex}");
                throw new Exception("Не удалось отредактировать задачу в БД");
            }
        }
        public void Delete(int id)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Id", id));
                string query = "DeleteTask";
                ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось удалить Task на уровне TaskRepository: {ex}");
                throw new Exception("Не удалось удалить задачу из БД");
            }
        }



    }
}
