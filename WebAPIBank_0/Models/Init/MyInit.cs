using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebAPIBank_0.Models.Context;
using WebAPIBank_0.Models.Entities;

namespace WebAPIBank_0.Models.Init
{
     
    // yms34243423@gmail.com
    public class MyInit:CreateDatabaseIfNotExists<MyContext>
    {
        protected override void Seed(MyContext context)
        {
            CustomerCardInfo cif = new CustomerCardInfo();
            cif.CardUserName = "Ahmet Gokdemir";
            cif.CardNumber = "1111 1111 1111 1111";
            cif.CardExpiryYear = 2022;
            cif.CardExpiryMonth = 12;
            cif.SecurityNumber = "222";
            cif.Limit = 15000;
            cif.Balance = 10000;
            context.CustomerCardInfos.Add(cif);
            context.SaveChanges();
        }
    }
}