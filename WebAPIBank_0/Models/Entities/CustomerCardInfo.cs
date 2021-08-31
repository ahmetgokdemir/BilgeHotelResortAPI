using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIBank_0.Models.Entities
{
    public class CustomerCardInfo
    {
        public int ID { get; set; }
        public string CardUserName { get; set; }
        public string SecurityNumber { get; set; } //(CVV number)
        public string CardNumber { get; set; }
        public int CardExpiryMonth { get; set; } //Son kullanım tarihi (Ay)
        public int CardExpiryYear { get; set; } //Son kullanım tarihi (Yıl)
        public decimal Limit { get; set; }
        public decimal Balance { get; set; } // Kartta ne kadar para bulunduğu..

    }
}