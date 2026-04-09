using System;
using System.Collections.Generic;
using System.Text;
using JinjiKanri.Domain.Repositories.Interface;
using JinjiKanri.Domain.Services.Interface;
using JinjiKanri.Entity.Entities;
using JinjiKanri.Common.Encryption;

namespace JinjiKanri.Domain.Services.Implements
{
    public class VLoginService : IVLoginService
    {
        private readonly IVLoginRepository _vLoginRepository;
        public VLoginService(IVLoginRepository vLoginRepository) { 
            _vLoginRepository = vLoginRepository;
        }

        public Vlogin? Login (string username, string password)
        {
            Vlogin? authenticatedUser = _vLoginRepository.GetLoginUser(username, password);

            return authenticatedUser;

        }
    }
}
