public interface IMovementStrategy<T>
{
    void execute(T bat, float delta);
}