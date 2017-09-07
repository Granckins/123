using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.ComponentModel.DataAnnotations;

namespace Warehouse.Model.Db
{
    
    public class User  
    {
        public User() {
           
        }
        
        [Required]
        [Display(Name = "Логин")]
        public string UserName { get; set; }
        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [JsonIgnore]
        [Display(Name = "Пароль")]
        public string Password { get; set; }


        [JsonProperty("Password")]
       
        public string PasswordSetter
        {
            get { return ""; }
            set { this.Password = value; }
        } 
        
        
        public bool IsBlocked { get; set; } 
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }

        public string Id { get; set; } 
       
        public List<int> Roles { get; set; }
        public string RolesStr { get; set; }
        public DateTime? ValidToDate { get; set; } 
        public bool IsAdmin() {
            return Roles.IndexOf(1) != -1;
        }

        public bool IsOwner()
        {
            return Roles.IndexOf(2) != -1;
        }
       
        

        public string GetRoles() {
            var rolesDict = Loaders.Roles.GetInstance().List;
            return Roles!=null ? String.Join(",", Roles.Select(r => rolesDict[r])) : "";
        }
    }

   
}
