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
                Console.WriteLine("Operations:create_parking_lot,park,leave,status,registration_nmbers,slot_number");
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
                            retry = true;
                            break;
                        }
                    }
                    else if (input.Equals("exit"))//to exit the application
                    {
                        retry = true;
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

        //Class maintaing car details
        public class CarInfo
        {
            public string registration_num;
            public string color;

            public CarInfo(string reg_no, string col)
            {
                registration_num = reg_no;
                color = col;
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
                    StreamReader readFile = File.OpenText(inputFile);
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
                try
                {
                    string[] slotDetail = total_slot.Split(" ");
                    int totalSlot = int.Parse(slotDetail[1]);
                    for (int i = 1; i <= totalSlot; i++)
                    {
                        freeSlots.Add(i);
                    }
                    freeSlots.Sort();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("::CreateTotalSlots Exception::" + ex.Message);
                }
            }
            public bool ParkingMethod(string parking_details)
            {
                //On vehicle entry collect the car details .Allt the nearest slot and add the details to the master record 
                return true;
            }
            public void Parking_ExitUpdateSlotDetails(string clear_slot)
            {
                //On vehicle exit ,clear the entry from the master record and add the slot no to the freed slot.
            }
            public void DisplaySlotDetails()
            {
                //From the master record display the details
            }
            public string DisplayRegistrationNumber(string slotDetails)
            {
                //loop through the master record for the details
                string res = null;
                return res;
            }
            public string DisplaySlotNumber(string slotDetails)
            {
                //loop through the master record for the details
                string res = null;
                return res;
            }
        }
    }
}
