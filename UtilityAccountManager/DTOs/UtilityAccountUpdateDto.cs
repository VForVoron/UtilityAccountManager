﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UtilityAccountManager.Data.Models;

public class UtilityAccountUpdateDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public float Area { get; set; }
}
