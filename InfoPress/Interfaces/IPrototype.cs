namespace InfoPress.Interfaces
{
    public interface IPrototype<T>
    {
        T Clone();
    }
}