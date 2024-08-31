using System.Windows;
using System.Windows.Controls;

namespace Keyboard2DS4.MainWindow.CustomControlEx.DS4LayoutEx
{
    public class cDS4Layout : ItemsControl
    {
        static cDS4Layout()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(cDS4Layout), new FrameworkPropertyMetadata(typeof(cDS4Layout)));
        }

        public cDS4Layout() { }
    }
}
