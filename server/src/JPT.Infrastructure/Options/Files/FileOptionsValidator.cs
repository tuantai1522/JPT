using FluentValidation;

namespace JPT.Infrastructure.Options.Files
{
    public sealed class FileOptionsValidator : AbstractValidator<FileOptions>
    {
        public FileOptionsValidator()
        {
            RuleFor(x => x.DefaultUploadFile)
                .IsInEnum().WithMessage("Upload file type must be 'LocalHost' or 'Firebase'.");
            
            RuleFor(x => x.FirebaseUrl)
                .NotEmpty().WithMessage("FileOptions:FirebaseUrl is required");
            
            RuleFor(x => x.FirebaseBucketName)
                .NotEmpty().WithMessage("FileOptions:FirebaseBucketName is required");
            
            RuleFor(x => x.LocalhostUrl)
                .NotEmpty().WithMessage("FileOptions:LocalhostUrl is required");
        }
    }
}

