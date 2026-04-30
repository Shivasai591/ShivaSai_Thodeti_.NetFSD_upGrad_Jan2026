from datetime import datetime
import uuid
import re


class Ticket:
    ticket_count = 0

    def __init__(self, employee, dept, issue, category):
        # Regex validation
        if not re.match("^[A-Za-z ]+$", employee):
            raise ValueError("Invalid employee name")

        if not issue:
            raise ValueError("Issue cannot be empty")

        Ticket.ticket_count += 1

        self._ticket_id = str(uuid.uuid4())[:8]
        self._employee = employee.strip()
        self._dept = dept.strip()
        self._issue = issue.strip().lower()
        self._category = category.strip()
        self._priority = self._set_priority(category)
        self._status = "Open"
        self._created = datetime.now().strftime("%Y-%m-%d %H:%M:%S")

    # ---------- GETTERS ----------
    def get_ticket_id(self):
        return self._ticket_id

    def get_status(self):
        return self._status

    # ---------- SETTERS ----------
    def set_status(self, status):
        if status not in ["Open", "In Progress", "Closed"]:
            raise ValueError("Invalid status")
        self._status = status

    # ---------- STATIC METHOD ----------
    @staticmethod
    def priority_rules(category):
        rules = {
            "Server Down": "P1",
            "Internet Down": "P2",
            "Laptop Slow": "P3",
            "Password Reset": "P4"
        }
        return rules.get(category, "P3")

    # ---------- BUSINESS LOGIC ----------
    def _set_priority(self, category):
        return Ticket.priority_rules(category)

    def close_ticket(self):
        self._status = "Closed"

    # ---------- SPECIAL METHODS ----------
    def __str__(self):
        return f"[{self._ticket_id}] {self._employee} | {self._issue} | {self._status}"

    def __repr__(self):
        return f"Ticket({self._ticket_id}, {self._employee}, {self._status})"

    # ---------- TO DICT ----------
    def to_dict(self):
        return {
            "ticket_id": self._ticket_id,
            "employee": self._employee,
            "dept": self._dept,
            "issue": self._issue,
            "category": self._category,
            "priority": self._priority,
            "status": self._status,
            "created": self._created
        }


# ---------- INHERITANCE + POLYMORPHISM ----------
class IncidentTicket(Ticket):
    def __str__(self):
        return f"[INCIDENT] {super().__str__()}"


class ServiceRequest(Ticket):
    def __str__(self):
        return f"[SERVICE] {super().__str__()}"


# ---------- PROBLEM RECORD ----------
class ProblemRecord:
    def __init__(self, issue):
        self.issue = issue
        self.count = 1
        self.status = "Open"
        self.created = datetime.now().strftime("%Y-%m-%d %H:%M:%S")

    def increment(self):
        self.count += 1

    def close_problem(self):
        self.status = "Closed"

    def to_dict(self):
        return {
            "issue": self.issue,
            "count": self.count,
            "status": self.status,
            "created": self.created
        }