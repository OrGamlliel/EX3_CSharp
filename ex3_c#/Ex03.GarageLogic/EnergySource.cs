namespace Ex03.GarageLogic
{
    internal abstract class EnergySource
    {
        protected float m_RemainingEnergy;
        protected readonly float r_MaxEnergy;

        public EnergySource(float i_MaxEnergy)
        {
            r_MaxEnergy = i_MaxEnergy;
        }
        
        public float MaxEnergy
        {
            get { return r_MaxEnergy; }
        }

        public float RemainingEnergy
        {
            get { return m_RemainingEnergy; }
            set { m_RemainingEnergy = value; }
        }

        public abstract override string ToString();
    }
}
