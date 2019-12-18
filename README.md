# LarvaSharp

## What is this?

The idea was to fix an issue that data hoarders/web scrapers/on-the-spectrum scripters run into. And that is running a dozen scripts simultaneously from which you want to read output from and sometimes pass input into. This results in a terribly cluttered screen.

So the idea was to turn this:

![a](https://i.imgur.com/EuM69JF.gif)

Into this:

![b](https://i.imgur.com/upqs7pl.gif)

### Basically, it's an I/O manager capable of handling a plethora of scripts/modules that run as independent processes.

Also, Larva can do other things through hardcoded commands. Like evaluating a string as if you were to pass it into Python. Makes for a quick calculator.

Sky's the limit, really.

## Functionality/how-to guide.

### I/O

Each folder in the `modules` folder is a module. In each module, there should be an executable file (.py or .exe) **that has the same name as the module itself.** So in the module `marco` there should be an executable `marco.py`. When you run `start marco` from Larva, `marco.py` is what gets called.

In the module, you may also add a `help.larva` file. This way, if you were to run `help marco` from Larva, it would read from that file.

Commands sent from Larva are available to be read from the `{modulename}.txt` file in the `pipeline` folder. So for example, `marco` should read from `pipeline\marco.txt`, as those are the commands meant for that specific module.

If you wish to send output to Larva and have it printed out, append that output to `pipeline\larva.txt`. Larva has a clock cycle where each clock it checks the inputs of that file, flushes it, and prints out the contents.

Modules are entirely independent of Larva itself. You can edit, add, remove modules during Larva's runtime.

### Autostarting

In the `autostart.larva` file in the `pipeline` folder, you may add lines in the following format:
`x:a b c ... z`

Where x is the module name and a b c ... z are the arguments, seperated by spaces. So for example:
`marco:polo test test`

Would autostart `marco` on boot with the argv[3] of `{ "polo", "test", "test" }`.

### Runtime example

Assuming a module `marco` that simply prints out the arguments you start it with and an infinitely running module `adder` that adds together all the numbers you feed into its input and prints out the sum.

`start marco hehe xd` <- Starts a new process for module `marco` by calling its respective executable (assuming `marco.py`) with the arguments `hehe xd`.

marco.py executes and writes `hehe xd` into `pipeline\larva.txt`.

Larva prints out `hehe xd` as it flushes the `pipeline\larva.txt` file.

`start adder` <- Starts a new process for module `adder` by calling its respective executable (assuming `adder.py`).

`adder 5 3 9` <- Larva takes the arguments `{"5", "3", "9"}` and appends them to `pipeline\adder.txt` as a joined string (`5 3 9`).

Assuming adder operates on a clock cycle of one second, it flushes `pipeline\adder.txt` each second. It reads `5 3 9`, adds the numbers together into `17` and appends it to `pipeline\larva.txt`.

Larva prints out `17`.

`alive adder` <- lets you know that adder is currently running.

`kill adder` <- manually kill the process, as it is infinitely running.

`alive adder` <- lets you know that adder is dead.

Logo by [Freepik](https://www.flaticon.com/authors/freepik).