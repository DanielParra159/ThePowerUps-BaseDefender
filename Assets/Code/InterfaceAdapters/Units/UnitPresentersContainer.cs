using System;
using System.Collections.Generic;
using Domain.Services.EventDispatcher;
using Domain.UseCases.Gameplay.MoveUnits;

namespace Code.InterfaceAdapters.Units
{
    public class UnitPresentersContainer : IDisposable
    {
        private readonly EventDispatcherService _eventDispatcherService;
        private readonly Dictionary<int, UnitPresenter> _instanceIdToPresenter;

        public UnitPresentersContainer(EventDispatcherService eventDispatcherService)
        {
            _eventDispatcherService = eventDispatcherService;
            _instanceIdToPresenter = new Dictionary<int, UnitPresenter>();

            _eventDispatcherService.Subscribe<UnitsMoved>(OnUnitsMoved);
        }

        public void Dispose()
        {
            _eventDispatcherService.Unsubscribe<UnitsMoved>(OnUnitsMoved);
        }

        public void AddNewPresenter(int instanceId, UnitPresenter presenter)
        {
            _instanceIdToPresenter.Add(instanceId, presenter);
        }

        private void OnUnitsMoved(ISignal signal)
        {
            var data = (UnitsMoved) signal;
            foreach (var unitData in data.UnitsData)
            {
                _instanceIdToPresenter[unitData.InstanceId].OnUnitMoved(unitData.XPosition);
            }
        }
    }
}