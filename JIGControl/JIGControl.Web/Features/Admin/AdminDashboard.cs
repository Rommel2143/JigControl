using Dapper;
using JIGControl.Web.Common;

namespace JIGControl.Web.Features.Admin;

public sealed record SectionActivity(string Section, int MasterTotal, int NewThisMonth, DateTime? LastScan);
public sealed record ScannerActivity(string User, int Scans, DateTime LastScan);
public sealed record DayActivity(DateTime Day, int Scans);
public sealed record LabelCount(string Label, int Count);

/// Plant-wide numbers for the admin dashboard that the section pages don't already provide.
/// Coverage/missing per section comes from MissingBySectionHandler so it matches the Missing page.
public sealed record AdminActivity(
    List<SectionActivity> Sections,
    List<ScannerActivity> Scanners,
    List<DayActivity> Days,
    List<LabelCount> JigStatuses,
    List<LabelCount> ScanStatuses);

public sealed class AdminDashboardHandler(IDbConnectionFactory db)
{
    public async Task<AdminActivity> Handle(int year, int month, CancellationToken ct = default)
    {
        var from = new DateTime(year, month, 1);
        var p = new { year, month, from, to = from.AddMonths(1) };
        using var con = await db.OpenAsync(ct);

        var sections = await con.QueryAsync<(string Section, long MasterTotal, long NewThisMonth, DateTime? LastScan)>("""
            SELECT jm.section,
                   COUNT(*) AS master_total,
                   SUM(CASE WHEN jm.timestamp >= @from AND jm.timestamp < @to THEN 1 ELSE 0 END) AS new_this_month,
                   MAX(ls.last_scan) AS last_scan
            FROM jig_masterlist jm
            LEFT JOIN (SELECT jig_id, MAX(timestamp) AS last_scan FROM jig_inventory
                       WHERE scan_month = @month AND scan_year = @year GROUP BY jig_id) ls ON ls.jig_id = jm.jig_id
            GROUP BY jm.section
            """, p);

        var scanners = await con.QueryAsync<(string User, long Scans, DateTime LastScan)>("""
            SELECT CONCAT(ju.firstname, ' ', ju.lastname) AS user, COUNT(*) AS scans, MAX(ji.timestamp) AS last_scan
            FROM jig_inventory ji
            JOIN trc_user.jig_users ju ON ju.user_id = ji.user_id
            WHERE ji.scan_month = @month AND ji.scan_year = @year
            GROUP BY ji.user_id, ju.firstname, ju.lastname
            ORDER BY scans DESC
            """, p);

        var days = await con.QueryAsync<(DateTime Day, long Scans)>("""
            SELECT DATE(timestamp) AS day, COUNT(*) AS scans
            FROM jig_inventory
            WHERE scan_month = @month AND scan_year = @year
            GROUP BY DATE(timestamp)
            ORDER BY day
            """, p);

        var jigStatuses = await con.QueryAsync<(string? Label, long Count)>(
            "SELECT status AS label, COUNT(*) AS count FROM jig_masterlist GROUP BY status ORDER BY count DESC");

        var scanStatuses = await con.QueryAsync<(string? Label, long Count)>("""
            SELECT inventory_status AS label, COUNT(*) AS count FROM jig_inventory
            WHERE scan_month = @month AND scan_year = @year
            GROUP BY inventory_status ORDER BY count DESC
            """, p);

        static List<LabelCount> Labels(IEnumerable<(string? Label, long Count)> rows) =>
            rows.Select(r => new LabelCount(string.IsNullOrWhiteSpace(r.Label) ? "No status" : r.Label, (int)r.Count)).ToList();

        return new AdminActivity(
            sections.Select(s => new SectionActivity(s.Section ?? "", (int)s.MasterTotal, (int)s.NewThisMonth, s.LastScan)).ToList(),
            scanners.Select(s => new ScannerActivity(s.User, (int)s.Scans, s.LastScan)).ToList(),
            days.Select(d => new DayActivity(d.Day, (int)d.Scans)).ToList(),
            Labels(jigStatuses),
            Labels(scanStatuses));
    }
}
