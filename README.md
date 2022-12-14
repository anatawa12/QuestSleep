# QuestSleep

An world to sleep with VRChat for Quest.

[World Link Here](https://vrchat.com/home/world/wrld_c30ef1bd-8376-44e0-9e59-e0dd04ccc6be)

To build QuestSleep, you have to download & import the following unitypackages:

- [QvPen](https://booth.pm/ja/items/1555789)


## QuestNightMode

The nightmode fot both Quest and PC.

`Assets/anatawa12/QuestNightMode` folder contains this gimmick.

Putting `Assets/anatawa12/QuestNightMode/ShadowPlane.prefab` in your world and activate the GameObject starts NightMode.

To control darkness, you may put `Assets/anatawa12/QuestNightMode/NightSliderCanvas.prefab` and Drag `ShadowPlane/ShadowPlane` to `Renderer Of Material` field of `Darkness Controller` script.

Because this is intended for VRChat Quest, there's no support for vanilla Unity.

## RealStarSkybox

A skybox based on real stars.

`Assets/anatawa12/RealStarSkybox` folder contains this gimmick.

To use this skybox, set `Assets/anatawa12/RealStarSkybox/cubemap.mat` as Skybox and 
put `Assets/anatawa12/RealStarSkybox/ShaderRotationSetter.cs` to some GameObject.

Both UdonSharp and vanilla Unity are supported.
If your project have UdonSharp, the script will be compiled as a `UdonSharpBehaviour` and if not, it will be compiled as a `MonoBehaviour`.

This skybox should show almost real skybox based on your local datetime and set latitude.
