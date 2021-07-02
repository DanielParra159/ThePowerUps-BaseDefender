using System.Collections.Generic;
using System.ComponentModel;
using SystemUtilities;
using ApplicationLayer.Services.Server.Dtos.User;
using ApplicationLayer.Services.Server.Gateways.ServerData;
using Domain.DataAccess;
using Domain.Entities;
// using SRDebugger;
using UnityEngine;

namespace Code.Cheats
{
    public class GlobalCheats
    {
        public static readonly GlobalCheats Instance = new GlobalCheats();

        [Category("User")]
        public void DeleteUserData()
        {
            //SRDebug.Instance.HideDebugPanel();

            // borrar inventario
            var unitsDataAccess = ServiceLocator.Instance.GetService<UnitsDataAccess>();
            var userUnits = unitsDataAccess.GetAllUserUnits();
            
            var userDataGateway = ServiceLocator.Instance.GetService<UserDataGateway>();
            userDataGateway.Set(new IsInitializedDto(false));
             userDataGateway.Save().WrapErrors();
            unitsDataAccess.RemoveUnitsToUser(new List<UserUnit>(userUnits)).WrapErrors();
            
            //SRDebug.Instance.ShowDebugPanel(DefaultTabs.Console);
            Debug.LogWarning("User removed, restart the game");
        }
    }
}