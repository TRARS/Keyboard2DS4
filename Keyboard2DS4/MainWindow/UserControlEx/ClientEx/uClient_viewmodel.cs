using Keyboard2DS4.Core;
using Keyboard2DS4.Helper;
using Keyboard2DS4.MainWindow.CustomControlEx.KeyMapperEx;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Key = SharpDX.DirectInput.Key;

namespace Keyboard2DS4.MainWindow.UserControlEx.ClientEx
{
    //绑定用属性
    partial class uClient_viewmodel : NotificationObject
    {
        private uClient_model model = new();

        public bool L1
        {
            get => model.L1;
            set
            {
                if (model.L1 == value)
                    return;
                model.L1 = value;
                NotifyPropertyChanged();
            }
        }

        public bool L2
        {
            get => model.L2;
            set
            {
                if (model.L2 == value)
                    return;
                model.L2 = value;
                NotifyPropertyChanged();
            }
        }

        public bool R1
        {
            get => model.R1;
            set
            {
                if (model.R1 == value)
                    return;
                model.R1 = value;
                NotifyPropertyChanged();
            }
        }

        public bool R2
        {
            get => model.R2;
            set
            {
                if (model.R2 == value)
                    return;
                model.R2 = value;
                NotifyPropertyChanged();
            }
        }

        public bool L3
        {
            get => model.L3;
            set
            {
                if (model.L3 == value)
                    return;
                model.L3 = value;
                NotifyPropertyChanged();
            }
        }

        public bool R3
        {
            get => model.R3;
            set
            {
                if (model.R3 == value)
                    return;
                model.R3 = value;
                NotifyPropertyChanged();
            }
        }

        public bool Share
        {
            get => model.Share;
            set
            {
                if (model.Share == value)
                    return;
                model.Share = value;
                NotifyPropertyChanged();
            }
        }

        public bool Options
        {
            get => model.Options;
            set
            {
                if (model.Options == value)
                    return;
                model.Options = value;
                NotifyPropertyChanged();
            }
        }

        public bool Circle
        {
            get => model.Circle;
            set
            {
                if (model.Circle == value)
                    return;
                model.Circle = value;
                NotifyPropertyChanged();
            }
        }

        public bool Cross
        {
            get => model.Cross;
            set
            {
                if (model.Cross == value)
                    return;
                model.Cross = value;
                NotifyPropertyChanged();
            }
        }

        public bool Square
        {
            get => model.Square;
            set
            {
                if (model.Square == value)
                    return;
                model.Square = value;
                NotifyPropertyChanged();
            }
        }

        public bool Triangle
        {
            get => model.Triangle;
            set
            {
                if (model.Triangle == value)
                    return;
                model.Triangle = value;
                NotifyPropertyChanged();
            }
        }

        public bool DpadDown
        {
            get => model.DpadDown;
            set
            {
                if (model.DpadDown == value)
                    return;
                model.DpadDown = value;
                NotifyPropertyChanged();
            }
        }

        public bool DpadUp
        {
            get => model.DpadUp;
            set
            {
                if (model.DpadUp == value)
                    return;
                model.DpadUp = value;
                NotifyPropertyChanged();
            }
        }

        public bool DpadRight
        {
            get => model.DpadRight;
            set
            {
                if (model.DpadRight == value)
                    return;
                model.DpadRight = value;
                NotifyPropertyChanged();
            }
        }

        public bool DpadLeft
        {
            get => model.DpadLeft;
            set
            {
                if (model.DpadLeft == value)
                    return;
                model.DpadLeft = value;
                NotifyPropertyChanged();
            }
        }

        public Vector LX
        {
            get => model.LX;
            set
            {
                if (model.LX == value)
                    return;
                model.LX = value;
                NotifyPropertyChanged();
            }
        }

        public Vector RX
        {
            get => model.RX;
            set
            {
                if (model.RX == value)
                    return;
                model.RX = value;
                NotifyPropertyChanged();
            }
        }

        public bool PS
        {
            get => model.PS;
            set
            {
                if (model.PS == value)
                    return;
                model.PS = value;
                NotifyPropertyChanged();
            }
        }

        public bool Touchpad
        {
            get => model.Touchpad;
            set
            {
                if (model.Touchpad == value)
                    return;
                model.Touchpad = value;
                NotifyPropertyChanged();
            }
        }

        public bool Base
        {
            get => model.Base;
            set
            {
                if (model.Base == value)
                    return;
                model.Base = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<UIElement> DS4LayoutItems
        {
            get => model.DS4LayoutItems;
            set
            {
                if (model.DS4LayoutItems == value)
                    return;
                model.DS4LayoutItems = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<bool> DS4LayoutItemsOutline
        {
            get => model.BorderVisibility;
            set
            {
                if (model.BorderVisibility == value)
                    return;
                model.BorderVisibility = value;
                NotifyPropertyChanged();
            }
        }
    }

    //构造 主循环
    partial class uClient_viewmodel
    {
        public uClient_viewmodel()
        {
            Init();
            CallbackInit();
            MainLoop();
        }

        private void Init()
        {
            this.DS4LayoutItems = new()
            {
                CreateItem(nameof(Base), 806, 599, new(2,2,0,0)),
                CreateItem(nameof(L1), 104, 25, new(108,94,0,0)),
                CreateItem(nameof(L2), 99, 91, new(111,2,0,0)),
                CreateItem(nameof(R1), 104, 25, new(598,94,0,0)),
                CreateItem(nameof(R2), 99, 91, new(600,2,0,0)),
                CreateItem(nameof(Share), 27, 44, new(229,145,0,0)),
                CreateItem(nameof(Options), 27, 44, new(554,145,0,0)),
                CreateItem(nameof(Circle), 55, 55, new(684,219,0,0)),
                CreateItem(nameof(Cross), 55, 55, new(627,276,0,0)),
                CreateItem(nameof(Square), 55, 55, new(570,219,0,0)),
                CreateItem(nameof(Triangle), 55, 55, new(627,162,0,0)),
                CreateItem(nameof(DpadUp), 37, 52, new(137,183,0,0)),
                CreateItem(nameof(DpadDown), 37, 52, new(137,257,0,0)),
                CreateItem(nameof(DpadLeft), 52, 37, new(93,227,0,0)),
                CreateItem(nameof(DpadRight), 52, 37, new(167,227,0,0)),
                CreateItem(nameof(LX), 94, 94, new(230,311,0,0)),//LX
                CreateItem(nameof(RX), 94, 94, new(486,311,0,0)),//RX
                CreateItem(nameof(L3), 94, 94, new(230,311,0,0)),
                CreateItem(nameof(R3), 94, 94, new(486,311,0,0)),
                CreateItem(nameof(PS), 42, 42, new(384,343,0,0)),
                CreateItem(nameof(Touchpad), 262, 151, new(274,124,0,0)),
            };

            this.DS4LayoutItemsOutline = new(Enumerable.Range(0, OutLineIndexInfo.Values.Max() + 1).Select(idx => false));
        }

        private void CallbackInit()
        {
            Mediator.Instance.Register(KeyMapperMessageType.Instance.GetCurrentKeyMapperMouseEnterItemModel, (para) =>
            {
                if (para is MappingInfoPacket<Key> model)
                {
                    var key = model.BtnName;
                    if (model.BtnName == "L2Btn") { key = "L2"; }
                    if (model.BtnName == "R2Btn") { key = "R2"; }

                    if (OutLineIndexInfo.TryGetValue(key, out var value))
                    {
                        DS4LayoutItemsOutline[value] = true;
                    }

                    //Debug.WriteLine($"{model.BtnName} <- {model.BtnMapping.GetFirstKey}");
                }
                else
                {
                    DS4LayoutItemsOutline = new(Enumerable.Range(0, OutLineIndexInfo.Values.Max() + 1).Select(idx => false));
                }
            });
        }

        private void MainLoop()
        {
            Task.Run(async () =>
            {
                try
                {
                    while (true)
                    {
                        RealKeyboard.Instance.UpdateForKey(UpdateUI, UpdateMappingInfo());
                        await Task.Delay(2);
                    }
                }
                catch (Exception ex) { MessageBox.Show($"error: {ex.Message}"); }
            });
        }
    }

    //一般属性
    partial class uClient_viewmodel
    {
        Dictionary<string, int> OutLineIndexInfo = new Dictionary<string, int>()
        {
            { "L1", 0 },
            { "L2", 1 },
            { "R1", 2 },
            { "R2", 3 },
            { "L3", 4 },
            { "R3", 5 },
            { "Share", 6 },
            { "Options", 7 },
            { "Circle", 8 },
            { "Cross", 9 },
            { "Square", 10 },
            { "Triangle", 11 },
            { "DpadDown", 12 },
            { "DpadUp", 13 },
            { "DpadRight", 14 },
            { "DpadLeft", 15 },
            { "LX", 16 },
            { "RX", 17 },
            { "PS", 18 },
            { "Touchpad", 19 },

            { "LS_Up", 16 },
            { "LS_Down", 16 },
            { "LS_Left", 16 },
            { "LS_Right", 16 },
            { "RS_Up", 17 },
            { "RS_Down", 17 },
            { "RS_Left", 17 },
            { "RS_Right", 17 },
        };
    }

    //方法
    partial class uClient_viewmodel
    {
        //DS4LayoutItems
        private UIElement CreateItem(string prop, double w, double h, Thickness mg)
        {
            string? AssemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

            var grid = new Grid();
            {
                var imgPath = $"pack://application:,,,/{AssemblyName};component/MainWindow/Resources/ds4_{prop}.png";
                var img = new Image();
                {
                    img.HorizontalAlignment = HorizontalAlignment.Left;
                    img.VerticalAlignment = VerticalAlignment.Top;
                    img.Margin = mg;
                    img.Width = w;
                    img.Height = h;
                    img.Source = new BitmapImage(new Uri(imgPath, UriKind.Absolute));
                    img.Stretch = Stretch.Uniform;

                    if (prop != "Base" && prop != "LX" && prop != "RX")
                    {
                        img.SetBinding(Image.OpacityProperty, new Binding(prop)
                        {
                            Source = this,
                            Converter = new uClient_converter_bool2opacity(),
                            Mode = BindingMode.OneWay
                        });
                    }
                    if (prop == "LX" || prop == "RX")
                    {
                        img.SetBinding(Image.MarginProperty, new Binding(prop)
                        {
                            Source = this,
                            Converter = new uClient_converter_vector2margin(),
                            ConverterParameter = new Vector(mg.Left, mg.Top),
                            Mode = BindingMode.OneWay
                        });
                        img.SetBinding(Image.EffectProperty, new Binding(prop)
                        {
                            Source = this,
                            Converter = new uClient_converter_vector2pivot(),
                            Mode = BindingMode.OneWay
                        });
                    }
                    if (prop == "L3" || prop == "R3")
                    {
                        img.SetBinding(Image.MarginProperty, new Binding(prop == "L3" ? "LX" : "RX")
                        {
                            Source = this,
                            Converter = new uClient_converter_vector2margin(),
                            ConverterParameter = new Vector(mg.Left, mg.Top),
                            Mode = BindingMode.OneWay
                        });
                    }
                }
             
                var border = new Border();
                {
                    bool pressed = false;
                    bool sitck_pressed = false;

                    Func<string, string> getKey = (input) => 
                    {
                        if (input == "L2") { input = "L2Btn"; }
                        if (input == "R2") { input = "R2Btn"; }
                        return input;
                    };
                    Action btnPress = () => 
                    {
                        if (pressed is false)
                        {
                            string key = getKey.Invoke(prop);
                            if (cKeyMapper_viewmodel.Instance.MappingInfoDic.TryGetValue(key, out var value))
                            {
                                RealKeyboard.Instance.HitTest[value] = true;
                                Debug.WriteLine($"{key} press");
                            }
                            pressed = true;
                        }
                    };
                    Action btnRelease = () => 
                    {
                        if (pressed)
                        {
                            string key = getKey.Invoke(prop);
                            if (cKeyMapper_viewmodel.Instance.MappingInfoDic.TryGetValue(key, out var value))
                            {
                                RealKeyboard.Instance.HitTest[value] = false;
                                Debug.WriteLine($"{key} release");
                            }
                            pressed = false;
                        }
                    };
                    Action<int, bool> stickPush = (idx, is_left) =>
                    {
                        if (sitck_pressed is false)
                        {
                            var list = new List<string>();
                            var prefix = is_left ? "LS" : "RS";
                       
                            switch (idx)
                            {
                                case 0:
                                    list.Add($"{prefix}_Down");
                                    break;
                                case 1:
                                    list.Add($"{prefix}_Left"); list.Add($"{prefix}_Down");
                                    break;
                                case 2:
                                    list.Add($"{prefix}_Left");
                                    break;
                                case 3:
                                    list.Add($"{prefix}_Left"); list.Add($"{prefix}_Up");
                                    break;
                                case 4:
                                    list.Add($"{prefix}_Up");
                                    break;
                                case 5:
                                    list.Add($"{prefix}_Up"); list.Add($"{prefix}_Right");
                                    break;
                                case 6:
                                    list.Add($"{prefix}_Right");
                                    break;
                                case 7:
                                    list.Add($"{prefix}_Right"); list.Add($"{prefix}_Down");
                                    break;
                            }
                            list.ForEach(item => 
                            {
                                RealKeyboard.Instance.HitTest[cKeyMapper_viewmodel.Instance.MappingInfoDic[item]] = true;
                            });

                            sitck_pressed = true;
                        }
                    };
                    Action stickRelease = () => 
                    {
                        if (sitck_pressed)
                        {
                            var list = cKeyMapper_viewmodel.Instance.MappingInfoDic.Where(kvp => kvp.Key.Contains("LS_") || kvp.Key.Contains("RS_"))
                                                                                   .Select(kvp => kvp.Value)
                                                                                   .ToList();
                            list.ForEach(item =>
                            {
                                RealKeyboard.Instance.HitTest[item] = false;
                            });

                            sitck_pressed = false;
                        }
                    };

                    if (prop != "Base")
                    {
                        var thickness = 2;
                        var padding = 5;
                        border.HorizontalAlignment = HorizontalAlignment.Left;
                        border.VerticalAlignment = VerticalAlignment.Top;
                        border.Margin = new(mg.Left - thickness - padding, mg.Top - thickness - padding, mg.Right, mg.Bottom);
                        border.Width = w + (thickness + padding) * 2;
                        border.Height = h + (thickness + padding) * 2;
                        border.CornerRadius = new(5);
                        border.BorderBrush = new SolidColorBrush(Colors.LawnGreen);
                        border.BorderThickness = new(thickness);
                        border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString($"#7372C0FF"));
                        border.Effect = new DropShadowEffect() { Color = Colors.DarkGray, BlurRadius = 8, ShadowDepth = 0 };

                        border.SetBinding(Border.OpacityProperty, new Binding($"{nameof(DS4LayoutItemsOutline)}[{OutLineIndexInfo[prop]}]")
                        {
                            Source = this,
                            Converter = new uClient_converter_bool2opacity(),
                            Mode = BindingMode.OneWay
                        });
                        
                        //
                        if (prop == "L2" || prop == "R2")
                        {
                            border.Margin = new(mg.Left - thickness - padding, mg.Top - thickness - padding, mg.Right, mg.Bottom - 4);
                            border.Width = w + (thickness + padding) * 2;
                            border.Height = h + (thickness + padding) * 2 - 4;
                        }

                        //stick
                        if (prop == "L3" || prop == "R3" || prop.Contains("LX") || prop.Contains("RX"))
                        {
                            var padding_stick = 25;
                            border.Margin = new(mg.Left - thickness - padding_stick, mg.Top - thickness - padding_stick, mg.Right, mg.Bottom);
                            border.Width = w + (thickness + padding_stick) * 2;
                            border.Height = h + (thickness + padding_stick) * 2;
                            border.CornerRadius = new((Math.Max(border.Width, border.Height)) / 2);
                        }

                        border.MouseEnter += (s, e) =>
                        {
                            DS4LayoutItemsOutline[OutLineIndexInfo[prop]] = true;
                        };
                        border.MouseLeave += (s, e) =>
                        {
                            DS4LayoutItemsOutline[OutLineIndexInfo[prop]] = false;

                            btnRelease.Invoke();
                            stickRelease.Invoke();
                        };
                        border.MouseLeftButtonDown += (s, e) =>
                        {
                            if(prop == "L3" || prop == "R3")
                            {
                                var pt = e.GetPosition(border);
                                var idx = AngleCalculator.Instance.GetAngle(new(pt.X, pt.Y), new(border.ActualWidth / 2, border.ActualHeight / 2));

                                if(idx > -1)
                                {
                                    stickPush.Invoke(idx, prop == "L3"); return;
                                }
                            }

                            btnPress.Invoke();
                        };
                        border.MouseLeftButtonUp += (s, e) =>
                        {
                            btnRelease.Invoke();
                            stickRelease.Invoke();
                        };
                    }
                };

                grid.Children.Add(img);
                grid.Children.Add(border);
            }

            return grid;
        }

        //更新UI
        private void UpdateUI(Dictionary<string, ResultPacket> state)
        {
            Share = state["Share"].ButtonValue;
            Options = state["Options"].ButtonValue;
            L3 = state["ThumbLeft"].ButtonValue;
            R3 = state["ThumbRight"].ButtonValue;

            L1 = state["ShoulderLeft"].ButtonValue;
            R1 = state["ShoulderRight"].ButtonValue;
            L2 = state["TriggerLeft"].ButtonValue;
            R2 = state["TriggerRight"].ButtonValue;

            Triangle = state["Triangle"].ButtonValue;
            Circle = state["Circle"].ButtonValue;
            Cross = state["Cross"].ButtonValue;
            Square = state["Square"].ButtonValue;

            PS = state["PS"].ButtonValue;
            Touchpad = state["Touchpad"].ButtonValue;

            DpadUp = state["North"].ButtonValue;
            DpadDown = state["South"].ButtonValue;
            DpadLeft = state["West"].ButtonValue;
            DpadRight = state["East"].ButtonValue;

            LX = state["LX"].StickValue;
            RX = state["RX"].StickValue;
        }

        //更新映射信息
        private Dictionary<string, Key>? UpdateMappingInfo()
        {
            if (CounterAccessor.Instance["egvGXM2cy^r2epVK"] is CounterAccessor.InternalCounter counter)
            {
                if (counter.GetCount() == 0)
                {
                    return null;
                }
                else
                {
                    counter.Decrement();
                    return cKeyMapper_viewmodel.Instance.MappingInfoDic;
                }
            }

            return null;
        }
    }
}
