# DungeonLib
## Overview
DungeonLib aims to help developers create randomly-generated levels for games.
As these levels, or dungeons, are not bound to any form of display method, they can be used for ASCII-style graphics, up to 3D games.

## How to use
Create a dungeon by calling `Dungeon.Create(int width, int height)`.
A two-dimensional array called `Tiles` will represent the "map" of the dungeon

## FAQ
 - Why C#?  
   C# is the language I feel most comfortable writing with. I feel it's a beautiful language. If you wish to see DungeonLib being ported to Java, C++, Cobol or x86 asm, feel free to do so!

 - Why use `Dungeon.Create()` and not `new Dungeon()`?  
   Some developers, especially inexperienced one's, tend to forget that `new` could mean that there is a lot of work to do, simply because so many classes only assign some values and references. A `Dungeon`, however, takes quite a while to generate, CPU-cycle-wise. For this reason, I wanted to make it clear to anyone using this library that creating a new dungeon is not a task done in a loop or similar.

 - Why recursive and not iterative generation?  
   Because it was a lot easier to do. If you have the time, write an iterative version and make a pull request.

 - How do the X and Y coordinates work?  
   [0,0] is the upper, left corner. The first coordinate is X and determines how far to the right a tile is. The second coordinate is Y and determines how far down a coordinate is. An example:
   <table>
    <thead>
      <tr>
       <th>Coordinates</th>
       <th>Map</th>
      </tr>
    </thead>
    <tbody>
      <tr>
       <td><pre>[0,0]</pre></th>
       <td><pre>X###<br/>####<br/>####</pre></th>
      </tr>
      <tr>
       <td><pre>[1,0]</pre></th>
       <td><pre>#X##<br/>####<br/>####</pre></th>
      </tr>
      <tr>
       <td><pre>[0,1]</pre></th>
       <td><pre>####<br/>X###<br/>####</pre></th>
      </tr>
      <tr>
       <td><pre>[2,1]</pre></th>
       <td><pre>####<br/>##X#<br/>####</pre></th>
      </tr>
    </tbody>
   </table>
