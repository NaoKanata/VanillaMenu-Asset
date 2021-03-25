

namespace VVVanilla.Menu {
    public interface IMonitorUI<T> {
        void SetValue(T value);
        T GetValue();
    }
}