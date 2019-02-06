using System;
using System.Collections.Generic;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace MyResourceNameClient
{
    public class VehicleSpawner : BaseScript
    {

        private Vehicle vehicle;

        public VehicleSpawner()
        {
            EventHandlers.Add("onClientResourceStart", new Action<string>(GiveVehicle));
            EventHandlers.Add("simpleEvent", new Action<string>(ServerCallback));
            EventHandlers.Add("returnVehicle", new Action<string, float, float, float, float>(ReturnVehicle));
        }

        private void ReturnVehicle(string model, float posX, float posY, float posZ, float heading)
        {
            Vector3 vector = new Vector3(posX, posY, posZ);
            World.CreateVehicle(model, vector, heading);
            TriggerEvent("chat:addMessage", new
            {
                color = new[] { 255, 0, 0 },
                args = new[] { "[server]", $"wczytano veh!" }
            });
        }

        private void ServerCallback(string arg)
        {
            TriggerEvent("chat:addMessage", new
            {
                color = new[] { 255, 0, 0 },
                args = new[] { "[return]", $"event jest wywołany z serverside" }
            });
        }

        private void GiveVehicle(string resourceName)
        {
           if (GetCurrentResourceName() != resourceName) return;

            RegisterCommand("loadveh", new Action<int, List<object>, string>((source, args, raw) =>
            {
                TriggerServerEvent("loadveh", "arg");
                TriggerEvent("chat:addMessage", new
                {
                    color = new[] { 255, 0, 0 },
                    args = new[] { "[client]", $"wczytywanie veh..." }
                });
            }), false);

            RegisterCommand("bring", new Action<int, List<object>, string>((source, args, raw) =>
            {
                TriggerServerEvent("testEv", "arg");
                TriggerEvent("chat:addMessage", new
                {
                    color = new[] { 255, 0, 0 },
                    args = new[] { "[client]", $"leci request z clientside" }
                });
            }), false);


           RegisterCommand("respveh", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                var model = "adder";
                if (args.Count > 0)
                {
                    model = args[0].ToString();
                }

                var hash = (uint) GetHashKey(model);
                if (!IsModelInCdimage(hash) || !IsModelAVehicle(hash))
                {
                    TriggerEvent("chat:addMessage", new
                    {
                        color = new[] { 255, 0, 0 },
                        args = new[] { "[CarSpawner]", $"nie zespawnujesz {model}, ponieważ taki pojazd nie istnieje!" }
                    });
                    return;
                }

                vehicle = await World.CreateVehicle(model, Game.PlayerPed.Position, Game.PlayerPed.Heading);

                Game.PlayerPed.SetIntoVehicle(vehicle, VehicleSeat.Driver);
                TriggerServerEvent("saveveh", model, Game.PlayerPed.Position.X, Game.PlayerPed.Position.Y, Game.PlayerPed.Position.Z, Game.PlayerPed.Heading);
                TriggerEvent("chat:addMessage", new
                {
                    color = new[] { 255, 0, 0 },
                    args = new[] { "[CarSpawner]", $"zapisano veh!" }
                });

            }), false);

            RegisterCommand("rp", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                var player = PlayerPedId();
                var vehicle = GetVehiclePedIsIn(player, false);
                SetVehicleBodyHealth(vehicle, 1000);
                SetVehicleEngineHealth(vehicle, 1000);
                SetVehicleDeformationFixed(vehicle);
                SetVehicleFixed(vehicle);
                TriggerEvent("chat:addMessage", new
                {
                    color = new[] { 255, 0, 0 },
                    args = new[] { "[CarSpawner]", $"pojazd naprawiony!" }
                });
            }), false);

            RegisterCommand("sk", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                SetPedAsCop(PlayerPedId(), true);
                RequestModel(1581098148);
                //SetPlayerModel(PlayerId(), 1074457665);
                Game.Player.ChangeModel(1581098148);
                TriggerEvent("chat:addMessage", new
                {
                    color = new[] { 255, 0, 0 },
                    args = new[] { "[skin]", $"skin został zmieniony..." }
                });
            }), false);
        }
    }
}