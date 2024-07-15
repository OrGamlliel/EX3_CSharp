using Ex03.GarageLogic;
using System.Text;
using System;
using System.Collections.Generic;

namespace Ex03.ConsoleUI
{
    public class UserInterfaceManager
    {
        private readonly LogicEngineManager r_LogicEngineManager;
        private bool m_IsGarageOpen;
        private readonly StringBuilder r_StringBuilder;

        public UserInterfaceManager()
        {
            r_LogicEngineManager = new LogicEngineManager();
            r_StringBuilder = new StringBuilder();
            m_IsGarageOpen = true;
        }

        public void StartProgram()
        {
            while (m_IsGarageOpen)
            {
                handleMenuUserChoice();
            }
        }

        private void handleMenuUserChoice()
        {
            int mainMenuUserChoice;
            eUserInterfaceOptions userChoice;

            printMainMenuOptions();
            mainMenuUserChoice = getUserChoiceFromRange((int)Enum.GetValues(typeof(eUserInterfaceOptions)).GetValue(0), Enum.GetValues(typeof(eUserInterfaceOptions)).Length);
            userChoice = (eUserInterfaceOptions)mainMenuUserChoice;
            try
            {
                activateUserChoice(userChoice);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void activateUserChoice(eUserInterfaceOptions i_UserChoice)
        {
            switch (i_UserChoice)
            {
                case eUserInterfaceOptions.AddNewVehicle:
                    startAddingVehicleProcess();
                    break;
                case eUserInterfaceOptions.ShowAllPlateNumber:
                    showRequestedVehiclesPlates();
                    break;
                case eUserInterfaceOptions.UpdateCarStatus:
                    updateRequestedVehicleStatus();
                    break;
                case eUserInterfaceOptions.InflateTiers:
                    inflateRequestedVehicleTier();
                    break;
                case eUserInterfaceOptions.Refuel:
                    refuelRequestedVehicle();
                    break;
                case eUserInterfaceOptions.Recharge:
                    rechargeRequestedVehicle();
                    break;
                case eUserInterfaceOptions.ShowCarDetails:
                    showRequestedVehiclesDetails();
                    break;
                case eUserInterfaceOptions.Quit:
                    m_IsGarageOpen = false;
                    break;
            }
        }

        private string getVehiclePlateNumber()
        {
            string vehiclePlate;
            bool isPlateNumberValid = true;

            do
            {
                if (!isPlateNumberValid)
                {
                    Console.Write("Invalid vehicle plate number, ");
                }
                Console.Write("Please enter vehicle plate's number: ");
                vehiclePlate = Console.ReadLine();
                isPlateNumberValid = vehiclePlate.TrimEnd().Length != 0;
            } while (!isPlateNumberValid);

            return vehiclePlate;
        }

        private bool checkIfGarageEmpty()
        {
            bool isGarageEmpty;

            isGarageEmpty = r_LogicEngineManager.IsGarageEmpty();
            if (isGarageEmpty)
            {
                System.Console.WriteLine(String.Format("Garage is still empty, please add vehicles first{0}", System.Environment.NewLine));
            }

            return isGarageEmpty;
        }

        private void startAddingVehicleProcess()
        {
            string vehiclePlate;

            vehiclePlate = getVehiclePlateNumber();
            if (r_LogicEngineManager.isVehicleInGarage(vehiclePlate))
            {
                System.Console.Clear();
                System.Console.WriteLine(String.Format("Vehicle is already in the garage{0}status has changed to reparing{0}", System.Environment.NewLine));
            }
            else
            {
                addNewVehicle(vehiclePlate);
            }
        }

        private void addNewVehicle(string i_VehiclePlate)
        {
            eVehicleType vehicleType;
            List<VehicleRequirement> requirements;
            List<string> userInputs = new List<string>();

            System.Console.WriteLine("Please choose vehicle type");
            printEnumList(typeof(eVehicleType));
            vehicleType = (eVehicleType)getUserChoiceFromRange((int)Enum.GetValues(typeof(eVehicleType)).GetValue(0), Enum.GetValues(typeof(eVehicleType)).Length);
            requirements = r_LogicEngineManager.AddVehicleAndGetRequirements(i_VehiclePlate, vehicleType);
            getVehicleParametersFromUser(requirements, userInputs);
            System.Console.Clear();
            try
            {
                r_LogicEngineManager.SetVehicleReportValues(userInputs);
                System.Console.WriteLine(String.Format("vehicle was added successfully to the garage! {0}", System.Environment.NewLine));
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ValueOutOfRangeException ex)
            {
                Console.WriteLine(string.Format("{0} - min value: {1}, max value: {2}", ex.Message, ex.MinValue, ex.MaxValue));
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(String.Format("failed to add vehicle: {0}{1}", ex.Message, System.Environment.NewLine));
            }
        }

        private void getVehicleParametersFromUser(List<VehicleRequirement> i_Requirements, List<string> i_UserInputs)
        {
            System.Console.WriteLine("Please enter the following details -");
            foreach (VehicleRequirement requirement in i_Requirements)
            {
                i_UserInputs.Add(handleRequirement(requirement));
            }
        }

        private string handleRequirement(VehicleRequirement i_Requirement)
        {
            string userInput;
            bool isValidInput = true;

            do
            {
                if (!isValidInput)
                {
                    System.Console.Write("Invalid input! try again, ");
                }

                System.Console.Write(i_Requirement.RequestMessage);
                if (i_Requirement.RequirmentType == eRequirementType.Enum || i_Requirement.RequirmentType == eRequirementType.Boolean)
                {
                    userInput = getUserChoiceFromRange((int)Enum.GetValues(typeof(eRequirementType)).GetValue(0) + 1, i_Requirement.NumOfOptions).ToString();
                }
                else
                {
                    userInput = System.Console.ReadLine();
                    if (i_Requirement.RequirmentType == eRequirementType.Numeric)
                    {
                        isValidInput = checkIfNumeric(userInput);
                    }
                    else if (i_Requirement.RequirmentType == eRequirementType.String)
                    {
                        if (userInput.Length == 0)
                        {
                            isValidInput = false;
                        }
                        else
                        {
                            isValidInput = true;
                        }
                    }
                }
            } while (!isValidInput);

            return userInput;
        }

        private bool checkIfNumeric(string i_UserInput)
        {
            float numberForConversion;
            bool isNumeric = false;

            if (i_UserInput.Length != 0)
            {
                isNumeric = float.TryParse(i_UserInput, out numberForConversion);
            }

            return isNumeric;
        }

        private void showRequestedVehiclesPlates()
        {
            eVehicleStatus vehiclesPlatesToShow;
            int userChoice;

            printWhichPlatesList();
            userChoice = getUserChoiceFromRange((int)Enum.GetValues(typeof(eVehicleStatus)).GetValue(0), Enum.GetValues(typeof(eVehicleStatus)).Length + 1);
            if (userChoice < 4)
            {
                vehiclesPlatesToShow = (eVehicleStatus)userChoice;
                printPlatesList(r_LogicEngineManager.GetVehiclePlateNumbersByStatus(vehiclesPlatesToShow));
            }
            else
            {
                printPlatesList(r_LogicEngineManager.GetAllVehiclePlateNumbersInGarage());
            }
        }

        private void updateRequestedVehicleStatus()
        {
            System.Console.Clear();
            if (!checkIfGarageEmpty())
            {
                System.Console.Clear();
                try
                {
                    getVehiclePlateAndSetNewStatus();
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(String.Format("{0}{1}", ex.Message, System.Environment.NewLine));
                }
            }
        }

        private void inflateRequestedVehicleTier()
        {
            string vehiclePlate;
            bool isVehicleInGarage;

            if (!checkIfGarageEmpty())
            {
                vehiclePlate = getVehiclePlateNumber();
                System.Console.Clear();
                try
                {
                    isVehicleInGarage = r_LogicEngineManager.InflateVehicleWheelsToMax(vehiclePlate);
                    if (!isVehicleInGarage)
                    {
                        System.Console.WriteLine(String.Format("Vehicle was not found!{0}", System.Environment.NewLine));
                    }
                    else
                    {
                        System.Console.WriteLine(String.Format("Vehicle tiers were inflated successfully!{0}", System.Environment.NewLine));
                    }
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine(string.Format("{0} - min value: {1}, max value: {2}", ex.Message, ex.MinValue, ex.MaxValue));
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
            }
        }

        private void refuelRequestedVehicle()
        {
            string vehiclePlateNumber;
            eFuelType fuelType;
            float amountOfFuel;
            bool isVehicleInGarage;

            if (!checkIfGarageEmpty())
            {
                vehiclePlateNumber = getVehiclePlateNumber(); 
                System.Console.Clear();
                try
                {
                    isVehicleInGarage = r_LogicEngineManager.CheckVehiclePlateNumberOnFuel(vehiclePlateNumber);
                    if (!isVehicleInGarage)
                    {
                        System.Console.WriteLine(String.Format("Vehicle was not found!{0}", System.Environment.NewLine));
                    }
                    else
                    {
                        System.Console.WriteLine("Please choose fuel type");
                        printEnumList(typeof(eFuelType));
                        fuelType = (eFuelType)getUserChoiceFromRange((int)Enum.GetValues(typeof(eFuelType)).GetValue(0), Enum.GetValues(typeof(eFuelType)).Length);
                        amountOfFuel = getFloatValueFromUser("Invalid amount of fuel, amount of air must be numeric",
                            "Please enter amount of fuel to fill");
                        isVehicleInGarage = r_LogicEngineManager.RefuelVehicle(vehiclePlateNumber, fuelType, amountOfFuel);
                        System.Console.WriteLine("Vehicle was refueled successfully!{0}", System.Environment.NewLine);
                    }
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine(string.Format("{0} - min value: {1}, max value: {2}", ex.Message, ex.MinValue, ex.MaxValue));
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(String.Format("{0}{1}", ex.Message, System.Environment.NewLine));
                }
            }
        }

        private void rechargeRequestedVehicle()
        {
            string vehiclePlateNumber;
            float amountOfHoursToCharge;
            bool isVehicleInGarage;

            if (!checkIfGarageEmpty())
            {
                vehiclePlateNumber = getVehiclePlateNumber();
                System.Console.Clear();
                try
                {
                    isVehicleInGarage = r_LogicEngineManager.CheckVehiclePlateNumberOnElectric(vehiclePlateNumber);
                    System.Console.Clear();
                    if (!isVehicleInGarage)
                    {
                        System.Console.WriteLine(String.Format("Vehicle was not found!{0}", System.Environment.NewLine));
                    }
                    else
                    {
                        amountOfHoursToCharge = getFloatValueFromUser("Invalid amount of hours, amount of hours must be numeric",
                    "Please enter amount of hours to recharge");
                        isVehicleInGarage = r_LogicEngineManager.RechargeVehicle(vehiclePlateNumber, amountOfHoursToCharge);
                        System.Console.Clear();
                        System.Console.WriteLine(String.Format("Vehicle was succesfully charged!{0}", System.Environment.NewLine));
                    }
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine(string.Format("{0} - min value: {1}, max value: {2}", ex.Message, ex.MinValue, ex.MaxValue));
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(String.Format("{0}{1}", ex.Message, System.Environment.NewLine));
                }
            }
        }

        private float getFloatValueFromUser(string i_ErrorMsg, string i_RequestMsg)
        {
            float numaricAmountFromUser;
            string amountFromUser;
            bool isValidParameters = true;

            do
            {
                if (!isValidParameters)
                {
                    Console.WriteLine(i_ErrorMsg);
                }
                Console.WriteLine(i_RequestMsg);
                amountFromUser = Console.ReadLine();
                isValidParameters = float.TryParse(amountFromUser, out numaricAmountFromUser);
            } while (!isValidParameters);

            return numaricAmountFromUser;
        }

        private void showRequestedVehiclesDetails()
        {
            string vehiclePlate, vehicleDetails;

            if (!checkIfGarageEmpty())
            {
                vehiclePlate = getVehiclePlateNumber();
                System.Console.Clear();
                if (r_LogicEngineManager.GetFullVehicleDetailsByPlateNumber(vehiclePlate, out vehicleDetails))
                {
                    System.Console.WriteLine(String.Format("{0}{1}", vehicleDetails, System.Environment.NewLine));
                }
                else
                {
                    System.Console.WriteLine(String.Format("Vehicle was not found!{0}", System.Environment.NewLine));
                }
            }
        }

        private void getVehiclePlateAndSetNewStatus()
        {
            string vehiclePlate;
            eVehicleStatus newStatus;

            vehiclePlate = getVehiclePlateNumber();
            System.Console.WriteLine("Please pick the new status");
            printEnumList(typeof(eVehicleStatus));
            newStatus = (eVehicleStatus)getUserChoiceFromRange((int)Enum.GetValues(typeof(eVehicleStatus)).GetValue(0), Enum.GetValues(typeof(eVehicleStatus)).Length);
            System.Console.Clear();
            if (!r_LogicEngineManager.UpdateVehicleStatus(vehiclePlate, newStatus))
            {
                System.Console.WriteLine(String.Format("Vehicle was not found!{0}", System.Environment.NewLine));
            }
            else
            {
                System.Console.WriteLine(String.Format("Vehicle status was successfully updated{0}", System.Environment.NewLine));
            }
        }

        private void printMainMenuOptions()
        {
            r_StringBuilder.Clear();
            r_StringBuilder.AppendLine("Please choose an option to continue");
            foreach (eUserInterfaceOptions mainMenuOption in Enum.GetValues(typeof(eUserInterfaceOptions)))
            {
                r_StringBuilder.AppendLine(String.Format("{0}.{1}", (int)mainMenuOption, convertMenuEnumIntoString(mainMenuOption)));
            }

            System.Console.WriteLine(r_StringBuilder.ToString());
        }

        private void printPlatesList(List<string> i_StringList)
        {
            System.Console.Clear();
            System.Console.WriteLine("The vehicles' plates are:");
            if (i_StringList.Count == 0)
            {
                System.Console.WriteLine(String.Format("No vehicles with this status!{0}", System.Environment.NewLine));
            }
            else
            {
                foreach (string str in i_StringList)
                {
                    Console.WriteLine(str);
                }
            }

            System.Console.WriteLine(System.Environment.NewLine);
        }

        private void printWhichPlatesList()
        {
            System.Console.Clear();
            r_StringBuilder.Clear();
            r_StringBuilder.AppendLine("Which vehicles would you like to view");
            foreach (eVehicleStatus vehicleStatusOption in Enum.GetValues(typeof(eVehicleStatus)))
            {
                r_StringBuilder.AppendLine(String.Format("{0}.{1}", (int)vehicleStatusOption, vehicleStatusOption));
            }

            r_StringBuilder.AppendLine(String.Format("4. Show All Vehicles"));
            System.Console.WriteLine(r_StringBuilder.ToString());
        }

        private int getUserChoiceFromRange(int i_MinValueOnMenu, int i_MaxValueOnMenu)
        {
            bool isValidOption;
            string userInput;
            int userChoice;

            do
            {
                userInput = System.Console.ReadLine();
                isValidOption = int.TryParse(userInput, out userChoice);
                if (!isValidOption || userChoice > i_MaxValueOnMenu || userChoice < i_MinValueOnMenu)
                {
                     isValidOption = false;
                     System.Console.WriteLine("Invalid input, please choose an option from the list");
                }

            } while (!isValidOption);

            return userChoice;
        }

        private void printEnumList(Type i_EnumType)
        {
            string[] names = Enum.GetNames(i_EnumType);

            r_StringBuilder.Clear();
            for (int i = 0; i < names.Length; i++)
            {
                r_StringBuilder.Append(string.Format("{0}.{1}{2}", i + 1, names.GetValue(i), System.Environment.NewLine));
            }

            System.Console.WriteLine(r_StringBuilder.ToString());
        }

        private string convertMenuEnumIntoString(eUserInterfaceOptions i_MainMenuOption)
        {
            string mainMenuOption;

            switch (i_MainMenuOption)
            {
                case eUserInterfaceOptions.AddNewVehicle:
                    mainMenuOption = "Add new vehicle to garage.";
                    break;
                case eUserInterfaceOptions.ShowAllPlateNumber:
                    mainMenuOption = "Show vehicles' plate numbers.";
                    break;
                case eUserInterfaceOptions.UpdateCarStatus:
                    mainMenuOption = "Update car status.";
                    break;
                case eUserInterfaceOptions.InflateTiers:
                    mainMenuOption = "Inflate car tiers.";
                    break;
                case eUserInterfaceOptions.Refuel:
                    mainMenuOption = "Refuel car.";
                    break;
                case eUserInterfaceOptions.Recharge:
                    mainMenuOption = "Recharge electric car.";
                    break;
                case eUserInterfaceOptions.ShowCarDetails:
                    mainMenuOption = "Show car details.";
                    break;
                case eUserInterfaceOptions.Quit:
                    mainMenuOption = "Quit.";
                    break;
                default:
                    mainMenuOption = "";
                    break;
            }

            return mainMenuOption;
        }
    }
}