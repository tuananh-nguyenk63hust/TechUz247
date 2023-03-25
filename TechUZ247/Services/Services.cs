using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechUZ247.Models;

namespace TechUZ247
{
    public class Services : IServices
    {
        private readonly IJWTManagerRepository _jWTManager;
        private readonly Database _context;

        public Services(IJWTManagerRepository jWTManager, Database context)
        {
            _jWTManager = jWTManager;
            _context = context;
        }
        public async Task<LoginReponsiveModel> LogIn(LoginQueryModel loginQuery, bool isAdmin)
        {
            try
            {
                var token = _jWTManager.Authenticate(loginQuery, true);
                if (!isAdmin)
                {
                    int numberSQL = _context.Users.Where(s => s.Phone == loginQuery.UserName && s.Password == loginQuery.Password).ToList().Count();
                    if (numberSQL > 0)
                    {
                        return new LoginReponsiveModel
                        {
                            IsSuccess = true,
                            Token = token.Token
                        };
                    }
                    else
                    {
                        return new LoginReponsiveModel
                        {
                            IsSuccess = false,
                            Token = ""
                        };
                    }
                }
                else
                {
                    if (loginQuery.UserName == "admin" && loginQuery.Password == "BkaHust")
                    {
                        return new LoginReponsiveModel
                        {
                            IsSuccess = true,
                            Token = token.Token
                        };
                    }
                    return new LoginReponsiveModel
                    {
                        IsSuccess = false,
                        Token = ""
                    };
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new LoginReponsiveModel
                {
                    IsSuccess = false,
                    Token = ""
                };
            }

        }
        public async Task<LoginReponsiveModel> SignUp(SignUpQueryModel loginQuery)
        {
            try
            {
                LoginQueryModel loginQueryModel = new LoginQueryModel
                {
                    UserName = loginQuery.UserName,
                    Password = loginQuery.Password
                };
                var token = _jWTManager.Authenticate(loginQueryModel, false);
                var newGuild = Guid.NewGuid().ToString();
                while (IsGuidExists(newGuild))
                {
                    newGuild = Guid.NewGuid().ToString();
                }
                var newRecord = new Users
                {
                    Name = loginQuery.Name,
                    Phone = loginQuery.UserName,
                    Password = loginQuery.Password,
                    CodePassword = newGuild
                };
                var isStateExists = _context.Users.Where(s => s.Phone == loginQuery.UserName).ToList().Count;
                if (isStateExists == 0)
                {
                    _context.Entry(newRecord).State = EntityState.Added;
                    await _context.SaveChangesAsync();
                    return new LoginReponsiveModel
                    {
                        IsSuccess = true,
                        Token = token.Token
                    };
                }
                return new LoginReponsiveModel
                {
                    IsSuccess = false,
                    Token = ""
                };
            }
            catch (Exception ex)
            {
                return new LoginReponsiveModel
                {
                    IsSuccess = false,
                    Token = ""
                };
            }

        }
        public async Task<bool> ValidateUser()
        {
            return true;
        }
        public async Task<SignUpQueryAppModel> SignUpApp(SignUpRequestModel signUpRequestModel)
        {
            try
            {
                int result = _context.Users.Where(s => s.Phone == signUpRequestModel.UserName && s.CodePassword == signUpRequestModel.NumberSecurity).ToList().Count;
                if (result == 0)
                {
                    return new SignUpQueryAppModel
                    {
                        IsState = false
                    };
                }
                return new SignUpQueryAppModel
                {
                    IsState = true
                };
            }
            catch (Exception ex)
            {
                return new SignUpQueryAppModel
                {
                    IsState = false
                };
            }
        }
        private bool IsGuidExists(string guildString)
        {
            var records = _context.Users.Where(c => c.CodePassword == guildString).ToList().Count;
            if (records > 0) return true;
            return false;
        }
        public async Task<List<GetAllMember>> GetAllMember()
        {
            var result = new List<GetAllMember>();
            try
            {
                var sqlQuery = _context.Users.ToList();
                foreach (var record in sqlQuery)
                {
                    result.Add(new GetAllMember
                    {
                        UserName = record.Phone,
                        Password = record.Password,
                        Name = record.Name,
                        NumberAccount = record.CodePassword
                    });
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return result;
        }
        public async Task<bool> EditMember(EditNumberAccount editNumberAccount)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(c => c.Phone == editNumberAccount.Before);
                if (user != null)
                {
                    user.CodePassword = editNumberAccount.After;
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
