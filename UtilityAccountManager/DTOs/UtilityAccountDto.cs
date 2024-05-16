using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UtilityAccountManager.Data.Models;

public class UtilityAccountDto
{
    public string Id { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public virtual AddressModel Address { get; set; }
    public float Area { get; set; }
    public virtual ICollection<ResidentModel> Residents { get; set; } = new HashSet<ResidentModel>();
}
