using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspMvc.Helpers
{
    public class ImageHelper
    {
        public static string Logo(int scale)
        {
            return "<img src=\"https://i.imgur.com/uOHA9Fr.jpg\" style=\"width: {scale}%;height: auto;object-fit: contain;\"/>";
        }
    }
}