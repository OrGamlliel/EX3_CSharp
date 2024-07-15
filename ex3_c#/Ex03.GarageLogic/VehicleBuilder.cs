namespace Ex03.GarageLogic
{
    internal class VehicleBuilder
    {
        public Vehicle BuildNewVehicleByTypeAndPlateNumber(string i_PlateNumber, eVehicleType i_VehicleType)
        {
            Vehicle vehicle;
            const bool v_IsElectric = true;

            switch (i_VehicleType)
            {
                case eVehicleType.ElectricBike:
                    vehicle = new Bike(i_PlateNumber, v_IsElectric);
                    break;
                case eVehicleType.FuelBike:
                    vehicle = new Bike(i_PlateNumber, !v_IsElectric);
                    break;
                case eVehicleType.ElectricCar:
                    vehicle = new Car(i_PlateNumber, v_IsElectric);
                    break;
                case eVehicleType.FuelCar:
                    vehicle = new Car(i_PlateNumber, !v_IsElectric);
                    break;
                case eVehicleType.Truck:
                    vehicle = new Truck(i_PlateNumber);
                    break;
                default:
                    vehicle = null;
                    break;
            }

            return vehicle;
        }
    }
}
