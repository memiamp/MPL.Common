using System;

namespace MPL.Common.Reflection
{
    public class TypeFinderBaseClass
    {
    }

    public class TypeFinderDerivedClassA : TypeFinderBaseClass
    {
    }

    public class TypeFinderDerivedClassB : TypeFinderBaseClass
    {
    }

    public class TypeFinderDerivedClassC : TypeFinderDerivedClassA
    {
    }

    public abstract class TypeFinderAbstractBaseClass
    {
    }

    public class TypeFinderAbstractDerivedClassA : TypeFinderAbstractBaseClass
    {
    }

    public class TypeFinderAbstractDerivedClassB : TypeFinderAbstractDerivedClassA
    {
    }
}