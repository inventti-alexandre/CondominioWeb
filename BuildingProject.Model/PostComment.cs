namespace BuildingProject.Model
{
    public class PostComment
    {
        public int postCommentID { get; set; }
        public int postID { get; set; }
        public int commentID { get; set; }
        public virtual Post post { get; set; }
        public virtual Comment comment { get; set; }
    }
}
