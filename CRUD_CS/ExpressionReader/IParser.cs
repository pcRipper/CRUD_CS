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
        //String Functions

        //Numeric Functions

        //Date Functions
        public string RepresentDate(DateTime date);
        public Expression AddDays<Parser>(MyQueryTranslator<Parser> translator, MethodCallExpression method)
            where Parser : IParser, new(); 

    }
}
