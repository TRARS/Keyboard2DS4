using Keyboard2DS4.Helper;
using System.Windows.Controls;
using System.Windows.Input;
using Key = SharpDX.DirectInput.Key;

namespace Keyboard2DS4.MainWindow.CustomControlEx.KeyMapperEx
{
    partial class cKeyMapper_event
    {
        private void ListBoxItem_MouseLeave(object s, MouseEventArgs e)
        {
            Mediator.Instance.NotifyColleagues(KeyMapperMessageType.Instance.GetCurrentKeyMapperMouseEnterItemModel, null);
        }
        private void ListBoxItem_MouseEnter(object s, MouseEventArgs e)
        {
            Mediator.Instance.NotifyColleagues(KeyMapperMessageType.Instance.GetCurrentKeyMapperMouseEnterItemModel, (MappingInfoPacket<Key>)((ListBoxItem)s).DataContext);
        }

        private void ComboBox_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (((ComboBox)sender).IsDropDownOpen is false)
            {
                e.Handled = true;
            }
        }

        private void ComboBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true; // 阻止事件传播
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox)
            {
                CounterAccessor.Instance["egvGXM2cy^r2epVK"].Increment();
            }
        }
    }
}
