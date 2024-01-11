using System.ComponentModel;

namespace Models.Enums
{
    public enum StatusesEnum
    {
        [Description("AWB INITIAT")]
        AWBINITIAT = 1,
        [Description("PRELUAT")]
        PRELUAT = 2,
        [Description("IN CURS DE LIVRARE")]
        INCURSDELIVRARE = 3,
        [Description("IN DEPOZIT")]
        INDEPOZIT = 4,
        [Description("LIVRAT")]
        LIVRAT = 5
    }
}
