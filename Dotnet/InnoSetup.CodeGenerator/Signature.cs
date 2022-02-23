namespace InnoSetup.CodeGenerator;
public class Parameter
{
    public Parameter(string name, Type type, bool @out)
    {
        Name = name;
        Type = type;
        Out = @out;
    }

    public string Name { get; private set; }
    public Type Type { get; private set; }
    public bool Out { get; private set; }
}
public class Signature
{
    public Signature(string name, Type returnType, IReadOnlyList<Parameter> parameters)
    {
        Name = name;
        ReturnType = returnType;
        Parameters = parameters;
    }

    public string Name { get; }
    public Type ReturnType { get; private set; }

    public IReadOnlyList<Parameter> Parameters { get; private set; }
}
