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
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> logger;
        private readonly IService<EmployeeBLO> employeeService;
        private readonly IConverter<EmployeeBLO, EmployeeModel> converter;
        private readonly ISafeOperations<PositionBLO> positionService;
        private readonly IConverter<PositionBLO, PositionModel> positionConverter;
        private readonly IExporter<EmployeeBLO> employeeExporter;

        public EmployeeController(ILogger<EmployeeController> logger, IService<EmployeeBLO> employeeService, IConverter<EmployeeBLO, EmployeeModel> converter,
            ISafeOperations<PositionBLO> positionService, IConverter<PositionBLO, PositionModel> positionConverter, IExporter<EmployeeBLO> employeeExporter)
        {
            this.logger = logger;
            this.employeeService = employeeService;
            this.converter = converter;
            this.positionService = positionService;
            this.positionConverter = positionConverter;
            this.employeeExporter = employeeExporter;
        }
        public void InitializeViewBagSomething()
        {
            int page = 1;
            int size = 3;
            IList<PositionModel> positions = positionConverter.Convert(positionService.GetAll(page, size, ""));
            ViewData["positions"] = positions;
        }

        public IActionResult Index(string searchText, int page = 1, int size = 3)
        {
            try
            {
                if (string.IsNullOrEmpty(searchText))
                {
                    searchText = "";
                }

                IList<EmployeeModel> employees = new List<EmployeeModel>();
                employees = converter.Convert(employeeService.GetAll(page, size, searchText));
                int count = employeeService.GetCount(searchText);

                EmployeeViewModel employeeViewModel = new EmployeeViewModel
                {
                    PageViewModel = new PageViewModel(count, page, size),
                    FilterViewModel = new FilterViewModel(searchText),
                    Employees = employees
                };
                return View(employeeViewModel);
            }
            catch(Exception ex)
            {
                logger.LogError("Не удалось получить всех сотрудников");
                ErrorViewModel error = new ErrorViewModel()
                {
                    source = ex.Source,
                    message = ex.Message,
                    stackTrace = ex.StackTrace
                };
                return RedirectToAction("Error", "Home", error);
            }
        }

        [HttpGet("Employee/Create")]
        public IActionResult Create()
        {
            InitializeViewBagSomething();
            EmployeeModel employeeModel = new EmployeeModel();
            logger.LogInformation("Create Employee");
            return View("CreateOrEdit", employeeModel);
        }

        [HttpPost("Employee/Create")]
        public IActionResult Create(EmployeeModel employeeModel)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    EmployeeBLO employeeBLO = new EmployeeBLO();
                    employeeBLO = converter.Convert(employeeModel);
                    employeeService.Insert(employeeBLO);
                    logger.LogTrace($"Добавлен {this.ControllerContext.RouteData.Values["controller"].ToString()}");
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Create");
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Не удалось внести информацию о сотруднике");
                ErrorViewModel error = new ErrorViewModel()
                {
                    source = ex.Source,
                    message = ex.Message,
                    stackTrace = ex.StackTrace
                };
                return RedirectToAction("Error", "Home", error);
            }
        }


        [HttpGet("Employee/Edit/{id}")]
        public IActionResult Edit(int id)
        {
            try
            {
                InitializeViewBagSomething();
                logger.LogInformation("Edit Employee");
                EmployeeModel employee = new EmployeeModel();
                employee = converter.Convert(employeeService.GetById(id));
                return View("CreateOrEdit", employee);
            }
            catch (Exception ex)
            {
                logger.LogError("Не удалось получить информацию о сотруднике");
                ErrorViewModel error = new ErrorViewModel()
                {
                    source = ex.Source,
                    message = ex.Message,
                    stackTrace = ex.StackTrace
                };
                return RedirectToAction("Error", "Home", error);
            }
        }

        [HttpPost("Employee/Edit/{id}")]
        public IActionResult Edit(int id, EmployeeModel employeeModel)
        {
           try
            {
                if(ModelState.IsValid)
                {
                    EmployeeBLO employeeBLO = new EmployeeBLO();
                    employeeBLO = converter.Convert(employeeModel);
                    employeeService.Update(employeeBLO);
                    logger.LogTrace($"Изменена информация о {this.ControllerContext.RouteData.Values["controller"].ToString()}");
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Edit");
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Не удалось изменить информацию о сотруднике");
                ErrorViewModel error = new ErrorViewModel()
                {
                    source = ex.Source,
                    message = ex.Message,
                    stackTrace = ex.StackTrace
                };
                return RedirectToAction("Error", "Home", error);
            }
        }


        // /Employee/Delete/id
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                EmployeeModel employeeModel = new EmployeeModel();
                employeeModel = converter.Convert(employeeService.GetById(id));
                return View(employeeModel);
            }
            catch (Exception ex)
            {
                logger.LogError("Не удалось получить информацию о сотруднике");
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
                employeeService.Delete(id);
                logger.LogInformation($"Delete {this.ControllerContext.RouteData.Values["action"].ToString()}");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logger.LogError("Не удалось удалить сотрудника");
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
                var downloadName = "Employees.xlsx";
                byte[] reportBytes = employeeExporter.Export();
                return File(reportBytes, XlsxContentType, downloadName);
            }
            catch (Exception ex)
            {
                logger.LogError("Не удалось экспортировать файлы");
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