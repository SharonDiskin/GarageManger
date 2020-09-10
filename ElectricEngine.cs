namespace Ex03.GarageLogic
{
    // this class is a member of every vehicle that is powered by electricity
    public sealed class ElectricEngine : Engine
    {
        internal ElectricEngine(float i_Max, float i_CurrentCapacity) : base(i_Max, i_CurrentCapacity)
        {
        }

        public float CurrentEnergy
        {
            get
            {
                return m_CurrentCapacity;
            }
        }

        public float MaxEnergy
        {
            get
            {
                return r_MaxCapacity;
            }
        }

        public float PossibleAmountOfTimeToCharge
        {
            get
            {
                return r_MaxCapacity - m_CurrentCapacity
            }
        }

        internal void ChargeBattery(float i_ChargeToAdd)
        {
            if (i_ChargeToAdd + m_CurrentCapacity > r_MaxCapacity)
            {
                throw new ValueOutOfRangeException(i_ChargeToAdd, r_MaxCapacity.ToString(), r_MaxCapacity, 0);
            }

            m_CurrentCapacity += i_ChargeToAdd;
            m_PercentageOfEnergyLeft = m_CurrentCapacity * 100f / r_MaxCapacity;
        }
    }
}
