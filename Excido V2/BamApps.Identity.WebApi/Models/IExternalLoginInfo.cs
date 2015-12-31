using System.ComponentModel.DataAnnotations;

namespace BamApps.Identity.WebApi.Models {
    public interface IExternalLoginInfo {
        [Required]
        string Email { get; set; }

        [Required]
        string ExternalAccessToken { get; set; }

        [Required]
        string FirstName { get; set; }

        [Required]
        string LastName { get; set; }

        [Required]
        string Provider { get; set; }

        [Required]
        string UserName { get; set; }
    }
}