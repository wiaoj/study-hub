using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Posts.Queries.Domain.Entities;
[Table("Post", Schema = "dbo")]
public class PostEntity {
    [Key]
    public Guid PostId { get; set; }
    public String Author { get; set; }
    public DateTime DatePosted { get; set; }
    public String Message { get; set; }
    public Int32 Likes { get; set; }
    public virtual ICollection<CommentEntity> Comments { get; set; }
}