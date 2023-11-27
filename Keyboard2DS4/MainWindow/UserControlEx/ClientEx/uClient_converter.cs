using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Keyboard2DS4.MainWindow.UserControlEx.ClientEx
{
    internal class uClient_converter_bool2opacity : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 1.0 : 0.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    internal class uClient_converter_vector2margin : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var center = (Vector)parameter;
            var vector = (Vector)value;
            return new Thickness(center.X + (vector.X - 128) / 6, center.Y + (vector.Y - 128) / 6, 0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    internal class uClient_converter_vector2pivot : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var vector = (Vector)value;
            var idx = GetAngle((byte)vector.X, (byte)vector.Y);

            return idx < 0 ? null : new DropShadowEffect()
            {
                BlurRadius = 0,
                Direction = 90 - idx * 45,
                ShadowDepth = 5,
                Color = Colors.Cyan
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private int GetAngle(byte x, byte y, bool applyAngleDeadZone = false)
        {
            if (Math.Pow(x - 128, 2.0) + Math.Pow(y - 128, 2.0) < Math.Pow(64, 2.0))
            {
                return -1;
            }
            return AngleCalculate(x, y, applyAngleDeadZone);
        }
        private int AngleCalculate(byte x, byte y, bool applyAngleDeadZone)
        {
            var angle = -Math.Atan2(x - 128, y - 128) * 180 / Math.PI;
            angle += angle < 0 ? 360 : 0;
            angle += 22.5;
            var angle_idx = AngleDeadzone(angle / 45.0, applyAngleDeadZone); //(int)(angle / 45.0);// AngleDeadzone(angle / 45.0);//

            return (angle_idx == 8 ? 0 : angle_idx);
        }
        private int AngleDeadzone(double src, bool flag)
        {
            if (flag is false) { return (int)src; }

            int a = (int)src;//整数部分
            double b = src - a;//小数部分
            return (Math.Abs(b - 0.5) < 0.45) ? a : -1;
        }
    }
}
