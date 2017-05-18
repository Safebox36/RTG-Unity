## Introduction
Realistic Terrain Generation is a Minecraft mod that was created by Team RTG in response to the discontinuation of the Realistic World Gen mod created by ted80. Among the wide range of terrain generation tools both for Minecraft and game design in general, this one stands out as one of the best for it’s value worth. Having it exist in Unity would benefit the indie community greatly as it would not only allow a free alternative to some of the AAA terrain generation tools but also allow a greater quality of procedural generation in indie games as a whole. My hope from this project is that developers will work on both the Unity version and the original Minecraft mod collaboratively to provide an incredible procedural terrain generation system for years to come.

I would like to personally thank the developers of the RTG mod for doing an amazing job and continuing to support the mod as the Minecraft modding community continues to grow.
## Aims
My aim for this project is to convert the Realistic Terrain Generation mod from Java to C# for use in the Unity3D game engine, ambiguous of terrain tools or assets used. As such, the project will only give raw numbers with which an adapter will need to be created to handle said numbers for use with the terrain tool used. All code will be converted as faithfully as possible, even if redundant, to allow for conversion into other game engines that may not have the same functions.
## Plan
### Stage 1 - Heightmap
- Jave to C# code conversion
### Stage 2 - Terrain
*to do*
### Stage 3 - Adapter
*to do*
## Progress
### 0 – Unity Setup	(7/5/17)
Unity 5.6.0f3 will be used for this project with a 3D setup and a single scene containing a terrain object to test the program on. All scripts will be run at runtime to prevent lag during scene editing.
### 1 – Starting Point	(7/5/17)
Because of how complex the RTG system is, I’ve decided to start from the most isolated part of the code I could find; the api/config. More specifically, the property folder within api/config. After I’ve converted the scripts in this folder I’ll move onto the rest of the api/config folder then api/util & api/world.
