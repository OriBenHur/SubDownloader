using System;
using System.Windows.Forms;
using Microsoft.Win32;


namespace SubDownloader
{
    public partial class ProgramSettings : Form
    {
        private static RegistryKey StartupKey => Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

        private static bool StartOnStartup
        {
            get
            {
                var obj = StartupKey.GetValue("SubtitleDownloader");
                return obj != null && bool.Parse((string)obj);
            }
            set => StartupKey.SetValue("SubtitleDownloader", value);
        }

        public ProgramSettings()
        {
            InitializeComponent();
            btnRemoveTranslator.Enabled = false;
            _tempTranslators = Data.Instance.CustomNameTranslator;
            foreach (var key in _tempTranslators.Keys)
                lbTranslators.Items.Add(key + " = " + _tempTranslators[key]);
            numUpDownSimConnections.Value = Data.Instance.MaxSimConnections;
            numUpDownScanInterval.Value = Data.Instance.UpdateInterval;
            chkBoxRunAtStartup.Checked = StartOnStartup;
            _cbAutoMode.Checked = Data.Instance.AutoMode;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            StartOnStartup = chkBoxRunAtStartup.Checked;
            Data.Instance.AutoMode = _cbAutoMode.Checked;
            Data.Instance.UpdateInterval = (int)numUpDownScanInterval.Value;
            Data.Instance.MaxSimConnections = (int)numUpDownSimConnections.Value;
            Data.Instance.CustomNameTranslator = _tempTranslators;
            Data.Save();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void lbTranslators_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRemoveTranslator.Enabled = lbTranslators.SelectedIndex > -1;
        }

        private void btnAddTranslator_Click(object sender, EventArgs e)
        {
            if (txtTranslatorReplace.Text.Length == 0 || txtTranslatorWith.Text.Length == 0 || _tempTranslators.ContainsKey(txtTranslatorReplace.Text))
                return;
            _tempTranslators.Add(txtTranslatorReplace.Text, txtTranslatorWith.Text);
            lbTranslators.Items.Add(txtTranslatorReplace.Text + " = " + txtTranslatorWith.Text);
            txtTranslatorReplace.Clear();
            txtTranslatorWith.Clear();
        }

        private void btnRemoveTranslator_Click(object sender, EventArgs e)
        {
            var selectedIndex = lbTranslators.SelectedIndex;
            if (selectedIndex < 0)
                return;
            var strArray = ((string)lbTranslators.Items[selectedIndex]).Split('=');
            var key = "";
            if (strArray.Length == 2)
                key = strArray[0].Trim();
            _tempTranslators.Remove(key);
            lbTranslators.Items.RemoveAt(lbTranslators.SelectedIndex);
            btnRemoveTranslator.Enabled = false;
        }
    }
}
