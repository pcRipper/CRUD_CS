using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_CS.ExpressionReader
{
    public interface IParser
    {
        public string RepresentDate(DateTime date);
        public string CONCAT(Expression e); 
    }
}
