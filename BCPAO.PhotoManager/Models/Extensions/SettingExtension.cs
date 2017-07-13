using BCPAO.PhotoManager.Data;
using BCPAO.PhotoManager.Models;
using System.Collections.Generic;

namespace BCPAO.PhotoManager.Models.Extensions
{
    public static partial class Extensions
    {
        public static SettingViewModel ToViewModel(this Setting source)
        {
            var destination = new SettingViewModel();

            destination.Name = source.Name;
            destination.DisplayName = source.DisplayName;
            destination.Description = source.Description;
            destination.Value = source.Value;

            return destination;
        }

        public static List<SettingViewModel> ToListViewModel(this List<Setting> source)
        {
            var destination = new List<SettingViewModel>();

            if(source != null)
            {
                foreach(var item in source)
                {
                    destination.Add(item.ToViewModel());
                }
            }

            return destination;
        }

        public static Setting UpdateEntity(this SettingViewModel source, Setting destination)
        {
            if (destination == null)
            {
                destination = new Setting();
            }

            destination.Value = source.Value;

            return destination;
        }
    }
}
