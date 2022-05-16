using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Responses
{
    public class RegistrationResponse : AuthResult
    {
        public bool EmailConfirmed { get; set; }
        public string Code { get; set; }
    }
}
