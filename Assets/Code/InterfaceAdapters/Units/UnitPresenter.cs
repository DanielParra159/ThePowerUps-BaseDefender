namespace Code.InterfaceAdapters.Units
{
    public class UnitPresenter
    {
        private readonly UnitViewModel _model;

        public UnitPresenter(UnitViewModel model)
        {
            _model = model;
        }

        public void OnUnitMoved(float xPosition)
        {
            _model.XPosition.Value = xPosition;
        }
    }
}