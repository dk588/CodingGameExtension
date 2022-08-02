# Visual Studio Extension for Coding Game

This extension allow to use Visual Studio to push your code on [CodinGame.com](https://codingame.com).  
It uses [Selenium](https://www.selenium.dev/) to control the website directly from VS.  
You can use multiple files/class in your project.

Get the extension on the MarketStore : [CodinGameExtension](https://marketplace.visualstudio.com/items?itemName=RenaudR.CodinGameExtension) or in VS go to Extensions > Manage Extensions > CodinGameExtension


After the extension is installed, you need to add the Coding Extension Toolbar to your main Toolbar:
![Toolbar](Doc/Screenshot/Toolbar.png)
* First icon allow to launch the browser with CodinGame. The first time, you need to login then you can navigate to the exercice you want.
* The second button push the current project into your exercice.
* The third button do the same thing and launch the test.


## Limitations
It only works for C# and Python project but can easily be improved for others languages.  
In case you close the firefox window, you need to restart Visual Studio.  
It's not a Visual Studio Code extension.
Session cookies management is hazardous

----------
Please report bug or improvement request in [Github Issues](https://github.com/dk588/CodingGameExtension/issues)
