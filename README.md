# ParkingSlot
C# .NET Core parking System Problem description:
To design a parking slot .NET core application
# Constraints and assumptions:
* Syntax of each request should be followed as below.
For create                           :  create <total no of slots>
For park                             :  park <reg_no> <colour>
For leaving                          :  leave <slot_no>
For Registration no using slotNo     :  registration_numbers_for_cars_with_slot_Number <slot_no>
For Registration no using color      :  registration_numbers_for_cars_with_colour <colour>
For Slot no using Registration No    :  slot_numbers_for_registration_number <slot_no>
For Slot no using color              :  slot_numbers_for_cars_with_colour <colour>
For exit                             :   exit
* Input file should be a text file and should be given full path.
    Eg: c:/users/bharathi/testfile.txt

* The first statement of the input file should be create <slotno>.
* 'exit' command is used to exit the application.
* Commands and requests are not case sensitive.
Summary:
1.On create request, total no of slot is fixed.Slot maintained is sorted.
2.On park request, car details are collected and alloted the nearest slot. Allocated slot detail and car detail is stored in the master record.
3.On leave request, entry of the particular slot is deleted and the slot no is added to the freed slot.
4.On status request, Slot no,registration no and color of the vehicle is fetched from the master record and displayed.
5.On Get_Registration no using colour /Slot no ,master record is searched and displayed.
6.On Get_Slotno using colour/Registration no,slot no is fetched from master record.


# Sample file.txt
create 4
park TN-55X-9999 black
park TN-44X-8888 white
park TN-33X-9889 black
status
leave 2
status
registration_numbers_for_cars_with_Colour black
registration_numbers_for_cars_with_slot_Number 2
slot_numbers_for_cars_with_colour Red
park TN-22X-6767 Red


Executable is a self contained app. So it runs in machine without .netcore .
Download the ParkingLot.exe, ParkingLot.pdb  and testfile.txt from the below git path.
bharathidineshz/ParkingSlot/Executables

Open command prompt.
Method 1:
Give full path of the exe.Eg:c:/users/bharathi/ParkingLot.exe.then start with the commands mentioned above.
Method 2:
Enter the necessary commands in a text file .Eg:testFile.txt
Give full path of the exe with filename(FullPath) as argument.
Eg: c:/users/bharathi/ParkingLot.exe c:/users/test/testfile.txt




