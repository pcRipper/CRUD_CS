﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_CS.ExpressionReader
{
    class Parser_MySQL : IParser
    {
        public Expression AddDays<Parser_MySQL>(MyQueryTranslator<Parser_MySQL> translator, MethodCallExpression method)
            where Parser_MySQL : IParser, new()
        {

            translator.QueryString = "ADDDATE(";
            translator.Visit(method.Object);
            translator.QueryString = ",";
            translator.Visit(method.Arguments[0]);
            translator.QueryString = ")";

            return null;   
        }

        public string RepresentDate(DateTime date)
        {
            return date.ToString("dd-MM-yyyy");
        }

    }
}
