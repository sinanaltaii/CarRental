using CarRental.Application.Features.Categories.Commands.CreateCategory;
using CarRental.Application.Features.Categories.Commands.DeleteCategory;
using CarRental.Application.Features.Categories.Commands.UpdateCategory;
using CarRental.Application.Features.Categories.Queries;
using CarRental.Application.Features.Categories.Queries.GetCategories;
using CarRental.Application.Features.Categories.Queries.GetCategory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Web.Controllers;

public class CategoryController : Controller
{
    private readonly ILogger<CategoryController> _logger;
    private readonly IMediator _mediator;
    public CategoryController(ILogger<CategoryController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        var viewModelList = await _mediator.Send(new GetCategoriesListQuery());
        return View(viewModelList);
    }

    // GET: Categories/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var viewModel = await _mediator.Send(new GetCategoryQuery() { CategoryId = id });
        if (viewModel == null)
        {
            return Problem("Entity set 'ProductContext.Categories'  is null.");
        }

        return View(viewModel);
    }

    // GET: Categories/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Categories/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,BasePrice")] CategoryVm category)
    {
        // TODO: Make use of fluent validation
        if (!ModelState.IsValid)
            return View(category);

        await _mediator.Send(new CreateCategoryCommand() { Name = category.Name, BasePrice = category.BasePrice});
        return RedirectToAction(nameof(Index));
    }

    // GET: Categories/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var viewModel = await _mediator.Send(new GetCategoryQuery { CategoryId = id });

        if (viewModel == null)
        {
            return NotFound();
        }

        return View(viewModel);
    }

    // POST: Categories/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,BasePrice")] CategoryVm category)
    {
        // Todo: Deal with null, put fluent validation in a good use?

        if (!ModelState.IsValid)
            return View(category);

        var command = new UpdateCategoryCommand()
        {
            CategoryId = category.CategoryId,
            Name = category.Name,
            BasePrice = category.BasePrice
        };

        //return Problem("Entity set 'ProductContext.Categories'  is null.");
        await _mediator.Send(command);

        return RedirectToAction(nameof(Index));
    }

    // GET: Categories/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var query = new GetCategoryQuery { CategoryId = id };
        var categoryToDelete = await _mediator.Send(query);
        if (categoryToDelete == null)
        {
            return Problem("Entity set 'ProductContext.Categories'  is null.");
        }

        return View(categoryToDelete);
    }

    //// POST: Categories/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var command = new DeleteCategoryCommand() { CategoryId = id };
        await _mediator.Send(command);
        return RedirectToAction(nameof(Index));
    }
}