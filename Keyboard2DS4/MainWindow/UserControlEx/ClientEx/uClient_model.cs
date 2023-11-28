using System.Collections.ObjectModel;
using System.Windows;

namespace Keyboard2DS4.MainWindow.UserControlEx.ClientEx
{
    partial class uClient_model
    {
        public bool L1 { get; set; }
        public bool L2 { get; set; }
        public bool R1 { get; set; }
        public bool R2 { get; set; }
        public bool L3 { get; set; }
        public bool R3 { get; set; }
        public bool Share { get; set; }
        public bool Options { get; set; }
        public bool Circle { get; set; }
        public bool Cross { get; set; }
        public bool Square { get; set; }
        public bool Triangle { get; set; }
        public bool DpadDown { get; set; }
        public bool DpadUp { get; set; }
        public bool DpadRight { get; set; }
        public bool DpadLeft { get; set; }
        public Vector LX { get; set; } = new Vector(128, 128);
        public Vector RX { get; set; } = new Vector(128, 128);
        public bool PS { get; set; }
        public bool Touchpad { get; set; }
        public bool Base { get; set; }

        public ObservableCollection<UIElement> DS4LayoutItems { get; set; }
        public ObservableCollection<bool> BorderVisibility { get; set; }
    }
}
