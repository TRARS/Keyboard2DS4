using System.Windows;
using System.Windows.Controls;

namespace Keyboard2DS4.MainWindow.CustomControlEx.KeyMapperEx
{
    public class cKeyMapper : Control
    {
        static cKeyMapper()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(cKeyMapper), new FrameworkPropertyMetadata(typeof(cKeyMapper)));
        }

        public cKeyMapper()
        {
            this.HorizontalAlignment = HorizontalAlignment.Left;
            this.VerticalAlignment = VerticalAlignment.Top;

            DataContext = cKeyMapper_viewmodel.Instance;
        }
    }
}
