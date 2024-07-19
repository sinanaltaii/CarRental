//using CarRental.Domains;
//using CarRental.Tests.IntegrationTests.Setup;

//namespace CarRental.Tests.IntegrationTests.CarRental.DataAccess
//{
//    [Collection("CarRentalContextCollection")]
//    public class CategoryTests
//    {
//        private readonly CarRentalContextFixture _fixture;

//        public CategoryTests(CarRentalContextFixture fixture)
//        {
//            _fixture = fixture;
//        }

//        [Fact]
//        public async Task GivenDbIsUp_WhenRetrievingCategory_ThenCategoryEntityIsReturned()
//        {
//            var category = await _fixture.Context.Categories.FindAsync(1);

//            Assert.NotNull(category);
//            Assert.Equal(1, category.Id);
//        }

//        [Fact]
//        public async Task GivenDbIsUp_WhenAddingCategoryLastbil_ThenCategoryAdded()
//        {
//            var category = new Category { Name = "Lastbil", BasePrice = 150.00m};
//            await _fixture.Context.Categories.AddAsync(category);
//            await _fixture.Context.SaveChangesAsync();

//            Assert.NotNull(category);
//            Assert.NotEqual(0, category.Id);
//        }

//        [Fact]
//        public async Task GivenDbIsUp_WhenUpdatingCategory_ThenCategoryUpdated()
//        {
//            var sut = new Category { Name = "Sport", BasePrice = 250.00m};
//            await _fixture.Context.AddAsync(sut);
//            await _fixture.Context.SaveChangesAsync();
//            sut.Name = "Exclusive";
//            _fixture.Context.Categories.Update(sut);
//            await _fixture.Context.SaveChangesAsync();

//            var updated = await _fixture.Context.Categories.FindAsync(sut.Id);

//            Assert.Equal("Exclusive", sut.Name);
//        }

//        [Fact]
//        public async Task GivenDbIsUp_WhenDeletingCategory_ThenCategoryDeleted()
//        {
//            var sut = new Category { Name = "I am an unwanted category" };
//            await  _fixture.Context.AddAsync(sut);
//            await _fixture.Context.SaveChangesAsync();

//            _fixture.Context.Categories.Remove(sut);
//            await _fixture.Context.SaveChangesAsync();

//            var category = await _fixture.Context.Categories.FindAsync(sut.Id);

//            Assert.Null(category);
//        }
//    }
//}
