﻿using System.ComponentModel.DataAnnotations;

namespace BCPAO.PhotoManager.Models.Enums
{
    public enum UserType
    {
        [Display(Name = "Courier")]
        COURIER,

        [Display(Name = "Dispatcher")]
        DISPATCHER,

        [Display(Name = "Accountant")]
        ACCOUNTANT,

        [Display(Name = "Normal User")]
        NORMAL_USER
    }
}
