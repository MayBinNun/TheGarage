﻿using System;
using Ex03.GarageLogic;
using static Ex03.GarageLogic.FuelEngine;
using static Ex03.GarageLogic.Garage;

namespace Ex03.ConsoleUI
{
    public class GarageUI
    {
        private readonly Garage r_MyGarage = new Garage();

        string m_MainMenu = "What would you like to do? Pleace choose a number <1-8> : \n" +
                           "1 - Enter a new vehicle \n" +
                           "2 - View the list of vehicles in the garage (by license number) \n" +
                           "3 - Fueling vehicle (fuel vehicles only) \n" +
                           "4 - Charge your vehicle (Electric vehicles only) \n" +
                           "5 - Fill up Air pressure vehicle tires \n" +
                           "6 - Get vehicle information \n" +
                           "7 - Change vehicle status \n" +
                           "8 - Exit \n";


        public void GargeOpen()
        {
            bool openGarage = true;

            Console.Clear();
            Console.WriteLine("Hello and welcome to our Garage! \n");

            while (openGarage)
            {
                Console.WriteLine(m_MainMenu);
                
                string userAnswer = Console.ReadLine();
                int userChoise = 0;
                bool tryToParse = int.TryParse(userAnswer, out userChoise);

                while (!tryToParse || tryToParse && !(userChoise > 0 && userChoise < 9)) {
                    Console.WriteLine("You made a wrong choice, please choose a number from 1-8: ");
                    userAnswer = Console.ReadLine();
                    tryToParse = int.TryParse(userAnswer, out userChoise);
                }

                Console.Clear();
                switch (userChoise)
                {
                    case 1:
                        EnterNewCar.EnterNewCarUI(r_MyGarage);
                        break;
                    case 2:
                        CurrentVehiclesUI.GetCurrentVehiclesUI(r_MyGarage);
                        break;
                    case 3:
                        FillUpEnergyUI.FuelVehical(r_MyGarage);
                        break;
                    case 4:
                        FillUpEnergyUI.ChargeYourVehicle(r_MyGarage);
                        break;
                    case 5:
                        FillUpTiresAirPressure();
                        break;
                    case 6:
                       CurrentVehiclesUI.GetVehicleInformation(r_MyGarage);
                        break;
                    case 7:
                        ChangeVehicleCondition();
                        break;
                    case 8:
                        Console.WriteLine("Thank you, have a nice day!");
                        openGarage = !true;
                        break;
                }
            }  
        }

        public static string AskForLicenseNum()
        {
            bool validLicenseNum = !true;
            string userAnswer = string.Empty;

            Console.WriteLine("Please enter your license number:");

            while (!validLicenseNum)
            {
                try
                {
                    userAnswer = Console.ReadLine();
                    validLicenseNum = UIValidation.IsANumberValidation(userAnswer);
                }
                catch (FormatException fe)
                {
                    Console.WriteLine("This is not valid license number, please use only numbers:");
                    validLicenseNum = !true;
                }
            }

            return userAnswer;
        }

        public static int AskForAmount()
        {
            string userAnswer = string.Empty;
            bool validAmount = !true;
            int amountToCharge = 0;

            while (!validAmount)
            {
                try
                {
                    validAmount = true;
                    userAnswer = Console.ReadLine();
                    amountToCharge = UIValidation.EnergyAmount(userAnswer);
                }
                catch (FormatException fe)
                {
                    Console.WriteLine(fe.Message + " Please use only numbers:");
                    validAmount = !true;
                }
            }

            return amountToCharge;
        }

        public void FillUpTiresAirPressure()
        {
            string licenseNumber = AskForLicenseNum();

            try
            {
                r_MyGarage.FillUpTires(licenseNumber);
                Console.WriteLine("The wheels were successfully filled! \n");
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine(ane.Message + " Filling the wheels failed, maybe you filled them above capacity? \n");
            }
            
        }


        public void ChangeVehicleCondition()
        {
            string licenseNumber = AskForLicenseNum();
            string userAnswer = string.Empty;
            bool validCondition = !true;

            Console.WriteLine("Please enter the current vehicle status <Repair, Fix, Paid>");

            while (!validCondition)
            {
                userAnswer = Console.ReadLine();
                eCarCondition tryToParse;
                validCondition = Enum.TryParse(userAnswer, out tryToParse);

                if (!validCondition)
                {
                    Console.WriteLine("Please choose one of the following <Repair, Fix, Paid>:");
                }
            }

            try
            {
                r_MyGarage.ChangeVehicleCondition(licenseNumber, userAnswer);
                Console.WriteLine("Vehicle status successfully updated \n");
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine(ane.Message);
            }
            
        }
    }
}
