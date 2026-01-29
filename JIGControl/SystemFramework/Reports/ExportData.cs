using System;
using System.Windows.Forms;
using ClosedXML.Excel;
using Guna.UI2.WinForms;

namespace QCInventoryF2.Reports
{
    public static class ExportData
    {
        public static void ToExcel(Guna2DataGridView dgv, string sheetName = "Report")
        {
            if (dgv == null || dgv.Rows.Count == 0)
            {
                MessageBox.Show("No data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel Files (*.xlsx)|*.xlsx";
                sfd.FileName = $"Export_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                if (sfd.ShowDialog() != DialogResult.OK)
                    return;

                using (XLWorkbook wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add(sheetName);

                    int colIndex = 1;

                    // 🔹 Add column headers
                    foreach (DataGridViewColumn col in dgv.Columns)
                    {
                        if (col.Visible)
                        {
                            ws.Cell(1, colIndex).Value = col.HeaderText;
                            ws.Cell(1, colIndex).Style.Font.Bold = true;
                            colIndex++;
                        }
                    }

                    int rowIndex = 2;

                    // 🔹 Add rows
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (row.IsNewRow) continue;

                        colIndex = 1;
                        foreach (DataGridViewColumn col in dgv.Columns)
                        {
                            if (col.Visible)
                            {
                                ws.Cell(rowIndex, colIndex).Value = row.Cells[col.Index].Value?.ToString();
                                colIndex++;
                            }
                        }
                        rowIndex++;
                    }

                    // 🔹 Beautify
                    ws.Columns().AdjustToContents();
                    ws.SheetView.FreezeRows(1);

                    wb.SaveAs(sfd.FileName);
                }

                MessageBox.Show("Export completed successfully!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
