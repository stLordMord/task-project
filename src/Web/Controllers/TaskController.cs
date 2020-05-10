using System;
using System.Collections.Generic;
using ApplicationCore;
using ApplicationCore.Exporter;
using ApplicationCore.Services;
using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Converter;
using Web.Models;
using Web.ViewModel;

namespace Web.Controllers
{
    public class TaskController : Controller
    {
        private readonly ILogger<TaskController> logger;
        private readonly IService<TaskBLO> taskService;
        private readonly IConverter<TaskBLO, TaskModel> taskConverter;
        private readonly IService<ProjectBLO> projectService;
        private readonly IConverter<ProjectBLO, ProjectModel> projectConverter;
        private readonly IService<EmployeeBLO> employeeService;
        private readonly IConverter<EmployeeBLO, EmployeeModel> employeeConverter;
        private readonly ISafeOperations<StatusBLO> statusService;
        private readonly IConverter<StatusBLO, StatusModel> statusConverter;
        private readonly IExporter<TaskBLO> taskExporter;
        
        public TaskController(ILogger<TaskController> logger, IService<TaskBLO> taskService, IConverter<TaskBLO, 
            TaskModel> taskConverter, IService<ProjectBLO> projectService, IConverter<ProjectBLO, 
                ProjectModel> projectConverter, IService<EmployeeBLO> employeeService, IConverter<EmployeeBLO, EmployeeModel> employeeConverter,
            ISafeOperations<StatusBLO> statusService, IConverter<StatusBLO, StatusModel> statusConverter, IExporter<TaskBLO> taskExporter)
        {
            this.logger = logger;
            this.taskService = taskService;
            this.taskConverter = taskConverter;
            this.projectService = projectService;
            this.projectConverter = projectConverter;
            this.employeeService = employeeService;
            this.employeeConverter = employeeConverter;
            this.statusService = statusService;
            this.statusConverter = statusConverter;
            this.taskExporter = taskExporter;
        }
        public void InitializeViewBagSomething()
        {
            string searchText = "";
            int page = 1;
            int size = 3;
            int projectCount = projectService.GetCount(searchText);
            IList<ProjectModel> projects = projectConverter.Convert(projectService.GetAll(page, projectCount, searchText));
            ViewData["projects"] = projects;

            int employeeCount = employeeService.GetCount(searchText);
            IList<EmployeeModel> employees = employeeConverter.Convert(employeeService.GetAll(page, employeeCount, searchText));
            ViewData["employees"] = employees;

            IList<StatusModel> statuses = statusConverter.Convert(statusService.GetAll(page, size, searchText));
            ViewData["statuses"] = statuses;
        }


        public IActionResult Index(string searchText, int page = 1, int size = 3)
        {
            try
            {
                if (string.IsNullOrEmpty(searchText))
                {
                    searchText = "";
                }
                IList<TaskModel> tasks = new List<TaskModel>();

                tasks = taskConverter.Convert(taskService.GetAll(page, size, searchText));
                int count = taskService.GetCount(searchText);

                TaskViewModel taskViewModel = new TaskViewModel
                {
                    PageViewModel = new PageViewModel(count, page, size),
                    FilterViewModel = new FilterViewModel(searchText),
                    Tasks = tasks
                };
                return View(taskViewModel);
            }
            catch (Exception ex)
            {
                logger.LogError("Не удалось получить все задачи");
                ErrorViewModel error = new ErrorViewModel()
                {
                    source = ex.Source,
                    message = ex.Message,
                    stackTrace = ex.StackTrace
                };
                return RedirectToAction("Error", "Home", error);
            }
        }

        [HttpGet("Task/Create")]
        public IActionResult Create()
        {
            InitializeViewBagSomething();
            TaskModel taskModel = new TaskModel();
            return View("CreateOrEdit", taskModel);

        }
        [HttpPost("Task/Create")]
        public IActionResult Create(TaskModel taskModel)
        {
           try
           {
                if (ModelState.IsValid)
                {
                    TaskBLO taskBLO = new TaskBLO();
                    taskBLO = taskConverter.Convert(taskModel);
                    taskService.Insert(taskBLO);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Create");
                }
           }
           catch (Exception ex)
           {
                logger.LogError("Не удалось создать задачу");
                ErrorViewModel error = new ErrorViewModel()
                {
                    source = ex.Source,
                    message = ex.Message,
                    stackTrace = ex.StackTrace
                };
                return RedirectToAction("Error", "Home", error);
           }
        }


        [HttpGet("Task/CreateByProject")]
        public IActionResult CreateByProject(ProjectModel project)
        {
            InitializeViewBagSomething();
            TaskModel taskModel = new TaskModel();
            IList<ProjectModel> projects = new List<ProjectModel>();
            projects.Add(project);
            ViewData["projects"] = projects;
            return View("CreateOrEdit", taskModel);
        }

        [HttpPost("Task/CreateByProject")]
        public IActionResult CreateByProject(TaskModel taskModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TaskBLO taskBLO = new TaskBLO();
                    taskBLO = taskConverter.Convert(taskModel);
                    taskService.Insert(taskBLO);
                    return RedirectToAction("Edit", "Project", taskModel.ProjectId);
                }
                else
                {
                    return RedirectToAction("Create");
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Не удалось внести информацию о задаче");
                ErrorViewModel error = new ErrorViewModel()
                {
                    source = ex.Source,
                    message = ex.Message,
                    stackTrace = ex.StackTrace
                };
                return RedirectToAction("Error", "Home", error);
            }
        }

        [HttpGet("Task/Edit/{id}")]
        public IActionResult Edit(int id)
        {
            try
            {
                InitializeViewBagSomething();
                TaskModel task = new TaskModel();
                logger.LogInformation("Edit Task");
                task = taskConverter.Convert(taskService.GetById(id));
                return View("CreateOrEdit", task);
            }
            catch (Exception ex)
            {
                logger.LogError("Не удалось получить информацию о задаче");
                ErrorViewModel error = new ErrorViewModel()
                {
                    source = ex.Source,
                    message = ex.Message,
                    stackTrace = ex.StackTrace
                };
                return RedirectToAction("Error", "Home", error);
            }
        }



        // /Task/Edit/id
        [HttpPost("Task/Edit/{id}")]
        public IActionResult Edit(int id, TaskModel taskModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TaskBLO taskBLO = new TaskBLO();
                    taskBLO = taskConverter.Convert(taskModel);
                    taskService.Update(taskBLO);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Edit");
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Не удалось изменить информацию о задаче");
                ErrorViewModel error = new ErrorViewModel()
                {
                    source = ex.Source,
                    message = ex.Message,
                    stackTrace = ex.StackTrace
                };
                return RedirectToAction("Error", "Home", error);
            }
        }

        // /Task/Delete/id
        [HttpGet]
        public IActionResult Delete(int id)
        {
            TaskModel taskModel = new TaskModel();
            taskModel = taskConverter.Convert(taskService.GetById(id));
            return View(taskModel);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                logger.LogInformation($"Удаление {this.ControllerContext.RouteData.Values["action"].ToString()}");
                taskService.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logger.LogError("Не удалось удалить задачу");
                ErrorViewModel error = new ErrorViewModel()
                {
                    source = ex.Source,
                    message = ex.Message,
                    stackTrace = ex.StackTrace
                };
                return RedirectToAction("Error", "Home", error);
            }

        }

        public IActionResult Export()
        {
            try
            {
                string XlsxContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var downloadName = "Tasks.xlsx";
                byte[] reportBytes = taskExporter.Export();
                return File(reportBytes, XlsxContentType, downloadName);
            }
            catch (Exception ex)
            {
                logger.LogError("Не удалось экспортировать задачи");
                ErrorViewModel error = new ErrorViewModel()
                {
                    source = ex.Source,
                    message = ex.Message,
                    stackTrace = ex.StackTrace
                };
                return RedirectToAction("Error", "Home", error);
            }
        }
    }
}