using System.Windows;
using System.Windows.Controls;

namespace Keyboard2DS4.MainWindow.CustomControlEx.KeyMapperEx
{
    public partial class cKeyMapper : Control
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

    public partial class cKeyMapper
    {
        public CornerRadius BorderCornerRadius
        {
            get { return (CornerRadius)GetValue(BorderCornerRadiusProperty); }
            set { SetValue(BorderCornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty BorderCornerRadiusProperty = DependencyProperty.Register(
            name: "BorderCornerRadius",
            propertyType: typeof(CornerRadius),
            ownerType: typeof(cKeyMapper),
            typeMetadata: new FrameworkPropertyMetadata(new CornerRadius(0), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
        );
    }
}
