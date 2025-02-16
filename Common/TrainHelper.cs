using Common.Data;

namespace Common
{
    public static class TrainHelper
    {
        public static bool IsValidTrainNumber(uint number)
        {
            return 0 < number && number < 4_00000;
        }

        public static bool IsValidPMDNumber(uint number)
        {
            return 8_00000 <= number && number <= 8_99999;
        }

        public static string GetCategory(TrainType type)
        {
            return type switch
            {
                TrainType.Sluz => "Služ",
                _ => type.ToString()
            };
        }
    }
}
