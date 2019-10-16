import sys
sys.path.append("modules")
import PyBoiler

boiler = PyBoiler.Boilerplate()

PyBoiler.Log(" ".join(boiler.real_args)).to_larva()
