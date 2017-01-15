# Magic Word Counter (Code-Behind Style)

## What the program does
It counts words, with custom options:

- Exclude articles
- Count quotations as only 1 word 

## Code-Behind Style
### What is it?
The code-behind is exactly what it says on the tin: the code is behind the form (the thing the user sees).  Whenever an event is raised by the form, the code-behind can do things in response to it, including changing how the form works.  This simplicity makes it attractive to beginners because there's not much it can't do.  There are some problems with this approach, however.
### Why it is not ideal

- **It encourages closely coupled code.**  When things are closely-coupled, they rely heavily on each other.  In this program, the logic that does the word counting depends directly on the form, which in turn depends directly on the word counting code.  Changing one will likely mean requiring the other be changed too.  In team projects this can be problematic because there are often different jobs to be done: writing the core logic and designing how the form looks.
- **It is not easily portable.**  If one were to adapt this project for use in other environemnts, like a Windows 10 store app or on the web, one would have to rewrite the code because WPF is a different environment.
- **It is not easily testable.**  There are two common ways to test code.  The obvious and sometimes easiest way is to just run the program and try it out.  A popular alternative is the use of unit tests, which are pieces of code that ensure other pieces of code work properly.  Ideally, unit tests only test one unit of code.  The tests in this project reference the form as a whole because it was not written in an easily testable way.

Using the code-behind does directly cause these problems and it is possible to avoid them.  However, there exists a better way that reduces the amount of code that must be written: [MVVM](https://github.com/ecenterhub/MagicWordCounter-MVVM-Style).