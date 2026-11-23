using System.ComponentModel.DataAnnotations;

namespace TimesheetApi.Dtos
{
    // Voor POST: de client kiest geen status, een nieuw project start altijd als Active.
    public class ProjectInputDto
    {
        [Required(ErrorMessage = "Een naam is verplicht")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Een naam telt tussen 1 en 100 tekens")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Een klant is verplicht")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Een klant telt tussen 1 en 100 tekens")]
        public string Client { get; set; } = "";
    }
}
