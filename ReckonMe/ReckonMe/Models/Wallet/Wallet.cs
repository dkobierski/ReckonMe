namespace ReckonMe.Models.Wallet
{
    public class Wallet : BaseDataObject
    {
        private string _name = string.Empty;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _description = string.Empty;

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
    }
}
