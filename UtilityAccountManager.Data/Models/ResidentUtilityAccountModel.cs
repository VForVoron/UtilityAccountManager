using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UtilityAccountManager.Data.Models;

public class ResidentUtilityAccountModel
{
    public int ResidentId {  get; set; }
    public ResidentModel Resident { get; set; }
    public string UtilityAccountNumber { get; set; }
    public UtilityAccountModel UtilityAccount { get; set; }
}
