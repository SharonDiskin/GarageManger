using System;

namespace Ex03.GarageLogic
{
    internal sealed class Car : Vehicle
    {
        private readonly string[] r_RequiredInitParameters =
        {
            colorOptions(), 
            "number of doors"
        };

        private eCarColor m_CarColor;
        private int m_NumberOfDoors;

        public enum eCarColor
        {
            Red = 1,
            White,
            Green,
            Gray
        }

        internal Car(
            string i_ModelName,
            string i_LicenseNumberNumber,
            int i_NumberOfWheel,
            string i_WheelManufacturer,
            float i_CurrentAirPressure,
            float i_MaximumAirPressure,
            Engine i_Engine,
            eCarColor i_CarColor,
            int i_NumOfDoors) :
            base(
                i_ModelName,
                i_LicenseNumberNumber,
                i_NumberOfWheel,
                i_WheelManufacturer,
                i_CurrentAirPressure,
                i_MaximumAirPressure,
                i_Engine)
        {
            m_CarColor = i_CarColor;
            m_NumberOfDoors = i_NumOfDoors;
        }

        public override string ToString()
        {
            return $"{base.ToString()}Car specific information:{Environment.NewLine}Color: {m_CarColor}, Number of doors: {m_NumberOfDoors}";
        }

        public override string[] Parameters => r_RequiredInitParameters;

        public override void InitParameters(string[] i_VehicleParameters)
        {
            if (int.Parse(i_VehicleParameters[0]) < 0 || int.Parse(i_VehicleParameters[0]) > 4)
            {
                throw new ValueOutOfRangeException(float.Parse(i_VehicleParameters[0]), "car color", 4, 1);
            }

            if (int.Parse(i_VehicleParameters[1]) > 5 || float.Parse(i_VehicleParameters[1]) < 2)
            {
                throw new ValueOutOfRangeException("number of car doors must be between 2 and 5");
            }

            m_CarColor = (eCarColor)int.Parse(i_VehicleParameters[0]);
            m_NumberOfDoors = int.Parse(i_VehicleParameters[1]);
        }

        private static string colorOptions()
        {
            string options = $"Car Color:{Environment.NewLine}";
            int colorNumber = 1;

            string[] possibleColors = Enum.GetNames(typeof(eCarColor));
            foreach (string color in possibleColors)
            {
                options += $"{colorNumber}. {color}{Environment.NewLine}";
                colorNumber++;
            }

            return options;
        }
    }
}
