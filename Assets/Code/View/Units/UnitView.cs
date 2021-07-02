using System;
using Code.InterfaceAdapters.Units;
using UniRx;
using UnityEngine;

namespace Code.View.Units
{
    public class UnitView : ViewBase
    {
        private UnitViewModel _model;
        private Transform _myTransform;

        private void Awake()
        {
            _myTransform = transform;
        }

        public void SetModel(UnitViewModel model)
        {
            _model = model;
            _model
                .XPosition
                .Subscribe(UpdatePosition)
                .AddTo(_disposables);
        }

        private void UpdatePosition(float xPosition)
        {
            var oldPosition = _myTransform.position;
            _myTransform.position = new Vector3(xPosition, oldPosition.y, oldPosition.z);
        }
    }
}