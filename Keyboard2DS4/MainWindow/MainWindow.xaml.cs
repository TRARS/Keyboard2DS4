using Keyboard2DS4.Helper;
using Keyboard2DS4.Helper.Extensions;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using WF = System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Keyboard2DS4.MainWindow
{
    //无边框相关处理
    public partial class MainWindow : Window
    {
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var handle = new WindowInteropHelper(this).Handle;
            if (handle != IntPtr.Zero)
            {
                var index = (int)Win32.GetWindowLongIndex.GWL_EXSTYLE;
                var style = (int)Win32.WindowStyles.WS_EX_NOACTIVATE;
                Win32.SetWindowLong(handle, index, style);
                HwndSource.FromHwnd(handle).AddHook(new HwndSourceHook(this.WindowProc));
            }
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            var handle = new WindowInteropHelper(this).Handle;
            if (handle != IntPtr.Zero)
            {
                HwndSource.FromHwnd(handle).RemoveHook(this.WindowProc);
            }
            base.OnClosing(e);
        }

        private IntPtr WindowProc(IntPtr handle, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == (int)Win32.WindowMessages.WM_NCHITTEST)
            {
                var result = this.OnNcHitTest(handle, wParam, lParam);
                if (result != null)
                {
                    handled = true;
                    return result.Value;
                }
            }
            if (msg == (int)Win32.WindowMessages.WM_SIZE)
            {
                //this.LayoutRoot.Margin = new Thickness(0);
            }
            if (msg == (int)Win32.WindowMessages.WM_DPICHANGED)
            {
                //handled = true;
            }
            return IntPtr.Zero;
        }
        private IntPtr? OnNcHitTest(IntPtr handle, IntPtr wParam, IntPtr lParam)
        {
            var screenPoint = new Point((int)lParam & 0xFFFF, ((int)lParam >> 16) & 0xFFFF);
            var clientPoint = this.PointFromScreen(screenPoint);
            var borderHitTest = this.GetBorderHitTest(clientPoint);
            if (borderHitTest != null)
            {
                return (IntPtr)borderHitTest;
            }
            clientPoint.Y -= this.BorderThickness.Top;// 边框补正
            clientPoint.X -= this.BorderThickness.Left;
            var chromeHitTest = this.GetChromeHitTest(clientPoint);
            if (chromeHitTest != null)
            {
                return (IntPtr)chromeHitTest;
            }
            return null;
        }
        private Win32.HitTestResult? GetBorderHitTest(Point point)
        {
            if (this.WindowState != WindowState.Normal) return null;
            if (this.ResizeMode == ResizeMode.NoResize) return null;

            var 边距 = (Math.Max(this.BorderThickness.Left * 2, 4));//MainWindow.BorderThickness
            var top = (point.Y <= 边距);
            var bottom = (point.Y >= this.Height - 边距);
            var left = (point.X <= 边距);
            var right = (point.X >= this.Width - 边距);

            if (top && left) return Win32.HitTestResult.HTTOPLEFT;
            if (top && right) return Win32.HitTestResult.HTTOPRIGHT;
            if (top) return Win32.HitTestResult.HTTOP;

            if (bottom && left) return Win32.HitTestResult.HTBOTTOMLEFT;
            if (bottom && right) return Win32.HitTestResult.HTBOTTOMRIGHT;
            if (bottom) return Win32.HitTestResult.HTBOTTOM;

            if (left) return Win32.HitTestResult.HTLEFT;
            if (right) return Win32.HitTestResult.HTRIGHT;

            return null;
        }
        private Win32.HitTestResult? GetChromeHitTest(Point point)
        {
            var result = VisualTreeHelper.HitTest(this.Chrome, point);
            if (result != null)
            {
                var button = result.VisualHit.FindVisualAncestor<Button>();
                var checkbox = result.VisualHit.FindVisualAncestor<CheckBox>();
                if (button == null && checkbox == null)
                {
                    return Win32.HitTestResult.HTCAPTION;
                }
            }
            return null;
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = (this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized);
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Maximized) { this.WindowState = WindowState.Normal; }
        }
    }

    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            NotifyIcon();
            this.Left = this.Top = 5;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Mediator.Instance.Register(MessageType.WindowPosReset, para =>
            {
                this.TryMoveToPrimaryMonitor((Vector)para);
            });

            Mediator.Instance.Register(MessageType.WindowClose, _ =>
            {
                MainWindowCloseAnimation.Invoke(this, () => { noti.Visible = false; noti.Dispose(); Environment.Exit(0); });
            });

            this.TryMoveToPrimaryMonitor(new(0, 0));
        }
    }

    public partial class MainWindow
    {
        Action<MainWindow, Action> MainWindowCloseAnimation = (window, callback) =>
        {
            Storyboard sb = new();
            double StartTime = 0;
            double EndTime = 0.25;
            double diff = 0.05;

            var DAUKF = new DoubleAnimationUsingKeyFrames();
            {
                DAUKF.KeyFrames.Add(new EasingDoubleKeyFrame()
                {
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(StartTime)),
                    Value = 1
                });
                DAUKF.KeyFrames.Add(new EasingDoubleKeyFrame()
                {
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(EndTime)),
                    Value = 0
                });
            }
            Storyboard.SetTargetName(DAUKF, window.Name);
            Storyboard.SetTargetProperty(DAUKF, new PropertyPath(UIElement.OpacityProperty));

            var OAUKF = new ObjectAnimationUsingKeyFrames();
            {
                OAUKF.KeyFrames.Add(new DiscreteObjectKeyFrame()
                {
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(EndTime + diff)),
                    Value = Visibility.Collapsed
                });
            }
            Storyboard.SetTarget(OAUKF, window);
            Storyboard.SetTargetProperty(OAUKF, new PropertyPath(UIElement.VisibilityProperty));

            sb.Completed += (s, e) => { callback(); };
            sb.Children.Add(DAUKF);
            sb.Children.Add(OAUKF);
            sb.Begin(window);
        };

        WF.NotifyIcon noti;

        private void NotifyIcon()
        {
            noti = new WF.NotifyIcon()
            {
                Text = Application.ResourceAssembly.GetName().Name,
                Visible = true,
                Icon = System.Drawing.Icon.ExtractAssociatedIcon(WF.Application.ExecutablePath)
            };
            ShowInTaskbar = false;

            WF.ContextMenuStrip cs = new();
            {
                WF.ToolStripMenuItem tsm_Close = new() { Text = "Exit", Image = noti.Icon.ToBitmap() };
                tsm_Close.Click += (s, e) => 
                {
                    MainWindowCloseAnimation(this, () =>
                    {
                        noti.Visible = false; noti.Dispose(); Environment.Exit(0);
                    });
                };

                cs.Items.Add(tsm_Close);
            }
            noti.ContextMenuStrip = cs;

            //
            noti.MouseUp += (s, e) =>
            {
                if (e.Button == WF.MouseButtons.Left)
                {
                    if (this.WindowState == WindowState.Minimized)
                    {
                        this.Show();
                        this.WindowState = WindowState.Normal;
                    }
                    else
                    {
                        this.WindowState = WindowState.Minimized; 
                        this.Hide();
                    }
                }
            };
        }
    }
}
