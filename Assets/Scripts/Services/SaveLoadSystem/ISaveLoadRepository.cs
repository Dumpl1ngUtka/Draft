namespace Services.SaveLoadSystem
{
    public interface ISaveLoadRepository<T>
    {
        public T LoadDataFrom(string path);
        
        public void SaveDataTo(T data, string path);
    }
}