using System;
using System.Collections.Generic;

namespace DataAccess;

public partial class Activeproduct
{
    public string Ean { get; set; } = null!;

    public string? Name { get; set; }

    public DateTime? Expirydate { get; set; }
}
