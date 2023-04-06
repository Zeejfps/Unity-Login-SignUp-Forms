namespace Common.Controllers
{
    public interface IWidgetController
    {
        bool ProcessInputEvent(InputEvent inputEvent);
        void Dispose();
    }
}