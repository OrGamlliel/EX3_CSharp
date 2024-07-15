using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class LogicEngineManager
    {
        private readonly Garage r_Garage;

        public LogicEngineManager()
        {
            r_Garage = new Garage();
        } 

        public bool IsGarageEmpty()
        {
            return r_Garage.IsGarageEmpty();
        }

        public bool isVehicleInGarage(string i_VehiclePlateNumber)
        {
            return r_Garage.IsVehicleInGarage(i_VehiclePlateNumber);
        }

        public List<string> GetAllVehiclePlateNumbersInGarage()
        {
            return r_Garage.GetPlateNumbers();
        }

        public List<string> GetVehiclePlateNumbersByStatus(eVehicleStatus i_Status)
        {
            return r_Garage.GetPlateNumbersByStatus(i_Status);
        }

        public bool UpdateVehicleStatus(string i_VehiclePlateNumber, eVehicleStatus i_NewCarStatus)
        {
            return r_Garage.UpdateVehicleStatus(i_VehiclePlateNumber, i_NewCarStatus);
        }

        public bool InflateVehicleWheelsToMax(string i_VehiclePlateNumber)
        {
            return r_Garage.InflateVehicleWheels(i_VehiclePlateNumber);
        }

        public bool RefuelVehicle(string i_VehiclePlateNumber, eFuelType i_FuelType, float i_AmountOfFuelToFill)
        {
            return r_Garage.FillVehicleFuelTank(i_VehiclePlateNumber, i_FuelType, i_AmountOfFuelToFill);
        }

        public bool CheckVehiclePlateNumberOnFuel(string i_VehiclePlateNumber)
        {
            return r_Garage.CheckVehiclePlateNumberAndFuel(i_VehiclePlateNumber);
        }

        public bool CheckVehiclePlateNumberOnElectric(string i_VehiclePlateNumber)
        {
            return r_Garage.CheckVehiclePlateNumberAndElectric(i_VehiclePlateNumber);
        }

        public bool RechargeVehicle(string i_VehiclePlateNumber, float i_AmountOfBatteryToCharge)
        {
            return r_Garage.ChargeVehicleBattery(i_VehiclePlateNumber, i_AmountOfBatteryToCharge);
        }

        public bool GetFullVehicleDetailsByPlateNumber(string i_VehiclePlateNumber,out string io_VehicleInformation)
        {
            return r_Garage.GetVehicleData(i_VehiclePlateNumber,out io_VehicleInformation);
        }

        public List<VehicleRequirement> AddVehicleAndGetRequirements(string i_PlateNumber, eVehicleType i_VehicleType)
        {
            return r_Garage.AddVehicleAndGetRequirements(i_PlateNumber, i_VehicleType);
        }

        public void SetVehicleReportValues(List<string> i_ArgumentList)
        {
            r_Garage.SetVehicleReportValues(i_ArgumentList);
        }
    }
}