# ParkingSlot
C# .NET Core parking System Problem description:
To design a parking slot .NET core application
Constraints and assumptions:
* Syntax of each request should be followed as below.
For create                                             :  create <total no of slots>
For park                                                :  park <reg_no> <color>
For leaving                                           :  leave <slot_no>
For Registration no using slotNo     :  registration_numbers <slot_no>
For Registration no using color        :  registration_numbers <color>
For Slot no using Registration No    :  slot_numbers_registration_number <slot_no>
For Slot no using color                       :  slot_numbers_color <color>
For exit                                                 :   exit
* Input file should be a text file and should be given full path.
    Eg: c:/users/bharathi/testfile.txt

* The first statement of the input file /interactive command should be create <slotno>.
* Create request should be used only once in the file or in the interactive method.
* 'exit' command is used to exit the application.
* All the commands and requests are case sensitive

Sample file.txt
create 3
park TN-33X-9999 black
park TN-44X-8888 white
park TN-55X-7777 black
leave 2
status
registration_numbers 1
registration_numbers black
slot_numbers_color black
slot_numbers_registration_number TN-55X-7777








