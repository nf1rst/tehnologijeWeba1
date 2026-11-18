using HeavyArchives.Application.DTOs.Theme;

namespace HeavyArchives.Application.DTOs.Label
{
    public class LabelWithThemesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Status { get; set; }
        public DateOnly FoundingDate { get; set; }
        public ICollection<MinimalThemeDTO> Themes { get; set; }
    }
}
