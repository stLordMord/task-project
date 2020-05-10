using ApplicationCore;
using Common;
using Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Converter
{
    public class EmployeeConverter : IConverter<EmployeeDTO, EmployeeBLO>
    {
        private readonly ILogger<EmployeeConverter> logger;
        public EmployeeConverter(ILogger<EmployeeConverter> logger)
        {
            this.logger = logger;
        }

        public IList<EmployeeBLO> Convert(IList<EmployeeDTO> listDTO)
        {
            if (listDTO == null)
            {
                logger.LogError("Значение не может быть null");
                throw new ArgumentNullException(nameof(listDTO));
            }
            List<EmployeeBLO> employees = new List<EmployeeBLO>();
            foreach (EmployeeDTO employeeDTO in listDTO)
            {
                EmployeeBLO employee = Convert(employeeDTO);
                employees.Add(employee);
            }
            return employees;
        }

        public EmployeeBLO Convert(EmployeeDTO employeeDTO)
        {
            if (employeeDTO == null)
            {
                logger.LogError("Значение не может быть null");
                throw new ArgumentNullException(nameof(employeeDTO));
            }
            EmployeeBLO employeeBLO = new EmployeeBLO()
            {
                Id = employeeDTO.Id,
                Name = employeeDTO.Name,
                Surname = employeeDTO.Surname,
                Patronymic = employeeDTO.Patronymic,
                PositionId = employeeDTO.PositionId,
                Position = new PositionBLO()
                {
                    Id = employeeDTO.Position.Id,
                    Name = employeeDTO.Position.Name
                }
            };
            return employeeBLO;
        }

        public EmployeeDTO Convert(EmployeeBLO employeeBLO)
        {
            if (employeeBLO == null)
            {
                logger.LogError("Значение не может быть null");
                throw new ArgumentNullException(nameof(employeeBLO));
            }
            EmployeeDTO employeeDTO = new EmployeeDTO()
            {
                Id = employeeBLO.Id,
                Name = employeeBLO.Name,
                Surname = employeeBLO.Surname,
                Patronymic = employeeBLO.Patronymic,
                PositionId = employeeBLO.PositionId
            };
            return employeeDTO;
        }
    }
}
