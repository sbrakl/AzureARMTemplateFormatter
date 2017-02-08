# AzureARMTemplateFormatter
Formats Azure ARM template to view great in Notepad++

Notepad++ is my favorite text editor. I love editing ARM template with Notepad++.
But, I had the problem with the json collapsing. 

In the long Azure ARM Template, when I collapse the resource section, I had no clue, what's resource it holds in collapse section till I expanded again.

The problem, which I demonstrated below in animated gif. (Have patience while it loads!)

![Notepad++ collapse without formating](https://github.com/sbrakl/AzureARMTemplateFormatter/raw/master/images/PreFormatTemplate.gif) 

If you problem viewing animated gif, you can view at [here](http://recordit.co/ut7c1O8VnN)

This application would format the ARM template by re-arranging the type part such that, when you collapse the resource section, type is visible

![Notepad++ collapse post formating](![Notepad++ collapse without formating](https://github.com/sbrakl/AzureARMTemplateFormatter/raw/master/images/PreFormatTemplate.gif) 

If you problem viewing animated gif, you can view at [here](http://recordit.co/livBqzbCmw)

This is the screenshot of the windows application

![ARM Template Formatter Screen shoot](https://github.com/sbrakl/AzureARMTemplateFormatter/raw/master/images/Screenshoot1.png) 

It been developed in .NET 4.5 as Windows Application using Visual Studio 2015

If you check "Replace Inplace edit" option, it would overwrite existing ARM template with formatted one, else will create new copy with "_formatted" prefix