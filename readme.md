## Photo & Image Process Tools
1. Automatic add mask to face(s) in photo(s)
2. Form window with addin-support feature

## Requirements

1. dotnet framework 4.0 ( WinForm & WPF )
2. System.Windows.Forms.Ribbon35 ( Fake Ribbon UI interface )
3. NGettext & My NGettext.Winform ( I18N )
4. Mono.Addin framework ( addin base library )
5. Cyotek.Windows.Forms.ImageBox ( Image Display )
6. Cyotek.Windows.Forms.ColorPicker ( ColorDialogEx )
6. GDIPlusX ( some GID+ effect not included in System.Drawing )
7. NewtonSoft JSON.Net ( addin / app config file load & save )
8. Accord ( Image Process base )
9. Accord Image ( Image Process Filters )
10. Accord Vision ( maybe )

## Dev Env.

1. VisualStudio 2015 Express for Desktop

## Tools/Demo Application
### AutoMask

Automatic add mask to face(s) in photo(s)

### DialogTest

ColorDialogEx & FontDialogEx usage demo app

### ColorMatrixTest

Save/Load/Test ColorMatrix effect for Grayscale addin

## Common Library

### NetCharm.Common

Common Library for custom ColorDialog, FontDialog, and others UI custom controls such as color slider, value slider...

#### ColorDialog / ColorDialogEx

using Cyotek.Windows.Forms.ColorPicker controls as color elements. 

( ColorDialogEx is a component call ColorDialog )

#### FontDialog / FontDialogEx

Because System.Drawing is supported TTF font only, so FontDialog using System.Media to display / pick font.
( FontDialogEx is a component call FontDialog )

### NetCharm.Image.AddinHost

Addin Host Support library

## PhotoTool

Main Form framework for load & display addins for special feature

## Addins

Now has some internal filters

### App

1. Image Editor
2. Batch Process

### Action

1. Pin object(picture/text...) to Image
2. Crop
3. Rotate
4. Resize
ToDo...

### Filter

1. Blur
2. Sharper
3. Grayscale
4. Invert
4. Hue Tint
5. HSL Filter
6. More ToDo...

### FormatIn

ToDo...

### FormatOut

ToDo...

