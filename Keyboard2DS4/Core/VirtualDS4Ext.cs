using Keyboard2DS4.Core.DS4OutDevices;
using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.DualShock4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Key = SharpDX.DirectInput.Key;

namespace Keyboard2DS4.Core
{
    partial class VirtualDS4Ext
    {
        private static readonly Lazy<VirtualDS4Ext> lazyObject = new(() => new VirtualDS4Ext());
        public static VirtualDS4Ext Instance => lazyObject.Value;
    }

    partial class VirtualDS4Ext
    {
        static Func<ResultPacket> CreateButtonValue = () => { return new ResultPacket() { ButtonValue = false }; };
        static Func<ResultPacket> CreateStickValue = () => { return new ResultPacket() { StickValue = new(128, 128) }; };

        //
        readonly Dictionary<string, ResultPacket> result = new()
        {
            { nameof(DualShock4Button.Share), CreateButtonValue() },      //Share
            { nameof(DualShock4Button.Options), CreateButtonValue() },    //Options
            { nameof(DualShock4Button.ThumbLeft), CreateButtonValue() },  //L3
            { nameof(DualShock4Button.ThumbRight), CreateButtonValue() }, //R3

            { nameof(DualShock4Button.ShoulderLeft), CreateButtonValue() },  //L1
            { nameof(DualShock4Button.ShoulderRight), CreateButtonValue() }, //R1
            { nameof(DualShock4Button.TriggerLeft), CreateButtonValue() },   //L2Btn
            { nameof(DualShock4Button.TriggerRight), CreateButtonValue() },  //R2Btn

            { nameof(DualShock4Button.Triangle), CreateButtonValue() }, //Triangle
            { nameof(DualShock4Button.Circle), CreateButtonValue() },   //Circle
            { nameof(DualShock4Button.Cross), CreateButtonValue() },    //Cross
            { nameof(DualShock4Button.Square), CreateButtonValue() },   //Square

            { nameof(DualShock4SpecialButton.Ps).ToUpper(), CreateButtonValue() }, //Ps
            { nameof(DualShock4SpecialButton.Touchpad), CreateButtonValue() },     //Touchpad

            { nameof(DualShock4DPadDirection.North), CreateButtonValue() }, //↑
            { nameof(DualShock4DPadDirection.South), CreateButtonValue() }, //↓
            { nameof(DualShock4DPadDirection.West), CreateButtonValue() },  //←
            { nameof(DualShock4DPadDirection.East), CreateButtonValue() },  //→

            { "LX", CreateStickValue() },
            { "RX", CreateStickValue() },
        };

        //Button Mapping
        DualShock4Packet<DualShock4Button> mappingButton = new()
        {
            { DualShock4Button.Share, Key.Unknown },      //Share
            { DualShock4Button.Options, Key.Unknown },    //Options
            { DualShock4Button.ThumbLeft, Key.Unknown },  //L3
            { DualShock4Button.ThumbRight, Key.Unknown }, //R3

            { DualShock4Button.ShoulderLeft, Key.Unknown },  //L1
            { DualShock4Button.ShoulderRight, Key.Unknown }, //R1
            { DualShock4Button.TriggerLeft, Key.Unknown },   //L2Btn
            { DualShock4Button.TriggerRight, Key.Unknown },  //R2Btn

            { DualShock4Button.Triangle, Key.Unknown }, //Triangle
            { DualShock4Button.Circle, Key.Unknown },   //Circle
            { DualShock4Button.Cross, Key.Unknown },    //Cross
            { DualShock4Button.Square, Key.Unknown },   //Square
        };

        //SpecialButton Mapping
        DualShock4Packet<DualShock4SpecialButton> mappingSpButton = new()
        {
            { DualShock4SpecialButton.Ps, Key.Unknown },      //Ps
            { DualShock4SpecialButton.Touchpad, Key.Unknown },//Touchpad
        };

        //DPadDirection Mapping
        DualShock4Packet<DualShock4DPadDirection> mappingDPadDirection = new()
        {
            {DualShock4DPadDirection.North, Key.Unknown }, //↑
            {DualShock4DPadDirection.South, Key.Unknown }, //↓
            {DualShock4DPadDirection.West, Key.Unknown },  //← 
            {DualShock4DPadDirection.East, Key.Unknown },  //→
        };


        //
        Dictionary<string, Vector> StickDirection = new()
        {
            { "↑", new(128,0) },
            { "↓", new(128,255) },
            { "←", new(0,128) },
            { "→", new(255,128) },

            { "↑→", new(128+90,128-90) },
            { "↑←", new(128-90,128-90) },
            { "→↓", new(128+90,128+90) },
            { "↓←", new(128-90,128+90) },
        };
        //LeftStick Mapping
        DualShock4Packet<Vector> mappingLeftStick = new()
        {
            { new(128,0), Key.Unknown },   //↑
            { new(128,255), Key.Unknown }, //↓
            { new(0,128), Key.Unknown },   //←
            { new(255,128), Key.Unknown }, //→
        };
        //RightStick Mapping
        DualShock4Packet<Vector> mappingRightStick = new()
        {
            { new(128,0), Key.Unknown },   //↑
            { new(128,255), Key.Unknown }, //↓
            { new(0,128), Key.Unknown },   //←
            { new(255,128), Key.Unknown }, //→
        };
    }

    //构造
    partial class VirtualDS4Ext
    {
        IDualShock4Controller ds4 = new ViGEmClient().CreateDualShock4Controller();

        private VirtualDS4Ext()
        {
            ds4.AutoSubmitReport = false;
            ds4.Connect();
        }
    }

    //更新DS4报文
    partial class VirtualDS4Ext
    {
        public struct TouchInfo
        {
            public byte TouchPacketCounter;

            public byte Touch0_RawTrackingNum;
            public short Touch0_x;
            public short Touch0_y;

            //public byte Touch1_RawTrackingNum; 
            //public short Touch1_x;
            //public short Touch1_y;
        }

        private byte frameCounter; // 0, 1, 2...62, 63, 0....
        private byte FrameCounter
        {
            get
            {
                byte currentValue = frameCounter;
                frameCounter = (byte)((frameCounter + 1) & 0x3F); // 自增并且限制在 0~63 范围
                return currentValue;
            }
        }

        private byte[] rawOutReportEx = new byte[63];
        private DS4_REPORT_EX outDS4Report;

        private enum StickSide
        {
            Left, Right
        }

        public unsafe Dictionary<string, ResultPacket> Update(Dictionary<Key, bool> dic_key2bool, Dictionary<string, Key>? dic_str2key, TouchInfo? touchInfo = null)
        {
            //
            this.UpdateMappingInfo(dic_str2key);

            //
            var tempButtons = (ushort)0;
            var tempSpecial = (ushort)0;
            var tempDPad = DualShock4DPadDirection.None;

            unchecked
            {
                //常规按钮
                SetButton(mappingButton, dic_key2bool, ref tempButtons);

                //特殊按钮
                SetSpButton(mappingSpButton, dic_key2bool, ref tempSpecial);

                //十字键
                SetDpad(mappingDPadDirection, dic_key2bool, ref tempDPad);

                //
                outDS4Report.wButtons = tempButtons;
                outDS4Report.bSpecial = (byte)(tempSpecial | (this.FrameCounter << 2));
                outDS4Report.wButtons |= tempDPad.Value;
            }

            // L2 R2
            outDS4Report.bTriggerL = (byte)(dic_key2bool[mappingButton[DualShock4Button.TriggerLeft]] ? 255 : 0);
            outDS4Report.bTriggerR = (byte)(dic_key2bool[mappingButton[DualShock4Button.TriggerRight]] ? 255 : 0);

            // Stick
            SetStick(mappingLeftStick, dic_key2bool, out outDS4Report.bThumbLX, out outDS4Report.bThumbLY, StickSide.Left);
            SetStick(mappingRightStick, dic_key2bool, out outDS4Report.bThumbRX, out outDS4Report.bThumbRY, StickSide.Right);

            // Touch
            if (touchInfo is TouchInfo touch)
            {
                // touch packet
                outDS4Report.bTouchPacketsN = 1;
                outDS4Report.sCurrentTouch.bPacketCounter = touch.TouchPacketCounter;
                outDS4Report.sCurrentTouch.bIsUpTrackingNum1 = touch.Touch0_RawTrackingNum;
                outDS4Report.sCurrentTouch.bTouchData1[0] = (byte)(touch.Touch0_x & 0xFF);
                outDS4Report.sCurrentTouch.bTouchData1[1] = (byte)((touch.Touch0_x >> 8) & 0x0F | (touch.Touch0_y << 4) & 0xF0);
                outDS4Report.sCurrentTouch.bTouchData1[2] = (byte)(touch.Touch0_y >> 4);

                //outDS4Report.sCurrentTouch.bIsUpTrackingNum2 = touch.Touch1_RawTrackingNum;//state.TrackPadTouch1.RawTrackingNum;
                //outDS4Report.sCurrentTouch.bTouchData2[0] = (byte)(touch.Touch1_x & 0xFF);
                //outDS4Report.sCurrentTouch.bTouchData2[1] = (byte)((touch.Touch1_x >> 8) & 0x0F | (touch.Touch1_y << 4) & 0xF0);
                //outDS4Report.sCurrentTouch.bTouchData2[2] = (byte)(touch.Touch1_y >> 4);
            }

            DS4OutDeviceExtras.CopyBytes(ref outDS4Report, rawOutReportEx);

            ds4.SubmitRawReport(rawOutReportEx);

            return result;
        }

        private bool Contains<T>(List<T> list, params T[] values)
        {
            return values.All(value => list.Contains(value));
        }

        private void SetButton(DualShock4Packet<DualShock4Button> dic0, Dictionary<Key, bool> dic1, ref ushort temp)
        {
            foreach (var kvp in dic0)
            {
                var key = kvp.Key;
                var value = kvp.Value;
                if (dic1[value]) { temp |= key.Value; }

                result[$"{key}"].ButtonValue = dic1[value];
            }
        }
        private void SetSpButton(DualShock4Packet<DualShock4SpecialButton> dic0, Dictionary<Key, bool> dic1, ref ushort temp)
        {
            foreach (var kvp in dic0)
            {
                var key = kvp.Key;
                var value = kvp.Value;
                if (dic1[value]) { temp |= key.Value; }

                result[$"{key}"].ButtonValue = dic1[value];
            }
        }
        private void SetDpad(DualShock4Packet<DualShock4DPadDirection> dic0, Dictionary<Key, bool> dic1, ref DualShock4DPadDirection temp)
        {
            List<DualShock4DPadDirection> DPadList = new();
            foreach (var kvp in dic0)
            {
                var key = kvp.Key;
                var value = kvp.Value;
                if (dic1[value]) { DPadList.Add(key); }

                result[$"{key}"].ButtonValue = dic1[value];
            }

            if (Contains(DPadList, DualShock4DPadDirection.North, DualShock4DPadDirection.East)) temp = DualShock4DPadDirection.Northeast;
            else if (Contains(DPadList, DualShock4DPadDirection.North, DualShock4DPadDirection.West)) temp = DualShock4DPadDirection.Northwest;
            else if (Contains(DPadList, DualShock4DPadDirection.North)) temp = DualShock4DPadDirection.North;
            else if (Contains(DPadList, DualShock4DPadDirection.South, DualShock4DPadDirection.East)) temp = DualShock4DPadDirection.Southeast;
            else if (Contains(DPadList, DualShock4DPadDirection.East)) temp = DualShock4DPadDirection.East;
            else if (Contains(DPadList, DualShock4DPadDirection.South, DualShock4DPadDirection.West)) temp = DualShock4DPadDirection.Southwest;
            else if (Contains(DPadList, DualShock4DPadDirection.South)) temp = DualShock4DPadDirection.South;
            else if (Contains(DPadList, DualShock4DPadDirection.West)) temp = DualShock4DPadDirection.West;
        }
        private void SetStick(DualShock4Packet<Vector> dic0, Dictionary<Key, bool> dic1, out byte x, out byte y, StickSide side)
        {
            List<Vector> list = new();

            foreach (var kvp in dic0)
            {
                var key = kvp.Key;
                var value = kvp.Value;
                if (dic1[value]) list.Add(key);
            }

            Vector vector = new(128, 128);

            if (Contains(list, StickDirection["↑"], StickDirection["→"])) vector = StickDirection["↑→"];
            else if (Contains(list, StickDirection["↑"], StickDirection["←"])) vector = StickDirection["↑←"];
            else if (Contains(list, StickDirection["↑"])) vector = StickDirection["↑"];
            else if (Contains(list, StickDirection["→"], StickDirection["↓"])) vector = StickDirection["→↓"];
            else if (Contains(list, StickDirection["→"])) vector = StickDirection["→"];
            else if (Contains(list, StickDirection["↓"], StickDirection["←"])) vector = StickDirection["↓←"];
            else if (Contains(list, StickDirection["↓"])) vector = StickDirection["↓"];
            else if (Contains(list, StickDirection["←"])) vector = StickDirection["←"];

            x = (byte)Math.Clamp(vector.X, 0x00, 0xff);
            y = (byte)Math.Clamp(vector.Y, 0x00, 0xff);

            result[side == StickSide.Left ? "LX" : "RX"].StickValue = new(x, y);
        }
    }

    //更新映射信息
    partial class VirtualDS4Ext
    {
        private void UpdateMappingInfo(Dictionary<string, Key>? mpInfo)
        {
            if (mpInfo is null) { return; }

            mappingButton[DualShock4Button.Share] = mpInfo["Share"];
            mappingButton[DualShock4Button.Options] = mpInfo["Options"];
            mappingButton[DualShock4Button.ThumbLeft] = mpInfo["L3"];
            mappingButton[DualShock4Button.ThumbRight] = mpInfo["R3"];

            mappingButton[DualShock4Button.ShoulderLeft] = mpInfo["L1"];
            mappingButton[DualShock4Button.ShoulderRight] = mpInfo["R1"];
            mappingButton[DualShock4Button.TriggerLeft] = mpInfo["L2Btn"];
            mappingButton[DualShock4Button.TriggerRight] = mpInfo["R2Btn"];

            mappingButton[DualShock4Button.Triangle] = mpInfo["Triangle"];
            mappingButton[DualShock4Button.Circle] = mpInfo["Circle"];
            mappingButton[DualShock4Button.Cross] = mpInfo["Cross"];
            mappingButton[DualShock4Button.Square] = mpInfo["Square"];

            mappingSpButton[DualShock4SpecialButton.Ps] = mpInfo["PS"];
            mappingSpButton[DualShock4SpecialButton.Touchpad] = mpInfo["Touchpad"];

            mappingDPadDirection[DualShock4DPadDirection.North] = mpInfo["DpadUp"];
            mappingDPadDirection[DualShock4DPadDirection.South] = mpInfo["DpadDown"];
            mappingDPadDirection[DualShock4DPadDirection.West] = mpInfo["DpadLeft"];
            mappingDPadDirection[DualShock4DPadDirection.East] = mpInfo["DpadRight"];

            mappingLeftStick[new(128, 0)] = mpInfo["LS_Up"];
            mappingLeftStick[new(128, 255)] = mpInfo["LS_Down"];
            mappingLeftStick[new(0, 128)] = mpInfo["LS_Left"];
            mappingLeftStick[new(255, 128)] = mpInfo["LS_Right"];

            mappingRightStick[new(128, 0)] = mpInfo["RS_Up"];
            mappingRightStick[new(128, 255)] = mpInfo["RS_Down"];
            mappingRightStick[new(0, 128)] = mpInfo["RS_Left"];
            mappingRightStick[new(255, 128)] = mpInfo["RS_Right"];
        }
    }
}
