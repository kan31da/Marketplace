namespace Marketplace.Core.Constants
{
    public static class ProductConstants
    {
        public static class AddProduct
        {
            public const double MIN_VALUE = 0.00;    
            public const double MAX_VALUE = 9999999.00;
            
            public const int NAME_LENTGH = 60;
            public const int DESCRIPTION_LENTGH = 600;

            public const int QUANTITY_MIN = 0;
            public const int QUANTITY_MAX = 1000000;

            public const string PRODUCT_DECIMAL_PRECISION = "decimal(7,2)";

            public const int IMAGE_PATH_LENTGH = 600;
        }
    }
}
