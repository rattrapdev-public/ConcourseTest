using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NetCoreLibrary.Application;
using NetCoreLibrary.Application.ViewModels;
using NetCoreRefresher.Controllers;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace NetCoreRefresherTests.Controllers
{
    [TestFixture]
    public class OrganizationApiControllerTests
    {
        [Test]
        public void Search_returns_matching_organizations_from_OrganizationService()
        {
            // Arrange

            var organization1 = new OrganizationViewModel
                {OrganizationId = Guid.NewGuid(), OrganizationName = "Organization1", IsEnabled = true};
            var organization2 = new OrganizationViewModel
            {
                OrganizationId = Guid.NewGuid(), OrganizationName = "Organization2", IsEnabled = true
            };
            var organizationService = Substitute.For<IOrganizationService>();
            organizationService.Search(Arg.Any<string>()).Returns(new List<OrganizationViewModel>
                {organization1, organization2});
            var sut = new OrganizationApiController(organizationService);

            // Act

            var result = sut.Search(String.Empty);
            
            // Assert
            
            result.GetType().ShouldBe(typeof(JsonResult));
            var jsonResult = (JsonResult) result;
            var viewModels = ((IEnumerable<OrganizationViewModel>) jsonResult.Value).ToList();
            viewModels.Count.ShouldBe(2);
            viewModels.Any(x => x.OrganizationId == organization1.OrganizationId).ShouldBeTrue();
            viewModels.Any(x => x.OrganizationId == organization2.OrganizationId).ShouldBeTrue();
        }
    }
}