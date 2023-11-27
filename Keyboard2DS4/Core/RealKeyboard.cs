using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Linq;
using Key = SharpDX.DirectInput.Key;

namespace Keyboard2DS4.Core
{
    partial class RealKeyboard
    {
        private static readonly Lazy<RealKeyboard> lazyObject = new(() => new RealKeyboard());
        public static RealKeyboard Instance => lazyObject.Value;
    }

    partial class RealKeyboard
    {
        Dictionary<Key, bool> mapping = new();

        private RealKeyboard()
        {
            foreach (var item in Enum.GetValues(typeof(Key)).Cast<Key>().ToList())
            {
                mapping.TryAdd(item, false);
            }

            GetKeyboard();
        }
    }

    partial class RealKeyboard
    {
        // キーボード取得用変数
        private Keyboard keyboard;
        private KeyboardState currentState;

        private void GetKeyboard()
        {
            // 入力周りの初期化
            DirectInput dinput = new DirectInput();

            if (dinput is not null)
            {
                // キーボード入力周りの初期化
                keyboard = new Keyboard(dinput);
                if (keyboard != null)
                {
                    // バッファサイズを指定
                    keyboard.Properties.BufferSize = 128;
                }
            }
        }

        // キー入力処理
        public void UpdateForKey(Action<Dictionary<string, ResultPacket>> callback, Dictionary<string, Key>? mpInfo)
        {
            // 初期化が出来ていない場合、処理終了
            if (keyboard == null) { return; }

            // キャプチャするデバイスを取得
            keyboard.Acquire();
            keyboard.Poll();
            currentState = keyboard.GetCurrentState();

            foreach (var key in mapping.Keys)
            {
                mapping[key] = currentState.IsPressed(key);
            }

            callback.Invoke(VirtualDS4.Instance.Update(mapping, mpInfo));
        }
    }
}
