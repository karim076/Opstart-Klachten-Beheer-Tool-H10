using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlachtenBeheerTool
{
    public class Klacht
    {
        public string Row { get; set; }
        public string Id { get; set; }
        public string Source { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DateOfVisit { get; set; }
        public string ReviewScore { get; set; }
        public string Message { get; set; }
    }
}
