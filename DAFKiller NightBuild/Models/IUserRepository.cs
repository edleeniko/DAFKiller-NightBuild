﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DAFKiller_NightBuild.Models
{
    public interface IUserRepository
    {
        bool AuthenticateUser(NetworkCredential credential);
        bool PayedUser(NetworkCredential credential);
        void Add(UserModel userModel);
        void Edit(UserModel userModel);
        void Remove(int id);
        UserModel GetById(int id);
        UserModel GetByUsername(string name);
        IEnumerable<UserModel> GetByAll();
    }
}
