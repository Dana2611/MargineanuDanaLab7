using MargineanuDanaLab7.Models;
using Microsoft.Maui.Devices.Sensors;
using Plugin.LocalNotification;

namespace MargineanuDanaLab7;

public partial class ShopPage : ContentPage
{
    public ShopPage()
    {
        InitializeComponent();
    }

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;
        await App.Database.SaveShopAsync(shop);
        await Navigation.PopAsync();
    }

    async void OnShowMapButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;
        var address = shop.Adress;
        //var locations = await Geocoding.GetLocationsAsync(address);

        var options = new MapLaunchOptions {Name = "Magazinul meu preferat" };
        //var location = locations?.FirstOrDefault();
        //var myLocation = await Geolocation.GetLocationAsync();
        var location = new Location(46.73767307051609, 23.486980481345043);
        var myLocation = new Location(46.73977339715321, 23.484909295363842);

        var distance = myLocation.CalculateDistance(location, DistanceUnits.Kilometers);

        if (distance < 12)
        {
            var request = new NotificationRequest
            {
                Title = "Ai de facut cumparaturi in apropiere!",
                Description = address,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(1)
                }
            };
            LocalNotificationCenter.Current.Show(request);
        }
        await Map.OpenAsync(location, options);
        }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;
        await App.Database.DeleteShopAsync(shop.ID);
        await Navigation.PopAsync();
    }

}