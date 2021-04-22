using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public abstract class Promotion 
    {
        public string Name { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public decimal Discount { get; set; }

        // [Ignore]
        public PromotionParameters Parameters { get; set; }
        public string ParametersJson { get; set; }
    }

    public abstract class PromotionParameters
    {

    }

    public class HappyHoursPromotion : PromotionParameters
    {
        public TimeSpan BeginHour { get; set; }
        public TimeSpan EndHour { get; set; }
    }

    public class WomansDayPromotion : PromotionParameters
    {
        public Gender Gender { get; set; }
        public DateTime Day { get; set; }
    }

    // | Name | From | To | Discount | BeginHour | EndHour | Gender | Day |

    // | Name | From | To | Discount | xml / json |
}
