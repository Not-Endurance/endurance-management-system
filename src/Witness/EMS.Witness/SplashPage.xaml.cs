namespace EMS.Witness;

public partial class SplashPage : ContentPage
{
    public SplashPage()
    {
        InitializeComponent();
        Loaded += SplashPage_Loaded;
    }

    private async void SplashPage_Loaded(object? sender, EventArgs e)
    {
        // Optional: load data or delay
        await Task.Delay(2000); // simulate load

        // Navigate to main shell or Blazor page
        Application.Current.MainPage = new MainPage();
    }
}