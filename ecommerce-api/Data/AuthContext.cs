using ecommerce_api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ecommerce_api.Data
{
    public class AuthContext : DbContext
    {
        public AuthContext(DbContextOptions<AuthContext> options) : base(options) { }
       
       
    }
}