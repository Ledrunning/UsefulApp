using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneralContract;

namespace ExchangeService
{
    public class MoneyConverter : IExServiceContract
    {
        private const decimal ConvertCoefficient = 50; 


        public decimal Get(decimal money, string inValue, string outValue)
        {
            // На выходе доллары
            if (inValue == "Рубль" && outValue == "Доллар")
            {
                return money / ConvertCoefficient;
            }
            else if (inValue == "Доллар" && outValue == "Рубль")
            {
                return money * ConvertCoefficient;
            }
           // else
                return money;
        }

    }
}
