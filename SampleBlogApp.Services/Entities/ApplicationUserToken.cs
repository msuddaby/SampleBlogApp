using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SampleBlogApp.Services.Entities
{
    public class ApplicationUserToken: IdentityUserToken<string>
    {
    }
}
