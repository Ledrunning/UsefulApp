using TotalContract;

namespace ExchangeService
{
    public class MoneyConverter : IExServiceContract
    {
        private const decimal ConvertCoefficient = 50;


        public decimal Get(decimal money, string inValue, string outValue)
        {
            // На выходе доллары
            if (inValue == "Рубль" && outValue == "Доллар")
                return money / ConvertCoefficient;
            if (inValue == "Доллар" && outValue == "Рубль")
            {
                return money * ConvertCoefficient;
            }

            return money;
        }
    }
}