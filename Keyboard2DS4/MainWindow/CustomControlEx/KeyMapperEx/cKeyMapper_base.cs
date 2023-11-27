using Keyboard2DS4.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keyboard2DS4.MainWindow.CustomControlEx.KeyMapperEx
{
    //飞消息用
    public class KeyMapperMessageType
    {
        private static readonly Lazy<KeyMapperMessageType> lazyObject = new(() => new KeyMapperMessageType());
        public static KeyMapperMessageType Instance => lazyObject.Value;

        public string GetCurrentKeyMapperMouseEnterItemModel { get; init; }

        public KeyMapperMessageType()
        {
            GetCurrentKeyMapperMouseEnterItemModel = nameof(GetCurrentKeyMapperMouseEnterItemModel) + GenerateRandomString(16);
        }

        private Random random = new Random();
        private string GenerateRandomString(int length)
        {
            const string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(chars.Length);
                stringBuilder.Append(chars[index]);
            }

            return stringBuilder.ToString();
        }
    }

    //
    public enum AutoCycle
    {
        On,
        Off
    }
    public class MappingInfo<T> : NotificationObject
    {
        public Func<bool> Condition { get; set; }
        public T[] Keys { get; set; }

        //循环选项
        private List<string> _CycleEnumList = new();
        public List<string> CycleEnumList
        {
            get { return _CycleEnumList; }
            set { _CycleEnumList = value; NotifyPropertyChanged(); }
        }
        private bool _Cycle;
        public bool Cycle
        {
            get => _Cycle;
            set { _Cycle = value; NotifyPropertyChanged(); }
        }

        //第一个Key
        private List<T> _KeyEnumList0 = new();//List<KeyboardKeys>
        public List<T> KeyEnumList0
        {
            get { return _KeyEnumList0; }
            set { _KeyEnumList0 = value; NotifyPropertyChanged(); }
        }
        private int _SelectedKey0 = 0;
        public int SelectedKey0
        {
            get { return _SelectedKey0; }
            set { _SelectedKey0 = value; NotifyPropertyChanged(); }
        }
        //第二个Key
        private List<T> _KeyEnumList1 = new();
        public List<T> KeyEnumList1
        {
            get { return _KeyEnumList1; }
            set { _KeyEnumList1 = value; NotifyPropertyChanged(); }
        }
        private int _SelectedKey1 = 0;
        public int SelectedKey1
        {
            get { return _SelectedKey1; }
            set { _SelectedKey1 = value; NotifyPropertyChanged(); }
        }

        //Key汇总
        public T[] GetKeys
        {
            get
            {
                if (SelectedKey0 > 0 && SelectedKey1 > 0)
                {
                    return new T[] { KeyEnumList0[SelectedKey0], KeyEnumList1[SelectedKey1] };
                }
                else if (SelectedKey0 > 0)
                {
                    return new T[] { KeyEnumList0[SelectedKey0] };
                }
                else if (SelectedKey1 > 0)
                {
                    return new T[] { KeyEnumList1[SelectedKey1] };
                }
                else
                {
                    return new T[0];
                }
            }
        }
        //取第一个Key
        public T GetFirstKey => KeyEnumList0[SelectedKey0];

        public MappingInfo(Func<bool> condition, T[] keys, AutoCycle? autocycle = null)
        {
            Condition = condition;
            Keys = keys;
            Cycle = autocycle switch
            {
                AutoCycle.On => true,
                AutoCycle.Off => false,
                null => false,
                _ => false
            };


            CycleEnumList = Enum.GetValues(typeof(AutoCycle))
                                .Cast<AutoCycle>()
                                .Select(e => e.ToString())
                                .ToList();

            KeyEnumList0 = Enum.GetValues(typeof(T)).Cast<T>().ToList();
            KeyEnumList1 = Enum.GetValues(typeof(T)).Cast<T>().ToList();

            if (Keys.Length > 0) { SelectedKey0 = KeyEnumList0.IndexOf((Keys[0])); }
            if (Keys.Length > 1) { SelectedKey1 = KeyEnumList1.IndexOf((Keys[1])); }
        }
    }

    //绑定到UI用
    public class MappingInfoPacket<T>
    {
        //按键别名
        public string DisplayName { get; set; }

        //按键原名
        public string BtnName { get; set; }

        //映射信息
        public MappingInfo<T> BtnMapping { get; set; }

        //注释
        public string Comment { get; set; }
    }
}
