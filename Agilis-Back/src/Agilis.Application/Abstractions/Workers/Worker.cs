﻿using System;
using System.Threading.Tasks;

namespace Agilis.Application.Abstractions.Workers
{
    public abstract class Worker
    {
        protected readonly IServiceProvider ServiceProvider;

        public Worker(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public abstract Task WorkAsync();
    }
}
