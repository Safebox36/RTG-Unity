## Introduction
Realistic Terrain Generation is a Minecraft mod that was created by Team RTG in response to the discontinuation of the Realistic World Gen mod created by ted80. Among the wide range of terrain generation tools both for Minecraft and game design in general, this one stands out as one of the best for it’s value worth. Having it exist in Unity would benefit the indie community greatly as it would not only allow a free alternative to some of the AAA terrain generation tools but also allow a greater quality of procedural generation in indie games as a whole. My hope from this project is that developers will work on both the Unity version and the original Minecraft mod collaboratively to provide an incredible procedural terrain generation system for years to come.

I would like to personally thank the developers of the RTG mod for doing an amazing job and continuing to support the mod as the Minecraft modding community continues to grow.
## Aims
My aim for this project is to convert the Realistic Terrain Generation mod from Java to C# for use in the Unity3D game engine, ambiguous of terrain tools or assets used. As such, the project will only give raw numbers with which an adapter will need to be created to handle said numbers for use with the terrain tool used. All code will be converted as faithfully as possible, even if redundant, to allow for conversion into other game engines that may not have the same functions.
## Standardisations
In order to convert the code as close to the original as possible, some standardisations have to be made. They are as follows:
- All **math functions** will use **System** instead of **UnityEngine** to allow for a greater level of functionality
- **Static code blocks** in Java will be named **static void temp()** in Unity ~~until such a time when it can be implemented properly~~ then called in the class constructor. **As of Progress Update 6!**
- If **interface** is not used then it will be **implemented regardless**
- ~~**Heightmap data** will be saved in a **bitmap file** until such a time when a custom file format can be created. **R** will represent **height**, **G** will represent **block** and **B** will represent **block variations** (such as biome or state).~~ **Stored in a 2D array variable as of Progress Update 6!**
- ~~Due to difficulty converting **OpenSimplexNoise** to C#, all relevant scripts will be substituted with **digitalshadow's C# port** with the values of the original Java scripts.~~ **Fixed as of Progress Update 5!**
## Plan
### Stage 1 - Heightmap
- ~~Jave to C# code conversion~~ **Mostly!**
- Substitute Minecraft code for generic alternative (using Forge as a baseline)
### Stage 2 - Terrain
- Output to **height map**
- Output to **terrain map**
- Output to **geography map**
- Output to **details map**
### Stage 3 - Adapter
- Create adapter for **Unity Terrain**
- Create adapter for **UniBlocks** asset (as test case)
## Progress
### 0 – Unity Setup	(7/5/17)
Unity 5.6.0f3 will be used for this project with a 3D setup and a single scene containing a terrain object to test the program on. All scripts will be run at runtime to prevent lag during scene editing.
### 1 – Starting Point	(7/5/17)
Because of how complex the RTG system is, I’ve decided to start from the most isolated part of the code I could find; the api/config. More specifically, the property folder within api/config. After I’ve converted the scripts in this folder I’ll move onto the rest of the api/config folder then api/util & api/world.
### 2 - Identifying Key Code (19/6/17)
Having attempted to identify which code is necessary to complete Stage 1, I believe I now have enough to work backwards from the output code necessary to generate the noise and subsequent terrain.

Terrain code has been created and completed (with the exception of a few); block, world and chunk interfaces need to be created before any further conversion can be carried out.
### 3 - Block to IBlockState (22/6/17)
Prior to today, I renamed every IBlockState to Block but have since implemented the use of IBlockState and will need to go back and undo all previous changes so as to keep consistency with the original code.
### 4 - Terrain and Surface Separation (4/7/17)
I have fully converted the world/gen/terrain scripts and am now moving on to the world/gen/surface scripts. I spoke to WhichOnesPink on the RTG Discord group and he's explained that world/gen/terrain is responsible for the height values while world/gen/surface is responsible for the blocks that will appear at those height values. With that in mind, I've decided to deviate quite dramatically from the original source code for converting the surface scripts to C# to take into account the raw numbers rather than the block values themselves. Having said that, I will use the existing block values as a guideline before adapting it further.
### 5 - Block to Pixel (14/7/17)
I spent the majority of today trying to separate any ties to Minecraft by changing all references of Block to Pixel. The code works as well as it previously did but I also attempted to add the Biome scripts again with a little more success. They're not fully implemented and one part of the code relies on Minecraft's generation scripts to carry out the event, meaning I either need to find a workaround or neglect these scripts altogether.

I'm hoping to find a solution to the problem but thus far I've been unsuccessful. Additionally, I've managed to convert the Java version of OpenSimplexNoise from the original project and replaced the existing implementation of digitalshadow's C# version. There were a few minor differences in how the code was handled but it shouldn't have been too noticeable either way. I've also attempted to convert several Biome scripts but have had to delete them due to heavy dependency on Minecraft code.
### 6 - Biome Scripts Complete (30/7/17)
I spent the past week adapting all 60 Biome scripts (and the two base scripts) to C# and as of today have no errors. I had to remove any code referring to Biome Decoration as I'm not ready to implement that feature yet, meaning that as of now all base code is finished and should be fully funtional.

The next step is to test it, and as I've only tested the Terrain scripts on a 2D plane material, I will most likely continue to test the data using this method until I create a Unity Terrain adapter or third-party asset adapter.