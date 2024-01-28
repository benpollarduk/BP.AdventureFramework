# Region

## Overview

A Region is the intermediate level location in a game. An Overworld can contain multiple Regions. A Region can contain multiple Rooms.

```
Overworld
├── Region
│   ├── Room
│   ├── Room
│   ├── Room
├── Region
│   ├── Room
│   ├── Room
```

A Region represents a 3D space. 
* The **x** location always refers to the horizontal axis, with lower values being west and higher values being east.
* The **y** location always refers to the vertical axis, with lower values being south and higher values being north.
* The **z** location always refers to the depth axis, with lower values being down and higher values being up.

## Use

A Region can be simply instantiated with a name and description.

```csharp
var region = new Region("Name", "Description.");
```

Rooms can be added to the Region with the **AddRoom** method. The x, y and z location within the Region must be specified.

```csharp
region.AddRoom(room, 0, 0, 0);
```

Rooms can be removed from a Region with the **RemoveRoom** method.

```csharp
region.RemoveRoom(room);
```

The Region can be traversed with the **Move** method.

```csharp
region.Move(Direction.North);
```

The Region can be traversed with the **Move** method.

```csharp
region.Move(Direction.North);
```

The start position, that is the position that the Player will start in when entering a Region, can be specified with **SetStartPosition**.

```csharp
region.SetStartPosition(0, 0, 0);
```

The **UnlockDoorPair** method can be used to unlock an **Exit** in the current Room, which will also unlock the corresponding Exit in the adjoining **Room**.
```csharp
region.UnlockDoorPair(Direction.East);
```

Like all Examinable objects, Regions can be assigned custom commands.

```csharp
region.Commands =
[
    new CustomCommand(new CommandHelp("Warp", "Warp to the start."), true, (game, args) =>
    {
        region.JumpToRoom(0, 0, 0);
        return new Reaction(ReactionResult.OK, "You warped to the start.");
    })
];
```

## RegionMaker

The RegionMaker simplifies the creation of a Region. Rooms are added to the Region with a specified **x**, **y** and **z** position within the Region.

```csharp
var regionMaker = new RegionMaker("Region", "Description.")
{
    [0, 0, 0] = new Room("Room 1", "Description of room 1."),
    [1, 0, 0] = new Room("Room 2", "Description of room 2."),
};
```

The main benefit of using a RegionMaker is that it allows multiple instances of a Region to be created from a single definition of a Region.

```csharp
var region = regionMaker.Make();
```





