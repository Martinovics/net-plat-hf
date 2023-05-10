using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace NetPlatHF.DAL.Data;

// Add profile data for application users by adding properties to the AppUser class
public class AppUser : IdentityUser
{
    public string ApiKey { get; set; }


    public AppUser()
    { 
        ApiKey = Guid.NewGuid().ToString().Replace("-", "");
    }
}

