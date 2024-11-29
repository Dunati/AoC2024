# AdventOfCode
Advent of Code runner
https://adventofcode.com/

This is the Advent of Code framework that I have been using for a few years to minimize the amount of boilerplate I have to deal with when working on the puzzles.

What this runner does:
The runner will download inputs (if not already chached locally and if the puzzle has been released), run tests (if you have them) and output the results (via Trace) if all tests pass. The boilerplate Day class contains a single function which takes a part number (1 or 2) and the input as a string. It returns a string which is compared the the expected results for tests and is printed to the debug output for solutions. For each part, this function is called for each defined test, and if all tests pass then it will be called a final time with the cached input.

Tests:
There are two kinds of tests which can be run. 
First, you can declare a public void method with the TestAttribute in the Day class you are running and that code will be run. Fail an assert to get messages printed to the output or throw an exception to abort the run.
Second, you can create a tests.yaml file which contains an array of test cases which should look something like this for the examples for https://adventofcode.com/2023/day/1 :
```
- expected: 142 # Expected value
  part: 1 # Omit or set to 0 to run on all parts
  ignore: false # If set to true the test will not be run
  input_text: | # Inline text to use as input for the test
    1abc2
    pqr3stu8vwx
    a1b2c3d4e5f
    treb7uchet
- expected: 281
  part: 2
  input_file: part2.txt # If the inline text is a problem due to size, formatting or escaping, you can set a relative path to a file here
```

Setup:
The only setup which should be required is to save your AoC session token so that puzzle inputs can be dowloaded:

1. Log in to Advent of Code 
2. Right click the page and click "inspect"
3. Navigate to the "Network" tab
4. Click repoad
5. Click on any request, and go to the "Headers" tab
6. Search through the "Request Headers" for a header named cookie.
7. You should find one value named Cookie that starts with "session=", followed by a long string of hexadecimal characters. Copy the hex part of the value (without "session=") and save it to Cookie.txt

Command line options:
NewDay			This will copy the template Day from Source/Day00 to the next available numbered day (Source/Day01, Source/Day02, etc.). The new class will have a namespace which matches the directory name to prevent clashes.
all				This will run all Day solutions defined in the project
<day> [part]	You can also explicity specify a day number to run, along with an optional part (1 or 2) to run something other than the latest

Configuration:
App.config    contains application configuration.



