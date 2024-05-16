using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UtilityAccountManager.Data.Models;

public class AddressModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string City { get; set; }

    [Required]
    [StringLength(100)]
    public string Street { get; set; }

    [Required]
    [StringLength(10)]
    public string HouseNumber { get; set; }

    [Required]
    [Range(1,10000)]
    public int FlatNumber { get; set; }

    [Required]
    [Range(1,10)]
    public int Zipcode { get; set; }

    [NotMapped]
    public string FullAddress => $"{City}, ул. {Street}, д. {HouseNumber}, номер кв. — {FlatNumber}, почтовый индекс — {Zipcode}.";
}