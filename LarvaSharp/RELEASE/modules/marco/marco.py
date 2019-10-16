import sys
boiler_path = "\\".join(sys.argv[0].split("\\")[:-2])
sys.path.append(boiler_path)
import PyBoiler

my = PyBoiler.Boilerplate()

PyBoiler.Log(" ".join(my.args)).to_larva()
