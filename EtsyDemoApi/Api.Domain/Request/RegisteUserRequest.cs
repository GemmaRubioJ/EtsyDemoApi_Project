using Api.Infraestructura.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Domain.Request
{
    public class RegisterUserRequest
    {
        public string Email { get; set; } = "";
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";

        public NameDto? Name { get; set; }
        public AddressDto? Address { get; set; }
        public string? Phone { get; set; }
    }

    public class NameDto
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
    }

    public class AddressDto
    {
        public string? City { get; set; }
        public Geolocation? geolocation { get; set; }
        public string? Street { get; set; }
        public int Number { get; set; }  // Asegúrate de convertir esto de string a int en el backend si es necesario
        public string? Zipcode { get; set; }
    }
}
