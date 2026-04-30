import json
import csv
import os
from functools import wraps
from logger import log_info, log_warning, log_error

TICKET_FILE = "data/tickets.json"
PROBLEM_FILE = "data/problems.json"
BACKUP_FILE = "data/backup.csv"

# Ensure data folder exists
os.makedirs("data", exist_ok=True)


# ------------------ JSON LOAD ------------------
def load_json(file):
    try:
        if not os.path.exists(file):
            log_warning(f"File not found: {file}")
            return []

        with open(file, "r") as f:
            return json.load(f)

    except json.JSONDecodeError:
        log_error(f"JSON format issue in {file}")
        return []

    except Exception as e:
        log_error(f"Failed to read {file}: {e}")
        return []


# ------------------ JSON SAVE ------------------
def save_json(file, data):
    try:
        with open(file, "w") as f:
            json.dump(data, f, indent=4)

        log_info(f"Data saved to {file}")

    except Exception as e:
        log_error(f"Failed to save {file}: {e}")


# ------------------ JSON APPEND ------------------
def append_json(file, record):
    try:
        data = load_json(file)
        data.append(record)
        save_json(file, data)

        log_info(f"Record appended to {file}")

    except Exception as e:
        log_error(f"Append failed: {e}")


# ------------------ CSV BACKUP ------------------
def backup_to_csv(tickets):
    try:
        if not tickets:
            log_warning("No tickets to backup")
            return

        with open(BACKUP_FILE, "w", newline="") as f:
            writer = csv.DictWriter(f, fieldnames=tickets[0].keys())
            writer.writeheader()
            writer.writerows(tickets)

        log_info("Backup saved to CSV")
        print("✅ Backup saved to CSV")

    except Exception as e:
        log_error(f"CSV backup failed: {e}")
        print("❌ Backup failed")


# ------------------ GENERATOR ------------------
def ticket_generator(tickets):
    for t in tickets:
        yield t


# ------------------ SORT FUNCTION ------------------
def sort_tickets(tickets, key="priority"):
    try:
        return sorted(tickets, key=lambda x: x.get(key, ""))
    except Exception as e:
        log_error(f"Sorting failed: {e}")
        return tickets


# ------------------ DECORATOR ------------------
def log_decorator(func):
    @wraps(func)
    def wrapper(*args, **kwargs):
        log_info(f"Function started: {func.__name__}")
        try:
            result = func(*args, **kwargs)
            log_info(f"Function completed: {func.__name__}")
            return result
        except Exception as e:
            log_error(f"Error in {func.__name__}: {e}")
            raise
    return wrapper