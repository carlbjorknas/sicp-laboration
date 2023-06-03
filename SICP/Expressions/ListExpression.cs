namespace SICP.Expressions;

public class ListExpression : Expression
{
    private readonly Expression? _left;
    private readonly Expression? _right;

    public ListExpression(Expression? left, Expression? right)
    {
        _left = left;
        _right = right;
    }

    protected ListExpression() { }

    public Expression Car => _left ?? throw new Exception("Cannot 'Car' the empty list.");
    public Expression Cdr => _right ?? throw new Exception("Cannot 'Cdr' the empty list.");
    public Expression Cadr => ((ListExpression)Cdr).Car;
    public Expression Cddr => ((ListExpression)Cdr).Cdr;
    public Expression Caddr => ((ListExpression)Cddr).Car;
    public Expression Cdddr => ((ListExpression)Cddr).Cdr;
    public Expression Cadddr => ((ListExpression)Cdddr).Car;

    public override string ToString()
        => "List";

    public List<Expression> AsFlatDotNetList()
    {
        var list = this;
        var dotNetList = new List<Expression>();
        while (list != EmptyListExpression.Instance)
        {
            dotNetList.Add(list.Car);
            list = (ListExpression)list.Cdr;
        }
        return dotNetList;
    }
}
