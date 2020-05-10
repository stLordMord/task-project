using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Infrastructure.Repositories.Converter;

namespace Infrastructure.Repositories
{
    public class ProjectRepository : BaseRepository<ProjectDTO>, IRepository<ProjectDTO>
    {
        private readonly ILogger<IRepository<ProjectDTO>> logger;
        private readonly IRepository<EmployeeDTO> employeeRepository;
        private readonly ISafeRepository<StatusDTO> statusRepository;
        protected override string ConnectionString { get; }
        public ProjectRepository(string connectionString, 
            ILogger<IRepository<ProjectDTO>> logger, 
            IRepository<EmployeeDTO> employeeRepository,
            ISafeRepository<StatusDTO> statusRepository,
            IReaderConverter<ProjectDTO> projectConverter,
            IReaderConverter<TaskDTO> taskConverter) : base(logger)
        {
            this.logger = logger;
            ConnectionString = connectionString;
            this.employeeRepository = employeeRepository;
            this.statusRepository = statusRepository;
            convert = projectConverter.converterToDTO;
            convertTask = taskConverter.converterToDTO;
        }

        private readonly Func<SqlDataReader, List<ProjectDTO>> convert;
        private readonly Func<SqlDataReader, List<TaskDTO>> convertTask;

        public int GetCount(string searchText)
        {
            try
            {
                string query = "GetCountOfProjects";
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@searchText", searchText));
                var count = Execute(query, parameters, ScalarConverter.convert);
                return count.FirstOrDefault();
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось получить количестов Project на уровне ProjectRepository: {ex}");
                throw new Exception("Не удалось получить количество проектов");
            }
        }

        public IList<ProjectDTO> GetAll(int page, int size, string searchText)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@page", page));
                parameters.Add(new SqlParameter("@size", size));
                parameters.Add(new SqlParameter("@searchText", searchText));
                string query = "GetAllProjects";
                IList<ProjectDTO> projects = Execute(query, parameters, convert);
                return projects;
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось получить список Project на уровне ProjectRepository: {ex}");
                throw new Exception("Не удалось получить список проектов");
            }
        }

        public ProjectDTO GetById(int id)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Id", id));
                string query = "GetProjectById";
                List<ProjectDTO> projects = Execute(query, parameters, convert);
                ProjectDTO project = new ProjectDTO();
                project = projects.FirstOrDefault();

                project.Tasks = GetTasksByProjectId(id);

                return project;
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось найти Project на уровне ProjectRepository: {ex}");
                throw new Exception("Не удалось найти проект с таким ID в БД");
            }
        }

        private List<SqlParameter> SqlParameters(ProjectDTO projectDTO)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Name", projectDTO.Name));
            parameters.Add(new SqlParameter("@ShortName", projectDTO.ShortName));
            parameters.Add(new SqlParameter("@Description", projectDTO.Description));
            return parameters;
        }

        public int Insert(ProjectDTO projectDTO)
        {
            try
            {
                List<SqlParameter> parameters = SqlParameters(projectDTO);
                string query = "InsertProject";
                var projectId = Execute(query, parameters, ScalarConverter.convert);
                return projectId.FirstOrDefault();
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось создать Project на уровне ProjectRepository: {ex}");
                throw new Exception("Не удалось добавить проект в БД");
            }
        }

        public void Update(ProjectDTO projectDTO)
        {
            try
            {
                List<SqlParameter> parameters = SqlParameters(projectDTO);
                parameters.Add(new SqlParameter("@Id", projectDTO.Id));
                string query = "UpdateProject";
                ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось обновить Project на уровне ProjectRepository: {ex}");
                throw new Exception("Не удалось отредактировать проект в БД");
            }

        }

        public void Delete(int id)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Id", id));
                string query = "DeleteProject";
                ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось удалить Project на уровне ProjectRepository: {ex}");
                throw new Exception("Не удалось удалить проект из БД так как у него есть задачи");
            }
        }


        private IList<TaskDTO> GetTasksByProjectId(int id)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@ProjectId", id));
                string query = "GetTasksByProjectId";
                IList<TaskDTO> tasks = Execute(query, parameters, convertTask);

                foreach (var task in tasks)
                {
                    task.Employee = employeeRepository.GetById(task.EmployeeId);
                    task.Status = statusRepository.GetById(task.StatusId);
                }
                return tasks;
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось получить TasksByProjectId на уровне TaskRepository: {ex}");
                throw new Exception("Не удалось найти задачи по данному проекту");
            }
        }
    }
}
