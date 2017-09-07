using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WarehouseDB.Models
{
    public class LogOnModel
    {
        [Display(Name = "Логин")]
        [Required]
        public string UserName { get; set; }


        [Display(Name = "Пароль")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name = "Запомнить меня?")]
        public bool RememberMe { get; set; }
    }
}