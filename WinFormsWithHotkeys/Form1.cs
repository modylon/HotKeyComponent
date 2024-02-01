namespace WinFormsWithHotkeys
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            hotKeyComponent1.RegisterHotKey(Keys.A, Keys.Control);
            hotKeyComponent1.RegisterHotKeys(this);
            hotKeyComponent1.HotKeyPressed += HotKeyComponent1_HotKeyPressed;
            PopulateComboBoxForKey();
            PopulateComboBoxForModifier();
            listBox1.DataSource = null;
            listBox1.DataSource = hotKeyComponent1.GetHotKeyInfo();
        }

        private void PopulateComboBoxForModifier()
        {
            Array keysArray = Enum.GetValues(typeof(Keys));
            foreach (Keys key in keysArray)
            {
                comboBox1.Items.Add(key);
            }
        }

        private void PopulateComboBoxForKey()
        {
            Array keysArray = Enum.GetValues(typeof(Keys));
            foreach (Keys key in keysArray)
            {
                comboBox2.Items.Add(key);
            }
        }

        private void HotKeyComponent1_HotKeyPressed(object sender, HotKeyEventArgs e)
        {
            MessageBox.Show($"Hotkey pressed: {e.Modifiers} + {e.Key}");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var modifier = comboBox1.SelectedItem is Keys keys ? keys : Keys.None;
            var key = comboBox2.SelectedItem is Keys modifiers ? modifiers : Keys.None;
            hotKeyComponent1.RegisterHotKey(key, modifier);
            var hotkeys = hotKeyComponent1.GetHotKeyInfo();
            listBox1.DataSource = null;
            listBox1.DataSource = hotkeys;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var selListItems = listBox1.SelectedItems;
            var source = (List<HotKeyComponent.HotKeyInfo>)listBox1.DataSource!;

            foreach (var itm in selListItems.OfType<HotKeyComponent.HotKeyInfo>().ToList())
            {
                source.Remove(itm);
                hotKeyComponent1.UnregisterHotKey(itm);
            }
            listBox1.DataSource = null;
            listBox1.DataSource = source;
        }
    }
}
