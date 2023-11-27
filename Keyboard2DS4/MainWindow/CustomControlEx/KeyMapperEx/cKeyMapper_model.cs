using System.Collections.ObjectModel;
using Key = SharpDX.DirectInput.Key;

namespace Keyboard2DS4.MainWindow.CustomControlEx.KeyMapperEx
{
    internal class cKeyMapper_model
    {
        public int UniformGridColumns { get; set; }
        public ObservableCollection<MappingInfoPacket<Key>> MappingInfoPacketList { get; set; }
    }
}
