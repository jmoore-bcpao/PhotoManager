﻿using BCPAO.PhotoManager.Data;
using System.Collections.Generic;

namespace BCPAO.PhotoManager.Models.Extensions
{
    public static partial class Extensions
    {
        public static PermissionViewModel ToViewModel(this Permission source)
        {
            var destination = new PermissionViewModel();

            destination.Id = source.Id;
            destination.Name = source.Name;
            destination.Group = source.Group;
            destination.Description = source.Description;

            return destination;
        }
    }

    public static partial class Extensions
    {
        public static List<PermissionViewModel> ToListViewModel(this List<Permission> source)
        {
            var destination = new List<PermissionViewModel>();

            if(source != null)
            {
                foreach(var item in source)
                {
                    destination.Add(item.ToViewModel());
                }
            }

            return destination;
        }
    }
}
