using JinjiKanri.Base.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using JinjiKanri.Entity.Entities;

namespace JinjiKanri.Domain.Repositories.Interface
{
    public interface IVLoginRepository : IBaseRepository<Vlogin>
    {
        Vlogin? GetLoginUser(string username, string encryptedPassword);
    }
}
