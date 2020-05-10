using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class EmployeeModel
    {
        [Display(Name = "№")]
        public int Id { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Поле Имя обязательно для заполнения")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Поле Фамилия обязательно для заполнения")]
        [MaxLength(50)]
        public string Surname { get; set; }

        [Display(Name = "Отчество")]
        [Required(ErrorMessage = "Поле Отчество обязательно для заполнения")]
        [MaxLength(50)]
        public string Patronymic { get; set; }

        [Display(Name = "Должность")]
        [Required]
        [Range(1, 100000, ErrorMessage = "Выберите Должность")]
        public int PositionId { get; set; }

        [Display(Name = "Должность")]
        public string PositionName { get; set; }

    }
}
