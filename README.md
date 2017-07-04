# Mathlab - Creating Multilingual Support for Windows Applications

A Windows Application for enabling users to work in softwarwes having a foreign language interface

## Purpose 

### To provide a simple UI to read and translate text from any windows application
### To reduce the language dependency 
### To allow users from multiple lingual backgrounds to work on the application simultaneously

## Versions

### version 0.0.1: The initial csv file used has the Japanese text in the PG-1000 UI and its English equivalent.
### version 0.0.2: This can read data from csv,accessdb, xml format files and add to accessdb and xml format files. The primary window has only one textbox for displaying text under the mouse after conversion(if one is available). The subsidiary window allows to add key value pairs to the xml and accessdb format files. Supported languages are Japanese and English.
### version 1.0.0: 

##Features

### Multiple privileges to ensure data protection

This has multiple user authentications, namely admin, editor and user. User can only use the application, Editor can edit the database and Admin can not just edit the database but add users and modify the privileges. 

### Support for 9 different languages

This has support for 9 languages, namely Arabic, English, German, Italian, Japanese, Korean, Norwegian, Spanish and Swedish

### Easy editing options

The Editors and Admin can copy, paste and delete rows directly from/to the application to/from any Excel file, thereby allowing hassle free manipulation

### Simple UI with features like high

## How it works

### User Login & Signup 



### Choose Language



### Main Form

1. It parses the xml file, curently from the database folder.
2. It stores the values in the form of a list with key value pairs.
3. It shows a simple textbox.
4. After the start button is clicked, it starts reading the text under the mouse.
5. The text is then compared against the key in the dictionary.
5. If there is a match, it replaces it with its value in the dictionary.
6. It then displays the text in the textbox.
7. The program can be started or stopped at any point of time using the corresponding start and stop buttons.
8. To exit the program, one can use the close button or directly close the exe file.

### Conversion Data table



### Add Conversion Data table



### User Privilege Table



### Add User Table






## Motivation

MNCs have various centres of development and there may arise a scenario wherin an application might not have been developed to be used overseas. Hencce, without having an understanding of the language, the software may be rndered useless.

## Installation

The application has no prerequisites. One however needs to check that the relative path of the XML files are not disturbed.

## API Reference

https://msdn.microsoft.com/en-us/library/ms747327(v=vs.110).aspx
https://code.msdn.microsoft.com/windowsapps/c-getting-the-windows-da1bd524


## Contributors

Mayank Goel, Intern, ABB Private Ltd.

## License

Copyright with ABB Private Ltd.