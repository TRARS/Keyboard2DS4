﻿using Keyboard2DS4.Helper;
using System.Windows;
using System.Windows.Controls;

namespace Keyboard2DS4.MainWindow.UserControlEx.TitleBarEx
{
    public partial class uTitleBar : UserControl
    {
        public uTitleBar()
        {
            InitializeComponent();
            this.DataContext = new uTitleBar_viewmodel();
        }
    }

    public partial class uTitleBar
    {
        #region 缺省按钮的
        private void ResetPosButton_Click(object sender, RoutedEventArgs e)
        {
            Mediator.Instance.NotifyColleagues(MessageType.WindowPosReset, new Vector(0, 0));
        }
        private void MinimizeButton_Click(object sender, RoutedEventArgs e) { throw new System.NotImplementedException(); }
        private void MaximizeButton_Click(object sender, RoutedEventArgs e) { throw new System.NotImplementedException(); }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Mediator.Instance.NotifyColleagues(MessageType.WindowClose, null);
        }
        #endregion
    }

    public partial class uTitleBar
    {
        public new bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }
        public static new readonly DependencyProperty IsActiveProperty = DependencyProperty.Register(
            name: "IsActive",
            propertyType: typeof(bool),
            ownerType: typeof(uTitleBar),
            typeMetadata: new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
        );
    }
}
