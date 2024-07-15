using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class Truck : Vehicle
    {
        private const int k_AmountOfTruckWheels = 14;
        private const int k_TruckMaxAirPressure = 26;
        private const eFuelType k_TruckFuelType = eFuelType.Soler;
        private const float k_TruckFuelTankCapacity = 135f;
        private bool m_IsCarryingHazardousMaterials;
        private float m_CargoVolume;

        public Truck(string i_PlateNumber)
        {
            base.PlateNumber = i_PlateNumber;
            base.m_PercentOfEnergySource = new FuelEnergySource(k_TruckFuelType, k_TruckFuelTankCapacity);
            base.m_VehicleWheels = new List<Wheel>(k_AmountOfTruckWheels);
            base.InitAllWheels(k_TruckMaxAirPressure);
        }
        
        public override void SetVehicleValues(List<string> i_ArgumentsArray)
        {
            SetBasicVehicleInformation(i_ArgumentsArray);
            SetWheelsInformation(i_ArgumentsArray);
            setTruckValues(i_ArgumentsArray);
        }

        private void setTruckValues(List<string> i_ArgumentsArray)
        {
            bool isStringParsingToFloat;
            float cargoVolume;

            if (i_ArgumentsArray[4] == "1")
            {
                m_IsCarryingHazardousMaterials = true;
            }
            else
            {
                m_IsCarryingHazardousMaterials = false;
            }

            isStringParsingToFloat = float.TryParse(i_ArgumentsArray[5], out cargoVolume);
            if (!isStringParsingToFloat)
            {
                throw new FormatException("Cargo volume must be Numaric!");
            }
            else
            {
                m_CargoVolume = cargoVolume;
            }
        }

        public override List<VehicleRequirement> GetVehicleRequirements()
        {
            List<VehicleRequirement> truckRequirements = new List<VehicleRequirement>();

            truckRequirements.AddRange(base.GetBasicVehicleRequirements());
            truckRequirements.Add(new VehicleRequirement(eRequirementType.Boolean, @"Is carrying hazardous materials: 
1. Yes
2. No
", 2));
            truckRequirements.Add(new VehicleRequirement(eRequirementType.Numeric, "Cargo volume: ", 1));
            truckRequirements.AddRange(base.GetVehicleWheelsRequirements());

            return truckRequirements;
        }

        public override string ToString()
        {
            return string.Format(@"{0}
Carrying hazardous materials: {1}
Cargo volume: {2}", EnergySource.ToString(), m_IsCarryingHazardousMaterials.ToString(), m_CargoVolume.ToString());
        }
    }
}
