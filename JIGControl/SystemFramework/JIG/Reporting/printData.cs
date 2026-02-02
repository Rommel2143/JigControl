using System;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace QCInventoryF2.JIG.Reporting
{
    public partial class printData : Form
    {
        private ReportDataset _report;

        public printData()
        {
            InitializeComponent();
        }

        private void printData_Load(object sender, EventArgs e)
        {
            // Create a single ReportDataset instance
            _report = new ReportDataset();

            // Load scanned JIGs
            _report.LoadJIGData();

            // Load missing/unscanned JIGs
            _report.LoadJIGMissing();

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

    }
}
