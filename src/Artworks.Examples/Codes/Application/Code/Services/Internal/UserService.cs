using Artworks.Examples.Codes.Application.Code.CommonModel;
using Artworks.Examples.Codes.Application.Code.CommonModel.Contracts;
using Artworks.Infrastructure.Application.Service;

namespace Artworks.Examples.Codes.Application.Code.Services.Internal
{
    public class UserService : ApplicationService<Model>, IUserService
    {
        public UserService(IUserRepository repository)
            : base(repository)
        {

        }

    }
}
