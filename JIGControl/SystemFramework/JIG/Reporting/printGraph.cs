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
    public partial class printGraph : Form
    {
        private ReportDataset _report;
        public printGraph()
        {
            InitializeComponent();
            cmbMonth.SelectedIndex = DateTime.Now.Month - 1; 
        }

        private void printGraph_Load(object sender, EventArgs e)
        {
            // Create a single ReportDataset instance
            _report = new ReportDataset();

            // Load datasets
            _report.LoadJIGData(cmbMonth.SelectedIndex+1);      // scanned
            _report.LoadJIGMissing(cmbMonth.SelectedIndex + 1);   // missing / unscanned
            _report.LoadJIGSummary(cmbMonth.SelectedIndex + 1);   // summary per section

            // Clear any previous data sources
            reportViewer1.LocalReport.DataSources.Clear();

            // Add datasets to the report
            reportViewer1.LocalReport.DataSources.Add(
                new Microsoft.Reporting.WinForms.ReportDataSource(
                    "JIG_Data",
                    (System.Data.DataTable)_report.JIG_Data));

            reportViewer1.LocalReport.DataSources.Add(
                new Microsoft.Reporting.WinForms.ReportDataSource(
                    "JIG_Missing",
                    (System.Data.DataTable)_report.JIG_Missing));

            reportViewer1.LocalReport.DataSources.Add(
                new Microsoft.Reporting.WinForms.ReportDataSource(
                    "JIG_Summary",
                    (System.Data.DataTable)_report.JIG_Summary));

            // Refresh the report
            reportViewer1.RefreshReport();
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            printGraph_Load(sender, e);
        }
    }
}
