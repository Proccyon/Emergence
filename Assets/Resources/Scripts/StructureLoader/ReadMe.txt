
//summary
This folder contains most of what is needed to read scenes and turn them into structure instances.
All instances of things like items, actors and blocks are stored in a room instances.
A structure is basically a readonly room. Rooms can be initialized given a structure.
These scripts allow you to place gameobjects in a scene and automatically create a structure instance.
This instance is stored as a static variable in the StructureLoader prefab.


//Creating a new StructureScene
1)Create a new Scene by going to file -> new scene. Give it a name like HouseScene or OverworldScene.
2)Place the StructureControl prefab in the new scene.
3)Find StructureControl in the Hierarchy and change StructureHeight and StructureWidth
to the preffered value. You can change this at any time.
4)Go to /GameStructure/Classes/Actor/ExtendedActors/Chicken for example. Drag the chicken prefab to a spot within
the structure. This way you can add actors, tiles or blocks to the structure.
5)When done with (4) go to the StructureLoader script. Create a new structure variable
with 'public static Structure StructureVariable = new Structure();' 
where OverworldStructure is the name of the variable.
in Awake() place 'LoadStructure("SceneName",StructureVariable);'
where SceneName is the name of the scene you chose in (1).
6) You can now acces the Structure from anywhere by typing StructureLoader.StructureVariable.
Use the structure to initialize a new room or add the structure to an existing room(for example create a new tree with the world).

//In depth
Each scene that is designed to be structure contains a StructureControl prefab (with script StructureProperties).
This does exactly what you imagine it does, it stores the properties of the structure.
StructureHeight and StructureWidth are properties set in the editor. 
At awake the RoomControl creates a new structure instance (run order is changed in project settings to run first).
After that block, actor and tile prefabs will add an instance of a block, actor or tile to the structure instance.
This is everything that Structure control does (It also adds the black borders in editor, those dont do anything).

The game should always be run from the DrawingScene(not from the sctucture scenes!).
The StructureLoader prefab(with script StructureLoader) should be placed in the DrawingScene.
This prefab loads scenes(additively) with a given name that have to contain a StructureControl prefab.
Loading the scene returns a delegate called "completed" that runs as soon as the scene is actually loaded.
This usefull because scenes don't load immediately. 
if you load the scene in awake() it will be loaded in start(),not earlier.
We add a method to the delegate 'completed' that copies the structure in StructureControl
and reloades the DrawingScene. There is a method in StructureLoader which does all of the above, 'LoadStructure'.
Give it a scene name and a structure instance and it will change this instance to be identical to the one in RoomControl.
That is all, the scene can now be used to initialize a room.




