import time
import sys

while True:
    with open("pipeline\\larva.txt", "a") as fptr:
         fptr.write(f"{str(sys.argv)}\n")
    time.sleep(2)
