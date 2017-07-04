## Synopsis

A WInform Application that displays the text under the mouse.

## Purpose 

A simple UI for enabling users to work in softwarwes having a foreign language interface.

## Versions

Version1: The initial csv file used has the Japanese text in the PG-1000 UI and its English equivalent.

## How it works

1. It reads the csv file, curently from the location C:\temp\file.csv
2. It stores the content as a dictionary with key value pairs.
3. It shows a simple textbox and a start and a stop button.
4. After the start button is clicked, it starts reading the text under the mouse.
5. The text is then compared against the key in the dictionary.
5. If there is a match, it replaces it with its value in the dictionary.
6. It then displays the text in the textbox.
7. The program can be started or stopped at any point of time using the corresponding start and stop buttons.
8. To exit the program, one can use the close button or directly close the exe file.

## Motivation

MNCs have various centres of development and there may arise a scenario wherin an application might not have been developed to be used overseas. Hencce, without having an understanding of the language, the software may be rndered useless.

## Installation

No installation required

## API Reference

https://msdn.microsoft.com/en-us/library/ms747327(v=vs.110).aspx
https://code.msdn.microsoft.com/windowsapps/c-getting-the-windows-da1bd524

## Tests

Describe and show how to run the tests with code examples.

## Contributors

Mayank Goel

## License

Copyright with ABB Private Ltd.