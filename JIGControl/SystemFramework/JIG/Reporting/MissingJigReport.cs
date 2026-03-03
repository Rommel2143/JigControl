using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QCInventoryF2.JIG.Reporting
{
    public partial class MissingJigReport : Form
    {
        private ReportDataset _report;
        public MissingJigReport()
        {
            InitializeComponent();
            cmbMonth.SelectedIndex = DateTime.Now.Month - 1;
        }

        private void MissingJigReport_Load(object sender, EventArgs e)
        {
            // Create a single ReportDataset instance
            _report = new ReportDataset();

            // Load scanned JIGs
            _report.LoadJIGData(cmbMonth.SelectedIndex + 1);

            // Load missing/unscanned JIGs
            _report.LoadJIGMissing(cmbMonth.SelectedIndex + 1);

            // Clear any previous data sources
            reportViewer1.LocalReport.DataSources.Clear();

            // Add both datasets to the report
            reportViewer1.LocalReport.DataSources.Add(
                new Microsoft.Reporting.WinForms.ReportDataSource(
                    "JIG_Data",
                    (System.Data.DataTable)_report.JIG_Data));

            reportViewer1.LocalReport.DataSources.Add(
                new Microsoft.Reporting.WinForms.ReportDataSource(
                    "JIG_Missing",
                    (System.Data.DataTable)_report.JIG_Missing));

            // Refresh the report
            reportViewer1.RefreshReport();
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            MissingJigReport_Load(sender, e);
        }
    }
}
