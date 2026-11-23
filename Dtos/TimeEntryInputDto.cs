using System.ComponentModel.DataAnnotations;

namespace TimesheetApi.Dtos
{
    // RegisteredBy staat hier bewust NIET in: dat halen we uit het JWT-token,
    // de client mag dat niet zelf kiezen.
    public class TimeEntryInputDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Kies een geldig project")]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Een omschrijving is verplicht")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Een omschrijving telt tussen 1 en 200 tekens")]
        public string Description { get; set; } = "";

        [Range(0.01, 24, ErrorMessage = "Het aantal uren moet groter zijn dan 0 (max. 24)")]
        public decimal Hours { get; set; }

        // nullable + [Required]: zo krijg je een 400 als de datum ontbreekt
        [Required(ErrorMessage = "Een datum is verplicht")]
        public DateTime? Date { get; set; }
    }
}
