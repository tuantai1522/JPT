using JPT.Core.Common;
using MediatR;

namespace JPT.UseCases.Features.Files.UploadFile;

public sealed record UploadFileCommand(Stream Stream, string FileName, string MimeType) : IRequest<Result<UploadFileResponse>>;
