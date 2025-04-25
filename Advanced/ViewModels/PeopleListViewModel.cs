using Advanced.Models;

namespace Advanced.ViewModels
{
    public sealed class PeopleListViewModel
    {
        public IEnumerable<Person> People { get; set; } = Enumerable.Empty<Person>();
        public IEnumerable<string> Cities { get; set; } = Enumerable.Empty<string>();
        public string SelectedCity { get; set; } = string.Empty;

        public string GetClass(string? city) =>
            SelectedCity == city ? "bg-info text-white" : string.Empty;
    }
}
