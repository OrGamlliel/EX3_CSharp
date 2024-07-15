using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public enum eLicenseType
    {
        A1 = 1,
        A2,
        AA,
        B1
    }

    internal class Bike : Vehicle
    {
        private const int k_AmountOfBikeWheels = 2;
        private const int k_BikeMaxAirPressure = 31;
        private const eFuelType k_BikeFuelType = eFuelType.Octan98;
        private const float k_BikeFuelTankCapacity = 6.4f;
        private const float k_BikeBatteryHoursCapacity = 2.6f;
        protected eLicenseType m_BikeLicenseType;
        protected int m_EngineVolume;

        public Bike(string i_PlateNumber, bool i_IsElectric)
        {
            base.PlateNumber = i_PlateNumber;
            if (i_IsElectric)
            {
                base.m_PercentOfEnergySource = new ElectricEnergySource(k_BikeBatteryHoursCapacity);
            }
            else
            {
                base.m_PercentOfEnergySource = new FuelEnergySource(k_BikeFuelType, k_BikeFuelTankCapacity);
            }

            base.m_VehicleWheels = new List<Wheel>(k_AmountOfBikeWheels);
            base.InitAllWheels(k_BikeMaxAirPressure);
        }
        
        public override void SetVehicleValues(List<string> i_ArgumentsArray)
        {
            SetBasicVehicleInformation(i_ArgumentsArray);
            SetWheelsInformation(i_ArgumentsArray);
            setBikeValues(i_ArgumentsArray);
        }

        private void setBikeValues(List<string> i_ArgumentsArray)
        {
            int engineVolume, enumIndex;
            bool isStringParsingToInt;

            isStringParsingToInt = int.TryParse(i_ArgumentsArray[4], out enumIndex);
            if (!isStringParsingToInt)
            {
                throw new FormatException("invalid bike license choice, please pick an option from list (numeric input)");
            }
            else
            {
                m_BikeLicenseType = (eLicenseType)enumIndex;
            }

            isStringParsingToInt = int.TryParse(i_ArgumentsArray[5], out engineVolume);
            if (!isStringParsingToInt)
            {
                throw new FormatException("invalid bike engine volume, please enter a numeric value");
            }
            else
            {
                m_EngineVolume = engineVolume;
            }
        }
        
        public override List<VehicleRequirement> GetVehicleRequirements()
        {
            List<VehicleRequirement> bikeRequirements = new List<VehicleRequirement>();

            bikeRequirements.AddRange(base.GetBasicVehicleRequirements());
            bikeRequirements.Add(new VehicleRequirement(eRequirementType.Enum, string.Format(@"License type: 
1. {0}
2. {1}
3. {2}
4. {3}
", eLicenseType.A1, eLicenseType.A2, eLicenseType.AA, eLicenseType.B1), Enum.GetValues(typeof(eLicenseType)).Length));
            bikeRequirements.Add(new VehicleRequirement(eRequirementType.Numeric, "Engine volume: ", 1));
            bikeRequirements.AddRange(base.GetVehicleWheelsRequirements());

            return bikeRequirements;
        }

        public override string ToString()
        {
            return string.Format(@"{0}
License type: {1}
Engine volume: {2}", m_PercentOfEnergySource.ToString(), m_BikeLicenseType.ToString(), m_EngineVolume.ToString());
        }
    }
}
