using UniRx;

namespace Code.InterfaceAdapters.Units
{
    public class UnitViewModel
    {
        private readonly string _id;
        public readonly ReactiveProperty<float> XPosition;

        public UnitViewModel(string id)
        {
            _id = id;
            XPosition = new FloatReactiveProperty();
        }
    }
}