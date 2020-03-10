using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class BookingHelperTests
    {
        private Mock<IBookingRepository> _bookingRepository;


        [SetUp]
        public void SetUp()
        {
            _bookingRepository = new Mock<IBookingRepository>();

        }
        
        [Test]
        public void OverlappingBookingExists_StartsAndEndsAfterAnExistingBooking_ReturnsEmptyString()
        {
            _bookingRepository.Setup(r => r.GetActiveBookings(1)).Returns(new List<Booking>
            {
                new Booking
                {
                    Id = 2,
                    ArrivalDate = new DateTime(2017,12,1,14,0,0),
                    DepartureDate = new DateTime(2017,12,6,10,0,0),
                    Reference = "a"
                }
            }.AsQueryable());
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = new DateTime(2017, 12, 7, 14, 0, 0),
                DepartureDate = new DateTime(2017, 12, 10, 10, 0, 0),
                Reference = "a",
            }, _bookingRepository.Object);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void OverlappingBookingExists_StartsAndEndsBeforeExistingBooking_ReturnsEmptyString()
        {
            
        }


        [Test]
        public void OverlappingBookingExists_StartsDuringExistingBooking_ReturnsEmptyString()
        {

        }

        [Test]
        public void OverlappingBookingExists_EndsDuringExistingBooking_ReturnsEmptyString()
        {

        }
    }
}