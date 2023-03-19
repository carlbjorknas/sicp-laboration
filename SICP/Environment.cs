namespace SICP;

public class Environment
{
    private Dictionary<string, string> _varToValueMap = new Dictionary<string, string>();

    public void AddVariable(string name, string value)
    {
        _varToValueMap.Add(name, value);
    }

    public string GetValue(string name)
    {
        return _varToValueMap[name];
    }
}
