using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIBank_0.DesignPatterns.SingletonPattern;
using WebAPIBank_0.DTOClasses;
using WebAPIBank_0.Models.Context;
using WebAPIBank_0.Models.Entities;

namespace WebAPIBank_0.Controllers
{
    public class PaymentController : ApiController
    {
        MyContext _db;

        public PaymentController()
        {
            _db = DBTool.DBInstance;
        }


        //Asagıdaki Action sadece test icindir...

        // Test için Postman'e gerek yok .. Çünkü Get'i test etmek browser lada mümkün.. localhost.../api/Payment/GetAll init çalışıp veritabanını oluşturacak..
        // string yapılar referans tiptir. Bir yapıyı global alanda tanımlarsanız ve onlara bşr değer verilmezse bunların varsayılan/default değerleri null dır.. İlkel tipteki yapıları (int,decimal) ise tanımlarsanız ve onlara da değer vermezseniz bunların varsayılan/default değerleri 0 dır.

        // new PaymentDTO gösteriliyor CustomerCardInfos değil.. CustomerCardInfos değerleri PaymentDTO'ya atadık..

        //[HttpGet]
        //public List<PaymentDTO> GetAll()
        //{
        //    return _db.CustomerCardInfos.Select(x => new PaymentDTO
        //    {
        //        CardNumber = x.CardNumber,
        //        SecurityNumber = x.SecurityNumber,
        //        CardExpiryMonth = x.CardExpiryMonth,
        //        CardExpiryYear = x.CardExpiryYear,
        //        CardUserName = x.CardUserName
        //    }).ToList();
        //}


        [HttpPost]
        public IHttpActionResult ReceivePayment(PaymentDTO item)
        {


            CustomerCardInfo cif = _db.CustomerCardInfos.FirstOrDefault(x => x.CardNumber == item.CardNumber && x.SecurityNumber == item.SecurityNumber && x.CardUserName == item.CardUserName && x.CardExpiryYear == item.CardExpiryYear && x.CardExpiryMonth == item.CardExpiryMonth);



            if (cif != null)
            {
                if (cif.CardExpiryYear < DateTime.Now.Year)
                {
                    return BadRequest("Expired Card");
                }
                else if(cif.CardExpiryYear == DateTime.Now.Year)
                {
                    if (cif.CardExpiryMonth<DateTime.Now.Month)
                    {
                        return BadRequest("Expired Card");  //400lü statu code dur.
                    }


                    if(cif.Balance >= item.BookingPrice)
                    {
                        cif.Balance -= item.BookingPrice;
                        _db.SaveChanges();
                       
                        return Ok(); //200lü statu code dur.
                    }
                    else
                    {
                        return BadRequest("Balance Exceeded");
                    }


                }

                // else if(cif.CardExpiryYear > DateTime.Now.Year) kısmıdır..
                if (cif.Balance >= item.BookingPrice)
                {
                    cif.Balance -= item.BookingPrice;
                    _db.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest("Balance Exceeded");
                }

            }
            else
            {
                //Kart bulunamazsa
                return BadRequest("Card Not Found");
            }

           
        }
    }
}
