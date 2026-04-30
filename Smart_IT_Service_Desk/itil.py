from datetime import datetime, timedelta
from utils import load_json, save_json
from logger import log_warning, log_info, log_error

SLA_RULES = {
    "P1": 1,
    "P2": 4,
    "P3": 8,
    "P4": 24
}

PROBLEM_FILE = "data/problems.json"


# ---------------- SLA CHECK ----------------
def check_sla(ticket):
    try:
        created = datetime.strptime(ticket["created"], "%Y-%m-%d %H:%M:%S")
        sla_hours = SLA_RULES.get(ticket["priority"], 8)

        deadline = created + timedelta(hours=sla_hours)

        if datetime.now() > deadline and ticket["status"] != "Closed":
            log_warning(f"SLA Breached for Ticket {ticket['ticket_id']}")
            return True

        return False

    except Exception as e:
        log_error(f"SLA Check Failed: {e}")
        return False


# ---------------- ESCALATION ----------------
def escalate_ticket(ticket):
    print(f"🚨 Escalating Ticket: {ticket['ticket_id']}")
    log_warning(f"Ticket Escalated: {ticket['ticket_id']}")


# ---------------- PROBLEM MANAGEMENT ----------------
def problem_management(issue):
    try:
        issue = issue.strip().lower()

        problems = load_json(PROBLEM_FILE)
        found = False

        for p in problems:
            if p["issue"] == issue:
                p["count"] += 1
                found = True

                if p["count"] == 5:
                    print("⚠ Problem Record Created:", issue)

                    # Create ITIL-style problem record
                    p["created"] = datetime.now().strftime("%Y-%m-%d %H:%M:%S")
                    p["status"] = "Open"
                    p["severity"] = "High"

                    log_info(f"Problem Record Created for issue: {issue}")

                break

        if not found:
            problems.append({
                "issue": issue,
                "count": 1,
                "status": "Monitoring"
            })

        save_json(PROBLEM_FILE, problems)
        return True

    except Exception as e:
        log_error(f"Problem Management Failed: {e}")
        return False