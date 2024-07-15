namespace Ex03.GarageLogic
{
    internal class Wheel
    {
        private string m_ManufacturerName;
        private float m_CurrentAirPressure;
        private readonly float r_MaxAirPressure;

        public Wheel(float i_MaxAirPressure)
        {
            r_MaxAirPressure = i_MaxAirPressure;
        }

        private void inflateTier(float i_AmountOfAirToInflate)
        {
            if (i_AmountOfAirToInflate + m_CurrentAirPressure > MaxAirPressure)
            {
                throw new ValueOutOfRangeException("The amount of air to inflate is over the wheel's maximum capacity!", 0, r_MaxAirPressure-CurrentAirPressure);
            }
            else
            {
                CurrentAirPressure += i_AmountOfAirToInflate;
            }
        }

        public void InflateTierToMax()
        {
            inflateTier(MaxAirPressure - CurrentAirPressure);
        }

        public string ManufacturerName
        {
            get { return m_ManufacturerName; }
            set { m_ManufacturerName = value; }
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
            set
            {
                if (value <= r_MaxAirPressure)
                {
                    if (value >= 0)
                    {
                        m_CurrentAirPressure = value;
                    }
                    else
                    {
                        throw new ValueOutOfRangeException("Air pressure cannot be a negative number", 0, r_MaxAirPressure-CurrentAirPressure);
                    }
                }
                else
                {
                    throw new ValueOutOfRangeException("Excessive amount of air to inflate", 0, r_MaxAirPressure-CurrentAirPressure);
                }
            }
        }

        public float MaxAirPressure
        {
            get { return r_MaxAirPressure; }
        }
    }
}
