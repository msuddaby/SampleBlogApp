using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleBlogApp.Services.Helpers;

namespace SampleBlogApp.Services.Entities
{
    public class RoleDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public Dictionary<string, Permissions> Permissions { get; set; }
    }
}
