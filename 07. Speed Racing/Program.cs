using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace _07.Speed_Racing
{
    class Program
    {
        static void Main(string[] args)
        {
            // Solution to the problem: https://github.com/RAstardzhiev/Software-University-SoftUni/tree/master/C%23%20OOP%20Basics/Defining%20Classes%20-%20Exercise
            // TODO: Define a class Car that keeps track of a car’s Model
            // TODO: fuel amount - where the fuel amount should be printed to two decimal places after the separator
            // TODO: fuel consumption for 1 kilometer and distance traveled
            // TODO: Implement a method in the Car class to calculate whether or not a car can move that distance
            // TODO: if it can the car’s fuel amount should be reduced by the amount of used fuel
            // TODO: its distance traveled should be increased by the amount of kilometers traveled, 
            // TODO: otherwise the car should not move (Its fuel amount and distance traveled should stay the same)
            // TODO: and you should print on the console “Insufficient fuel for the drive”
            // TODO: A Car’s Model is unique - there will never be 2 cars with the same model.
            // TODO: Receive user input from console: On the first line of the input you will receive a number N – the number of cars you need to track,
            // on each of the next N lines you will receive information for a car in the following format “<Model> <FuelAmount> <FuelConsumptionFor1km>”, all cars start at 0 kilometers traveled.
            // TODO: After the N lines, until the command “End” is received, you will receive a commands in the following format “Drive <CarModel>  <amountOfKm>”.
            // TODO: After the “End” command is received, print each car and its current fuel amount and distance traveled in the format “<Model> <fuelAmount>  <distanceTraveled>”, where the fuel amount should be printed to two decimal places after the separator.

            /*
            
            2 sets of test data, to paste into console window (ensure enter after End):

            2
            AudiA4 23 0,3
            BMW-M2 45 0,42
            Drive BMW-M2 56
            Drive AudiA4 5
            Drive AudiA4 13
            End


            3
            AudiA4 18 0,34
            BMW-M2 33 0,41
            Ferrari-488Spider 50 0,47
            Drive Ferrari-488Spider 97
            Drive Ferrari-488Spider 35
            Drive AudiA4 85
            Drive AudiA4 50
            End

            */

            CarsCollection carColl = new CarsCollection();
            Car car;

            // prep vars for user input
            string input;
            // line number to detect 1st line
            int line = 0;
            int carsNumber = 0;
            string model;
            float fuelAmount;
            float distanceToDrive;
            float litersOfFuelBurntPerKm;

            while (true)
            {
                input = Console.ReadLine();
                // depending on number N provided in first line, next N lines should be car models
                string[] tokens = input.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                if (tokens.Count() == 1 && String.Equals(tokens[0], "end", StringComparison.InvariantCultureIgnoreCase))
                {
                    // 1 token, and its "end" - it's end of user input
                    break;
                }
                else if (tokens.Count() == 1 && Int32.TryParse(input, out carsNumber) && line == 0)
                {
                    // 1st line, contains one token, and it's integer number - it's car number. Actual variable assignment inside if statement

                }
                else if (tokens.Count() == 3 && !String.Equals(tokens[0], "drive", StringComparison.InvariantCultureIgnoreCase))
                {
                    // 3 tokens, not starting with drive - it's a new car model to add
                    model = tokens[0];
                    fuelAmount = float.Parse(tokens[1]);
                    litersOfFuelBurntPerKm = float.Parse(tokens[2]);

                    car = new Car(model, fuelAmount, litersOfFuelBurntPerKm);
                    // add a new car to car collection object
                    bool success = carColl.TryAdd(car);
                }
                else if (tokens.Count() == 3 && String.Equals(tokens[0], "drive", StringComparison.InvariantCultureIgnoreCase))
                {
                    // 3 tokens, STARTING with drive - let's drive existing cars
                    model = tokens[1];
                    distanceToDrive = float.Parse(tokens[2]);

                    List<Car> carsFound = carColl.FindCarByModel(model);
                    // found one car of that model and can this car can travel that far (trveled in Try method, actually - if it returns true)
                    if (carsFound.Count() == 1 && carsFound[0].TryTravelDistance(distanceToDrive))
                    {
                        // nothing to do
                    }
                    else if (carsFound.Count() > 1 || carsFound.Count() == 0)
                    {
                        Console.WriteLine("Something wrong with model names, found {0} matching models", carsFound.Count());
                    }
                    else if (!carsFound[0].TryTravelDistance(distanceToDrive))
                    {
                        Console.WriteLine("Insufficient fuel for the drive");
                    }
                }
                else
                {
                    // unknown format
                    Console.WriteLine("Unknown line format, ignoring");
                }
                line++;
            }
            foreach (Car myCar in carColl)
            {
                Console.WriteLine("{0} - {1:F2} - {2}",myCar.Model,myCar.GetFuelAmount(),myCar.GetDistanceTraveled());
            }
        }
    }
}
