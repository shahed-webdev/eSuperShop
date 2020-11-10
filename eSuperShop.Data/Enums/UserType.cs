using System.ComponentModel;

namespace eSuperShop.Data
{
    public enum UserType
    {
        [Description("Admin")]
        Admin,

        [Description("Sub-Admin")]
        SubAdmin,

        [Description("Customer")] 
        Customer,

        [Description("Seller")]
        Seller
    }
}