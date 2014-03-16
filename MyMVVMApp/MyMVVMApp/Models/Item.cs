using MyMVVMApp.ViewModels.Base;

namespace MyMVVMApp.Models
{
    // Elemento
    public class Item: BindableBase
    {
        // Nombre
        private string _name;
        public string Name { get { return this._name; } set { this.SetProperty(ref this._name, value); } }

        // Descripción
        private string _description;
        public string Description { get { return this._description; } set { this.SetProperty(ref this._description, value); } }

        // Constructor
        public Item()
        {
        }
    }
}
