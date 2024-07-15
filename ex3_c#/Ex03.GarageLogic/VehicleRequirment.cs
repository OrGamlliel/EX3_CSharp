namespace Ex03.GarageLogic
{
    public class VehicleRequirement
    {
        private readonly eRequirementType r_RequirementType;
        private readonly string r_RequestMessage;
        private readonly int r_NumOfOptions;

        public VehicleRequirement(eRequirementType i_RequirementType, string i_RequestMessage, int i_NumOfOptions)
        {
            r_RequirementType = i_RequirementType;
            r_RequestMessage = i_RequestMessage;
            r_NumOfOptions = i_NumOfOptions;
        }

        public eRequirementType RequirmentType
        {
            get { return r_RequirementType; }
        }

        public string RequestMessage
        {
            get { return r_RequestMessage; }
        }

        public int NumOfOptions
        {
            get { return r_NumOfOptions; }
        }
    }
}
