namespace Luna
{
    public class LunaStarterOption
    {
        public LunaStarterOption()
        {
            EnableLunaFilters = true;
        }

        public bool EnableLunaFilters { get; set; }

        public bool EnableLunaModelValid { get; set; } = true;

        public bool EnableLunaGlobalExceptionHandle { get; set; } = true;
    }
}