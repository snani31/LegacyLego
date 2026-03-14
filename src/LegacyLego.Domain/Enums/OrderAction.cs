using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace LegacyLego.Domain.Enums
{
    public enum OrderAction : byte
    {
        Create,
        Pay,
        Expire,
        Cancel,
        Refund
    }
}
