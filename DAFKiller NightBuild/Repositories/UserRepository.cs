﻿using DAFKiller_NightBuild.Models;
using DAFKiller_NightBuild.ViewModels;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CompID;
using DAFKiller.Core;

namespace DAFKiller_NightBuild.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        //Fields
        static string filename = "userdata.dat";
        static string workingDirectory = Environment.CurrentDirectory;
        static string fileUserData = $"{workingDirectory}\\{filename}";

        public void Add(UserModel userModel)
        {
            throw new NotImplementedException();
        }


        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;
            using (var connection = GetAutorizationDBConnection())
            using (var command = new MySqlCommand())
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "select * from users where UserName=@username and Password=@password and HwId=@hwid";
                    command.Parameters.Add("@username", MySqlDbType.VarChar).Value = credential.UserName;
                    command.Parameters.Add("@password", MySqlDbType.VarChar).Value = credential.Password;
                    command.Parameters.Add("@hwid", MySqlDbType.VarChar).Value = CompID.CompID.UniqueId;
                    validUser = command.ExecuteScalar() == null ? false : true;
                    GetByUsername(credential.UserName);
                }
                catch (Exception ex)
                {
                    validUser = false;
                }
            }
            return validUser;
        }


        public void Edit(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> GetByAll()
        {
            throw new NotImplementedException();
        }

        public UserModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public UserModel GetByUsername(string username)
        {
            UserModel user = null;
            using (var connection = GetAutorizationDBConnection())
            using (var command = new MySqlCommand())
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "select * from users where UserName=@username and HwId=@hwid";
                    command.Parameters.Add("@username", MySqlDbType.VarChar).Value = username;
                    command.Parameters.Add("@hwid", MySqlDbType.VarChar).Value = CompID.CompID.UniqueId;
                    using(var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user = new UserModel()
                            {
                                Id = Convert.ToInt32(reader[0]),
                                HwId= reader[1].ToString(),
                                UserRights= (UserModel.UserRightsFlags)(int)reader[2],
                                UserName= reader[3].ToString(),
                                Password= String.Empty,
                                Email= reader[5].ToString(),
                            };
                            SaverLoader.Save(user, fileUserData);
                        }
                    }
                }
                catch (Exception ex) { }
            }
            return user;
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
