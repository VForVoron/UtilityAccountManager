using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UtilityAccountManager.Data.Models;

public class ResidentDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName} {Patronymic}".Trim();
    public int Age { get; set; }
    public virtual ICollection<ResidentUtilityAccountModel> ResidentUtilityAccounts { get; set; } = new HashSet<ResidentUtilityAccountModel>();
}
