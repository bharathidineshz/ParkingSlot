using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingLot.Program;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;

namespace ParkingLot.Program.Tests
{
    [TestClass()]
    public class ParkingTests
    {
        [TestMethod()]
        public void Test_CreateTotalSlots()
        {
            Parking req = new Parking();
            req.CreateTotalSlots("create 3");
            int expected = req.freeSlots.Count;
            expected.Should().Be(3);
            //Assert.Fail();
        }
        [TestMethod()]
        public void Test_ParkingMethod()
        {
            var req = new Parking();
            req.CreateTotalSlots("create 3");
            req.ParkingMethod("park TN-37X-9999 black").Should().BeTrue();

        }

        [TestMethod()]
        public void Test_UpdateSlotDetails()
        {
            var req = new Parking();
            req.CreateTotalSlots("create 3");
            req.ParkingMethod("park TN-37X-9999 black");
            req.ParkingMethod("park TN-44X-5555 black");
            req.Parking_ExitUpdateSlotDetails("leave 1");
            var testCarDetails = new CarInfo("TN-44X-5555", "black");
            var testDict = new Dictionary<int, CarInfo>();
            testDict.Add(2, testCarDetails);
            testDict.Should().BeEquivalentTo(req.parkingInfo);
        }

        [TestMethod()]
        public void Test_DisplaySlotDetail()
        {
            var req = new Parking();
            req.CreateTotalSlots("create 3");
            req.ParkingMethod("park TN-37X-9999 black").Should().BeTrue();
            req.ParkingMethod("park TN-44X-5555 black").Should().BeTrue();
        }

        [TestMethod()]
        public void Test_DisplayRegistrationNumber()
        {
            var req = new Parking();
            req.CreateTotalSlots("create 4");
            req.ParkingMethod("park TN-55X-9999 black");
            req.ParkingMethod("park TN-44X-5555 black");
            req.ParkingMethod("park TN-33X-3333 white");
            string res = req.DisplayRegistrationNumber("registration_numbers_for_cars_with_slot_number 1");
            res.Should().BeEquivalentTo("TN-55X-9999");
            res = req.DisplayRegistrationNumber("registration_numbers_for_cars_with_colour black");
            string test = "TN-55X-9999 TN-44X-5555";
            res.Should().BeEquivalentTo(test);
        }

        [TestMethod()]
        public void Test_DisplaySlotNumber()
        {
            var req = new Parking();
            req.CreateTotalSlots("create 4");
            req.ParkingMethod("park TN-55X-9999 black");
            req.ParkingMethod("park TN-44X-5555 black");
            req.ParkingMethod("park TN-33X-3333 white");
            string res = req.DisplaySlotNumber("slot_numbers_for_cars_with_colour black");
            string test = "1 2";
            res.Should().BeEquivalentTo(test);
            res = req.DisplaySlotNumber("slot_numbers_for_registration_number TN-44X-5555");
            test = "2";
            res.Should().BeEquivalentTo(test);

        }
    }
}