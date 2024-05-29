﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuapi.Application.Common.Interfaces.Persistence;
using yuapi.Domain.MenuAggregate;

namespace yuapi.Infrastructure.Persistence
{
    public class MenuRepository : IMenuRepository
    {
        private readonly DataContext _context;

        public MenuRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<int> Add(Menu menu)
        {
            var newMenu= await _context.Menus.AddAsync(menu);
            var result = await _context.SaveChangesAsync();
            return result;
        }
    }
}
