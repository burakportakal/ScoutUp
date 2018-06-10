using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScoutUp.ViewModels
{
    public class RegisterViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string UserFirstName { get; set; }
        public string UserSurname { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString =
                "{0:dd/MM/yyyy}",
            ApplyFormatInEditMode = true)]
        public DateTime UserBirthDate { get; set; }
        public int UserGender { get; set; }
        public bool IsFirstLogin { get; set; }
       
        public string UserAbout { get; set; }
        public string UserCity { get; set; }
    }
}