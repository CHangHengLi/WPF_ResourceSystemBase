namespace WPFThemingDemo.Models
{
    public class ComboItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ComboItemModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
} 