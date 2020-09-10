using System;

namespace Ex03.GarageLogic
{
    // this class is a member of every vehicle that is powered by gasoline
    public sealed class FuelEngine : Engine
    {
        private readonly eFuelType r_FuelType;

        public enum eFuelType
        {
            Soler,
            Octan95,
            Octan96,
            Octan98
        }

        public FuelEngine(float i_MaxFuelCapacity, eFuelType i_FuelType, float i_CurrentCapacity) : base(i_MaxFuelCapacity, i_CurrentCapacity)
        {
            r_FuelType = i_FuelType;
        }

        internal float Fuel
        {
            get
            {
                return m_CurrentCapacity;
            }
        }

        internal float Size
        {
            get
            {
                return r_MaxCapacity;
            }
        }

        public float PossibleAmountOfFuelToAdd
        {
            get
            {
               return r_MaxCapacity - m_CurrentCapacity;
            }
        }

        public eFuelType FuelType
        {
            get
            {
                return r_FuelType;
            }
        }

        internal void Refuel(float i_FuelToAdd, eFuelType i_FuelType)
        {
            if (i_FuelType != r_FuelType)
            {
                throw new ArgumentException($"{i_FuelType} is incompatible with component with fuel type {r_FuelType}");
            }
            else if (i_FuelToAdd + m_CurrentCapacity > r_MaxCapacity)
            {
                throw new ValueOutOfRangeException(i_FuelToAdd, nameof(m_CurrentCapacity), r_MaxCapacity, 0);
            }
            else
            {
                m_CurrentCapacity += i_FuelToAdd;
                m_PercentageOfEnergyLeft = m_CurrentCapacity * 100f / r_MaxCapacity;
            }
        }
    }
}