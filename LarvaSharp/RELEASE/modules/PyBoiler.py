import sys
import time
import os
import datetime
import subprocess

class Boilerplate:
    """Class meant for use by the subprocesses. Contains boilerplate functionality."""
    def __init__(self):
        self.name = sys.argv[0].split("\\")[-1].split(".")[0]
        self.dir_path = "\\".join(sys.argv[0].split("\\")[:-1])
        self.larva_pid = sys.argv[1]
        self.real_args = sys.argv[2:]
    
    def read_from_larva(self) -> list:
        """Check if there are new inputs from Larva.

        Returns a list where each element is a command (line) from Larva."""
        return file_flush(pipe_path(self.name))

    def larva_alive(self) -> None:
        """Check if Larva is still alive. If not, kill the process."""
        if not pid_alive(self.larva_pid):
            exit(1)

class Log:
    def __init__(self, contents: str, use_timestamp=True):
        self.contents = str(contents)
        self.name = sys.argv[0].split("\\")[-1].split(".")[0]
        self.use_timestamp = use_timestamp

    def build(self) -> str:
        log = f"{timestamp()} " * self.use_timestamp
        log += f"{self.name}: {self.contents}"
        return log

    def to_larva(self, pipeline=True) -> None:
        """Display the log.
        
        Use the pipeline if `pipeline`, else print out the log."""
        if pipeline:
            file_write(pipe_path("larva"), self.build(), "a")
        else:
            print(self.build())

def tick(x: float) -> float:
    """Sleep for x seconds. Returns x."""
    time.sleep(x)
    return x

def file_read(path: str) -> str:
    """Wrapper function to read from a file in one line.
    
    Returns empty string if file doesn't exist."""
    if os.path.isfile(path):
        with open(path, "r") as fptr:
            return fptr.read()
    return ""

def file_write(path: str, contents="", mode="w") -> None:
    """Wrapper function to write to a file in one line."""
    with open(path, mode) as fptr:
        fptr.write(f"{contents}\n")

def file_flush(path: str) -> list:
    """Read from a file and return the contents as a list split by newlines, leaving the file empty afterwards.
    
    Returns empty list is file doesn't exist."""
    contents = file_read(path).strip()
    if not contents:
        return []
    file_write(path)
    return [line for line in contents.split("\n") if line]

def timestamp() -> str:
    """Returns a timestamp in the form of %H:%M:%S"""
    return f"[{datetime.datetime.now().strftime('%H:%M:%S')}]"

def pid_alive(pid) -> bool:
    """Check if given process (pid) is still alive."""
    capture = subprocess.run(f'TASKLIST /FI "PID eq {pid}" /FO CSV /NH', capture_output=True)
    return str(capture.stdout)[2] == '"' # Don't question this

def pipe_path(name: str, extension=".txt") -> str:
    """Build a path to the filename in the pipeline folder."""
    return f"pipeline\\{name}{extension}"
