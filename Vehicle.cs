using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace VehicleSpawnerServ
{
    public class Vehicle : BaseScript
    {
        private string model;
        private float posX;
        private float posY;
        private float posZ;
        private float heading;

        public Vehicle()
        {

        }

        public Vehicle(string model, float posX, float posY, float posZ, float heading)
        {
            this.model = model;
            this.posX = posX;
            this.posY = posY;
            this.posZ = posZ;
            this.heading = heading;
        }

        public string getModel()
        {
            return this.model;
        }

        public float getPosX()
        {
            return this.posX;
        }

        public float getPosY()
        {
            return this.posY;
        }

        public float getPosZ()
        {
            return this.posZ;
        }

        public float getHeading()
        {
            return this.heading;
        }
    }
}
