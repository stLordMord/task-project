using Common;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class EmployeeDBRepository : IRepository<EmployeeDTO>
    {
        private readonly ILogger<IRepository<EmployeeDTO>> logger;
        private readonly TrainingContext context;
        private DbSet<EmployeeDTO> employeeDTOs;
        private DbSet<PositionDTO> positionDTOs;

        public EmployeeDBRepository(TrainingContext context, ILogger<IRepository<EmployeeDTO>> logger)
        {
            this.context = context;
            this.logger = logger;
            employeeDTOs = context.Employees;
            positionDTOs = context.Positions;
        }
        public int GetCount(string searchText)
        {
            logger.LogTrace("Метод GetCount на уровне EmployeeDBRepository");
            try
            {
                int count = employeeDTOs.Count(p => p.Surname.Contains(searchText));
                return count;
            }
            catch(Exception ex)
            {
                logger.LogError($"Не удалось получить количество Employee на уровне EmployeeDBRepository: {ex}");
                throw new Exception("Не удалось получить количество сотрудников через EF");
            }
        }
        public IList<EmployeeDTO> GetAll(int page, int size, string searchText)
        {
            logger.LogTrace("Метод GetAll на уровне EmployeeDBRepository");
            IList <EmployeeDTO> employees = new List<EmployeeDTO>();
            try
            {
                employees = employeeDTOs.Where(p => p.Surname.Contains(searchText))
                    .Skip((page - 1) * size)
                    .Take(size)
                    .ToList();

                foreach (var emp in employees)
                {
                    emp.Position = positionDTOs.FirstOrDefault(e => e.Id == emp.PositionId);
                }
            }
            catch(Exception ex)
            {
                logger.LogError($"Не удалось получить список Employee на уровне EmployeeDBRepository: {ex}");
                throw new Exception("Не удалось получить список сотрудниклов в БД через EF");
            }
            return employees;
        }

        public EmployeeDTO GetById(int id)
        {
            logger.LogTrace("Метод GetById на уровне EmployeeDBRepository");
            EmployeeDTO employee = new EmployeeDTO();
            try
            {
                employee = employeeDTOs.FirstOrDefault(p => p.Id == id);
                employee.Position = positionDTOs.FirstOrDefault(e => e.Id == employee.PositionId);
            }
            catch(Exception ex)
            {
                logger.LogError($"Не удалось найти Employee на уровне EmployeeDBRepository: {ex}");
                throw new Exception("Не удалось найти сотрудника с таким ID в БД через EF");
            }
            return employee;
        }

        public int Insert(EmployeeDTO objDTO)
        {
            try
            {
                employeeDTOs.Add(objDTO);
                return context.SaveChanges();

            }
            catch(Exception ex)
            {
                logger.LogError($"Не удалось создать Employee на уровне EmployeeDBRepository: {ex}");
                throw new Exception("Не удалось добавить сотрудника в БД через EF");
            }
        }

        public void Update(EmployeeDTO objDTO)
        {
            try
            {
                employeeDTOs.Update(objDTO);
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                logger.LogError($"Не удалось обновить Employee на уровне EmployeeDBRepository: {ex}");
                throw new Exception("Не удалось отредактировать сотрудника в БД через EF");
            }
        }

        public void Delete(int id)
        {
            try
            {
                EmployeeDTO employee = employeeDTOs.SingleOrDefault(s => s.Id == id);
                employeeDTOs.Remove(employee);
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                logger.LogError($"Не удалось обновить Employee на уровне EmployeeDBRepository: {ex}");
                throw new Exception("Не удалось удалить сотрудника из БД через EF так как у него есть задачи");
            }
        }
    }
}
