using SocialMedia.Posts.Common.DTOs;

namespace SocialMedia.Posts.Commands.Api.DTOs;
public sealed record NewPostResponse(Guid Id, String Message) : BaseResponse(Message);