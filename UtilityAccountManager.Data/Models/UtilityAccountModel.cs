using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UtilityAccountManager.Data.Models;

public class UtilityAccountModel
{
    private DateTime _endDate;

    [Required]
    [StringLength(15)]
    public string Id { get; set; } = null!;

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime StartDate { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime EndDate
    {
        get => _endDate;
        set
        {
            if (value <= StartDate)
                throw new ArgumentException("Дата окончания действия ЛС должна быть больше даты начала действия ЛС.");
            
            _endDate = value;
        }
    }

    public int AddressId { get; set; }

    [ForeignKey("AddressId")]
    public virtual AddressModel Address { get; set; }

    [Range(0.1, double.MaxValue, ErrorMessage = "Площадь помещения должна быть больше 0.")]
    public float Area { get; set; }

    public virtual ICollection<ResidentUtilityAccountModel> ResidentUtilityAccounts { get; set; } = new HashSet<ResidentUtilityAccountModel>();
}