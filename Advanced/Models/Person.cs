namespace Advanced.Models
{
    public sealed class Person
    {
        public long PersonId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public long DepartmentId { get; set; }
        public long LocationId { get; set; }

        public Department? Department { get; set; }
        public Location? Location { get; set; }
    }
}
