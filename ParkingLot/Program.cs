using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace ParkingLot
{
    class Program
    {
        static void Main(string[] args)
        {
            Parking park_obj = new Parking();
            if (args.Length != 0)
            {
                //If argument is passed ,its assumed to be a .txt file.File path should be a full path.
                if (File.Exists(args[0]))
                {
                    if (new FileInfo(args[0]).Length != 0) //check if the file is empty
                    {
                        park_obj.ParseFile(args[0]);
                    }
                    else
                    {
                        Console.WriteLine("Empty file");
                    }
                }
                else
                {
                    Console.WriteLine("Not a valid path");
                }
            }
            else
            {
                //Interactive method
                //Syntax for each request
                Console.WriteLine("Operations Syntax:create_parking_lot,park,leave,status,registration_numbers_for_cars_with_slot_Number"+
                    "registration_numbers_for_cars_with_colour,slot_numbers_for_registration_number,slot_numbers_for_cars_with_colour");
                string input;
                bool retry = false;
                do
                {
                    input = Console.ReadLine();
                    //First operation is to create total slots
                    if (input.StartsWith("create"))
                    {
                        park_obj.CreateTotalSlots(input); //creates total slot 
                        input = Console.ReadLine();
                        while (input != "exit")
                        {
                            park_obj.OperationCheck(input);
                            input = Console.ReadLine();
                        }
                        if (input.Equals("exit"))
                        {
                            //retry = true;
                            break;
                        }
                    }
                    else if (input.Equals("exit"))//to exit the application
                    {
                        //retry = true;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("First step is to define total slots");
                        retry = true;
                    }
                } while (retry);
            }
        }

    }

    //Class maintaing car details
    public class CarInfo
    {
        public string registration_num;
        public string colour;

        public CarInfo(string reg_no, string col)
        {
            registration_num = reg_no;
            colour = col;
        }
    }

    public class Parking
    {
        public List<int> freeSlots = new List<int>(); //Initially freslot = totalslot

        //Master Record maintaing slot details and car details
        public Dictionary<int, CarInfo> parkingInfo = new Dictionary<int, CarInfo>();

        public void ParseFile(string inputFile)
        {
            try
            {
                StreamReader readFile = File.OpenText(inputFile);//file path is a full path
                string content;
                while ((content = readFile.ReadLine()) != null)
                {
                    OperationCheck(content);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("::ParseFile Exception::" + ex.Message);
            }
        }

        public void OperationCheck(string line)
        {
            try
            {
                //Checking for keywords in the request
                if (line.StartsWith("create"))
                {
                    CreateTotalSlots(line);
                }
                else if (line.StartsWith("park"))
                {
                    ParkingMethod(line);
                }
                else if (line.StartsWith("leave"))
                {
                    Parking_ExitUpdateSlotDetails(line);
                }
                else if (line.StartsWith("status"))
                {
                    DisplaySlotDetails();

                }
                else if (line.StartsWith("registration_numbers"))
                {
                    DisplayRegistrationNumber(line);
                }
                else if (line.StartsWith("slot_numbers"))
                {
                    DisplaySlotNumber(line);
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("::OperationCheck Exception::" + ex.Message);
            }
        }
        public void CreateTotalSlots(string total_slot)
        {
            //Initially free slot = total slot
            try
            {
                string[] slotDetail = total_slot.Split(" ");
                int totalSlot = int.Parse(slotDetail[1]);
                for (int i = 1; i <= totalSlot; i++)
                {
                    freeSlots.Add(i);
                }
                freeSlots.Sort();//sorting the list so that first element will be the nearest slot.
            }
            catch (Exception ex)
            {
                Console.WriteLine("::CreateTotalSlots Exception::" + ex.Message);
            }
        }
        public bool ParkingMethod(string parking_details)
        {
            //On vehicle entry collect the car details .Allt the nearest slot and add the details to the master record 
            bool res = false;
            try
            {

                string[] carDetails = parking_details.Split(" ");
                if (carDetails.Length > 0)
                {
                    CarInfo newCar = new CarInfo(carDetails[1], carDetails[2]);
                    int slotNo = freeSlots.First();
                    parkingInfo.Add(slotNo, newCar);
                    freeSlots.Remove(slotNo);//once a slot is added to the master record ,remove the entry from free slot list.
                    res = true;
                }
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine("::ParkingMethod Exception::" + ex.Message);
                return res;
            }
        }
        public void Parking_ExitUpdateSlotDetails(string clear_slot)
        {
            //On vehicle exit ,clear the entry from the master record and add the slot no to the freed slot.
            try
            {
                string[] input = clear_slot.Split(" ");
                int slotNo = int.Parse(input[1]);
                //Remove the Car details and slot details from the record
                foreach (var i in parkingInfo)
                {
                    if (i.Key == slotNo)
                    {
                        parkingInfo.Remove(slotNo);
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                //Adding the same slot no to the freed slot details
                freeSlots.Add(slotNo);
                freeSlots.Sort();
            }
            catch (Exception ex)
            {
                Console.WriteLine("::Parking_ExitUpdateSlotDetails Exception::" + ex.Message);
            }
        }
        public void DisplaySlotDetails()
        {
            //From the master record display the details
            try
            {
                if (parkingInfo.Count > 0)
                {
                    Console.WriteLine("SlotNo RegistrationNo Colour");
                    foreach (var i in parkingInfo)
                    {
                        Console.WriteLine(i.Key.ToString().PadRight(7) + i.Value.registration_num.ToString().PadRight(15) + i.Value.colour.ToString().PadRight(10));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("::DisplaySlotDetails Exception::" + ex.Message);
            }
        }
        public string DisplayRegistrationNumber(string slotDetails)
        {
            //loop through the master record for the details
            string res = null;
            try
            {
                string[] slotInfo = slotDetails.Split(" ");
                int slotNum = 0;
                string colour;
                bool result = int.TryParse(slotInfo[1], out slotNum);
                //to find registration nm of the car in the specified slot
                if (result)
                {
                    foreach (var i in parkingInfo)
                    {
                        if (i.Key == slotNum)
                        {
                            res = i.Value.registration_num;
                            Console.WriteLine("Registration num :: " + res);
                        }
                    }
                    if (string.IsNullOrEmpty(res))
                    {
                        Console.WriteLine("Not found");
                    }
                }
                else
                {
                    colour = slotInfo[1];
                    foreach (var i in parkingInfo)
                    {
                        if (i.Value.colour.EndsWith(colour))
                        {
                            if (!string.IsNullOrEmpty(res))
                                res = res + " " + i.Value.registration_num;
                            else
                                res = i.Value.registration_num;
                        }
                    }
                    if (string.IsNullOrEmpty(res))
                    {
                        Console.WriteLine("Not found");
                    }
                    Console.WriteLine("Registration no:" + res);
                }
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine("::DisplayRegistrationNumber Exception::" + ex.Message);
                return res;
            }
        }
        public string DisplaySlotNumber(string slotDetails)
        {
            //loop through the master record for the details
            string res = null;
            try
            {
                string[] slotInfo = slotDetails.Split(" ");
                string choice = slotInfo[1]; //check if its color or registration number
                if (slotInfo[0].EndsWith("colour"))
                {
                    foreach (var i in parkingInfo)
                    {
                        if (i.Value.colour == choice)
                        {
                            if (!string.IsNullOrEmpty(res))
                                res = res + " " + i.Key.ToString();
                            else
                                res = i.Key.ToString();
                            Console.WriteLine("SlotNo:" + res);
                        }
                    }
                    if(string.IsNullOrEmpty(res))
                    {
                        Console.WriteLine("Not found");
                    }
                }
                else if (slotInfo[0].EndsWith("registration_number"))
                {
                    foreach (var i in parkingInfo)
                    {
                        if (i.Value.registration_num == choice)
                        {
                            res = i.Key.ToString();
                            Console.WriteLine("SlotNo:" + res);
                            break;
                        }
                    }
                    if (string.IsNullOrEmpty(res))
                    {
                        Console.WriteLine("Not found");
                    }
                }
                else
                {
                    Console.WriteLine("Choose valid option to get the slot number");
                }
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine("::DisplaySlotNumber Exception::" + ex.Message);
                return res;
            }

        }
    }
}
