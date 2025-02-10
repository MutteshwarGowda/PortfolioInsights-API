using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwMetrics.Application.Enums
{
    public enum ErrorCode
    {
        NotFound = 404,
        ServerError = 500,

        //validation errors should be in the range 100 - 199
        ValidationError = 101,

        //Infrastructure errors should be in range 200-299
        IdentityCreationFailed = 202,


        //Application error should be in range 300 - 399
        PortfolioUpdateNotPossible = 300,
        PortfolioDeleteNotPossible = 301,
        InteractionRemovalNotAuthorized = 302,
        IdentityUserAlreadyExists = 303,
        IdentityUserDoesNotExist = 304,
        IncorrectPassword = 305,
        UnauthorizedAccountRemoval = 306,
        CommentRemovalNotAuthorized = 307,
        PortfolioManagerMismatch = 308,

        UnknownError = 999

    }
}
