using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.DTOS.Identity
{
    public class UserDto
    {
        public string DisplayName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Tokens { get; set; } = default!;
    }
}
