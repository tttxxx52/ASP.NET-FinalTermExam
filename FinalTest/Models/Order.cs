using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalTest.Models
{

    public class Order
    {

        [Required]
        [DisplayName("客戶代號")]
        public int CustomerID { get; set; }

        [Required]
        [DisplayName("公司名稱")]
        public string CompanyName { get; set; }
        [Required]
        [DisplayName("聯絡人名稱")]
        public string ContactName { get; set; }

        [Required]
        [DisplayName("聯絡人代號")]
        public int ContactTitle { get; set; }

        [Required]
        [DisplayName("創立日期")]
        public DateTime? CreationDate { get; set; }

        [Required]
        [DisplayName("地址")]
        public string Address { get; set; }

        [Required]
        [DisplayName("城市")]
        public string City { get; set; }


        [DisplayName("地區")]
        public string Region { get; set; }

        [Required]
        [DisplayName("國家")]
        public string Country { get; set; }
        [Required]
        [DisplayName("電話")]
        public int Phone { get; set; }


        [DisplayName("傳真")]
        public int Fax { get; set; }

        [DisplayName("CodeType")]
        public string CodeType { get; set; }


        [DisplayName("CodeId")]
        public string CodeId { get; set; }


        [DisplayName("CodeVal")]
        public string CodeVal { get; set; }


        [DisplayName("聯絡人職稱")]
        public string CodeValName { get; set; }

        [DisplayName("郵遞區號")]
        public string PostalCode { get; set; }
       

    }
}