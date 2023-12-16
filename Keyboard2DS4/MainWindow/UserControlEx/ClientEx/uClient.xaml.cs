using System.Windows.Controls;

namespace Keyboard2DS4.MainWindow.UserControlEx.ClientEx
{
    public partial class uClient : UserControl
    {
        public uClient()
        {
            InitializeComponent();
            this.DataContext = new uClient_viewmodel();
        }
    }
}
