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

        public static class DeliveryAddress
        {
            public const int MAX_LENTH = 200;
        }

        public static class Order
        {
            public const string ORDER_STATUS_IN_PROGRESS = "In progress";
            public const string ORDER_STATUS_ON_DELIVERY = "In Delivery";
            public const string ORDER_STATUS_FINISHED = "Finished";
        }

        public static class Date
        {
            public const string DATETIME_FORMAT = "dd.MM.yyyy";
        }
    }
}
