namespace Ex03.GarageLogic
{
    internal class ElectricEnergySource : EnergySource
    {
        public ElectricEnergySource(float i_BatteryMaxHours) : base(i_BatteryMaxHours)
        {

        }
        
        public void ChargeBattery(float i_HoursAmountToCharge)
        {
            if (i_HoursAmountToCharge + m_RemainingEnergy > r_MaxEnergy)
            {
                throw new ValueOutOfRangeException("The amount of hours is over the battery's limit!", 0, r_MaxEnergy-RemainingEnergy);
            }
            else
            {
                m_RemainingEnergy += i_HoursAmountToCharge;
            }
        }

        public override string ToString()
        {
            return string.Format("Hours left in the battery: {0} / {1}", RemainingEnergy.ToString(), MaxEnergy.ToString());
        }
    }
}
