namespace ReckonMe.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            LoadApplication(new ReckonMe.App());
        }
    }
}