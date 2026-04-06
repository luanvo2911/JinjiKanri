using System;
using System.Collections.Generic;
using System.Text;
using JinjiKanri.Base.Implementation;
using JinjiKanri.Domain.Repositories.Interface;
using JinjiKanri.Entity.Entities;

namespace JinjiKanri.Domain.Repositories.Implements
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(JinjiKanriContext dbContext) : base(dbContext)
        {
        }
    }
}
