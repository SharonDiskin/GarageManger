using System;

namespace Ex03.GarageLogic
{
    internal sealed class Motorcycle : Vehicle
    {
        private readonly string[] r_RequiredInitParameters =
        {
            supportedLicense(),
            "Motorcycle Engine Capacity"
        };

        private eMotorcycleLicenseType m_MotorcycleLicenseType;
        private int m_EngineCapacity;

        internal Motorcycle(
            string i_ModelName,
            string i_LicenseNumberNumber,
            int i_NumberOfWheel,
            string i_WheelManufacturer,
            float i_CurrentAirPressure,
            float i_MaximumAirPressure,
            Engine i_Engine,
            eMotorcycleLicenseType i_LicenseType,
            int i_EngineCapacity) : base
            (i_ModelName,
            i_LicenseNumberNumber,
            i_NumberOfWheel,
            i_WheelManufacturer,
            i_CurrentAirPressure,
            i_MaximumAirPressure,
            i_Engine)
        {
            m_MotorcycleLicenseType = i_LicenseType;
            m_EngineCapacity = i_EngineCapacity;
        }

        public override string ToString()
        {
            return
                $"{base.ToString()}LicenseNumber type: {m_MotorcycleLicenseType}{Environment.NewLine}Engine capacity: {m_EngineCapacity}";
        }

        public override void InitParameters(string[] i_VehicleParameters)
        {
            if (int.Parse(i_VehicleParameters[0]) > 4 || int.Parse(i_VehicleParameters[0]) < 1)
            {
                throw new ValueOutOfRangeException("value must between 1 to 4");
            }

            if (float.Parse(i_VehicleParameters[1]) < 0)
            {
                throw new ValueOutOfRangeException("value must be greater than 0");
            }

            m_MotorcycleLicenseType = (eMotorcycleLicenseType)int.Parse(i_VehicleParameters[0]);
            m_EngineCapacity = int.Parse(i_VehicleParameters[1]);
        }

        public override string[] Parameters
        {
            get
            {
                return r_RequiredInitParameters;
            }
        }

        private static string supportedLicense()
        {
            string options = $"Motorcycle LicenseNumber Type:{Environment.NewLine}";
            int colorNumber = 1;

            string[] possibleColors = Enum.GetNames(typeof(eMotorcycleLicenseType));
            foreach (string color in possibleColors)
            {
                options += $"{colorNumber}. {color}{Environment.NewLine}";
                colorNumber++;
            }

            return options;
        }

        public enum eMotorcycleLicenseType
        {
            A = 1,
            A1,
            B1,
            B2
        }
    }
}
