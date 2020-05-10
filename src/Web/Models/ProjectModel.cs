using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class ProjectModel
    {
        [Display(Name = "№")]
        public int ProjectId { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Поле Название обязательно для заполнения")]
        [MaxLength(50)]
        public string FullName { get; set; }

        [Display(Name = "Сокращенное название")]
        [Required(ErrorMessage = "Поле Сокращенное название обязательно для заполнения")]
        [MaxLength(50)]
        public string ShortName { get; set; }

        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Поле Описание обязательно для заполнения")]
        [MaxLength(150, ErrorMessage = "Допустимая длина - 150 символов")]
        public string Description { get; set; }

        public IList<TaskModel> Tasks { get; set; }
        public string UrlBase { get; set; }
    }
}
