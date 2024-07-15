using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class VehicleReport
    {
        private string m_VehicleOwnerName;
        private string m_VehicleOwnerPhoneNumber;
        private eVehicleStatus m_VehicleStatus;
        private readonly Vehicle r_Vehicle;

        public VehicleReport(Vehicle i_Vehicle)
        {
            r_Vehicle = i_Vehicle;
        }

        public void SetValues(List<string> i_ArgumentList)
        {
            VehicleOwnerName = i_ArgumentList[0];
            m_VehicleOwnerPhoneNumber = i_ArgumentList[1];
            m_VehicleStatus = eVehicleStatus.Repairing;
            r_Vehicle.SetVehicleValues(i_ArgumentList);
        }

        public string VehicleOwnerName
        {
            get { return m_VehicleOwnerName; }
            set
            {
                if (isAllLetters(value) && value.Length > 0)
                {
                    m_VehicleOwnerName = value;
                }
                else
                {
                    throw new System.ArgumentException("Invalid vehicle owner name!");
                }
            }
        }

        public Vehicle Vehicle
        {
            get { return r_Vehicle; }
        }

        public eVehicleStatus VehicleStatus
        {
            get { return m_VehicleStatus; }
            set { m_VehicleStatus = value; }
        }

        public List<VehicleRequirement> GetVehicleReportRequirements()
        {
            List<VehicleRequirement> vehicleReportRequirements = new List<VehicleRequirement>();

            vehicleReportRequirements.Add(new VehicleRequirement(eRequirementType.String, "Owner's name: ", 1));
            vehicleReportRequirements.Add(new VehicleRequirement(eRequirementType.String, "Owner's phone number: ", 1));
            vehicleReportRequirements.AddRange(r_Vehicle.GetVehicleRequirements());

            return vehicleReportRequirements;
        }

        public override string ToString()
        {
            return string.Format(@"
Plate number: {0}
Model name: {1}
Owner's name: {2}
Vehicle's status: {3}

Wheels info: 
{4}
{5}
", r_Vehicle.PlateNumber, r_Vehicle.Model, m_VehicleOwnerName, m_VehicleStatus, r_Vehicle.GetAllWheelsToString(), r_Vehicle.ToString());
        }

        private bool isAllLetters(string i_String)
        {
            bool isAllLetters = true;

            foreach (char charcter in i_String)
            {
                if (!char.IsLetter(charcter))
                {
                    isAllLetters = false;
                }
            }

            return isAllLetters;
        }
    }
}
