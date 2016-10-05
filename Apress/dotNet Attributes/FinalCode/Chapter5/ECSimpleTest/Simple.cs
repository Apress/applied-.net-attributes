//  Compile using the following command:
//  eccsc /target:library /r:ECFramework.dll Simple.cs

using System;
using ECFramework;

//  Here are the good versions.

/*
public class ExceptionRiddledClass
{
    [Throws(typeof(ArgumentException))]
    public void DangerousMethod() {}
    
    [Throws(typeof(InvalidCastException))]
    public void InnocentMethod()
    {
        throw new InvalidCastException();
    }
}

public class TestClass
{
    public void UnmarkedMethod()
    {
        ExceptionRiddledClass erc = new ExceptionRiddledClass();

        try
        {
            erc.InnocentMethod();    
        }
        catch(InvalidCastException ice) {}
    }

    [Throws(typeof(SystemException))]
    public void MarkedMethod()
    {
        ExceptionRiddledClass erc = new ExceptionRiddledClass();
        erc.DangerousMethod();    
    }
}
*/

//  Here are the bad versions.

public class ExceptionRiddledClass
{
    [Throws(typeof(ArgumentException))]
    public void DangerousMethod() {}
    
    public void InnocentMethod()
    {
        throw new InvalidCastException();
    }
}

public class TestClass
{
    public void UnmarkedMethod()
    {
        ExceptionRiddledClass erc = new ExceptionRiddledClass();
        erc.InnocentMethod();    
    }

    [Throws(typeof(SystemException))]
    public void MarkedMethod()
    {
        ExceptionRiddledClass erc = new ExceptionRiddledClass();
        erc.DangerousMethod();    
    }
}
