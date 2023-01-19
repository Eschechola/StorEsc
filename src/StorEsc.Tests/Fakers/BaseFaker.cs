namespace StorEsc.Tests.Fakers;

public abstract class BaseFaker<T> : IFaker<T> where T : class
{
    public abstract T GetValid();

    public abstract T GetInvalid();

    public IList<T> GetValidList(int amount = 10)
    {
        var validList = new List<T>();
        
        for(var i = 0; i < amount; i++)
            validList.Add(GetValid());

        return validList;
    }
    
    public IList<T> GetInvalidList(int amount = 10)
    {
        var validList = new List<T>();
        
        for(var i = 0; i < amount; i++)
            validList.Add(GetInvalid());

        return validList;
    }
}