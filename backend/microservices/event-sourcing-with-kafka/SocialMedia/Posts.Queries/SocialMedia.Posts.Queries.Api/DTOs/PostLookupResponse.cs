using SocialMedia.Posts.Common.DTOs;
using SocialMedia.Posts.Queries.Domain.Entities;

namespace SocialMedia.Posts.Queries.Api.DTOs;
public sealed record PostLookupResponse(IEnumerable<PostEntity> Posts, String Message) : BaseResponse(Message);