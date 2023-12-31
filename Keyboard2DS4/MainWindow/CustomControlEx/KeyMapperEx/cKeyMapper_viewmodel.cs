﻿using Keyboard2DS4.Helper;
using Keyboard2DS4.Helper.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Key = SharpDX.DirectInput.Key;

namespace Keyboard2DS4.MainWindow.CustomControlEx.KeyMapperEx
{
    //单例
    public partial class cKeyMapper_viewmodel : NotificationObject
    {
        private static readonly Lazy<cKeyMapper_viewmodel> lazyObject = new(() => new cKeyMapper_viewmodel());
        public static cKeyMapper_viewmodel Instance => lazyObject.Value;
    }

    //对外
    public partial class cKeyMapper_viewmodel
    {
        public Dictionary<string, Key> MappingInfoDic => MappingInfoPacketList.ToDictionary(x => x.BtnName, x => x.BtnMapping.GetFirstKey);
    }

    //构造
    public partial class cKeyMapper_viewmodel
    {
        cKeyMapper_model model = new();

        public int UniformGridColumns
        {
            get { return model.UniformGridColumns; }
            set
            {
                model.UniformGridColumns = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<MappingInfoPacket<Key>> MappingInfoPacketList
        {
            get { return model.MappingInfoPacketList; }
            set
            {
                model.MappingInfoPacketList = value;
                NotifyPropertyChanged();
            }
        }

        public cKeyMapper_viewmodel()
        {
            //通知属性初始化
            this.UniformGridColumns = 2;
            this.MappingInfoPacketList = new();

            //按键原名
            List<string> BtnNameList = new()
            {
                "Share",
                "Options",
                "L3",
                "R3",

                "L1",
                "R1",
                "L2Btn",
                "R2Btn",

                "Triangle",
                "Cross",
                "Square",
                "Circle",

                "PS",
                "Touchpad",

                "DpadUp",
                "DpadDown",
                "DpadLeft",
                "DpadRight",

                "LS_Up",
                "LS_Down",
                "LS_Left",
                "LS_Right",

                "RS_Up",
                "RS_Down",
                "RS_Left",
                "RS_Right",
            };

            //缺省映射信息
            Dictionary<string, MappingInfo<Key>> BtnMappingCondition = new Dictionary<string, MappingInfo<Key>>
            {
                { "Share",   new (()=> false,   [Key.D1], null) },
                { "Options", new (()=> false,   [Key.D2], null) },
                { "L3",      new (()=> false,   [Key.D3], null) },
                { "R3",      new (()=> false,   [Key.D4], null) },

                { "L1",    new (()=> false,     [Key.D5], null) },
                { "R1",    new (()=> false,     [Key.D6], null) },
                { "L2Btn", new (()=> false,     [Key.D7], null) },
                { "R2Btn", new (()=> false,     [Key.D8], null) },

                { "Triangle", new (()=> false,  [Key.I], null) },
                { "Cross",    new (()=> false,  [Key.K], null) },
                { "Square",   new (()=> false,  [Key.J], null) },
                { "Circle",   new (()=> false,  [Key.L], null) },

                { "PS",       new (()=> false,  [Key.D9], null) },
                { "Touchpad", new (()=> false,  [Key.D0], null) },

                { "DpadUp",    new (()=> false, [Key.T], null) },
                { "DpadDown",  new (()=> false, [Key.G], null) },
                { "DpadLeft",  new (()=> false, [Key.F], null) },
                { "DpadRight", new (()=> false, [Key.H], null) },

                { "LS_Up",    new (()=> false,  [Key.W], null) },
                { "LS_Down",  new (()=> false,  [Key.S], null) },
                { "LS_Left",  new (()=> false,  [Key.A], null) },
                { "LS_Right", new (()=> false,  [Key.D], null) },

                { "RS_Up",    new (()=> false,  [Key.Up], null) },
                { "RS_Down",  new (()=> false,  [Key.Down], null) },
                { "RS_Left",  new (()=> false,  [Key.Left], null) },
                { "RS_Right", new (()=> false,  [Key.Right], null) }
            };

            //调序
            //读取至MappingInfoPacketList
            CalculateColumnsAndRows(BtnNameList).ForEach(x =>
            {
                this.MappingInfoPacketList.Add(new()
                {
                    DisplayName = x,
                    BtnName = x,
                    BtnMapping = BtnMappingCondition[x],
                    Comment = $"No Comment: {x}",
                });
            });
        }
    }
    //对内
    public partial class cKeyMapper_viewmodel
    {
        private List<string> CalculateColumnsAndRows(List<string> inputList)
        {
            int totalChildren = inputList.Count;
            int columns = UniformGridColumns;
            int rows = (int)Math.Ceiling((double)totalChildren / columns);

            //Task.Run(() => { MessageBox.Show($"Columns: {columns}, Rows: {rows}"); });
            int[] orderArray = Enumerable.Range(0, columns)
                                         .SelectMany(i => Enumerable.Range(0, rows), (i, j) => i + j * columns)
                                         .ToList().Take(totalChildren).ToArray();

            return inputList.ReArrange(orderArray);
        }
    }
}
