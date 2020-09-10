namespace Ex03.GarageLogic
{
    public static class VehicleCreator
    {
        public static Vehicle CreateVehicle(
            eVehicleTypes i_ChosenVehicleToCreate,
            string i_ModelName,
            string i_LicenseNumber,
            string i_WheelManufacturer)
        {
            float i_CurrentEnergy = 0f, i_TruckCargoVolume = 0f;
            int i_CurrentWheelAirPressure = 0,
                i_NumberOfDoors = 0,
                i_MotorcycleEngineCapacity = 0;
            Car.eCarColor i_CarColor = Car.eCarColor.Gray; 
                Motorcycle.eMotorcycleLicenseType i_MotorcycleLicense = Motorcycle.eMotorcycleLicenseType.B2;
                bool i_IsDangerousTruck = false;
                Vehicle newVehicle;

            switch (i_ChosenVehicleToCreate)
            {
                case eVehicleTypes.FuelMotorcycle:
                    newVehicle = new Motorcycle(
                        i_ModelName,
                        i_LicenseNumber,
                        2,
                        i_WheelManufacturer,
                        i_CurrentWheelAirPressure,
                        (float)Vehicle.Wheel.eAirPressure.ForMotorcycle,
                        new FuelEngine(5.5f, FuelEngine.eFuelType.Octan95, i_CurrentEnergy),
                        i_MotorcycleLicense,
                        i_MotorcycleEngineCapacity);
                    break;

                case eVehicleTypes.ElectricMotorcycle:
                    newVehicle = new Motorcycle(
                        i_ModelName,
                        i_LicenseNumber,
                        2,
                        i_WheelManufacturer,
                        i_CurrentWheelAirPressure,
                        (float)Vehicle.Wheel.eAirPressure.ForMotorcycle,
                        new ElectricEngine(1.6f, i_CurrentEnergy), 
                        i_MotorcycleLicense,
                        i_MotorcycleEngineCapacity);
                    break;

                case eVehicleTypes.FuelCar:
                    newVehicle = new Car(
                        i_ModelName,
                        i_LicenseNumber,
                        4,
                        i_WheelManufacturer,
                        i_CurrentWheelAirPressure,
                        (float)Vehicle.Wheel.eAirPressure.ForCar,
                        new FuelEngine(50f, FuelEngine.eFuelType.Octan96, i_CurrentEnergy),
                        i_CarColor,
                        i_NumberOfDoors);
                    break;

                case eVehicleTypes.ElectricCar:
                    newVehicle = new Car(
                        i_ModelName,
                        i_LicenseNumber,
                        4,
                        i_WheelManufacturer,
                        i_CurrentWheelAirPressure,
                        (float)Vehicle.Wheel.eAirPressure.ForCar,
                        new ElectricEngine(4.8f, i_CurrentEnergy),
                        i_CarColor,
                        i_NumberOfDoors);
                    break;

                case eVehicleTypes.Truck:
                    newVehicle = new Truck(
                        i_ModelName,
                        i_LicenseNumber,
                        16,
                        i_WheelManufacturer,
                        i_CurrentWheelAirPressure,
                        (float)Vehicle.Wheel.eAirPressure.ForTruck,
                        new FuelEngine(105f, FuelEngine.eFuelType.Soler, i_CurrentEnergy),
                        i_IsDangerousTruck,
                        i_TruckCargoVolume);
                    break;

                default:
                    newVehicle = null;
                    break;
            }

            return newVehicle;
        }

        public enum eVehicleTypes
        {
            FuelMotorcycle = 1,
            ElectricMotorcycle,
            FuelCar,
            ElectricCar,
            Truck
        }
    }
}