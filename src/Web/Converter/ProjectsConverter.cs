using ApplicationCore;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Converter
{
    public class ProjectsConverter : IConverter<ProjectBLO, ProjectModel>
    {

        public IList<ProjectModel> Convert(IList<ProjectBLO> listBLO)
        {
            List<ProjectModel> projects = new List<ProjectModel>();
            foreach (ProjectBLO projectBLO in listBLO)
            {
                ProjectModel projectModel = new ProjectModel()
                {
                    ProjectId = projectBLO.Id,
                    FullName = projectBLO.Name,
                    ShortName = projectBLO.ShortName,
                    Description = projectBLO.Description,
                };
                projects.Add(projectModel);
            }
            return projects;
        }

        public ProjectModel Convert(ProjectBLO projectBLO)
        {
            ProjectModel projectModel = new ProjectModel()
            {
                ProjectId = projectBLO.Id,
                FullName = projectBLO.Name,
                ShortName = projectBLO.ShortName,
                Description = projectBLO.Description,
                Tasks = Convert(projectBLO.Tasks)
        };
            return projectModel;
        }

        private IList<TaskModel> Convert(IList<TaskBLO> tasksBLO)
        {
            List<TaskModel> tasks = new List<TaskModel>();
            foreach (TaskBLO task in tasksBLO)
            {
                TaskModel taskModel = new TaskModel()
                {
                    Id = task.Id,
                    ProjectId = task.ProjectId,
                    Name = task.Name,
                    Timing = task.Timing,
                    DateStart = task.DateStart,
                    DateEnd = task.DateEnd,
                    StatusId = task.StatusId,
                    EmployeeId = task.EmployeeId,
                    EmployeeName = task.Employee.Surname + " " + task.Employee.Name + " " + task.Employee.Patronymic,
                    StatusName = task.Status.Name
                };
                tasks.Add(taskModel);
            }
            return tasks;
        }


        public ProjectBLO Convert(ProjectModel projectModel)
        {
            ProjectBLO projectBLO = new ProjectBLO()
            {
                Id = projectModel.ProjectId,
                Name = projectModel.FullName,
                ShortName = projectModel.ShortName,
                Description = projectModel.Description
            };
            return projectBLO;
        }
    }
}
