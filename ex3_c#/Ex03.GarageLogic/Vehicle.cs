using System.Collections.Generic;
using System;

namespace Ex03.GarageLogic
{
    public enum eVehicleType
    {
        Truck = 1,
        FuelCar,
        ElectricCar,
        FuelBike,
        ElectricBike
    }

    public enum eVehicleStatus
    {
        Repairing = 1,
        Repaired,
        Paid
    }

    internal abstract class Vehicle
    {
        protected string m_Model;
        protected string m_PlateNumber;
        protected List<Wheel> m_VehicleWheels;
        protected EnergySource m_PercentOfEnergySource;

        public EnergySource EnergySource
        {
            get { return m_PercentOfEnergySource; }
        }

        public string Model
        {
            get { return m_Model; }
            set { m_Model = value; }
        }

        public string PlateNumber
        {
            get { return m_PlateNumber; }
            set { m_PlateNumber = value; }
        }

        protected void InitAllWheels(int i_MaxAirPressure)
        {
            for (int i = 0; i < m_VehicleWheels.Capacity; i++)
            {
                m_VehicleWheels.Add(new Wheel(i_MaxAirPressure));
            }
        }

        protected void SetBasicVehicleInformation(List<string> i_ArgumentsArray)
        {
            float remainingEnergyPercent, remainingEnergy;
            bool isStringParsingToFloat;

            Model = i_ArgumentsArray[2];
            isStringParsingToFloat = float.TryParse(i_ArgumentsArray[3], out remainingEnergyPercent);
            if (!isStringParsingToFloat)
            {
                throw new FormatException("Remaining energy must be a numeric number!");
            }
            else if (remainingEnergyPercent > 100 || remainingEnergyPercent < 0)
            {
                throw new ValueOutOfRangeException("Invalid energy source remaining percent", 0, 100);
            }
            else
            {
                remainingEnergy = (remainingEnergyPercent / 100) * m_PercentOfEnergySource.MaxEnergy;
                m_PercentOfEnergySource.RemainingEnergy = remainingEnergy;
            }
        }

        public string GetAllWheelsToString()
        {
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

            stringBuilder.AppendLine(string.Format("Wheels air pressure: {0}", m_VehicleWheels[0].CurrentAirPressure));
            stringBuilder.AppendLine(string.Format("Wheels manufacturer: {0}", m_VehicleWheels[0].ManufacturerName));

            return stringBuilder.ToString();
        }

        protected void SetWheelsInformation(List<string> i_ArgumentsArray)
        {
            float currentWheelAirPressure;
            bool isStringParsingToFloat;

            isStringParsingToFloat = float.TryParse(i_ArgumentsArray[7], out currentWheelAirPressure);
            if (!isStringParsingToFloat)
            {
                throw new FormatException("Tier air pressure must be numeric!");
            }

            for (int i = 0; i < m_VehicleWheels.Capacity; i++)
            {
                m_VehicleWheels[i].ManufacturerName = i_ArgumentsArray[6];
                m_VehicleWheels[i].CurrentAirPressure = currentWheelAirPressure;
            }
        }

        public List<VehicleRequirement> GetBasicVehicleRequirements()
        {
            List<VehicleRequirement> vehicleRequirements = new List<VehicleRequirement>();

            vehicleRequirements.Add(new VehicleRequirement(eRequirementType.String, "Model name: ", 1));
            vehicleRequirements.Add(new VehicleRequirement(eRequirementType.Numeric, "Remaining energy (Percent): ", 1));

            return vehicleRequirements;
        }

        public List<VehicleRequirement> GetVehicleWheelsRequirements()
        {
            List<VehicleRequirement> vehicleRequirements = new List<VehicleRequirement>();

            vehicleRequirements.Add(new VehicleRequirement(eRequirementType.String, string.Format("Wheels Manufacturer name: "), 1));
            vehicleRequirements.Add(new VehicleRequirement(eRequirementType.Numeric, string.Format(
                "Wheels Current air pressure: (Max : {0}) ", m_VehicleWheels[0].MaxAirPressure), 1));

            return vehicleRequirements;
        }

        public void InflateAllWheels()
        {
            foreach (Wheel wheel in m_VehicleWheels)
            {
                wheel.InflateTierToMax();
            }
        }

        public abstract void SetVehicleValues(List<string> i_ArgumentsArray);

        public abstract List<VehicleRequirement> GetVehicleRequirements();

        public abstract override string ToString();
    }
}
