using System;
namespace Website
{
    public class StateContainer
    {
        public string Property { get; set; } = "Initial value from StateContainer";
        public string ShowMenu { get; set; } = "";

        public event Action OnChange;

        public void SetProperty(string value)
        {
            Property = value;
            NotifyStateChanged();
        }

        public void ToggleMenu()
        {
            ShowMenu = ShowMenu == "" ? "is-active" : "";
        }

        public void HideMenu()
        {
            ShowMenu = "";
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
