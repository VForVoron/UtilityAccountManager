using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityAccountManager.Data.Models;

public class ResidentUtilityAccountDto
{
    public int ResidentId {  get; set; }
    public ResidentModel Resident { get; set; }
    public string UtilityAccountNumber { get; set; }
    public UtilityAccountModel UtilityAccount { get; set; }
}
