using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SocialMedia.Posts.Queries.Domain.Entities;
[Table("Comment", Schema = "dbo")]
public class CommentEntity {
    [Key]
    public Guid CommentId { get; set; }
    public String Username { get; set; }
    public DateTime CommentDate { get; set; }
    public String Comment { get; set; }
    public Boolean Edited { get; set; }
    public Guid PostId { get; set; }

    [JsonIgnore]
    public virtual PostEntity Post { get; set; }
}