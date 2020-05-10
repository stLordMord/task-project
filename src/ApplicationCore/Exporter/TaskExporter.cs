using ApplicationCore.Services;
using Common;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ApplicationCore.Exporter
{
    public class TaskExporter : IExporter<TaskBLO>
    {
        private readonly IService<TaskBLO> taskService;

        public TaskExporter(IService<TaskBLO> taskService)
        {
            this.taskService = taskService;
        }

        public byte[] Export()
        {
            byte[] reportBytes;
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheetTask;
                worksheetTask = package.Workbook.Worksheets["Задачи"] != null ? package.Workbook.Worksheets["Задачи"] : package.Workbook.Worksheets.Add("Задачи");
                int page = 1;
                string searchText = "";
                int size = taskService.GetCount(searchText);
                IList<TaskBLO> tasks = taskService.GetAll( page,  size, "");
                int totalRows = tasks.Count;
                worksheetTask.Cells[1, 1].Value = "№";
                worksheetTask.Cells[1, 2].Value = "Проект";
                worksheetTask.Cells[1, 3].Value = "Название";
                worksheetTask.Cells[1, 4].Value = "Отведенное время";
                worksheetTask.Cells[1, 5].Value = "Начало";
                worksheetTask.Cells[1, 6].Value = "Конец";
                worksheetTask.Cells[1, 7].Value = "Статус";
                worksheetTask.Cells[1, 8].Value = "Исполнитель";
                int i = 0;
                for (int row = 2; row <= totalRows + 1; row++)
                {
                    worksheetTask.Cells[row, 1].Value = tasks[i].Id;
                    worksheetTask.Cells[row, 2].Value = tasks[i].Project.ShortName;
                    worksheetTask.Cells[row, 3].Value = tasks[i].Name;
                    worksheetTask.Cells[row, 4].Value = tasks[i].Timing;
                    worksheetTask.Cells[row, 5].Value = tasks[i].DateStart;
                    worksheetTask.Cells[row, 5].Style.Numberformat.Format = "dd-MM-yyyy";
                    worksheetTask.Cells[row, 6].Value = tasks[i].DateEnd;
                    worksheetTask.Cells[row, 6].Style.Numberformat.Format = "dd-MM-yyyy";
                    worksheetTask.Cells[row, 7].Value = tasks[i].Status.Name;
                    worksheetTask.Cells[row, 8].Value = tasks[i].Employee.Surname + " " + tasks[i].Employee.Name + " " + tasks[i].Employee.Patronymic;
                    i++;
                }
                worksheetTask.Cells.AutoFitColumns();
                reportBytes = package.GetAsByteArray();
            }
            return reportBytes;
        }
    }
}
