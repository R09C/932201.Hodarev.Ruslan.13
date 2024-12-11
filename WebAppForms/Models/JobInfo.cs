using System.ComponentModel.DataAnnotations;

namespace WebAppForms.Models
{
    public class JobInfo
    {
        [Required(ErrorMessage = "Должность обязательна")]
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "Образование обязательно")]
        public string Education { get; set; }
    }
}
