using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReceiptScanner.Models
{
    public class Request
    {
        public Image Image { get; set; }
        public List<Feature> Features { get; set; }
        public ImageContext ImageContext { get; set; }
    }
}