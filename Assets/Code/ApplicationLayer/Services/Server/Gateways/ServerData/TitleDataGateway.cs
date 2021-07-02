using System;
using System.Collections.Generic;
using ApplicationLayer.Services.Server.Dtos.Server;
using Domain.Services.Serializer;

namespace ApplicationLayer.Services.Server.Gateways.ServerData
{
    public class TitleDataGateway : GatewayImpl
    {
        public TitleDataGateway(SerializerService serializerService,
                                    GetDataService getDataService,
                                    SetDataService setDataService)
            : base(serializerService, getDataService, setDataService)
        {
        }

        protected override void InitializeTypeToKey(out Dictionary<Type, string> typeToKey)
        {
            typeToKey = new Dictionary<Type, string>
                        {
                            {typeof(InitialUnitsDto), "InitialUnits"}
                        };
        }
    }
}
