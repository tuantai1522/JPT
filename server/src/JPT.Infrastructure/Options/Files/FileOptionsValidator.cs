using FluentValidation;

namespace JPT.Infrastructure.Options.Files
{
    public sealed class FileOptionsValidator : AbstractValidator<FileOptions>
    {
        public FileOptionsValidator()
        {
            RuleFor(x => x.DefaultUploadFile)
                .IsInEnum().WithMessage("Upload file type must be 'LocalHost' or 'Firebase'.");
            
            RuleFor(x => x.FirebaseStorageUrl)
                .NotEmpty().WithMessage("FileOptions:FirebaseStorageUrl is required");
            
            RuleFor(x => x.FirebaseBucketName)
                .NotEmpty().WithMessage("FileOptions:FirebaseBucketName is required");
        }
    }
}

