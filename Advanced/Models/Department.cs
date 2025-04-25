namespace Advanced.Models
{
    public sealed class Department
    {
        public long DepartmentId { get; set; }
        public string Name { get; set; } = string.Empty;

        public IEnumerable<Person>? People { get; set; }
    }
}
