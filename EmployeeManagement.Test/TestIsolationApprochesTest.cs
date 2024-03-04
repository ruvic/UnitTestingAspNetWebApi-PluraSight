

using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.DbContexts;
using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.DataAccess.Services;
using EmployeeManagement.Services.Test;
using EmployeeManagement.Test.HttpMessageHandlers;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit.Sdk;

namespace EmployeeManagement.Test
{
    public class TestIsolationApprochesTest
    {
        [Fact]
        public async Task AttendedCourseAsync_CourseAttended_SuggestedBonusMustBeCorrectlyRecalculated()
        {
            //Arrange

            //1. Creating Database in memory
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            var optionsBuilder = new DbContextOptionsBuilder<EmployeeDbContext>()
                .UseSqlite(connection);

            var dbContext = new EmployeeDbContext(optionsBuilder.Options);
            dbContext.Database.Migrate();

            //2. Building dependencies (Repo and Services)

            var employeeManagementRepo = new EmployeeManagementRepository(dbContext);
            var employeeService = new EmployeeService(employeeManagementRepo, new EmployeeFactory());

            //3. Getting existing employee (Megan Jones)
            var internalEmployee = await employeeManagementRepo.GetInternalEmployeeAsync(Guid.Parse("72f2f5fe-e50c-4966-8420-d50258aefdcb"));

            //4. Getting existing course (Dealing with customer 101)
            var courseToAttend = await employeeManagementRepo.GetCourseAsync(Guid.Parse("844e14ce-c055-49e9-9610-855669c9859b"));

            if(internalEmployee == null || courseToAttend == null)
            {
                throw new XunitException("Arranging the test failed because either internalEmployee or courseToAttend is null");
            }

            //Get Expected SuggestedBonus
            var expectedSuggestedBonus = internalEmployee.YearsInService
                * (internalEmployee.AttendedCourses.Count+1) * 100;

            //Act
            await employeeService.AttendCourseAsync(internalEmployee, courseToAttend);

            //Assert
            Assert.Equal(expectedSuggestedBonus, internalEmployee.SuggestedBonus);
        }

        [Fact]
        public async Task PromoteInternalEmployeeAsync_IsEligible_JobLevelMustBeIncreased()
        {
            //Arrange 
            var httpClient = new HttpClient(new TestablePromotionEligibilityHandler(true));
            var promotionService = new PromotionService(httpClient, new EmployeeManagementTestDataRepository());
            var internalEmployee = new InternalEmployee("Ruvic", "MBOGNI", 5, 3000, false, 1);

            //Act 
            await promotionService.PromoteInternalEmployeeAsync(internalEmployee);

            //Assert
            Assert.Equal(2, internalEmployee.JobLevel);
        }


    }
}
