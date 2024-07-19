using CarRental.Application.Features.Categories.Queries.GetCategories;
using CarRental.Application.Features.Vehicle;
using CarRental.Application.Features.Vehicle.Commands.CreateVehicle;
using CarRental.Application.Features.Vehicle.Commands.DeleteVehicle;
using CarRental.Application.Features.Vehicle.Commands.UpdateVehicle;
using CarRental.Application.Features.Vehicle.Queries.GetVehicle;
using CarRental.Application.Features.Vehicle.Queries.GetVehicles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Web.Controllers
{
    public class VehicleController : Controller
    {
        private readonly ILogger<VehicleController> _logger;
        private readonly IMediator _mediator;
        public VehicleController(ILogger<VehicleController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var vm = await _mediator.Send(new GetVehiclesListQuery());
            return View(vm);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var vehicleVm = await _mediator.Send(new GetVehicleQuery() { VehicleId = id });

            return View(vehicleVm);
        }

        // GET: Categories/Create
        public async Task<IActionResult> Create()
        {
            var categories = await _mediator.Send(new GetCategoriesListQuery());
            var vm = new VehicleVm() {Categories = categories};
            return View(vm);
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleVm vehicleVm)
        {
            await _mediator.Send(new CreateVehicleCommand()
            {
                CategoryId = vehicleVm.Category.CategoryId,
                PlateNumber = vehicleVm.PlateNumber,
                Make = vehicleVm.Make,
                Model = vehicleVm.Model
            });
            return RedirectToAction(nameof(Index));
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var categories = await _mediator.Send(new GetCategoriesListQuery());
            var vehicleVm = await _mediator.Send(new GetVehicleQuery {VehicleId = id});
            vehicleVm.Categories = categories;
            return View(vehicleVm);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VehicleVm vehicleVm)
        {
            var command = new UpdateVehicleCommand()
            {
                VehicleId = vehicleVm.VehicleId,
                CategoryId = vehicleVm.Category.CategoryId,
                PlateNumber = vehicleVm.PlateNumber,
                Make = vehicleVm.Make,
                Model = vehicleVm.Model
            };

            await _mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var query = new GetVehicleQuery() { VehicleId = id };
            var vehicleToDelete = await _mediator.Send(query);

            return View(vehicleToDelete);
        }

        //// POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var command = new DeleteVehicleCommand() { VehicleId = id };
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }
    }
}
