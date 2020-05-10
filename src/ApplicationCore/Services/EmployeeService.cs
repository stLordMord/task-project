using Common;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace ApplicationCore.Services
{
    public class EmployeeService : IService<EmployeeBLO>
    {
        private readonly IRepository<EmployeeDTO> employeeRepository;
        private readonly IConverter<EmployeeDTO, EmployeeBLO> employeeConverter;
        private readonly ILogger<EmployeeService> logger;

        public EmployeeService(ILogger<EmployeeService> logger, IRepository<EmployeeDTO> employeeRepository, IConverter<EmployeeDTO, EmployeeBLO> employeeConverter)
        {
            this.logger = logger;
            this.employeeRepository = employeeRepository;
            this.employeeConverter = employeeConverter;
        }

        public int GetCount(string searchText)
        {
            int count;
            try
            {
                count = employeeRepository.GetCount(searchText);
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось получить количество Employee {ex}");
                throw ex;
            }
            return count;
        }

        public IList<EmployeeBLO> GetAll(int page, int size, string searchText)
        {
            IList<EmployeeBLO> employees = new List<EmployeeBLO>();
            try
            {
                employees = employeeConverter.Convert(employeeRepository.GetAll(page, size, searchText));
            }
            catch(Exception ex)
            {
                logger.LogError($"Не удалось получить список Employee на уровне EmployeeService: {ex}");
                throw ex;
            }
            return employees;
        }
        public EmployeeBLO GetById(int id)
        {
            EmployeeBLO employee = new EmployeeBLO();
            try
            {
                employee = employeeConverter.Convert(employeeRepository.GetById(id));
            }
            catch(Exception ex)
            {
                logger.LogError($"Не удалось найти Employee на уровне EmployeeService: {ex}");
                throw ex;
            }
            return employee;
        }
        public int Insert(EmployeeBLO employeeBLO)
        {
            int employeeId;
            try
            {
                EmployeeDTO employee = new EmployeeDTO();
                employee = employeeConverter.Convert(employeeBLO);
                employeeId = employeeRepository.Insert(employee);
            }
            catch(Exception ex)
            {
                logger.LogError($"Не удалось создать Employee на уровне EmployeeService: {ex}");
                throw ex;
            }
            return employeeId;
        }

        public void Update(EmployeeBLO employeeBLO)
        {
            try
            {
                EmployeeDTO employee = new EmployeeDTO();
                employee = employeeConverter.Convert(employeeBLO);
                employeeRepository.Update(employee);
            }
            catch(Exception ex)
            {
                logger.LogError($"Не удалось обновить Employee на уровне EmployeeService: {ex}");
                throw ex;
            }

        }

        public void Delete(int id)
        {
            try
            {
                employeeRepository.Delete(id);
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось обновить Employee {ex}");
                throw ex;
            }
        }
    }
}