## CRUD with Expression Tree reader
### How does it work?
+ ### [Basic Expression Tree reader, that creates `WHERE` statement](#basic-expression-tree-reader)
  + ### [Inhereted Readers for each DB](#inhereted-readers-bd)
+ ### [Separated reader for `SET` statements](#translator-transformer)
+ ### [Inhereted readers are using specialized Interfaces](#interfaces-i-readers)
+ ### [Extended formal functionality for types](#extended-functional)
***
### [How to use](#getting-started)?
+ ### [Entities](#entities)
+ ### [Expanding](#expanding)
+ ### [Customisation](#customisation)
***
### <a id="basic-expression-tree-reader"></a> Basic Expression Tree reader ###
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
```cs
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

### <a id="translator-transformer"></a>Separated reader ###
When things goes to parsing `SET` statement there are another rules, so for this case i made an child for basic expression reader, who treats `VisitNew` method is another way.
According to the rules for Update method in CRUD you must pass two Expressions : 
 * `Expression<Func<EntityType,EntityType>>` -> to transform some record (class on side of c#) to new, changing something in it;
 * `Expression<Predicate<EntityType>>` -> to set the condition of selecting;
Basicly, for creating `SET` you must work with constructor, that changing something from previous instance, in order to make new.
Here comes overloaded version from basic Expression reader, that looks like that :
```cs
protected override Expression VisitNew(NewExpression node)
{
    bool added = false;

    for (int k = 0; k < node.Arguments.Count; k++)
    {
        var argument = node.Arguments[k];
        string name = node.Constructor.GetParameters()[k].Name;

        added = true;

        sb.Append($"{name} = ");
        this.Visit(argument);

        if (added == true)
        {
            sb.Append(", ");
        }
    }

    return node;
}
```
Class, that holds this method, called MyQueryTranslatorTransformer unlike an ancestor - MyQueryTranslator.

### <a id="inhereted-readers-bd"></a>MySQL Reader
Because of each DBMS provides different approach for types, functions, relations and syntax in general it is not possible to create single parser for 2 DB's.
In this project you can find two inhereted versions of basic Expression Reader aka `MyQueryTranslator` for `MySQL` and `Postgre`.
`MySQL` overrides many of existing Visit-methods in order to extend functional or replace it at all.
For example this overrided method `VisitMethodCall` that handles methods :
```cs
protected override Expression VisitMethodCall(MethodCallExpression m)
{
    switch (m.Method.Name)
    {
        case "AddDays":
            return parser.AddDays(this, m);
            break;
        case "DATEDIFF":
            return parser.DATEDIFF(this, m);
            break;
        default:
            return base.VisitMethodCall(m);
            break;
    }
}
```
This method is only adding some functional on top of the existing, using some instance of interface member, that i will talk about later.

### <a id="interfaces-i-readers"></a> Interfaces in Inhereted readers
In order to make this code more flexible and independent,i added required interface to each basic db readers, for `MySQL_Translator` it looks like this :
```cs
public interface Parser_MySQL
{
    public Expression StringLength(MyQueryTranslator translator, MemberExpression member);

    public Expression AddDays(MyQueryTranslator translator, MethodCallExpression method);

    public Expression DATEDIFF(MyQueryTranslator translator, MethodCallExpression method);

    public string RepresentDate(DateTime date);
}
```
With implementation in the form of `Parser_MySQL_Basic`, translator on his self requires it like this :
```cs
    class MySQL_Translator<Parser> : MyQueryTranslator
        where Parser : Parser_MySQL, new()
    {}
```
### <a id="extended-functional"></a>Extended types functional ###
Many functions in, for example MySQL, does not match with `C#` ideology : some properties in cs are functions, some functions are static and so on.
So, in order to resolve it in some way i added static class and some static bodyless functions in it, just to represent it db-like.
Example for `MySQL` : 
```cs
public static class MySQL_FEX
{
    //Numeric Functions
    public static double AVG(this double x) => x;
    public static int CEIL(this float x) => x;
    public static int FLOOR(this int x) => x;
    // Date Functions
    public static int DATEDIFF(this DateTime time, DateTime x) => 0;
}
```
***
## <a id ="getting-started"></a>Getting started ##
### <a id="entities"></a>Entities
### <a id="expanding"></a>Expanding
### <a id="customisation"></a>Customisation
