using System;
using System.Linq;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    internal class UI
    {
        private const int r_MaxDashes = 56; /// We will use it in order to calculate amount of dashes to print in the all the menus
        private readonly Garage r_Garage;

        public UI()
        {
            r_Garage = new Garage();
        }

        internal void Start()
        {
            int selectedOption;
            do
            {
                PrintOptions();
                selectedOption = getSelection(1, 8);
                goToSelection(selectedOption);
            }
            while ((Garage.eFunctionalities)selectedOption != Garage.eFunctionalities.Exit);
        }

        internal void PrintOptions()
        {
            Console.WriteLine("{0}Please choose one on the following options:", Environment.NewLine);
            string[] serviceOptions = Enum.GetNames(typeof(Garage.eFunctionalities));

            for (int i = 0; i < serviceOptions.Length; i++)
            {
                serviceOptions[i] = seperateEnumWords(serviceOptions[i]).ToString();
                int amountOfDashes = r_MaxDashes - serviceOptions[i].Length; /// We calculate amount of dashes to print in each line of the menu
                string pad = new string('-', amountOfDashes);

                Console.WriteLine("{2} {1}> {0}", i + 1, pad, serviceOptions[i]);
            }
        }

        private void goToSelection(int i_UserChoice)
        {
            /// If there is no data to display/modify and the user chooses something rather than adding data or exit, we return
            if (i_UserChoice != (int)Garage.eFunctionalities.AddNewVehicle &&
                i_UserChoice != (int)Garage.eFunctionalities.Exit && garageIsEmpty())
            {
                return;
            }

            switch ((Garage.eFunctionalities)i_UserChoice)
            {
                case Garage.eFunctionalities.AddNewVehicle:
                    placeAVehicleInGarage();
                    break;
                case Garage.eFunctionalities.DisplayLicenses:
                    displayLicensesOfVehiclesInGarage();
                    break;
                case Garage.eFunctionalities.ChangeVehicleStatus:
                    changeVehicleStatus();
                    break;
                case Garage.eFunctionalities.InflateWheelsToMaximum:
                    inflateVehicleWheelsToMaximumAirCapacity();
                    break;
                case Garage.eFunctionalities.RefuelVehicle:
                    refillEnergyToVehicle(Garage.eFunctionalities.RefuelVehicle);
                    break;
                case Garage.eFunctionalities.RechargeVehicle:
                    refillEnergyToVehicle(Garage.eFunctionalities.RechargeVehicle);

                    break;
                case Garage.eFunctionalities.DisplayVehicleDetails:
                    showDataOfAVehicle();
                    break;
                case Garage.eFunctionalities.Exit:
                    Console.WriteLine("Thanks for visiting us :) Hope to see you soon");
                    break;
                default:
                    Console.WriteLine("invalid selection, please try again");
                    break;
            }
        }

        private string getNumberString()
        {
            string number;
            bool isNumber, isEmpty;

            do
            {
                number = Console.ReadLine();
                isNumber = number.All(char.IsDigit);

                isEmpty = string.IsNullOrWhiteSpace(number);

                if (isEmpty)
                {
                    Console.WriteLine("You must enter a value");
                }

                if (!isNumber)
                {
                    Console.WriteLine("The data you enterd must contain only digits");
                }
            }
            while (!isNumber || isEmpty);

            return number;
        }

        private string getNameString()
        {
            string name;
            bool isAllLetters, isEmpty;

            do
            {
                name = Console.ReadLine();
                isAllLetters = name.All(char.IsLetter);
                isEmpty = string.IsNullOrWhiteSpace(name);

                if (isEmpty)
                {
                    Console.WriteLine("You must enter a value");
                }

                if (!isAllLetters)
                {
                    Console.WriteLine("The data you entered must contain only letters");
                }
            }
            while (!isAllLetters || isEmpty);

            return name;
        }

        private string getNoneEmptyInput()
        {
            string input;
            bool isEmpty;

            do
            {
                input = Console.ReadLine();
                isEmpty = string.IsNullOrWhiteSpace(input);

                if (isEmpty)
                {
                    Console.WriteLine("You must enter a value");
                }
            }
            while (isEmpty);

            return input;
        }

        private int getSelection(int i_MinOption, int i_MaxOption)
        {
            int input;
            bool isInRange, isInt;

            do
            {
                isInt = int.TryParse(Console.ReadLine(), out input);
                isInRange = input >= i_MinOption && input <= i_MaxOption;

                if (!isInt)
                {
                    Console.WriteLine("Value must be an integer, please try again...");
                }
                else if (!isInRange)
                {
                    Console.WriteLine("Value must be between {0} and {1}, please try again...", i_MinOption, i_MaxOption);
                }
            }
            while (!isInRange || !isInt);

            return input;
        }

        private float getEnergyAmountToAdd(float i_MinAmount, float i_MaxAmount)
        {
            float input;
            bool isInRange, isFloat;

            do
            {
                isFloat = float.TryParse(Console.ReadLine(), out input);
                isInRange = i_MinAmount <= input && input <= i_MinAmount;

                if (!isFloat)
                {
                    Console.WriteLine("Value must be a number, please try again...");
                }
                else if (!isInRange)
                {
                    Console.WriteLine("Value must be between {0} and {1}, please try again...", i_MinAmount, i_MaxAmount);
                }
            }
            while (!isInRange || !isFloat);

            return input;
        }

        private Vehicle getVehicleByLicenseNumber(string i_LicenseNumber)
        {
            Vehicle vehicleByLicense = null;

            foreach (Garage.VehicleInformation vehicleInformation in r_Garage.AllVehiclesData)
            {
                if (vehicleInformation.Vehicle.LicenseNumber == i_LicenseNumber)
                {
                    vehicleByLicense = vehicleInformation.Vehicle;
                    break;
                }
            }

            return vehicleByLicense;
        }

        private bool isUpperCaseLetter(char i_someChar)
        {
            return 'A' <= i_someChar && i_someChar <= 'Z';
        }

        private StringBuilder seperateEnumWords(string i_SomeEnum)
        {
            StringBuilder enumWithSpace = new StringBuilder();
            enumWithSpace.Append(i_SomeEnum[0]);

            for (int i = 1; i < i_SomeEnum.Length; i++)
            {
                if (isUpperCaseLetter(i_SomeEnum[i]))
                {
                    enumWithSpace.Append(' ');
                }

                enumWithSpace.Append(i_SomeEnum[i]);
            }
            return enumWithSpace;
        }

        private void placeAVehicleInGarage()
        {
            VehicleCreator.eVehicleTypes vehicleTypeToCreate = selectVehicleToCreate();
            Console.WriteLine("Please provide the vehicle's license number");
            string licenseNumber = getNumberString();
            Garage.VehicleInformation vehicleInfo = r_Garage.GetVehicleByLicense(licenseNumber);

            if (vehicleInfo != null)
            {
                Console.WriteLine("This vehicle is already in the garage, status changed to 'in repair'");
                r_Garage.ModifyVehicleStatus(vehicleInfo, Garage.VehicleInformation.eVehicleStatus.InRepair);
            }
            else
            {
                string modelNumber = getModelNumber();
                string wheelManufacturer = getWheelManufacturer();

                Vehicle newVehicle = VehicleCreator.CreateVehicle(vehicleTypeToCreate, modelNumber, licenseNumber, wheelManufacturer);
                setupVehicle(newVehicle);
                getVehicleInformation(newVehicle);
            }
        }

        private string getWheelManufacturer()
        {
            Console.WriteLine("Enter wheel manufacturer: ");
            string wheelManufacturer = getNameString();

            return wheelManufacturer;
        }

        private string getModelNumber()
        {
            Console.WriteLine("Enter model: ");
            string modelNumber = getNoneEmptyInput();

            return modelNumber;
        }

        private VehicleCreator.eVehicleTypes selectVehicleToCreate()
        {
            Console.WriteLine("What type of vehicle would you like to create? ");
            string[] vehicleTypes = Enum.GetNames(typeof(VehicleCreator.eVehicleTypes));

            for (int i = 0; i < vehicleTypes.Length; i++)
            {
                vehicleTypes[i] = seperateEnumWords(vehicleTypes[i]).ToString();
                int amountOfDashes = r_MaxDashes - vehicleTypes[i].Length; /// We calculate amount of dashes to print in each line of the menu
                string pad = new string('-', amountOfDashes);

                Console.WriteLine("{2} {1}> {0}", i + 1, pad, vehicleTypes[i]);
            }

            VehicleCreator.eVehicleTypes selectedVehicle =
                (VehicleCreator.eVehicleTypes)getSelection(1, vehicleTypes.Length);

            return selectedVehicle;
        }

        private void getVehicleInformation(Vehicle io_VehicleToAdmit)
        {
            Console.WriteLine("Enter owner name");
            string name = getNameString();

            Console.WriteLine("Enter owner phone");
            string phoneString = getNumberString();

            r_Garage.AddNewVehicleToGarage(io_VehicleToAdmit, name, phoneString);
        }

        private void setupVehicle(Vehicle io_Vehicle)
        {
            setupVehicleParameters(io_Vehicle);
            setupVehicleWheelsAirPressure(io_Vehicle);
            setupVehicleEnergy(io_Vehicle);
        }

        private void setupVehicleWheelsAirPressure(Vehicle io_Vehicle)
        {
            bool airInitializationSucceeded = false;

            while (!airInitializationSucceeded)
            {
                try
                {
                    float startingAir = getStartingAirPressure();
                    io_Vehicle.SetInitialWheelAir(startingAir);

                    airInitializationSucceeded = true;
                }
                catch (ValueOutOfRangeException e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine("Please read the message above and try again");
                }
            }
        }

        private void setupVehicleParameters(Vehicle io_Vehicle)
        {
            bool isValid = false;
            string[] valuesToInit = new string[io_Vehicle.Parameters.Length];

            do
            {
                for (int i = 0; i < io_Vehicle.Parameters.Length; i++)
                {
                    Console.WriteLine("Enter {0}", io_Vehicle.Parameters[i]);
                    valuesToInit[i] = Console.ReadLine();
                }

                try
                {
                    io_Vehicle.InitParameters(valuesToInit);
                    isValid = true;
                }
                catch (ValueOutOfRangeException)
                {
                    Console.WriteLine("One of the values entered was invalid, please try again: ");
                }
                catch (FormatException)
                {
                    Console.WriteLine(
                        "one of the requested values was submitted in an invalid format, please try again: ");
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input, please try again: ");
                }
            }
            while (!isValid);
        }

        private float getStartingEnergy()
        {
            Console.WriteLine("Enter starting energy count: ");
            bool parseSucceeded = false;
            float startingFuel = 0f;

            while (!parseSucceeded)
            {
                try
                {
                    startingFuel = float.Parse(Console.ReadLine());
                    parseSucceeded = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("It seems like you didn't give me a number..Try Again");
                }
            }

            return startingFuel;
        }

        private void setupVehicleEnergy(Vehicle io_Vehicle)
        {
            bool airInitializationSucceeded = false;

            while (!airInitializationSucceeded)
            {
                try
                {
                    float startingEnergy = getStartingEnergy();
                    io_Vehicle.SetInitialEnergy(startingEnergy);

                    airInitializationSucceeded = true;
                }
                catch (ValueOutOfRangeException e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine("Please read the message above and try again");
                }
            }
        }

        private float getStartingAirPressure()
        {
            Console.WriteLine("Enter starting wheel air pressure: ");
            bool parseSucceeded = false;
            float startingAir = 0f;

            while (!parseSucceeded)
            {
                try
                {
                    startingAir = float.Parse(Console.ReadLine());
                    parseSucceeded = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("It seems like you didn't give me a number..Try Again");
                }
            }

            return startingAir;
        }

        private void displayLicensesOfVehiclesInGarage()
        {
            Console.WriteLine("Please select the status of vehicles you would like to display:");
            string[] optionalVehicleStatus = Enum.GetNames(typeof(Garage.VehicleInformation.eVehicleStatus));

            for (int i = 0; i < optionalVehicleStatus.Length; i++)
            {
                optionalVehicleStatus[i] = seperateEnumWords(optionalVehicleStatus[i]).ToString();
                int amountOfDashes = r_MaxDashes - optionalVehicleStatus[i].Length; /// We calculate amount of dashes to print in each line of the menu
                string pad = new string('-', amountOfDashes);

                Console.WriteLine("{2} {1}> {0}", i + 1, pad, optionalVehicleStatus[i]);
            }

            int selectionInput = getSelection(1, 4);

            foreach (Garage.VehicleInformation vehicleInfo in r_Garage.AllVehiclesData)
            {
                if (selectionInput == 4)
                {
                    Console.WriteLine(vehicleInfo.Vehicle.LicenseNumber);
                }
                else if (vehicleInfo.Status == (Garage.VehicleInformation.eVehicleStatus)selectionInput)
                {
                    Console.WriteLine(vehicleInfo.Vehicle.LicenseNumber);
                }
            }
        }

        private Garage.VehicleInformation getGarageDetailsByLicense()
        {
            Garage.VehicleInformation vehicleDetails = null;

            while (vehicleDetails == null)
            {
                Console.WriteLine("Please provide the vehicle's license number");
                string license = getNumberString();
                vehicleDetails = r_Garage.GetVehicleByLicense(license);

                if (vehicleDetails == null)
                {
                    Console.WriteLine("No matching vehicle found, please try again: ");
                }
            }

            return vehicleDetails;
        }

        private void changeVehicleStatus()
        {
            Garage.VehicleInformation chosenVehicle = getGarageDetailsByLicense();
            string[] optionalVehicleStatus = Enum.GetNames(typeof(Garage.VehicleInformation.eVehicleStatus));

            for (int i = 0; i < optionalVehicleStatus.Length; i++)
            {
                optionalVehicleStatus[i] = seperateEnumWords(optionalVehicleStatus[i]).ToString();
                int amountOfDashes = r_MaxDashes - optionalVehicleStatus[i].Length; // We calculate amount of dashes to print in each line of the menu
                string pad = new string('-', amountOfDashes);

                Console.WriteLine("{2} {1}> {0}", i + 1, pad, optionalVehicleStatus[i]);
            }

            int newStatus = getSelection(1, 3);
            r_Garage.ModifyVehicleStatus(chosenVehicle, (Garage.VehicleInformation.eVehicleStatus)(newStatus + 1));
        }

        private void inflateVehicleWheelsToMaximumAirCapacity()
        {
            string licenseNumber = getNumberString();
            Vehicle vehicleByLicense = getVehicleByLicenseNumber(licenseNumber);

            // If we found the vehicle we searched for, meaning it's not null 
            if (vehicleByLicense != null)
            {
                r_Garage.InflateWheelsToMaximumAirCapacity(vehicleByLicense);
            }
            else
            {
                Console.WriteLine("Vehicle with driving license {0} not found", licenseNumber);
            }
        }

        private void refillEnergyToVehicle(Garage.eFunctionalities i_TypeOfRefill)
        {
            Console.WriteLine("Please provide the vehicle's license number");
            string licenseNumber = getNumberString();
            Vehicle vehicleByLicense = getVehicleByLicenseNumber(licenseNumber);

            // If we found the vehicle we searched for, meaning it's not null 
            if (vehicleByLicense != null)
            {
                if (vehicleByLicense.Engine is FuelEngine fuelEngine)
                {
                    if (i_TypeOfRefill == Garage.eFunctionalities.RefuelVehicle)
                    {
                        float amountToRefuel = getEnergyAmountToAdd(0, fuelEngine.PossibleAmountOfFuelToAdd);
                        r_Garage.RefillEnergy(vehicleByLicense, amountToRefuel);
                    }
                    else
                    {
                        Console.WriteLine("This vehicle doesn't have a Fuel Engine");
                    }
                }
                else if (vehicleByLicense.Engine is ElectricEngine electricEngine)
                {
                    if (i_TypeOfRefill == Garage.eFunctionalities.RechargeVehicle)
                    {
                        float amountToRecharge = getEnergyAmountToAdd(0, electricEngine.PossibleAmountOfTimeToCharge);
                        r_Garage.RefillEnergy(vehicleByLicense, amountToRecharge);
                    }
                    else
                    {
                        Console.WriteLine("This vehicle doesn't have a Fuel Engine");
                    }
                }
            }
            else
            {
                Console.WriteLine("Vehicle with driving license {0} not found", licenseNumber);
            }
        }

        private bool garageIsEmpty()
        {
            bool isEmpty = r_Garage.AllVehiclesData.Count == 0;

            if (isEmpty)
            {
                Console.WriteLine("The garage doesn't contain any vehicle {0}", Environment.NewLine);
            }

            return isEmpty;
        }

        private void showDataOfAVehicle()
        {
            Console.WriteLine("Please provide the vehicle's license number");
            string licenseNumber = getNumberString();
            Vehicle vehicleByLicense = getVehicleByLicenseNumber(licenseNumber);

            // If we found the vehicle we searched for, meaning its not null 
            if (vehicleByLicense != null)
            {
                Console.WriteLine(vehicleByLicense);
            }
            else
            {
                Console.WriteLine("Vehicle with driving license {0} not found", licenseNumber);
            }

            Console.WriteLine(Environment.NewLine);
        }
    }
}