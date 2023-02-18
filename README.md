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
When things goes to parsing `SET` statement there are another rules, so for this case I made an child for basic expression reader, who treats `VisitNew` method is another way.
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
This method is only adding some functional on top of the existing, using some instance of interface member, that I will talk about later.

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
So, in order to resolve it in some way I added static class and some static bodyless functions in it, just to represent it db-like.
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
There are five rules for proper use :
* Every field must be a property and public ;
* Every field must go in same order as in the db table ;
* Every field must have the same name and in constructor ; 
* Entity must implement `CRUD_Entityt` interface functions in order to parse from `DataRow` and to `string`;
* Entity must have an empty new constructor ;

There is an example for `User` class in `C#` and `MySQL` table :
  * `CRUD_Entity` interface
  ```cs
  public interface CRUD_Entityt<Type> where Type : new()
  {
      public string toQuery();
      public Type fromQuery(DataRow toParse);
  }
  ```
  * `_User` class
  ```cs
  class _User : CRUD_Entityt<_User>
  {
      public _User() { }
      
      public _User(string _email, string _password, string _name, string _surname, DateTime _dob, double _sallary)
      {
          this._email = _email;
          this._password = _password;
          this._name = _name;
          this._surname = _surname;
          this._dob = _dob;
          this._sallary = _sallary;
      }

      public string _email { get; set; }
      public string _password { get; set; }
      public string _name { get; set; }
      public string _surname { get; set; }
      public DateTime _dob { get; set; }
      public double _sallary { get; set; }

      public _User fromQuery(DataRow toParse)
      {
          return new _User(
              toParse[0] as string,
              toParse[1] as string,
              toParse[2] as string,
              toParse[3] as string,
              Convert.ToDateTime(toParse[4]),
              Convert.ToDouble(toParse[5])
          );
      }

      public string toQuery()
      {
          return $"'{_email}','{_password}','{_name}','{_surname}','{_dob}',{_sallary}";
      }
  }
  ```
  And `MySQl` syntax :
  ```sql
    CREATE TABLE _User (
        _email    VARCHAR(40) NOT NULL PRIMARY KEY,
        _password CHAR(32) NOT NULL,
        _name	  VARCHAR(30),
        _surname  VARCHAR(30),
        _dob	  DATE,
        _sallary  DOUBLE
    )
  ```
### <a id="expanding"></a>Expanding ###
You can customise each class or interface simply inheriting them, overriding their functions or adding this functional on top.
There is only one problem with it : this project does not support relations.

## There is an example for `MySQL` in Program.cs, try it out!
