namespace ReckonMe.Models
{
    public class Member : BaseDataObject
    {
        /// <summary>
        /// The name
        /// </summary>
        private string _name = string.Empty;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
    }
}
