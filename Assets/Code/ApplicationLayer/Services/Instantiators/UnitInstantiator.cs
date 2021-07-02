using System.Threading.Tasks;
using ApplicationLayer.DataAccess;
using Code.InterfaceAdapters.Units;

namespace ApplicationLayer.Services.Instantiators
{
    public class UnitInstantiator : UnitInstantiatorGateway
    {
        private readonly UnitsFactory _unitsFactory;
        private readonly UnitPresentersContainer _unitPresentersContainer;

        public UnitInstantiator(UnitsFactory unitsFactory, UnitPresentersContainer unitPresentersContainer)
        {
            _unitsFactory = unitsFactory;
            _unitPresentersContainer = unitPresentersContainer;
        }
        public async Task<int> Instantiate(UnitConfiguration unitConfiguration)
        {
            var unitView = await _unitsFactory.Create(unitConfiguration);
            var unitViewModel = new UnitViewModel(unitConfiguration.Id);
            var unitPresenter = new UnitPresenter(unitViewModel);
           
            unitView.SetModel(unitViewModel);
            var instanceId = unitView.GetInstanceID();
            
            _unitPresentersContainer.AddNewPresenter(instanceId, unitPresenter);
            return instanceId;
        }
    }
}