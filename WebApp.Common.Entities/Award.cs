namespace Denis.UserList.Common.Entities
{
    public class Award
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public Award(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
