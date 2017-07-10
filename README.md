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

## Features

### Multiple privileges to ensure data protection

This has multiple user authentications, namely admin, editor and user. User can only use the application, Editor can edit the database and Admin can not just edit the database but add users and modify the privileges. 

### Support for 9 different languages

This has support for 9 languages, namely Arabic, English, German, Italian, Japanese, Korean, Norwegian, Spanish and Swedish

### Easy editing options

The Editors and Admin can copy, paste and delete rows directly from/to the application to/from any Excel file, thereby allowing hassle free manipulation

### Simple UI with features like high

## How it works

### User Login & Signup 

1. The user logins in using his/her registered email id and password.
2. The user can also signup with a new email id.
3. After the user clicks on submit, he/she is redirected to the choose language window or the conversion data table window based on his/her user privileges.

#### The default privilege of newly registered users is "user"
#### Users with "Admin" and "Editor" privileges are redirected to the conversion data table window while those with "User" privilege are directed to the choose language window

### Choose Language

1. The user has to chose one of the 9 languages as the source language of the software. 
#### The source language of the software refers to the language of the software's user interface

2. The user can chose one or more of the 9 languages as the target language.  
#### The target language refers to the language in which the user is comfortable in or wants for the software's user interface

### Main Form

1. In the background, one or more language dictionary(s) has been created with a list of source language (key) and target language (value) pairs.
2. It displays one or more text boxes with corresponding label(s) of the language(s) that has(ve) been selected as target language by the user.
3. As the user starts using the source software, Matlab application reads the text below the mouse.
4. In the background, the application compares the text it has thus read with the source language (key) values in the respective language dictionary.
5. If there is a match, it replaces it with its target language pair value from the corresponding dictionary.
6. It then displays the text in the textbox for the corresponding language.
7. To change language prefernces, user needs to close the window and login again.

#### The number of dictionaries that are formed is dependent on  the number of target languages chosen by the user.
#### The source launguage values in each dictionary is from the words or phrases that are pre-defined in the database for the language that has been selected as the source language.
#### The target launguage values in each dictionary is from the words or phrases that are pre-defined in the database for the language that has been selected as the target language.
#### Each language dictionary is named after the the target language since there is one common source language. 

### Conversion Data table

1. The language 


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