namespace YADBF
{
    public static class ObservablePropertyExtensions
    {
        public static bool IsTrue(this IReadOnlyObservableProperty<bool> prop)
        {
            return prop.Value == true;
        }

        public static bool IsFalse(this IReadOnlyObservableProperty<bool> prop)
        {
            return prop.Value == false;
        }
        
        public static bool Toggle(this ObservableProperty<bool> prop)
        {
            return prop.Value = !prop.Value;
        }
    }
}