using ApplicationCore.Services;
using Common;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ApplicationCore.Exporter
{
    public class EmployeeExporter: IExporter<EmployeeBLO>
    {
        private readonly IService<EmployeeBLO> employeeService;

        public EmployeeExporter(IService<EmployeeBLO> employeeService)
        {
            this.employeeService = employeeService;
        }

        public byte[] Export()
        {
            byte[] reportBytes;
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheetEmployee;
                worksheetEmployee = package.Workbook.Worksheets["Сотрудники"] != null ? package.Workbook.Worksheets["Сотрудники"] : package.Workbook.Worksheets.Add("Сотрудники");
                string searchText = "";
                int page = 1;
                int size = employeeService.GetCount(searchText);
                // Employees
                IList<EmployeeBLO> employees = employeeService.GetAll(page, size, "");
                int totalRows = employees.Count;
                worksheetEmployee.Cells[1, 1].Value = "№";
                worksheetEmployee.Cells[1, 2].Value = "Имя";
                worksheetEmployee.Cells[1, 3].Value = "Фамилия";
                worksheetEmployee.Cells[1, 4].Value = "Отчество";
                worksheetEmployee.Cells[1, 5].Value = "Должность";
                int i = 0;
                for (int row = 2; row <= totalRows + 1; row++)
                {
                    worksheetEmployee.Cells[row, 1].Value = employees[i].Id;
                    worksheetEmployee.Cells[row, 2].Value = employees[i].Name;
                    worksheetEmployee.Cells[row, 3].Value = employees[i].Surname;
                    worksheetEmployee.Cells[row, 4].Value = employees[i].Patronymic;
                    worksheetEmployee.Cells[row, 5].Value = employees[i].Position.Name;
                    i++;
                }
                worksheetEmployee.Cells.AutoFitColumns();
                reportBytes = package.GetAsByteArray();
            }
            return reportBytes;
        }
    }
}
