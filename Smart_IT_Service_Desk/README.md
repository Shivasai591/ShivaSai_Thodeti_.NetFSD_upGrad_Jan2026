# Smart IT Service Desk System (Python Project)

## Overview

The **Smart IT Service Desk System** is a Python-based application designed to simulate real-world IT service management operations. It enables efficient handling of support tickets, problem tracking, logging, monitoring, and report generation.

This project demonstrates core concepts of **automation, data handling, and system design**, making it highly relevant for roles in **Software Development, IT Support, and Data Engineering**.

---

## Objectives

* Automate IT support workflows
* Manage tickets and problem records
* Maintain logs for monitoring system activities
* Generate reports for analysis
* Provide a structured and scalable system

---

## Features

### Ticket Management

* Create new tickets
* Update ticket details
* Close resolved tickets
* Track ticket status

### Problem Management

* Record recurring issues
* Maintain problem history
* Link tickets with problems

### Logging System

* Logs system activities
* Stores logs in text files
* Helps in debugging and monitoring

### Monitoring

* Tracks system performance
* Identifies issues
* Ensures smooth operation

### Report Generation

* Generates reports from ticket data
* Helps analyze system usage

---

## Technologies Used

| Technology    | Purpose                   |
| ------------- | ------------------------- |
| Python        | Core programming language |
| JSON          | Data storage              |
| CSV           | Backup and reporting      |
| File Handling | Logging & persistence     |

---

## 📂 Project Structure

```bash
Smart_IT_Service_Desk/
│
├── main.py              # Entry point of the application
├── itil.py              # ITIL logic implementation
├── tickets.py           # Ticket management functions
├── reports.py           # Report generation
├── monitor.py           # Monitoring system
├── logger.py            # Logging functionality
├── utils.py             # Helper utilities
│
├── data/
│   ├── tickets.json     # Ticket records
│   ├── problems.json    # Problem records
│   ├── backup.csv       # Backup data
│   └── logs.txt         # System logs
│
├── screenshots/         # Output screenshots (optional)
│
├── requirements.txt     # Dependencies
└── README.md            # Project documentation
```

---

## Installation & Setup

### Prerequisites

* Python 3.x installed

### Steps to Run

1. Clone the repository:

```bash
git clone https://github.com/Shivasai591/ShivaSai_Thodeti_.NetFSD_upGrad_Jan2026.git
```

2. Navigate to project folder:

```bash
cd Smart_IT_Service_Desk
```

3. Run the application:

```bash
python main.py
```

---

## Sample Data

The system uses pre-existing data stored in:

```bash
data/
```

Includes:

* `tickets.json`
* `problems.json`
* `backup.csv`

---

##  Logs Output

System logs are stored in:

```bash
data/logs.txt
```

These logs track:

* Ticket creation
* Updates
* Errors
* System activity
---

##  Key Learnings

* File handling in Python
* JSON & CSV data processing
* Modular programming
* Logging and monitoring systems
* Real-world IT service workflow simulation
* Git & GitHub version control

---

##  Use Cases

* IT Help Desk Systems
* Ticket Management Tools
* System Monitoring Solutions
* Learning ITIL Concepts

---

##  Future Enhancements

* Add database integration (MySQL / MongoDB)
* Build web interface (Flask / Django)
* Add authentication system
* Deploy as a web service

---

##  Author

**ShivaSai Thodeti**

---

##  Conclusion

This project showcases a complete **end-to-end IT Service Desk system**, combining backend logic, data handling, and structured design.

It reflects strong understanding of **real-world system workflows and software development practices**.

---
