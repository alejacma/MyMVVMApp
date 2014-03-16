using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interactivity;

namespace MyMVVMApp.Views.Behaviors
{
    // Esta clase la uso para poder actualizar los valores asociados a un TextBox sin 
    // tener que esperar a que éste pierda el foco.
    //
    // Más info aquí:
    // Binding update on TextBox.TextChanged event using Behaviors
    // http://zoltanarvai.com/2009/07/22/binding-update-on-textbox-textchanged-event-using-behaviors/
    public class UpdateOnTextChangedBehavior: Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.TextChanged += new TextChangedEventHandler(this.AssociatedObject_TextChanged);
        }

        private void AssociatedObject_TextChanged(object sender, TextChangedEventArgs e)
        {
            BindingExpression binding = this.AssociatedObject.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            this.AssociatedObject.TextChanged -= new TextChangedEventHandler(this.AssociatedObject_TextChanged);
        }
    }
}
