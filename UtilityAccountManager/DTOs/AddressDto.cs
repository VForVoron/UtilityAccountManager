using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UtilityAccountManager.Data.Models;

public class AddressDto
{
    public int Id { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string HouseNumber { get; set; }
    public int FlatNumber { get; set; }
    public int Zipcode { get; set; }
    public string FullAddress => $"{City}, ул. {Street}, д. {HouseNumber}, номер кв. — {FlatNumber}, почтовый индекс — {Zipcode}.";
}