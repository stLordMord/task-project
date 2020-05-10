using ApplicationCore.Services;
using Common;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ApplicationCore.Exporter
{
    public class ProjectExporter : IExporter<ProjectBLO>
    {
        private readonly IService<ProjectBLO> projectService;

        public ProjectExporter(IService<ProjectBLO> projectService)
        {
            this.projectService = projectService;
        }

        public byte[] Export()
        {
            byte[] reportBytes;
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheetProject;
                worksheetProject = package.Workbook.Worksheets["Проекты"] != null ? package.Workbook.Worksheets["Проекты"] : package.Workbook.Worksheets.Add("Проекты");
                string searchText = "";
                int page = 1;
                int size = projectService.GetCount(searchText);
                IList<ProjectBLO> projects = projectService.GetAll(page, size, "");
                int totalRows = projects.Count;
                worksheetProject.Cells[1, 1].Value = "№";
                worksheetProject.Cells[1, 2].Value = "Название";
                worksheetProject.Cells[1, 3].Value = "Сокращенное название";
                worksheetProject.Cells[1, 4].Value = "Описание";
                int i = 0;
                for (int row = 2; row <= totalRows + 1; row++)
                {
                    worksheetProject.Cells[row, 1].Value = projects[i].Id;
                    worksheetProject.Cells[row, 2].Value = projects[i].Name;
                    worksheetProject.Cells[row, 3].Value = projects[i].ShortName;
                    worksheetProject.Cells[row, 4].Value = projects[i].Description;
                    i++;
                }
                worksheetProject.Cells.AutoFitColumns();
                reportBytes = package.GetAsByteArray();
            }
            return reportBytes;
        }
    }
}
