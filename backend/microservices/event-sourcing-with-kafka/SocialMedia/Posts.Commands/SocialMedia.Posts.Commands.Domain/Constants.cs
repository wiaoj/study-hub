namespace SocialMedia.Posts.Commands.Domain;
public static partial class Constants {
    public static class Messages {
        public const String InactivePostEdit = "You cannot edit the message of an inactive post!";
        public const String InactivePostLike = "You cannot like an inactive post!";
        public const String InactivePostAddComment = "You cannot add a comment to an inactive post!";
        public const String InactivePostEditComment = "You cannot edit a comment of an inactive post!";
        public const String InactivePostRemoveComment = "You cannot remove a comment of an inactive post!";
        public const String InactivePostDelete = "The post has already been removed!";
    }
}