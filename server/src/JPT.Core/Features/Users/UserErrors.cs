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
    
    public static readonly Error CanNotFindCvOfThisUser = Error.Validation(
        "Users.CanNotFindCvOfThisUser",
        $"Can not find CV of this user. Please try again.");
    
    public static readonly Error CompanyNameRequired = Error.Validation(
        "Users.CompanyNameRequired",
        "Company name is required with Role Employer");
    
    public static readonly Error AccessDenied = Error.Validation(
        "Users.AccessDenied",
        $"You don't have permission to access this resource.");
    
    public static readonly Error CanNotApplyThisJob = Error.Validation(
        "Users.CanNotApplyThisJob",
        $"You can't apply this job. Please wait response from employer.");
    
    public static readonly Error EmployerCanNotApplyJob = Error.Validation(
        "Users.EmployerCanNotApplyJob",
        $"Only Job seeker can apply job. Please try again.");
    
    public static readonly Error EmployerCanNotSaveJob = Error.Validation(
        "Users.EmployerCanNotSaveJob",
        $"Only Job seeker can save job. Please try again.");
    
    public static readonly Error AlreadySaveThisJob = Error.Validation(
        "Users.AlreadySaveThisJob",
        $"This user already saved this job");
    
    public static readonly Error UserNotSaveThisJobBefore = Error.Validation(
        "Users.UserNotSaveThisJobBefore",
        $"This job is not saved by user before. Please check again.");
}
