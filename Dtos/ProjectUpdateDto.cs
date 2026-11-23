using System.ComponentModel.DataAnnotations;

namespace TimesheetApi.Dtos
{
    // Voor PUT: hier mag de status wel mee, zo kan je een project afsluiten.
    public class ProjectUpdateDto
    {
        [Required(ErrorMessage = "Een naam is verplicht")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Een naam telt tussen 1 en 100 tekens")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Een klant is verplicht")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Een klant telt tussen 1 en 100 tekens")]
        public string Client { get; set; } = "";

        [Required(ErrorMessage = "Een status is verplicht")]
        [RegularExpression("^(Active|Closed)$", ErrorMessage = "De status is Active of Closed")]
        public string Status { get; set; } = "";
    }
}
