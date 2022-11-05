namespace StorEsc.Tests.Fakers;

public interface IFaker<T> where T : class
{
    T GetValid();
    T GetInvalid();
}