# Resistor Image Generator
C# project to generate colour coded resistor images for given values

![e.g. 27k](https://github.com/washcroft/ResistorImageGenerator/blob/master/example/27000.png "e.g. 27k")

# Usage

```
ResistorImageGenerator.exe Value Tolerance OutputPath
```

e.g. 27k ohm 5% tolerance

```
ResistorImageGenerator.exe 27000 5 C:\27000.png
```

Running the program without parameters will generate images for the entire E3 - E96 series' in the current directory.

# Pre-Generated Series

* [E3.zip](https://github.com/washcroft/ResistorImageGenerator/blob/master/example/E3.zip)
* [E6.zip](https://github.com/washcroft/ResistorImageGenerator/blob/master/example/E6.zip)
* [E12.zip](https://github.com/washcroft/ResistorImageGenerator/blob/master/example/E12.zip)
* [E24.zip](https://github.com/washcroft/ResistorImageGenerator/blob/master/example/E24.zip)
* [E48.zip](https://github.com/washcroft/ResistorImageGenerator/blob/master/example/E48.zip)
* [E96.zip](https://github.com/washcroft/ResistorImageGenerator/blob/master/example/E96.zip)

# License
```
MIT License

Copyright (c) 2018 Warren Ashcroft

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```