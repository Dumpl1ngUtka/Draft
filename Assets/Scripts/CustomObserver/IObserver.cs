namespace CustomObserver
{
    public interface IObserver<T>
    {
        public void UpdateObserver(T interactor);
    }
}