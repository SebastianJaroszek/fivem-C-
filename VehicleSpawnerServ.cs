using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace VehicleSpawnerServ
{
    public class VehicleSpawnerServ : BaseScript
    {

        private List<Vehicle> vehicleList;

        public VehicleSpawnerServ()
        {
            EventHandlers.Add("testEv", new Action<string>(TestEvent));
            EventHandlers.Add("saveveh", new Action<string, float, float, float, float>(SaveVehicle));
            EventHandlers.Add("loadveh", new Action<string>(LoadVehicle));
            sthTest();
            this.vehicleList = new List<Vehicle>();
        }

        private void LoadVehicle(string arg1)
        {
            Vehicle veh = vehicleList[0];
            TriggerClientEvent("returnVehicle", veh.getModel(), veh.getPosX(), veh.getPosY(), veh.getPosZ(), veh.getHeading());
        }

        private void SaveVehicle(string model, float posX, float posY, float posZ, float heading)
        {
            Vehicle vehicle = new Vehicle(model, posX, posY, posZ, heading);
            vehicleList.Add(vehicle);
        }

        private void TestEvent(string arg)
        {
            TriggerClientEvent("simpleEvent", "arg");
            Debug.WriteLine("event test...");
        }

        private void sthTest()
        {
            Debug.WriteLine("sthTest działa");
        }

    }
}
