
namespace VirtualKeyboard.ControlHandlers
{
    public interface IControlHandler
    {
        /// <summary>
        /// The editable value of the control
        /// </summary>
        string TextValue { get; set; }
    }
}
