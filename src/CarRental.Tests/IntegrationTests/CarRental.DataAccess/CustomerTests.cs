using Bogus;
using Bogus.Extensions.Sweden;
using CarRental.Domains;
using CarRental.Tests.IntegrationTests.Setup;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Tests.IntegrationTests.CarRental.DataAccess
{
    [Collection("CarRentalContextCollection")]
    public class CustomerTests
    {
        private readonly CarRentalContextFixture _fixture;

        public CustomerTests(CarRentalContextFixture fixture)
        {
            _fixture = fixture;
        }
        
        [Fact]
        public async Task GivenDbIsUp_WhenAddingCustomer_ThenCustomerAdded()
        {
            // Arrange
            var customer = FakeOnlyCustomer();
            
            // Act
            _ = await _fixture.Context.Customers.AddAsync(customer);
            await _fixture.Context.SaveChangesAsync();
            
            // Assert
            Assert.NotEqual(0, customer.Id);
        }
        
        [Fact]
        public async Task GivenDbIsUp_WhenAddingCustomerWithContactInfo_ThenCustomerAndRelatedContactInfoAdded()
        {
            // Arrange
            var customer = FakeCustomerWithContactInfo();
            
            // Act
            _ = await _fixture.Context.AddAsync(customer);
            _ = await _fixture.Context.SaveChangesAsync(CancellationToken.None);
            
            // Assert
            Assert.NotEqual((0,0), (customer.Id, customer.ContactInfo.Id));
        }

        [Fact]
        public async Task GivenDbIsUp_WhenDeletingCustomer_ThenCustomerAndRelatedContactInfoDeleted()
        {
            // Arrange
            var customer = FakeCustomerWithContactInfo();
            await _fixture.Context.Customers.AddAsync(customer);
            await _fixture.Context.SaveChangesAsync();
            
            // Act
            _fixture.Context.Entry(customer).State = EntityState.Deleted;
            var affectedRows = await _fixture.Context.SaveChangesAsync();
            
            // Assert
            Assert.Equal(2,affectedRows);
        }

        private static Customer FakeCustomerWithContactInfo()
        {
            var customer = new Faker<Customer>().Rules((faker, customer) =>
            {
                customer.FirstName = faker.Person.FirstName;
                customer.LastName = faker.Person.LastName;
                customer.PersonalIdentityNumber = faker.Person.Personnummer();
                customer.ContactInfo = new ContactInfo
                {
                    EmailAddress = faker.Person.Email,
                    StreetAddress = faker.Address.StreetAddress(),
                    PostalCode = faker.Address.ZipCode("#####"),
                    City = faker.Address.City()
                };
            }).Generate();

            return customer;
        }
        
        private static Customer FakeOnlyCustomer()
        {
            var fakeCustomer = new Faker<Customer>().Rules((faker, customer) =>
            {
                customer.FirstName = faker.Name.FirstName();
                customer.LastName = faker.Name.LastName();
                customer.PersonalIdentityNumber = faker.Person.Personnummer();
            }).Generate();

            return fakeCustomer;
        }
    }
}