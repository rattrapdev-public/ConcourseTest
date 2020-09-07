using System;

namespace NetCoreLibrary.Application.ViewModels
{
    public class OrganizationViewModel
    {
        public Guid OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public bool IsEnabled { get; set; }
    }
}