using System.Collections.Generic;
using System.Linq;
using Code.SharedTypes.Units;
using Domain.DataAccess;

namespace Domain.UseCases.Meta.CreateUser
{
    public class InitializeUserUseCase : UserInitializer
    {
        private readonly UserDataAccess _userDataAccess;
        private readonly UnitsDataAccess _unitsDataAccess;

        public InitializeUserUseCase(UserDataAccess userDataAccess, UnitsDataAccess unitsDataAccess)
        {
            _userDataAccess = userDataAccess;
            _unitsDataAccess = unitsDataAccess;
        }

        public async void Init()
        {
            if (_userDataAccess.IsNewUser())
            {
                var unitsId = _unitsDataAccess.GetInitialUnitsId();
                var unitToAdds = new List<UnitToAdd>(unitsId
                    .Select(unitId =>
                    {
                        var unitState = new UnitState(1);
                        var unitToAdd = new UnitToAdd(unitId, UnitToAdd.InitialUnitsAnnotation, unitState);
                        return unitToAdd;
                    }));
                var userId = _userDataAccess.GetUserId();
                await _unitsDataAccess.AddUnitsToUser(unitToAdds);

                await _userDataAccess.CreateLocalUser();
            }
        }
    }
}