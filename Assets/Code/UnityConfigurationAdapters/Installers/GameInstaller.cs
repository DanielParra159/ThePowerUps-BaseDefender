using System;
using System.Collections.Generic;
using SystemUtilities;
using ApplicationLayer.DataAccess;
using ApplicationLayer.Services.Instantiators;
using Code.InterfaceAdapters.Units;
using Domain.DataAccess;
using Domain.Services.EventDispatcher;
using Domain.UseCases.Gameplay;
using Domain.UseCases.Gameplay.MoveUnits;
using Domain.UseCases.Meta.Loading;
using UniRx;
using UnityEngine;

namespace Code.UnityConfigurationAdapters.Installers
{
    public class GameInstaller : MonoBehaviour
    {
        [SerializeField] private UnitsConfiguration _unitsConfiguration;

        private SpawnUnitUseCase _spawnUnitUseCase;
        private List<IDisposable> _disposables;
        private MoveUnitsUseCase _moveUnitsUseCase;

        public void Awake()
        {
            _disposables = new List<IDisposable>();

            var unitsDataAccess = ServiceLocator.Instance.GetService<UnitsDataAccess>();
            var eventDispatcherService = ServiceLocator.Instance.GetService<EventDispatcherService>();
            var unitsConfiguration = Instantiate(_unitsConfiguration);
            var unitsFactory = new UnitsFactory(unitsConfiguration);
            var unitPresentersContainer = new UnitPresentersContainer(eventDispatcherService)
                .AddTo(_disposables);
            var unitInstantiatorGateway = new UnitInstantiator(unitsFactory, unitPresentersContainer);
            var gameplayUnitsDataAccess = new GameplayUnitsRepository(unitInstantiatorGateway);
            _spawnUnitUseCase = new SpawnUnitUseCase(unitsDataAccess, gameplayUnitsDataAccess);
            _moveUnitsUseCase = new MoveUnitsUseCase(gameplayUnitsDataAccess, eventDispatcherService);

            // TODO: extract to init game
            unitsFactory.PreLoad();

            var loadingHide = ServiceLocator.Instance.GetService<LoadingHide>();
            loadingHide.Hide();
        }

        private void OnDestroy()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }

            _disposables.Clear();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _spawnUnitUseCase.Spawn("Unit001");
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                _spawnUnitUseCase.Spawn("Unit002");
            }

            _moveUnitsUseCase.Move(Time.deltaTime);
        }
    }
}