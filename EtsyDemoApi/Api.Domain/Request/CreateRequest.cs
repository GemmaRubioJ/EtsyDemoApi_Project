using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Domain.Request
{
    public class CreateRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
