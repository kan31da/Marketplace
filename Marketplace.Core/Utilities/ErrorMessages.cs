namespace Marketplace.Core.Utilities
{
    public static class ErrorMessages
    {
        public static class DbError
        {
            public const string SAVE_DB_ERROR = "Someting wrong try again";
        }
        public static class UserEdit
        {
            public const string REGULAREXPRESSION_ERROR_MESSAGES = "Is Shipper has only two options, True or False";
        }

        public static class AddProduct
        {
            public const string INVALID_PRODUCT_NAME_LENGTH = "The Product name must be between 1 and 60 characters";
            public const string INVALID_PRODUCT_PRICE = "The Product price must be floating numberr with precision 0.01 to 99999.99";
            public const string INVALID_PRODUCT_DESCRIPTION_LENTGH = "The Product Description must be between 1 and 600 characters";
            public const string INVALID_PRODUCT_QUANTITY_LENTGH = "The Product Quantity must be between 0 and 1000000";
            public const string INVALID_PRODUCT_IMAGE_LENGTH = "The Product image must be between 1 and 600 characters";
        }
    }
}
