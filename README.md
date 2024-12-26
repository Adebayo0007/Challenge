# FileNameParser

A lightweight package for dynamically resolving date-related keywords in file names. It expands keywords such as `NOW`, `YESTERDAY`, `TODAY`, and supports relative offsets (e.g., `NOW-1d`, `NOW+2h`) as well as custom date formats and so on when it is passed.

## Table of Contents

- [About](#about)
- [Features](#features)
- [clone](#clone)

## About

`FileNameParser` is a utility designed to simplify dynamic file name generation gotten from FTP downloader by interpreting date-related keywords and relative time offsets. 
This package is especially useful in scenarios where filenames contain dates (such as logs or reports) and need to be processed based on specific days or times.

With support for localization and flexible date formatting, it provides an efficient way to automate file processing tasks that rely on precise date-based naming conventions.

## Features

- **Keyword Parsing**: Expands date-related keywords like `NOW`, `YESTERDAY`, and `TODAY` into actual date/time values.
- **Relative Time Offsets**: Supports relative offsets like `NOW-1d` (yesterday), `NOW+2h` (two hours from now), etc.
- **Custom Date Formatting**: Supports `Format(NOW, "yyyy-MM-dd")` to output dates in any desired format.
- **Localization**: Supports date formatting based on different locales (e.g., `en-US`, `en-NG` for Nigeria, etc.).
- **Lightweight**: Minimal package size, easy to integrate into existing projects.

## Clone
git clone https://github.com/adebayo0007/Challenge.git
cd FileNameParser
dotnet build



