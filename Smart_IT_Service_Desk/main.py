from tickets import IncidentTicket
from utils import load_json, save_json, backup_to_csv, log_decorator, ticket_generator
from itil import check_sla, problem_management, escalate_ticket
from monitor import system_monitor
from reports import generate_report
from logger import log_info, log_error, log_warning

FILE = "data/tickets.json"


@log_decorator
def create_ticket():
    try:
        emp = input("Employee Name: ")
        dept = input("Department: ")
        issue = input("Issue: ")
        cat = input("Category: ")

        if not emp or not issue:
            raise ValueError("Empty values not allowed")

        t = IncidentTicket(emp, dept, issue, cat)

        data = load_json(FILE)
        data.append(t.to_dict())
        save_json(FILE, data)

        problem_management(issue)

        log_info(f"Ticket Created: {t.get_ticket_id()}")
        print("✅ Ticket created successfully")

    except Exception as e:
        log_error(str(e))
        print("❌ Error:", e)


def view_tickets():
    try:
        data = load_json(FILE)

        if not data:
            print("No tickets found")
            return

        # Sorting by priority
        data = sorted(data, key=lambda x: x["priority"])

        # Using generator
        for t in ticket_generator(data):
            print(t)

    except Exception as e:
        log_error(str(e))


def search_ticket():
    try:
        tid = input("Enter Ticket ID: ")
        data = load_json(FILE)

        result = list(filter(lambda t: t["ticket_id"] == tid, data))

        if result:
            print("✅ Found:", result[0])
            log_info(f"Ticket searched: {tid}")
        else:
            print("❌ Ticket not found")
            log_warning(f"Search failed for Ticket: {tid}")

    except Exception as e:
        log_error(str(e))


def update_ticket():
    try:
        tid = input("Enter Ticket ID: ")
        data = load_json(FILE)

        for t in data:
            if t["ticket_id"] == tid:
                new_status = input("New Status (Open/In Progress/Closed): ")

                if new_status not in ["Open", "In Progress", "Closed"]:
                    print("❌ Invalid status")
                    return

                t["status"] = new_status

                save_json(FILE, data)
                log_info(f"Ticket Updated: {tid}")
                print("✅ Ticket updated")
                return

        print("❌ Ticket not found")

    except Exception as e:
        log_error(str(e))


def close_ticket():
    try:
        tid = input("Enter Ticket ID: ")
        data = load_json(FILE)

        for t in data:
            if t["ticket_id"] == tid:
                t["status"] = "Closed"

                save_json(FILE, data)
                log_info(f"Ticket Closed: {tid}")
                print("✅ Ticket closed")
                return

        print("❌ Ticket not found")

    except Exception as e:
        log_error(str(e))


def delete_ticket():
    try:
        tid = input("Enter Ticket ID: ")
        data = load_json(FILE)

        new_data = list(filter(lambda t: t["ticket_id"] != tid, data))

        if len(data) == len(new_data):
            print("❌ Ticket not found")
            return

        save_json(FILE, new_data)
        log_warning(f"Ticket Deleted: {tid}")
        print("✅ Ticket deleted")

    except Exception as e:
        log_error(str(e))


def sla_check():
    try:
        data = load_json(FILE)

        for t in data:
            if check_sla(t):
                print("⚠ SLA BREACHED:", t["ticket_id"])
                log_warning(f"SLA Breach: {t['ticket_id']}")

                # Escalation added
                escalate_ticket(t)

    except Exception as e:
        log_error(str(e))


def backup():
    try:
        data = load_json(FILE)
        backup_to_csv(data)
        print("✅ Backup Completed")

    except Exception as e:
        log_error(str(e))


def menu():
    while True:
        print("""
1.Create Ticket
2.View Tickets
3.Search Ticket
4.Update Ticket
5.Close Ticket
6.Delete Ticket
7.SLA Check
8.Report
9.Monitor
10.Backup
11.Exit
""")

        ch = input("Choice: ")

        if ch == "1":
            create_ticket()
        elif ch == "2":
            view_tickets()
        elif ch == "3":
            search_ticket()
        elif ch == "4":
            update_ticket()
        elif ch == "5":
            close_ticket()
        elif ch == "6":
            delete_ticket()
        elif ch == "7":
            sla_check()
        elif ch == "8":
            generate_report()
        elif ch == "9":
            system_monitor()
        elif ch == "10":
            backup()
        elif ch == "11":
            print("Exiting...")
            break
        else:
            print("Invalid choice")


if __name__ == "__main__":
    menu()