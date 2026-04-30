from utils import load_json, ticket_generator
from collections import Counter
from datetime import datetime
from logger import log_info, log_error
from itil import check_sla


def generate_report():
    try:
        tickets = load_json("data/tickets.json")
        problems = load_json("data/problems.json")

        if not tickets:
            print("No data available")
            return

        total = len(tickets)
        open_t = len([t for t in tickets if t["status"] == "Open"])
        closed = len([t for t in tickets if t["status"] == "Closed"])
        high = len([t for t in tickets if t["priority"] == "P1"])

        # -------- Most common issue --------
        issues = [t["issue"] for t in tickets]
        most_common = Counter(issues).most_common(3)  # Top 3

        # -------- Department analysis --------
        departments = [t["dept"] for t in tickets]
        top_dept = Counter(departments).most_common(3)

        # -------- SLA breaches --------
        sla_breach = sum(1 for t in tickets if check_sla(t))

        # -------- Avg resolution time --------
        resolution_times = []
        for t in tickets:
            if t["status"] == "Closed":
                created = datetime.strptime(t["created"], "%Y-%m-%d %H:%M:%S")
                now = datetime.now()
                resolution_times.append((now - created).total_seconds() / 3600)

        avg_resolution = round(sum(resolution_times) / len(resolution_times), 2) if resolution_times else 0

        # -------- Monthly Report --------
        current_month = datetime.now().month
        monthly_tickets = [
            t for t in tickets
            if datetime.strptime(t["created"], "%Y-%m-%d %H:%M:%S").month == current_month
        ]

        # -------- OUTPUT --------
        print("\n===== DAILY REPORT =====")
        print("Total Tickets:", total)
        print("Open Tickets:", open_t)
        print("Closed Tickets:", closed)
        print("High Priority:", high)
        print("SLA Breaches:", sla_breach)

        print("\nTop Issues:")
        for issue, count in most_common:
            print(f"  {issue} → {count}")

        print("\nTop Departments:")
        for dept, count in top_dept:
            print(f"  {dept} → {count}")

        print("\nAvg Resolution Time (hrs):", avg_resolution)

        print("\n===== MONTHLY REPORT =====")
        print("Tickets this month:", len(monthly_tickets))

        # -------- Problem Records --------
        if problems:
            print("\nRepeated Problems:")
            for p in problems:
                print(f"  {p['issue']} → {p['count']} times")

        # -------- Generator usage --------
        print("\nSample Tickets (Generator):")
        for t in ticket_generator(tickets):
            print(t)
            break  # show one sample

        log_info("Report generated successfully")

    except Exception as e:
        log_error(f"Report generation failed: {str(e)}")
        print("❌ Error generating report:", e)