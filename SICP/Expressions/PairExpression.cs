﻿namespace SICP.Expressions;

public class PairExpression : Expression
{
    private readonly Expression? _left;
    private Expression? _right;

    public PairExpression(Expression? left, Expression? right)
    {
        _left = left;
        _right = right;
    }

    protected PairExpression() { }

    public Expression Car => _left ?? throw new Exception("Cannot 'Car' the empty list.");
    public Expression Cdr => _right ?? throw new Exception("Cannot 'Cdr' the empty list.");
    public Expression Cadr => ((PairExpression)Cdr).Car;
    public Expression Caadr => ((PairExpression)Cadr).Car;
    public Expression Cdadr => ((PairExpression)Cadr).Cdr;
    public Expression Cddr => ((PairExpression)Cdr).Cdr;
    public Expression Caddr => ((PairExpression)Cddr).Car;
    public Expression Cdddr => ((PairExpression)Cddr).Cdr;
    public Expression Cadddr => ((PairExpression)Cdddr).Car;

    public override string ToString()
    {
        if (this == EmptyListExpression.Instance)
            return "()";

        var str = "(";
        var current = this;
        while (true)
        {
            str += $"{current.Car}";

            if (current.Cdr is EmptyListExpression)
                break;
            else if (current.Cdr is PairExpression rest)
            {
                str += " ";
                current = rest;
            }
            else
            {
                str += $" . {current.Cdr}";
                break;
            }
        }

        str += ")";
        return str;
    }

    public List<Expression> ToDotNetList()
    {
        var list = this;
        var dotNetList = new List<Expression>();
        while (list != EmptyListExpression.Instance)
        {
            dotNetList.Add(list.Car);
            list = (PairExpression)list.Cdr;
        }
        return dotNetList;
    }

    public virtual PairExpression ShallowCopy()
    {
        var right = _right is PairExpression pe 
            ? pe.ShallowCopy() 
            : _right;
        return new PairExpression(_left, right);
    }

    public PairExpression LastPair
    {
        get
        {
            var current = this;
            while (current._right is PairExpression pe && pe != EmptyListExpression.Instance)
            {
                current = pe;
            }

            return current;
         }
    }

    public virtual void SetRight(Expression newRight)
    {
        _right = newRight;
    }
}
