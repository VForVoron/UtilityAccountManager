using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text.Json.Serialization;

namespace UtilityAccountManager.Data.Models;

public class ResidentModel
{
    public ResidentModel() { }


    public ResidentModel(ResidentModel residentModel)
    {
        Id = residentModel.Id;
        FirstName = residentModel.FirstName;
        LastName = residentModel.LastName;
        Patronymic = residentModel.Patronymic;
        Age = residentModel.Id;
    }

    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public string LastName { get; set; }

    [StringLength(50)]
    public string Patronymic { get; set; } = string.Empty;

    [NotMapped]
    public string FullName => $"{FirstName} {LastName} {Patronymic}".Trim();

    [Required]
    [Range(0, 130)]
    public int Age { get; set; }

    public virtual ICollection<ResidentUtilityAccountModel> ResidentUtilityAccounts { get; set; } = new HashSet<ResidentUtilityAccountModel>();
}
