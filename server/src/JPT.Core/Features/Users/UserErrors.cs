using JPT.Core.Common;

namespace JPT.Core.Features.Users;

public static class UserErrors
{
    public static Error NotFound(Guid userId) => Error.NotFound(
        "Users.NotFound",
        $"The user with the Id = '{userId}' was not found");

    public static Error Unauthorized() => Error.Failure(
        "Users.Unauthorized",
        "You are not authorized to perform this action.");

    public static readonly Error NotFoundByEmail = Error.NotFound(
        "Users.NotFoundByEmail",
        "The user with the specified email was not found");
    
    public static readonly Error InvalidPassword = Error.NotFound(
        "Users.InvalidPassword",
        "Password is not correct. Please try again.");

    public static readonly Error EmailNotUnique = Error.Conflict(
        "Users.EmailNotUnique",
        "The provided email is not unique");
    
    public static Error ExceedMaximumCv(int number) => Error.Validation(
        "Users.ExceedMaximumCv",
        $"This user exceeds the maximum number of CVs allowed ({number}). Please remove before adding new one");
    
    public static readonly Error InvalidNumberCv = Error.Validation(
        "Users.ExceedMaximumCv",
        $"The maximum number of CVs is not valid. Please try again.");
    
    public static readonly Error AlreadyAddThisCv = Error.Validation(
        "Users.AlreadyAddThisCv",
        $"This user already added this CV. Please try again.");
    
    public static readonly Error EmployerCanNotAddNewCv = Error.Validation(
        "Users.EmployerCanNotAddNewCv",
        $"Only Job seeker can add new CV. Please try again.");
    
    public static readonly Error CanNotFindCvOfThisUser = Error.NotFound(
        "Users.CanNotFindCvOfThisUser",
        $"Can not find CV of this user. Please try again.");
}
