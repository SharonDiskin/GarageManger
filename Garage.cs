using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly List<VehicleInformation> r_AllVehiclesData;

        public enum eFunctionalities
        {
            AddNewVehicle = 1,
            DisplayLicenses,
            ChangeVehicleStatus,
            InflateWheelsToMaximum,
            RefuelVehicle,
            RechargeVehicle,
            DisplayVehicleDetails,
            Exit
        }

        public Garage()
        {
            r_AllVehiclesData = new List<VehicleInformation>();
        }

        public void AddNewVehicleToGarage(Vehicle i_VehicleToAdmit, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            if (i_VehicleToAdmit == null)
            {
                throw new NullReferenceException("vehicle is null");
            }

            r_AllVehiclesData.Add(new VehicleInformation(i_OwnerName, i_OwnerPhoneNumber, i_VehicleToAdmit));
        }

        public List<VehicleInformation> AllVehiclesData
        {
            get
            {
                return r_AllVehiclesData;
            }
        }

        public Garage.VehicleInformation GetVehicleByLicense(string i_LicenseToSearch)
        {
            Garage.VehicleInformation vehicleToReturn = null;

            foreach (Garage.VehicleInformation vehicleDetails in r_AllVehiclesData)
            {
                if (vehicleDetails.Vehicle.LicenseNumber == i_LicenseToSearch)
                {
                    vehicleToReturn = vehicleDetails;
                    break;
                }
            }

            return vehicleToReturn;
        }

        public void ModifyVehicleStatus(VehicleInformation i_VehicleToModify, VehicleInformation.eVehicleStatus i_NewStatus)
        {
            i_VehicleToModify.Status = i_NewStatus;
        }

        public class VehicleInformation
        {
            private readonly string r_OwnerName;
            private readonly string r_OwnerPhoneNumber;
            private readonly Vehicle r_Vehicle;
            private eVehicleStatus m_VehicleStatus;

            public enum eVehicleStatus
            {
                InRepair = 1,
                Fixed = 2,
                PaidFor = 3
            }

            public VehicleInformation(string i_Owner, string i_OwnerPhone, Vehicle i_VehicleToAdd)
            {
                r_OwnerName = i_Owner;
                r_OwnerPhoneNumber = i_OwnerPhone;
                r_Vehicle = i_VehicleToAdd;
                m_VehicleStatus = eVehicleStatus.InRepair;
            }

            public string Owner
            {
                get
                {
                    return r_OwnerName;
                }
            }

            public string OwnerPhone
            {
                get
                {
                    return r_OwnerPhoneNumber;
                }
            }

            public eVehicleStatus Status
            {
                get
                {
                   return m_VehicleStatus;
                }
                set
                {
                    m_VehicleStatus = value;
                }
            }

            public Vehicle Vehicle
            {
                get
                {
                    return r_Vehicle;
                }
            }

            public override string ToString()
            {
                return string.Format(
"{0}{1}Garage details:{1}owner name: {2}, owner phone: {3}, current status: {4}",
r_Vehicle,
Environment.NewLine,
r_OwnerName,
r_OwnerPhoneNumber,
m_VehicleStatus);
            }
        }

        public void InflateWheelsToMaximumAirCapacity(Vehicle i_Vehicle)
        {
            i_Vehicle.InflateAllWheelsToMax();
        }

        public void RefillEnergy(Vehicle i_Vehicle, float i_AmountToAdd)
        {
            i_Vehicle.Engine.CurrentCapacity += i_AmountToAdd;
        }
    }
}
