## CRUD with Expression Tree reader
### How does it work?
+ ##### [Basic Expression Tree reader, that creates `WHERE` statement](#basic-expression-tree-reader)
 + ##### [Inhereted Readers for each DB](#inhereted-readers)
+ ##### [Separated reader for `SET` statements](#2)
+ ##### [Inhereted readers are using specialized Interfaces](#3)
 + ##### [Parser_MySQL](#3.1)
 + ##### [Parser_MySQL_Basic](#3.2)
+ ##### [Extended formal functionality for types](#4)
***
### Basic Expression Tree reader
All logical expressions, that are required for data selection from the database, are transferred to the crud in the form of expressioned predicates
```
Expression<Predicate<EntityType>>
``` 
This form of exprressionde predicates allows to parsing and braking them into single blocks for each operator, member access, method call and etc.

Basic Expression Reader parsing only common cases, such as :
* arithmetical binary or logical operators -> (`+`,`-`,`<`,`>`,`&&`,`||`) ;
* quotes -> (`'`,`"`) ;
* constants -> (`12`,`'hello'`,`true`) ;
* constructors -> `new DateTime.Now`;

All of these determinations occur through the standard iterative via switch, which determines the type of a particular argument or its name, and so on.

This is the example of parsing binary operator :
```
        protected override Expression VisitBinary(BinaryExpression b)
        {
            sb.Append("(");
            this.Visit(b.Left);

            switch (b.NodeType)
            {
                case ExpressionType.GreaterThan:
                    sb.Append(" > ");
                    break;

                case ExpressionType.GreaterThanOrEqual:
                    sb.Append(" >= ");
                    break;

                case ExpressionType.Divide:
                    sb.Append(" / ");
                    break;
                case ExpressionType.Multiply:
                    sb.Append(" * ");
                    break;
                case ExpressionType.Add:
                    sb.Append(" + ");
                    break;
			}
            this.Visit(b.Right);
            sb.Append(")");
            return b;
        }
```

### <a id="inhereted-readers"></a>Inhereted Readers ###

### Separated reader

### MySQL Reader

### Extended types functional

### Getting started
#### Entities
#### Expanding
#### Customisation