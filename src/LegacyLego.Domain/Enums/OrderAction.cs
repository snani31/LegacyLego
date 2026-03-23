namespace LegacyLego.Domain.Enums;

public enum OrderAction : byte
{
    Create,
    Pay,
    Expire,
    Cancel,
    Refund
}