using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Keyboard2DS4.Helper
{
    public partial class AngleCalculator
    {
        private static readonly Lazy<AngleCalculator> lazyObject = new(() => new AngleCalculator());
        public static AngleCalculator Instance => lazyObject.Value;
    }

    public partial class AngleCalculator
    {
        public int GetAngle(Vector point, Vector center, bool applyAngleDeadZone = false)
        {
            if (Math.Pow(point.X - center.X, 2.0) + Math.Pow(point.Y - center.Y, 2.0) < Math.Pow(Math.Min(center.X, center.Y) / 3, 2.0)) 
            {
                return -1;
            }
            return AngleCalculate(point.X, point.Y, center, applyAngleDeadZone);
        }
        private int AngleCalculate(double x, double y, Vector center, bool applyAngleDeadZone)
        {
            var angle = -Math.Atan2(x - center.X, y - center.Y) * 180 / Math.PI;
            angle += angle < 0 ? 360 : 0;
            angle += 22.5;
            var angle_idx = AngleDeadzone(angle / 45.0, applyAngleDeadZone);

            return angle_idx == 8 ? 0 : angle_idx;
        }
        private int AngleDeadzone(double src, bool flag)
        {
            if (flag is false) { return (int)src; }

            int a = (int)src;//整数部分
            double b = src - a;//小数部分
            return Math.Abs(b - 0.5) < 0.45 ? a : -1;
        }
    }
}
