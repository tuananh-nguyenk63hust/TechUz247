using System.Collections.Generic;
using System.Threading.Tasks;
using TechUZ247.Models;
namespace TechUZ247
{
     public interface IServices
    {
        public Task<LoginReponsiveModel> LogIn (LoginQueryModel loginQuery, bool isAdmin);
        public Task<LoginReponsiveModel> SignUp (SignUpQueryModel signUpQueryModel);
        public Task<bool> ValidateUser();
        public Task<SignUpQueryAppModel> SignUpApp (SignUpRequestModel signUpRequestModel);
        public Task<List<GetAllMember>> GetAllMember();
        public Task<bool> EditMember(EditNumberAccount editNumberAccount);
    }
}
