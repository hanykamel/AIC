﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Repository
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        int SaveChanges();
    }
}
