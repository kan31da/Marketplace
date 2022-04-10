namespace Marketplace.Infrastructure.DataConstants
{
    public static class ModelConstants
    {
        //APPLICATIONUSER

        public const int FIRSTNAME_LENTGH = 60;
        public const int LASTNAME_LENTGH = 60;
        public const int PHONE_LENTGH = 60;

        //PRODUCT
        public const int NAME_LENTGH = 60;
        public const int DESCRIPTION_LENTGH = 600;

        public const int QUANTITY_MIN = 0;
        public const int QUANTITY_MAX = 1000000;

        public const string PRODUCT_DECIMAL_PRECISION = "decimal(7,2)";

        //ORDER
        public const string ORDER_DECIMAL_PRECISION = "decimal(14,2)";

        //ORDERSTATUS
        public const int ORDERSTATUS_LENTGH = 60;

        //IMAGE
        public const int IMAGE_PATH_LENTGH = 600;

        //SUPPLIER
        public const int SUPPLIER_NAME_LENTGH = 100;
        public const int SUPPLIER_ADDRESS_LENTGH = 100;
        public const int SUPPLIER_PHONE_LENTG = 20;

        //SHIPPER
        public const int SHIPPER_NAME_LENTGH = 100;
        public const int SHIPPER_PHONE_LENTG = 20;
    }
}
