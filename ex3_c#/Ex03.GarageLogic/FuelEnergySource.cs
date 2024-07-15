using System;

namespace Ex03.GarageLogic
{
    public enum eFuelType
    {
        Octan95 = 1,
        Octan96,
        Octan98,
        Soler
    }
    internal class FuelEnergySource : EnergySource
    {
        private readonly eFuelType r_FuelType;

        public FuelEnergySource(eFuelType i_FuelType, float i_MaxFuelLevel) : base(i_MaxFuelLevel)
        {
            r_FuelType = i_FuelType;
        }
        
        public void FillFuel(float i_FuelAmountToFill, eFuelType i_FuelType)
        {
            if (r_FuelType != i_FuelType)
            {
                throw new ArgumentException(string.Format("The given fuel type '{0}' doesn't match the engine's fuel type '{1}'.", i_FuelType, r_FuelType));
            }
            else if (i_FuelAmountToFill + m_RemainingEnergy > r_MaxEnergy)
            {
                throw new ValueOutOfRangeException("The amount of fuel is over the tank's limit!", 0, r_MaxEnergy - RemainingEnergy);
            }
            else
            {
                m_RemainingEnergy += i_FuelAmountToFill;
            }
        }

        public override string ToString()
        {
            return string.Format(@"Fuel left in the tank: {0} / {1}
Fuel type: {2}", RemainingEnergy, MaxEnergy, r_FuelType);
        }
    }
}
