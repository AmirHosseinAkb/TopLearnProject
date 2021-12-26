using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Core.DTOs.User
{
    public class ChargeWalletViewModel
    {
        [Display(Name = "مبلغ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Amount { get; set; }
    }
    public class GetWalletsForShowViewModel
    {
        public int Amount { get; set; }
        public int TypeId { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsPayed { get; set; }

    }
}
