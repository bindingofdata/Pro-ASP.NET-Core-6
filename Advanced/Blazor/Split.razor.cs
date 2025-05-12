using Advanced.Models;

using Microsoft.AspNetCore.Components;

namespace Advanced.Blazor
{
    public partial class Split
    {
        [Inject]
        public DataContext? Context { get; set; }

        public IEnumerable<string> Names =>
            Context?.People.Select(person => person.FirstName)
            ?? Enumerable.Empty<string>();
    }
}
