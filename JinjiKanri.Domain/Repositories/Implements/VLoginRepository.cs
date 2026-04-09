using JinjiKanri.Base.Implementation;
using JinjiKanri.Domain.Repositories.Interface;
using JinjiKanri.Common.Encryption;
using System;
using System.Collections.Generic;
using System.Text;
using JinjiKanri.Entity.Entities;
using Microsoft.Identity.Client;

namespace JinjiKanri.Domain.Repositories.Implements
{
    public class VLoginRepository : BaseRepository<Vlogin>, IVLoginRepository
    {
        private readonly JinjiKanriContext _dbContext;

        public VLoginRepository(JinjiKanriContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Vlogin? GetLoginUser(string username, string password)
        {
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("Username and Password cannot be null or empty");
            }
            else
            {
                Vlogin? loginInfo = _dbContext.Vlogins.FirstOrDefault(v => v.Username == username);
                if (loginInfo == null || !Encryption.VerifyPassword(password, loginInfo.Password)) {
                    return null;
                }
                else
                {
                    return loginInfo;
                }
            }
            

        }
    }
}
