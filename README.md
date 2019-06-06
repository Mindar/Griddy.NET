# Griddy
Griddy is a small C#.NET library to simplify working with 2d grids. It uses locally-dense, sparse grids. Or to put it simply, it uses a sparse grid of chunks and each chunk is a dense grid.

![Nuget](https://img.shields.io/nuget/dt/Griddy.NET.svg)
![License](https://img.shields.io/badge/license-MIT-blue.svg)

## Usage
Griddy's API is really simple to use and understand:

```c#
Grid<bool> myGrid = new Grid<bool>();

// Use .SetCell(row, column, value) and .GetCell(row, column) to access grid cells
myGrid.SetCell(1337, -42, true);
var result = myGrid.GetCell(1337, -42);

if(result) {
	Console.WriteLine("Griddy rocks!");
}

// Output: "Griddy rocks!"
```

Alternatively you can use the indexing operator directly.
```c#
myGrid[1337, -42] = true;
var result = myGrid[1337, -42];
```

Griddy also works with classes as generic parameter.

```c#
Grid<Tree> plantedTrees = new Grid<Tree>();

myGrid[0, 0] = new Tree("Oak");
myGrid[0, 1] = new Tree("Cedar");
myGrid[0, 2] = new Tree("Binary");

var emptyResult = plantedTrees[999, 999];
// emptyResult == default(Tree) == null;

var goodResult = plantedTrees[0, 1];
// goodResult.ToString() == "Cedar";
```

You can use Griddy to efficiently work with really big grids.


```c#
Grid<int> plantedTrees = new Grid<int>();

myGrid[0, 0] = 1337;
myGrid[9999999, 9999999] = 0xDEADBEEF;

Console.WriteLine(myGrid.ChunkCount.ToString());
// Output: 2

Rectangle bounds = myGrid.ComputeApproxBounds();
// bounds.Width == 9999999
// bounds.Height == 9999999
```