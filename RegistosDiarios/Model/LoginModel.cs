using RegistosDiarios.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace RegistosDiarios.Model
{
    public class LoginModel : NotifyObject
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool IsBusy { get; set; }
    }
}
