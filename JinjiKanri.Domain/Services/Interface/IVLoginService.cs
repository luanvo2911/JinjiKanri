using JinjiKanri.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JinjiKanri.Domain.Services.Interface
{
    public interface IVLoginService
    {
        Vlogin? Login(string username, string password);
    }
}
