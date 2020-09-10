using System;

namespace Ex03.GarageLogic
{
    internal sealed class Truck : Vehicle
    {
        private readonly string[] r_RequiredInitParameters =
        {
            string.Format("is truck carrying dangerous payload:{0}1. Yes{0}2. No", Environment.NewLine), 
            "cargo volume"
        };

        private bool m_IsCarryingDangerousMaterials;
        private float m_CargoVolume;

        internal Truck(
            string i_ModelName,
            string i_LicenseNumberNumber,
            int i_NumberOfWheel,
            string i_WheelManufacturer,
            float i_CurrentAirPressure,
            float i_MaximumAirPressure,
            Engine i_Engine,
            bool i_TransportDangerousMaterials,
            float i_CargoVolume)
            : base(
                i_ModelName, 
                i_LicenseNumberNumber, 
                i_NumberOfWheel, 
                i_WheelManufacturer,
                i_CurrentAirPressure,
                i_MaximumAirPressure, 
                i_Engine)
        {
            m_IsCarryingDangerousMaterials = i_TransportDangerousMaterials;
            m_CargoVolume = i_CargoVolume;
        }

        public override string ToString()
        {
            return string.Format(
                "{0}Cargo volume: {1}{2}{3}",
                base.ToString(), 
                m_CargoVolume,
                Environment.NewLine,
                m_IsCarryingDangerousMaterials ? "Contains dangerous materials" : "does not contain dangerous materials");
        }

        public override void InitParameters(string[] i_VehicleParameters)
        {
            if (i_VehicleParameters[0] != "1" && i_VehicleParameters[0] != "2")
            {
                throw new ValueOutOfRangeException("value must be 1 or 2");
            }

            if (float.Parse(i_VehicleParameters[1]) < 0)
            {
                throw new ValueOutOfRangeException("value must be greater than 0");
            }

            m_IsCarryingDangerousMaterials = i_VehicleParameters[0] == "1";
            m_CargoVolume = float.Parse(i_VehicleParameters[1]);
        }

        public override string[] Parameters
        {
            get
            {
                return r_RequiredInitParameters;
            }
        }
    }
}
