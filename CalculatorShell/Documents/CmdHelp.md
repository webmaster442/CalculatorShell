# cd/cwd

Change the current directory

## Usage

```
cd [directory]
cd ..
cd ~
```

The Directory can be a subdirectory of the current directory or a full path.
If the directory is .. then the new directory will be the parent dir of the
current directory.

If the Directory is the ~ symbol, then the work directory will be the current
users profile directory

# clear/cls

Clear the Screen.

## Usage

```
clear
```

# cmdList

List all known commands and parseable function names.

## Usage

```
cmdlist
```

# date

Print current Date in year/month/day format.

## Usage

```
date
```

# divisors

List all the divisiors of a given number.

## Usage

```
divisors [number]
```

# eval

Evaluate an expression and print out the result.

The expression can be a  previously defined expression, which was set with
the exp command or an expression string like (33+x)*b

## usage

```
Eval [expression]
```

# exit

Exit the program

## Usage

```
exit
```

# expr

Expression list, print or set depending on argument count. Note: Expression
names must start with the $ char.

Without arguments it prints out the currently defined named expressions.

If one argument is given it prints out that specific expression.

If invoked with two arguments, then it saves an expression.

## Usage

```
expr
expr [expression name]
expr [expression name] [expression value]
```

# info

Get detailed info on an expressions result. In function it's identical to the
eval command, but gives more information.

For double numbers the value is displayed in decimal and in binary.

If the number is an integer, then the number is displayed in decimal, octal,
binary and hex. Also, the number of bytes also displayed.

## Usage

```
info [expression]
```

# ls/dir

List files and directories of the current working directory

## Usage

```
ls
```

# mem

Variable list, print or set depending on argument count.

Without arguments it prints out the currently defined variables, that can be
used in expressions.

If one argument is given it prints out that specific variables value.

If invoked with two arguments, then it sets a variable

## Usage

```
mem
mem [variable name]
mem [variable name] [expression value]
```

# mode

Set trigonometric mode for trigonometric functions.

Supported modes: deg for degrees, rad for radians and grad for gradians.

## Usage

```
mode deg
mode rad
mode grad
```

# sleep

Sleep for given seconds.

## Usage

```
sleep [seconds to sleep]
```

# stat

Display various statistical data of a given set of numbers

## Usage

```
stat [numbers]
```

# time

Print out the current time

## Usage

```
time
```

# unset

Remove a variable or an expression or clear all expressions and memory.

Operation depends on the number of arguments. If called with one argument, the
given variable or expression will be deleted. If called without arguments all 
variables and expressions will be deleted.

## Usage

```
unset
unset [variable name]
unset [expression name]
```
