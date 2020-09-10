namespace Ex03.GarageLogic
{
    // this class serves as a mutual base for the electric and fuel engines so that Vehicle is able to hold either of them
    public abstract class Engine
    {
        protected readonly float r_MaxCapacity;
        protected float m_PercentageOfEnergyLeft;
        protected float m_CurrentCapacity;

        protected Engine(float i_Max, float i_CurrentCapacity)
        {
            r_MaxCapacity = i_Max;
            m_CurrentCapacity = i_CurrentCapacity;

            m_PercentageOfEnergyLeft = m_CurrentCapacity * 100f / r_MaxCapacity;
        }

        internal float PercentageOfEnergyLeft => m_PercentageOfEnergyLeft;

        internal float CurrentCapacity
        {
            get
            {
               return m_CurrentCapacity;
            }
            set
            {
                m_CurrentCapacity = value;
            }
        }
    }
}
