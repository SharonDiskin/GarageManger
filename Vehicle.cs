using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private readonly ePowerSource r_PowerSource;
        protected readonly Engine r_Engine;
        private readonly string r_ModelName;
        private readonly string r_LicenseNumberNumber;
        private readonly Wheel[] r_VehicleWheels;
        private readonly int r_WheelCount;
        private readonly string[] r_RequiredInitParameters = null;

        internal Vehicle(
            string i_ModelName,
            string i_LicenseNumberNumber,
            int i_NumberOfWheel,
            string i_WheelManufacturer,
            float i_CurrentAirPressure,
            float i_MaximumAirPressure,
            Engine i_Engine)
        {
            r_ModelName = i_ModelName;
            r_LicenseNumberNumber = i_LicenseNumberNumber;
            r_WheelCount = i_NumberOfWheel;
            r_VehicleWheels = new Wheel[r_WheelCount];
            initializeWheels(i_NumberOfWheel, i_WheelManufacturer, i_CurrentAirPressure, i_MaximumAirPressure);
            r_Engine = i_Engine;

            r_PowerSource = r_Engine is ElectricEngine ? ePowerSource.Electricity : ePowerSource.Fuel;
        }

        private void initializeWheels(int i_NumberOfWheels, string i_WheelManufacturer, float i_CurrentAirPressure, float i_MaximumAirPressure)
        {
            for (int i = 0; i < i_NumberOfWheels; i++)
            {
                r_VehicleWheels[i] = new Wheel(i_WheelManufacturer, i_MaximumAirPressure, i_CurrentAirPressure);
            }
        }

        public string LicenseNumber
        {
            get
            {
                return r_LicenseNumberNumber;
            }
        }

        public virtual string[] Parameters
        {
            get
            {
                return r_RequiredInitParameters;
            }
        }

        public float MaxPossibleWheelAir
        {
            get
            {
                return r_VehicleWheels[0].MaxWheelAir;
            }
        }

        public float CurrentEnergy
        {
            get
            {
                float currentEnergy;

                if (r_Engine is ElectricEngine electricEngine)
                {
                    currentEnergy = electricEngine.CurrentEnergy;
                }
                else
                {
                    currentEnergy = ((FuelEngine) r_Engine).Fuel;
                }

                return currentEnergy;
            }
        }

        public float MaxEnergyCapacity
        {
            get
            {
                float maxCapacity;

                if (r_Engine is ElectricEngine electricEngine)
                {
                    maxCapacity = electricEngine.MaxEnergy;
                }
                else
                {
                    maxCapacity = ((FuelEngine) r_Engine).Size;
                }

                return maxCapacity;
            }
        }

        public float EnergyUntilFullTank
        {
            get
            {
                float capacity;

                if (r_Engine is ElectricEngine engine)
                {
                    capacity = engine.PossibleAmountOfTimeToCharge;
                }
                else
                {
                    capacity = ((FuelEngine) r_Engine).PossibleAmountOfFuelToAdd;
                }

                return capacity;
            }
        }

        public float PercentageOfEnergyLeft => r_Engine.PercentageOfEnergyLeft;

        public Engine Engine
        {
            get
            {
                return r_Engine;
            }
        }

        public override string ToString()
        {
            string energyInfo;
            if (r_PowerSource == ePowerSource.Electricity)
            {
                energyInfo = 
$"powered by electricity, current charge {CurrentEnergy}, max charge {MaxEnergyCapacity} ({PercentageOfEnergyLeft}%)";
            }
            else
            {
                energyInfo =
$"powered by fuel, current fuel in tank: {CurrentEnergy}, max fuel capacity: {MaxEnergyCapacity} ({PercentageOfEnergyLeft}%)";
            }

            string vehicleInformation =
$@"
Vehicle Information:
LicenseNumber number: {r_LicenseNumberNumber}
Model name: {r_ModelName}
{energyInfo}
Wheel Information (applies for {r_WheelCount} wheels):
";
            vehicleInformation += r_VehicleWheels[0].ToString();

            return vehicleInformation;
        }

        public abstract void InitParameters(string[] i_VehicleParameters);

        public void SetInitialWheelAir(float i_StartingWheelAir)
        {
            if (i_StartingWheelAir > r_VehicleWheels[0].MaxWheelAir)
            {
                throw new ValueOutOfRangeException(i_StartingWheelAir, "starting wheel air", r_VehicleWheels[0].MaxWheelAir, 0);
            }

            foreach (Wheel wheel in r_VehicleWheels)
            {
                wheel.InflateWheel(i_StartingWheelAir);
            }
        }

        public void SetInitialEnergy(float i_StartingEnergy)
        {
            if (i_StartingEnergy > MaxEnergyCapacity)
            {
                throw new ValueOutOfRangeException(i_StartingEnergy, "starting fuel", MaxEnergyCapacity, 0);
            }

            if (r_PowerSource == ePowerSource.Fuel)
            {
                ((FuelEngine)r_Engine).Refuel(i_StartingEnergy, ((FuelEngine)r_Engine).FuelType);
            }
            else
            {
                ((ElectricEngine)r_Engine).ChargeBattery(i_StartingEnergy);
            }
        }

        public void InflateAllWheelsToMax()
        {
            foreach (Wheel wheel in r_VehicleWheels)
            {
                wheel.SetAirToMax();
            }
        }

        public enum ePowerSource
        {
            Fuel,
            Electricity
        }

        internal class Wheel
        {
            private readonly string r_ManufacturerName;
            private readonly float r_MaxAirPressure;
            private float m_CurrentAirPressure;

            public enum eAirPressure
            {
                ForMotorcycle = 28,
                ForTruck = 30,
                ForCar = 32,
            }

            public Wheel(string i_ManufacturerName, float i_MaxAirPressure, float i_CurrentAirPressure)
            {
                r_ManufacturerName = i_ManufacturerName;
                m_CurrentAirPressure = i_CurrentAirPressure;
                r_MaxAirPressure = i_MaxAirPressure;
            }

            public override string ToString()
            {
                return 
$@"Manufacturer name: {r_ManufacturerName}, Current air pressure: {m_CurrentAirPressure}, Max air pressure: {r_MaxAirPressure}
";
            }

            public float MaxWheelAir
            {
                get
                {
                    return r_MaxAirPressure;
                }
            }

            internal void InflateWheel(float i_AirToAdd)
            {
                if (m_CurrentAirPressure + i_AirToAdd <= r_MaxAirPressure)
                {
                    m_CurrentAirPressure += i_AirToAdd;
                }
                else
                {
                    throw new ValueOutOfRangeException(i_AirToAdd, m_CurrentAirPressure.ToString(), r_MaxAirPressure, 0);
                }
            }

            internal void SetAirToMax()
            {
                m_CurrentAirPressure = r_MaxAirPressure;
            }
        }
    }
}
