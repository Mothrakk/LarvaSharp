import time
import sys
import os

my_name = sys.argv[0].split("\\")[-1].split(".")[0]
p = f"pipeline\\{my_name}.txt"

i = 0
while True:
     out = [str(i)]
     if os.path.isfile(p):
          with open(p, "r") as fptr:
               lines = fptr.read().strip().split("\n")
          if lines:
               with open(p, "w") as fptr:
                    pass
               out += lines
     with open("pipeline\\larva.txt", "a") as fptr:
          fptr.write("\n".join(out))
     time.sleep(2)
     i += 1