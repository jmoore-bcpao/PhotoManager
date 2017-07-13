﻿using System.ComponentModel.DataAnnotations;

namespace BCPAO.PhotoManager.Models.Enums
{
    public enum PackageStatus
    {
        [Display(Name = "Pending Pickup")]
        PendingPickup,

        [Display(Name = "Received By Location")]
        ReceivedByLocation,

        [Display(Name = "Dispatched To Courier")]
        DispatchedToCourier,

        [Display(Name = "Out For Delivery")]
        OutForDelivery,

        [Display(Name = "Delivered To Consignee")]
        Delivered,

        [Display(Name = "Undeliverable")]
        Undeliverable
    }
}
