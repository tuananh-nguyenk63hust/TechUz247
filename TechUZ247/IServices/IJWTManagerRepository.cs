using TechUZ247.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace TechUZ247
{
    public interface IJWTManagerRepository
    {
        TokenModel Authenticate(LoginQueryModel loginQueryModel, bool isLogin);
    }
}
