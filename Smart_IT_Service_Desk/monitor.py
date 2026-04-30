import psutil
from datetime import datetime
from tickets import IncidentTicket
from utils import load_json, save_json
from logger import log_warning, log_error
from itil import escalate_ticket

FILE = "data/tickets.json"


def system_monitor():
    try:
        cpu = psutil.cpu_percent()
        ram = psutil.virtual_memory().percent
        disk_usage = psutil.disk_usage('/')
        disk_free = disk_usage.free / disk_usage.total * 100

        # Network usage (bytes sent/received)
        net = psutil.net_io_counters()
        network_load = (net.bytes_sent + net.bytes_recv) / (1024 * 1024)  # MB

        issues = []

        if cpu > 90:
            issues.append(("High CPU Usage", "P1"))

        if ram > 95:
            issues.append(("High Memory Usage", "P1"))

        if disk_free < 10:
            issues.append(("Low Disk Space", "P1"))

        # Simple threshold for network load
        if network_load > 500:  # Example threshold
            issues.append(("High Network Usage", "P2"))

        if not issues:
            print("✅ System running normal")
            return

        data = load_json(FILE)

        for issue, priority in issues:
            # Prevent duplicate ticket
            recent_ticket = any(
                t["issue"] == issue and t["status"] != "Closed"
                for t in data
            )

            if not recent_ticket:
                ticket = IncidentTicket(
                    "System",
                    "IT",
                    issue,
                    "Server Down"
                )

                data.append(ticket.to_dict())

                timestamp = datetime.now().strftime("%Y-%m-%d %H:%M:%S")

                log_warning(
                    f"[{timestamp}] Auto Ticket: {issue} | CPU:{cpu}% RAM:{ram}% DiskFree:{round(disk_free,2)}% Net:{round(network_load,2)}MB"
                )

                print(f"⚠ Auto Ticket Created for: {issue}")

                # Escalate critical issues
                if priority == "P1":
                    escalate_ticket(ticket.to_dict())

        save_json(FILE, data)

    except Exception as e:
        log_error(f"Monitoring Failed: {str(e)}")
        print("❌ Monitoring Error:", e)