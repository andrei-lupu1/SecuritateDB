using System.ComponentModel;

namespace Models.Enums
{
    public enum RoleEnum
    {
        [Description("ADMIN")]
        ADMIN = 1,
        [Description("COURIER")]
        COURIER = 2,
        [Description("CLIENT")]
        CLIENT = 3
    }
}
