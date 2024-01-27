# Overworld

## Overview

An Overworld is the top level location in a game. A game can only contain a single Overworld. An Overworld can contain multiple Regions.

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

## Use

An Overworld can be simply instantiated with a name and description.

```csharp
var overworld = new Overworld("Name", "Description.");
```

Regions can be added to the Overworld with the **AddRegion** method.

```csharp
overworld.AddRegion(region);
```

Regions can be removed from an Overworld with the **RemoveRegion** method.

```csharp
overworld.RemoveRegion(region);
```

The Overworld can be traversed with the **Move** method.

```csharp
overworld.Move(region);
```

## OverworldMaker

The OverworldMaker simplifies the creation of the Overworld, when used in conjunction with RegionMakers.

```csharp
var overworldMaker = new OverworldMaker("Name", "Description.", regionMakers);
```

However, the main benefit of using an OverworldMaker is that it allows multiple instances of an Overworld to be created from a single definition of an Overworld.

```csharp
var overworld = overworldMaker.Make();;
```



