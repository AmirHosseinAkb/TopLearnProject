using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.Core.Security
{
    public static class ImageChecker
    {
        public static bool IsImage(this IFormFile image)
        {
            if (image == null)
            {
                return true;
            }
            try
            {
                var img = System.Drawing.Image.FromStream(image.OpenReadStream());
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
