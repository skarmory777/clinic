namespace Domain.Enums
{
    public static class UnitOfMeasure
    {
        public const string Unit = "unit";
        public const string Kilogram = "kg";
        public const string Gram = "g";
        public const string Liter = "l";
        public const string Milliliter = "ml";
        public const string Meter = "m";
        public const string Centimeter = "cm";
        public const string Package = "package";
        public const string Box = "box";
        public const string Pair = "pair";

        public static bool IsValid(string unit)
        {
            return unit switch
            {
                Unit or Kilogram or Gram or Liter or Milliliter 
                or Meter or Centimeter or Package or Box or Pair => true,
                _ => false
            };
        }

        public static string[] GetAll()
        {
            return new[]
            {
                Unit, Kilogram, Gram, Liter, Milliliter,
                Meter, Centimeter, Package, Box, Pair
            };
        }
    }
}