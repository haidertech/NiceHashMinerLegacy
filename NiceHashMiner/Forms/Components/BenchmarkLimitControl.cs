﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using NiceHashMiner.Enums;

namespace NiceHashMiner.Forms.Components {
    public partial class BenchmarkLimitControl : UserControl {

        public string GroupName {
            get {
                return groupBox1.Text;
            }
            set {
                if (value != null) groupBox1.Text = value;
            }
        }

        // int array reference property
        private int[] _timeLimits;
        public int[] TimeLimits {
            get { return _timeLimits; }
            set {
                if (value != null) {
                    _timeLimits = value;
                    for (int indexKey = 0; indexKey < _timeLimits.Length; ++indexKey) {
                        _textBoxes[(int)indexKey].Text = _timeLimits[indexKey].ToString();
                    }
                }
            }
        }
        // texbox references
        private TextBox[] _textBoxes;

        public BenchmarkLimitControl() {
            InitializeComponent();
            textBoxQuick.KeyPress += new KeyPressEventHandler(textBoxIntsOnly_KeyPress);
            textBoxStandard.KeyPress += new KeyPressEventHandler(textBoxIntsOnly_KeyPress);
            textBoxPrecise.KeyPress += new KeyPressEventHandler(textBoxIntsOnly_KeyPress);
            _textBoxes = new TextBox[] { textBoxQuick, textBoxStandard, textBoxPrecise };
        }

        #region Events
        private void textBoxQuick_TextChanged(object sender, EventArgs e) {
            setTimeLimit(BenchmarkPerformanceType.Quick, textBoxQuick.Text);
        }

        private void textBoxStandard_TextChanged(object sender, EventArgs e) {
            setTimeLimit(BenchmarkPerformanceType.Standard, textBoxStandard.Text);
        }

        private void textBoxPrecise_TextChanged(object sender, EventArgs e) {
            setTimeLimit(BenchmarkPerformanceType.Precise, textBoxPrecise.Text);
        }

        private void textBoxIntsOnly_KeyPress(object sender, KeyPressEventArgs e) {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) {
                e.Handled = true;
            }
        }
        #endregion // Events

        private void setTimeLimit(BenchmarkPerformanceType type, string numString) {
            if (_timeLimits == null) return;
            int value;
            if(Int32.TryParse(numString, out value)) {
                _timeLimits[(int)type] = value;
            }
        }

    }
}