using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ProjectController : Controller
    {
        private readonly ILogger<ProjectController> logger;
        private readonly IService<ProjectBLO> projectService;
        private readonly IConverter<ProjectBLO, ProjectModel> converter;
        private readonly IExporter<ProjectBLO> projectExporter;

        public ProjectController(ILogger<ProjectController> logger, IService<ProjectBLO> projectService, IConverter<ProjectBLO, ProjectModel> converter,
            IExporter<ProjectBLO> projectExporter)
        {
            this.logger = logger;
            this.projectService = projectService;
            this.converter = converter;
            this.projectExporter = projectExporter;
        }


        public IActionResult Index(string searchText, int page = 1, int size = 3)
        {
            logger.LogDebug($"Пользователь находится в контроллере  {this.ControllerContext.RouteData.Values["controller"].ToString()}");
            try
            {
                if (string.IsNullOrEmpty(searchText))
                {
                    searchText = "";
                }

                IList<ProjectModel> projects = new List<ProjectModel>();
                projects = converter.Convert(projectService.GetAll(page, size, searchText));

                int count = projectService.GetCount(searchText);

                ProjectViewModel projectViewModel = new ProjectViewModel
                {
                    PageViewModel = new PageViewModel(count, page, size),
                    FilterViewModel = new FilterViewModel(searchText),
                    Projects = projects
                };
                return View(projectViewModel);
            }
            catch (Exception ex)
            {
                logger.LogError("Не удалось получить все проекты");
                ErrorViewModel error = new ErrorViewModel()
                {
                    source = ex.Source,
                    message = ex.Message,
                    stackTrace = ex.StackTrace
                };
                return RedirectToAction("Error", "Home", error);
            }
        }


        [HttpGet("Project/Create")]
        public IActionResult Create()
        {
            ProjectModel projectModel = new ProjectModel()
            {
                Tasks = new List<TaskModel>()
            };
            return View("CreateOrEdit", projectModel);
        }

        [HttpPost("Project/Create")]
        public IActionResult Create(ProjectModel projectModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ProjectBLO projectBLO = new ProjectBLO();
                    projectBLO = converter.Convert(projectModel);
                    projectService.Insert(projectBLO);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Create");
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Не удалось внести информацию о проекте");
                ErrorViewModel error = new ErrorViewModel()
                {
                    source = ex.Source,
                    message = ex.Message,
                    stackTrace = ex.StackTrace
                };
                return RedirectToAction("Error", "Home", error);
            }
        }


        [HttpGet("Project/Edit/{id}")]
        public IActionResult Edit(int id)
        {
            ProjectModel projectModel = new ProjectModel()
            {
                Tasks = new List<TaskModel>()
            };
            projectModel = converter.Convert(projectService.GetById(id));
            return View("CreateOrEdit", projectModel);
        }

        [HttpPost("Project/Edit/{id}")]
        public IActionResult Edit(int id, ProjectModel projectModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ProjectBLO projectBLO = new ProjectBLO();
                    projectBLO = converter.Convert(projectModel);
                    projectService.Update(projectBLO);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Edit");
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Не удалось изменить информацию о проекте");
                ErrorViewModel error = new ErrorViewModel()
                {
                    source = ex.Source,
                    message = ex.Message,
                    stackTrace = ex.StackTrace
                };
                return RedirectToAction("Error", "Home", error);
            }
        }

        [HttpPost]
        public IActionResult Task(ProjectModel projectModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ProjectBLO projectBLO = new ProjectBLO();
                    projectBLO = converter.Convert(projectModel);
                    ProjectModel project = new ProjectModel();

                    if (projectModel.ProjectId == 0)
                    {
                        int id = projectService.Insert(projectBLO);
                        project = converter.Convert(projectService.GetById(id));
                    }
                    else
                    {
                        projectService.Update(projectBLO);
                        project = projectModel;
                    }
                    return RedirectToAction("CreateByProject", "Task", project);
                }
                else
                {
                    return RedirectToAction("Create");
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Не удалось внести информацию о проекте");
                ErrorViewModel error = new ErrorViewModel()
                {
                    source = ex.Source,
                    message = ex.Message,
                    stackTrace = ex.StackTrace
                };
                return RedirectToAction("Error", "Home", error);
            }
        }

        // /Project/Delete/{id}
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                ProjectModel projectModel = new ProjectModel();
                projectModel = converter.Convert(projectService.GetById(id));
                return View(projectModel);
            }
            catch (Exception ex)
            {
                logger.LogError("Не удалось получить информацию о проекте");
                ErrorViewModel error = new ErrorViewModel()
                {
                    source = ex.Source,
                    message = ex.Message,
                    stackTrace = ex.StackTrace
                };
                return RedirectToAction("Error", "Home", error);
            }
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                projectService.Delete(id);
                logger.LogInformation($"Удаление {this.ControllerContext.RouteData.Values["action"].ToString()}");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logger.LogError("Не удалось удалить проект");
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
                var downloadName = "Projects.xlsx";
                byte[] reportBytes = projectExporter.Export();
                return File(reportBytes, XlsxContentType, downloadName);
            }
            catch (Exception ex)
            {
                logger.LogError("Не удалось экспортировать проекты");
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