using System.Diagnostics;
using CarRental.Application.Features.Booking.Command.CreateBooking;
using CarRental.Application.Features.Booking.Command.PayBooking;
using CarRental.Application.Features.Booking.Queries;
using CarRental.Application.Features.Booking.Queries.GetBooking;
using CarRental.Application.Features.Booking.Queries.GetBookings;
using Microsoft.AspNetCore.Mvc;
using CarRental.Web.Models;
using MediatR;
using CarRental.Application.Features.Vehicle.Queries.GetVehicles;
using CarRental.Application.Features.Categories.Queries.GetCategories;

namespace CarRental.Web.Controllers;

public class BookingController : Controller
{
    private readonly ILogger<BookingController> _logger;
    private readonly IMediator _mediator;

    public BookingController(ILogger<BookingController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        var bookingsViewModelList = await _mediator.Send(new GetBookingsListQuery());
        return View(bookingsViewModelList);
    }

    // GET: Categories/Create
    public async Task<IActionResult> Create()
    {
        var availableVehicles = await _mediator.Send(new GetVehiclesListQuery());
        var categories = await _mediator.Send(new GetCategoriesListQuery());
        var bookingViewModel = new BookingViewModel() { Vehicles = availableVehicles, Categories = categories};

        return View(bookingViewModel);
    }

    // POST: Categories/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BookingViewModel bookingViewModel)
    {
        await _mediator.Send(new CreateBookingCommand()
        {
            VehicleId = bookingViewModel.Vehicle.VehicleId,
            Customer = new CustomerViewModel() {FirstName = bookingViewModel.Customer.FirstName, LastName = bookingViewModel.Customer.LastName, PersonalIdentityNumber = bookingViewModel.Customer.PersonalIdentityNumber},
            PickUpDateTime = bookingViewModel.PickUpDateTime,
            MileageReading = bookingViewModel.MileageReading,
            CategoryId = bookingViewModel.Category.CategoryId
        });

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Pay(int id)
    {
        var availableVehicles = await _mediator.Send(new GetVehiclesListQuery());
        var bookingViewModel = new BookingViewModel() { BookingId = id, Vehicles = availableVehicles };
        return View(bookingViewModel);
    }

    // POST: Categories/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Pay(BookingViewModel bookingViewModel, int extraBookingId)
    {
        var totalCost = await _mediator.Send(new PayBookingCommand()
        {
            BookId = bookingViewModel.BookingId,
            MileageReading = bookingViewModel.MileageReading,
            ReturnDateTime = bookingViewModel.ReturnDateTime,
            ExtraBookingId = extraBookingId

        });

        return RedirectToAction(nameof(Payment), new { cost = totalCost } );
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Payment(decimal cost)
    {
        return View(cost);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult ReturnCar()
    {
        return View();
    }

    // POST: Categories/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ReturnCar(BookingViewModel bookingViewModel, int extraBookingId)
    {
        var totalCost = await _mediator.Send(new PayBookingCommand()
        {
            BookId = bookingViewModel.BookingId,
            MileageReading = bookingViewModel.MileageReading,
            ReturnDateTime = bookingViewModel.ReturnDateTime,
            ExtraBookingId = extraBookingId

        });

        return RedirectToAction(nameof(Payment), new { cost = totalCost });
    }

}
