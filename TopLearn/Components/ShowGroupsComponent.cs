using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Components
{
    public class ShowGroupsComponent:ViewComponent
    {
        private ICourseService _courseService;
        public ShowGroupsComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var groups = _courseService.GetAllGroups();
            return await Task.FromResult(View("ShowGroupsComponent", groups));
        }
    }
}
