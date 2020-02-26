SD_JOYSTICK DOCUMENTATION
=========================

SD_Joystick presents you (the C# script writer) with a simple static interface to create and use an on screen joystick setup.

In the script you wish to use SD_Joystick you must have "using SiliconDroid;" at the top of your file (with the existing using statements).

When you have "using SiliconDroid;" at the top of your file, you can browse all available static functions by typing "SD_Joystick.fn", then in visual studio you should be presented with all available functions "fnc_XXXXX".

Try out the two demo scenes and look at the demo scripts to see how easy it is to create and use controls using SD_Joystick.

In short: You create your controls once during Awake of one of your game scripts. You then poll the controls per frame during the Update method of one of your game scripts.

GRAPHICAL CUSTOMISATION
=======================

You can use any graphics you like for your SD_Joystick controls, simply add your graphics in a paint program using some paint program.

Then when you create your controls use the correct sprite index for the graphic you added to the atlas.

The sprite atlas ( sd_joystick_texture.png ) consists of 64 sprites ( 8 rows * 8 columns ).

Sprite 0 is the bottom left sprite, sprite 63 is the top right sprite.

You can colorize all of the controls by calling SD_Joystick.fnc_SetColor().

You can colorize individual controls by calling SD_Joystick.fnc_SetLastCreatedControlColor() after you create a control.

NOTE: SD_Joystick will only ever incur one draw call for your game regardless of how many controls you use, even if they are all different colors.

ANCHORS
=======

A note on ANCHOR types: Positions are relative to the anchor, for example:

A BOTTOM_LEFT anchor has positive X and Y positions going right and up in screen space.
A BOTTOM_RIGHT anchor has positive X and Y positions going left and up in screen space.
A TOP_LEFT anchor has positive X and Y positions going right and down in screen space.
A TOP_RIGHT anchor has positive X and Y positions going left and down in screen space.