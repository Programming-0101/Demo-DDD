using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGrades.BusinessExceptions
{
    public class GradeBookUnavailableException : Exception
    {
        public GradeBookUnavailableException(string reason) : base(reason)
        {            
        }
    }
}
