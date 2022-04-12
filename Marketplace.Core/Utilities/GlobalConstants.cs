namespace Marketplace.Core.Utilities
{
    public static class GlobalConstants
    {
        public static class UserEdit
        {
            public const string REGULAREXPRESSION_IS_DELETED = $"^([Tt][Rr][Uu][Ee]|[Ff][Aa][Ll][Ss][Ee])$";
        }

        public static class CartProduct
        {
            public const int FIRST_QUANTITY = 1;
            public const int ZERO_QUANTITY = 0;
            public const int QUANTITY_MAX = 1000000;
        }
    }
}
