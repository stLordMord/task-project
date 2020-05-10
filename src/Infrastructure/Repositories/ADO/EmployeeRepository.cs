using Common;
using Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Infrastructure.Repositories.Converter;

namespace Infrastructure.Repositories
{
    public class EmployeeRepository : BaseRepository<EmployeeDTO>, IRepository<EmployeeDTO>
    {
        private readonly ILogger<IRepository<EmployeeDTO>> logger;
        private readonly ISafeRepository<PositionDTO> positionRepository;
        protected override string ConnectionString { get; }
        public EmployeeRepository(string connectionString, 
            ILogger<IRepository<EmployeeDTO>> logger, 
            ISafeRepository<PositionDTO> positionRepository,
            IReaderConverter<EmployeeDTO> employeeConverter) : base(logger)
        {
            this.logger = logger;
            this.positionRepository = positionRepository;
            ConnectionString = connectionString;
            convert = employeeConverter.converterToDTO;
        }
        
        private readonly Func<SqlDataReader, List<EmployeeDTO>> convert;

        public int GetCount(string searchText)
        {
            try
            {
                string query = "GetCountOfEmployees";
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@searchText", searchText));
                var count = Execute<int>(query, parameters, ScalarConverter.convert);
                return count.FirstOrDefault();
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось получить количество Employee на уровне EmployeeRepository: {ex}");
                throw new Exception("Не удалось получить количество сотрудников");
            }
        }

        public IList<EmployeeDTO> GetAll(int page, int size, string searchText)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@page", page));
                parameters.Add(new SqlParameter("@size", size));
                parameters.Add(new SqlParameter("@searchText", searchText));
                string query = "GetAllEmployees";
                IList<EmployeeDTO> employees = Execute(query, parameters, convert);
                
                foreach (var emp in employees)
                {
                    emp.Position = positionRepository.GetById(emp.PositionId);
                }
                return employees;
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось получить список Employee на уровне EmployeeRepository: {ex}");
                throw new Exception("Не удалось получить список сотрудниклов в БД");
            }
        }

        public EmployeeDTO GetById(int id)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Id", id));
                string query = "GetEmployeeById";
                List<EmployeeDTO> employees = Execute(query, parameters, convert);
                EmployeeDTO employee = new EmployeeDTO();
                employee = employees.FirstOrDefault();
                employee.Position = positionRepository.GetById(employee.PositionId);
                return employee;
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось найти Employee на уровне EmployeeRepository: {ex}");
                throw new Exception("Не удалось найти сотрудника с таким ID в БД");
            }
        }

        private List<SqlParameter> SqlParameters(EmployeeDTO employeeDTO)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Name", employeeDTO.Name));
            parameters.Add(new SqlParameter("@Surname", employeeDTO.Surname));
            parameters.Add(new SqlParameter("@Patronymic", employeeDTO.Patronymic));
            parameters.Add(new SqlParameter("@PositionId", employeeDTO.PositionId));
            return parameters;
        }

        public int Insert(EmployeeDTO employeeDTO)
        {
            try
            {
                List<SqlParameter> parameters = SqlParameters(employeeDTO);
                string query = "InsertEmployee";
                var employeeId = Execute(query, parameters, ScalarConverter.convert);
                return employeeId.FirstOrDefault();
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось создать Employee на уровне EmployeeRepository: {ex}");
                throw new Exception("Не удалось добавить сотрудника в БД");
            }
        }

        public void Update(EmployeeDTO employeeDTO)
        {
            try
            {
                List<SqlParameter> parameters = SqlParameters(employeeDTO);
                parameters.Add(new SqlParameter("@Id", employeeDTO.Id));
                string query = "UpdateEmployee";
                ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось обновить Employee на уровне EmployeeRepository: {ex}");
                throw new Exception("Не удалось отредактировать сотрудника в БД");
            }
        }

        public void Delete(int id)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Id", id));
                string query = "DeleteEmployee";
                ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось удалить Employee на уровне EmployeeRepository: {ex}");
                throw new Exception("Не удалось удалить сотрудника из БД так как у него есть задачи");
            }
        }
    }
}
