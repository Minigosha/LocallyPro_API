﻿/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 */


using Microsoft.AspNetCore.Identity;

namespace Repositories.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual Producer? Producer { get; set; }
    }
}
