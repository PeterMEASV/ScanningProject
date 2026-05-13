using System;
using System.Collections.Generic;

namespace DataAccess;

public partial class Producttemplate
{
    public string Ean { get; set; } = null!;

    public string? Name { get; set; }

    public int? Estimatedexpiry { get; set; }
}
