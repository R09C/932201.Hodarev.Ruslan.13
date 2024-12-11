using System;
using System.ComponentModel.DataAnnotations;

namespace WebAppForms.Models
{
    public class PersonalInfo
    {
        [Required(ErrorMessage = "Имя обязательно")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Фамилия обязательна")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email обязателен")]
        [EmailAddress(ErrorMessage = "Некорректный формат email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Дата рождения обязательна")]
        public DateTime? BirthDate { get; set; }
    }
}
