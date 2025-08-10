using FluentValidation;

namespace JPT.UseCases.Features.Files.UploadFile;

internal sealed class UploadFileCommandValidator : AbstractValidator<UploadFileCommand>
{
    public UploadFileCommandValidator()
    {
        RuleFor(c => c.Stream)
            .NotNull().WithMessage("File is required.")
            .Must(s => s.CanRead && s.Length > 0).WithMessage("Stream must be readable and not empty.");

        RuleFor(c => c.MimeType)
            .NotEmpty().WithMessage("MimeType is required.")
            .Must(IsImageContentType).WithMessage("Only image files are allowed.");

        RuleFor(c => c.FileName)
            .Must(IsImageExtension).WithMessage("File extension must be .jpg, .jpeg, .png, .gif.");
    }

    private bool IsImageContentType(string contentType)
    {
        List<string> allowedTypes = ["image/jpeg", "image/png", "image/gif", "image/webp"];
        return allowedTypes.Contains(contentType.ToLower());
    }

    private bool IsImageExtension(string fileName)
    {
        List<string> allowedExt = [".jpg", ".jpeg", ".png", ".gif", ".webp"];
        var ext = Path.GetExtension(fileName).ToLowerInvariant();
        return allowedExt.Contains(ext);
    }
}
