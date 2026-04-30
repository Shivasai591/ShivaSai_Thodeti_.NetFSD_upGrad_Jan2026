import logging
import os
from logging.handlers import RotatingFileHandler

LOG_FILE = "data/logs.txt"

# Ensure data folder exists
os.makedirs("data", exist_ok=True)

# Create logger
logger = logging.getLogger("IT_Service_Desk")
logger.setLevel(logging.DEBUG)

# Formatter
formatter = logging.Formatter(
    "%(asctime)s - %(levelname)s - %(message)s"
)

# ---------------- FILE HANDLER (WITH ROTATION) ----------------
file_handler = RotatingFileHandler(
    LOG_FILE,
    maxBytes=1_000_000,  # 1 MB
    backupCount=3,
    encoding="utf-8"
)
file_handler.setLevel(logging.DEBUG)
file_handler.setFormatter(formatter)

# ---------------- CONSOLE HANDLER ----------------
console_handler = logging.StreamHandler()
console_handler.setLevel(logging.INFO)
console_handler.setFormatter(formatter)

# Avoid duplicate handlers
if not logger.handlers:
    logger.addHandler(file_handler)
    logger.addHandler(console_handler)


# ---------------- LOG FUNCTIONS ----------------
def log_debug(msg):
    logger.debug(msg)


def log_info(msg):
    logger.info(msg)


def log_warning(msg):
    logger.warning(msg)


def log_error(msg):
    logger.error(msg)


def log_critical(msg):
    logger.critical(msg)