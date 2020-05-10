using ApplicationCore;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Converter
{
    public class EmployeesConverter : IConverter<EmployeeBLO, EmployeeModel>
    {
        public IList<EmployeeModel> Convert(IList<EmployeeBLO> listBLO)
        {
            List<EmployeeModel> employees = new List<EmployeeModel>();
            foreach (EmployeeBLO employeeBLO in listBLO)
            {
                EmployeeModel employeeModel = Convert(employeeBLO);
                employees.Add(employeeModel);
            }
            return employees;
        }

        public EmployeeModel Convert(EmployeeBLO employeeBLO)
        {
            EmployeeModel employeeModel = new EmployeeModel()
            {
                Id = employeeBLO.Id,
                Name = employeeBLO.Name,
                Surname = employeeBLO.Surname,
                Patronymic = employeeBLO.Patronymic,
                PositionId = employeeBLO.PositionId,
                PositionName = employeeBLO.Position.Name
            };
            return employeeModel;
        }
        public EmployeeBLO Convert(EmployeeModel employeeModel)
        {
            EmployeeBLO employeeBLO = new EmployeeBLO()
            {
                Id = employeeModel.Id,
                Name = employeeModel.Name,
                Surname = employeeModel.Surname,
                Patronymic = employeeModel.Patronymic,
                PositionId = employeeModel.PositionId
            };
            return employeeBLO;
        }
    }
}
