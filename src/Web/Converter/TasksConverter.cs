using ApplicationCore;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Converter
{
    public class TasksConverter : IConverter<TaskBLO, TaskModel>
    {
        public IList<TaskModel> Convert(IList<TaskBLO> listBLO)
        {
            List<TaskModel> tasks = new List<TaskModel>();
            foreach (TaskBLO taskBLO in listBLO)
            {
                TaskModel taskModel = Convert(taskBLO);
                tasks.Add(taskModel);
            }
            return tasks;
        }

        public TaskModel Convert(TaskBLO taskBLO)
        {
            TaskModel taskModel = new TaskModel()
            {
                Id = taskBLO.Id,
                ProjectId = taskBLO.ProjectId,
                Name = taskBLO.Name,
                Timing = taskBLO.Timing,
                DateStart = taskBLO.DateStart,
                DateEnd = taskBLO.DateEnd,
                StatusId = taskBLO.StatusId,
                EmployeeId = taskBLO.EmployeeId,
                ProjectName = taskBLO.Project.ShortName,
                EmployeeName = taskBLO.Employee.Surname + " " + taskBLO.Employee.Name + " " + taskBLO.Employee.Patronymic,
                StatusName = taskBLO.Status.Name
            };
            return taskModel;
        }
        public TaskBLO Convert(TaskModel taskModel)
        {
            TaskBLO taskBLO = new TaskBLO()
            {
                Id = taskModel.Id,
                ProjectId = taskModel.ProjectId,
                Name = taskModel.Name,
                Timing = taskModel.Timing,
                DateStart = taskModel.DateStart,
                DateEnd = taskModel.DateEnd,
                StatusId = taskModel.StatusId,
                EmployeeId = taskModel.EmployeeId
            };
            return taskBLO;
        }

    }
}
