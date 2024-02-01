using System.ComponentModel;

namespace WinFormsWithHotkeys
{
    public partial class HotKeyComponent : Component
    {
        public class HotKeyInfo
        {
            public Keys Key { get; set; }
            public Keys Modifiers { get; set; }
            public override string ToString()
            {
                return this.Modifiers.ToString() + "+" + this.Key.ToString();
            }
        }

        public List<HotKeyInfo> _hotKeys;
        public event EventHandler<HotKeyEventArgs> HotKeyPressed;

        public HotKeyComponent()
        {
            InitializeComponent();
            _hotKeys = new List<HotKeyInfo>();
        }

        public HotKeyComponent(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            _hotKeys = new List<HotKeyInfo>();

        }

        public List<HotKeyInfo> GetHotKeyInfo()
        {
            return _hotKeys;
        }

        public void RegisterHotKey(Keys key, Keys modifiers)
        {
            var hotKeyInfo = new HotKeyInfo { Key = key, Modifiers = modifiers };
            _hotKeys.Add(hotKeyInfo);
        }
        public void UnregisterHotKey(HotKeyInfo hotKeyInfo)
        {
            _hotKeys.Remove(hotKeyInfo);
        }

        public void UnregisterAllHotKeys()
        {
            _hotKeys.Clear();
        }

        public void UnregisterHotKeys(Control control)
        {
            control.KeyDown -= ControlKeyDown;
        }

        public void RegisterHotKeys(Control control)
        {
            control.KeyDown += ControlKeyDown;
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            foreach (var hotKeyInfo in _hotKeys)
            {
                if (e.KeyCode == hotKeyInfo.Key && e.Modifiers == hotKeyInfo.Modifiers)
                {
                    OnHotKeyPressed(new HotKeyEventArgs(hotKeyInfo.Key, hotKeyInfo.Modifiers));
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            }
        }
        protected virtual void OnHotKeyPressed(HotKeyEventArgs e)
        {
            HotKeyPressed?.Invoke(this, e);
        }
    }

    public class HotKeyEventArgs(Keys key, Keys modifiers) : EventArgs
    {
        public Keys Key { get; } = key;
        public Keys Modifiers { get; } = modifiers;
    }
}
