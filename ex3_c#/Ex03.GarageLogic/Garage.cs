using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex03.GarageLogic
{
    internal class Garage
    {
        private readonly Dictionary<string, VehicleReport> r_VehicleReports;
        private readonly VehicleBuilder r_VehicleBuilder;
        private VehicleReport m_AwaitingVehicleReport;

        public Garage()
        {
            r_VehicleReports = new Dictionary<string, VehicleReport>();
            r_VehicleBuilder = new VehicleBuilder();
        }

        public bool IsGarageEmpty()
        {
            return r_VehicleReports.Count == 0;
        }

        public bool IsVehicleInGarage(string i_VehiclePlateNumber)
        {
            VehicleReport currentVehicleReport;
            bool isVehicleInGarage;

            isVehicleInGarage = r_VehicleReports.TryGetValue(i_VehiclePlateNumber, out currentVehicleReport);
            if (isVehicleInGarage)
            {
                currentVehicleReport.VehicleStatus = eVehicleStatus.Repairing;
            }

            return isVehicleInGarage;
        }

        public List<VehicleRequirement> AddVehicleAndGetRequirements(string i_PlateNumber, eVehicleType i_VehicleType)
        {
            m_AwaitingVehicleReport = new VehicleReport(r_VehicleBuilder.BuildNewVehicleByTypeAndPlateNumber(i_PlateNumber, i_VehicleType));

            return m_AwaitingVehicleReport.GetVehicleReportRequirements();
        }

        public void SetVehicleReportValues(List<string> i_ArgumentList)
        {
            m_AwaitingVehicleReport.SetValues(i_ArgumentList);
            r_VehicleReports.Add(m_AwaitingVehicleReport.Vehicle.PlateNumber, m_AwaitingVehicleReport);
        }

        public List<string> GetPlateNumbers()
        {
            return r_VehicleReports.Keys.ToList<string>();
        }

        public List<string> GetPlateNumbersByStatus(eVehicleStatus i_VehicleStatus)
        {
            List<string> plateNumbersList = new List<string>();

            foreach (VehicleReport vehicleReport in r_VehicleReports.Values)
            {
                if (vehicleReport.VehicleStatus == i_VehicleStatus)
                {
                    plateNumbersList.Add(vehicleReport.Vehicle.PlateNumber);
                }
            }

            return plateNumbersList;
        }

        public bool UpdateVehicleStatus(string i_VehiclePlateNumber, eVehicleStatus i_VehicleStatus)
        {
            VehicleReport vehicleReport;
            bool isVehicleInGarage;

            isVehicleInGarage = r_VehicleReports.TryGetValue(i_VehiclePlateNumber, out vehicleReport);
            if (isVehicleInGarage)
            {
                vehicleReport.VehicleStatus = i_VehicleStatus;
            }

            return isVehicleInGarage;
        }

        public bool InflateVehicleWheels(string i_VehiclePlateNumber)
        {
            VehicleReport vehicleReport;
            bool isVehicleInGarage;

            isVehicleInGarage = r_VehicleReports.TryGetValue(i_VehiclePlateNumber, out vehicleReport);
            if (isVehicleInGarage)
            {
                vehicleReport.Vehicle.InflateAllWheels();
            }

            return isVehicleInGarage;
        }

        public bool CheckVehiclePlateNumberAndElectric(string i_VehiclePlateNumber)
        {
            VehicleReport vehicleReport;
            bool isVehicleInGarage;

            isVehicleInGarage = r_VehicleReports.TryGetValue(i_VehiclePlateNumber, out vehicleReport);
            if (vehicleReport.Vehicle.EnergySource is FuelEnergySource)
            {
                throw new ArgumentException("Error: vehicle is fuel based, cant charge it");
            }

            return isVehicleInGarage;
        }

        public bool CheckVehiclePlateNumberAndFuel(string i_VehiclePlateNumber)
        {
            VehicleReport vehicleReport;
            bool isVehicleInGarage;

            isVehicleInGarage = r_VehicleReports.TryGetValue(i_VehiclePlateNumber, out vehicleReport);
            if (vehicleReport.Vehicle.EnergySource is ElectricEnergySource)
            {
                throw new ArgumentException("Error: vehicle is electric, cant add fuel");
            }

            return isVehicleInGarage;
        }

        public bool FillVehicleFuelTank(string i_VehiclePlateNumber, eFuelType i_FuelType, float i_FuelAmount)
        {
            VehicleReport vehicleReport;
            bool isVehicleInGarage;

            isVehicleInGarage = r_VehicleReports.TryGetValue(i_VehiclePlateNumber, out vehicleReport);
            (vehicleReport.Vehicle.EnergySource as FuelEnergySource).FillFuel(i_FuelAmount, i_FuelType);

            return isVehicleInGarage;
        }

        public bool ChargeVehicleBattery(string i_VehiclePlateNumber, float i_ChargeAmount)
        {
            VehicleReport vehicleReport;
            bool isVehicleInGarage;

            isVehicleInGarage = r_VehicleReports.TryGetValue(i_VehiclePlateNumber, out vehicleReport);
            (vehicleReport.Vehicle.EnergySource as ElectricEnergySource).ChargeBattery(i_ChargeAmount);

            return isVehicleInGarage;
        }

        public bool GetVehicleData(string i_VehiclePlateNumber, out string o_VehicleInformation)
        {
            VehicleReport vehicleReport;
            bool isVehicleInGarage;

            o_VehicleInformation = "";
            isVehicleInGarage = r_VehicleReports.TryGetValue(i_VehiclePlateNumber, out vehicleReport);
            if (isVehicleInGarage)
            {
                o_VehicleInformation = vehicleReport.ToString();
            }

            return isVehicleInGarage;
        }
    }
}
