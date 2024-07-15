using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public enum eColor
    {
        White = 1,
        Black,
        Yellow,
        Red
    }

    public enum eDoorsAmount
    {
        Two = 1,
        Three,
        Four,
        Five
    }

    internal class Car : Vehicle
    {
        private const int k_AmountOfCarWheels = 5;
        private const int k_CarMaxAirPressure = 33;
        private const eFuelType k_CarFuelType = eFuelType.Octan95;
        private const float k_CarFuelTankCapacity = 46f;
        private const float k_CarBatteryHoursCapacity = 5.2f;
        protected eColor m_CarColor;
        protected eDoorsAmount m_CarDoorAmount;

        public Car(string i_PlateNumber, bool i_IsElectric)
        {
            base.PlateNumber = i_PlateNumber;
            if (i_IsElectric)
            {
                base.m_PercentOfEnergySource = new ElectricEnergySource(k_CarBatteryHoursCapacity);
            }
            else
            {
                base.m_PercentOfEnergySource = new FuelEnergySource(k_CarFuelType, k_CarFuelTankCapacity);
            }

            base.m_VehicleWheels = new List<Wheel>(k_AmountOfCarWheels);
            base.InitAllWheels(k_CarMaxAirPressure);
        }
        
        public override void SetVehicleValues(List<string> i_ArgumentsArray)
        {
            SetBasicVehicleInformation(i_ArgumentsArray);
            SetWheelsInformation(i_ArgumentsArray);
            setCarValues(i_ArgumentsArray);
        }

        private void setCarValues(List<string> i_ArgumentsArray)
        {
            int enumIndex;
            bool isStringParsingToInt;

            isStringParsingToInt = int.TryParse(i_ArgumentsArray[4], out enumIndex);
            if (!isStringParsingToInt)
            {
                throw new FormatException("Invalid color choice in car, please choose an option from the list (numeric)");
            }
            else
            {
                m_CarColor = (eColor)enumIndex;
            }

            isStringParsingToInt = int.TryParse(i_ArgumentsArray[5], out enumIndex);
            if (!isStringParsingToInt)
            {
                throw new FormatException("Invalid doors amount choice in car, please choose an option from the list (numeric)");
            }
            else
            {
                m_CarDoorAmount = (eDoorsAmount)enumIndex;
            }
        }

        public override List<VehicleRequirement> GetVehicleRequirements()
        {
            List<VehicleRequirement> carRequirements = new List<VehicleRequirement>();

            carRequirements.AddRange(base.GetBasicVehicleRequirements());
            carRequirements.Add(new VehicleRequirement(eRequirementType.Enum, string.Format(@"Color: 
1. {0}
2. {1}
3. {2}
4. {3}
", eColor.White, eColor.Black, eColor.Yellow, eColor.Red), Enum.GetValues(typeof(eColor)).Length));
            carRequirements.Add(new VehicleRequirement(eRequirementType.Enum, string.Format(@"Doors amount: 
1. {0}
2. {1}
3. {2}
4. {3}
", eDoorsAmount.Two, eDoorsAmount.Three, eDoorsAmount.Four, eDoorsAmount.Five), Enum.GetValues(typeof(eDoorsAmount)).Length));
            carRequirements.AddRange(base.GetVehicleWheelsRequirements());

            return carRequirements;
        }

        public override string ToString()
        {
            return string.Format(@"{0}
Color: {1}
Doors amount: {2}", EnergySource.ToString(), m_CarColor.ToString(), m_CarDoorAmount.ToString());
        }
    }
}
