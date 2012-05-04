using Domain;

namespace PresentationLayer
{
    public class EditSuggestionViewModel : CreateSuggestionViewModel
    {
        public Suggestion OriginalSuggestion { get; set; }
    }
}