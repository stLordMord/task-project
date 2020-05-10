using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class TaskModel
    {
        [Display(Name = "№")]
        public int Id { get; set; }

        [Display(Name = "Проект")]
        [Required(ErrorMessage = "Выберите проект")]
        [Range(1, 100000, ErrorMessage = "Выберите проект")]
        public int ProjectId { get; set; }
        [Display(Name = "Проект")]
        public string ProjectName { get; set; }

        [Display(Name = "Задача")]
        [Required(ErrorMessage = "Поле Задача обязательно для заполнения")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Display(Name = "Работа (часы)")]
        [Required(ErrorMessage = "Поле Работа обязательно для заполнения")]
        [Range(1, 10000, ErrorMessage = "Допустимый диапазон от 1 до 1 000")]
        public int Timing { get; set; }

        [Display(Name = "Дата начала")]
        [Required(ErrorMessage = "Поле Дата начала обязательно для заполнения")]
        [DataType(DataType.Date, ErrorMessage = "Выберите дату")]
        public DateTime DateStart { get; set; }

        [Display(Name = "Дата окончания")]
        [Required(ErrorMessage = "Поле Дата окончания обязательно для заполнения")]
        [DataType(DataType.Date, ErrorMessage = "Выберите дату")]
        public DateTime DateEnd { get; set; }

        [Display(Name = "Статус")]
        [Required]
        [Range(1, 100000, ErrorMessage = "Выберите Статус")]
        public int StatusId { get; set; }

        [Display(Name = "Статус")]
        public string StatusName { get; set; }

        [Display(Name = "Исполнитель")]
        [Required(ErrorMessage = "Выберите Исполнителя")]
        [Range(1, 100000, ErrorMessage = "Выберите Исполнителя")]
        public int EmployeeId { get; set; }

        [Display(Name = "Исполнитель")]
        public string EmployeeName { get; set; }
    }
}
