# 🌧️ Dyna-Wet Preview  (Time and weather effects) 
**Dynamic Time, Weather & Wet Surface System for Unity URP**

![Unity](https://img.shields.io/badge/Engine-Unity_6000.2.6f2-blue.svg)
![URP](https://img.shields.io/badge/Render_Pipeline-Universal_Render_Pipeline-Green.svg)
![License](https://img.shields.io/badge/License-MIT-lightgrey.svg)
![Status](https://img.shields.io/badge/Stage-Preview-orange.svg)

---
A Dynamic Realtime Weather system for Unity using the (URP) Universal Render Pipeline. with material surface related weather effects. 

This system is a culmination of the motivation and passion in my research regarding performant reltime dynamic weather and environment game effects, the goal was to create a complete Environment and Material Weather system with Day-Night cycle 
from scratch using all the built-in components of the UnityEngine. The result is Dyna-Wet.

## Overview

**Dyna-Wet Preview** is an, experimental **real-time dynamic time and weather system** built with **Unity 6000.2.6f2 (URP)**.  
It simulates full **day–night cycles**, **rain**, **puddle formation**, and **dynamic surface reactions** at the shader level — all updating in real-time.

This is a **preview version**, designed to demonstrate the core logic, shaders, and procedural systems behind a Fully Dynamic environment system. It’s the original  **unoptimized** version, stable and functional, and meant for **learning and educational purposes**.

---

## Key Features

### 🌦️ Dynamic Weather System
- Real-time transitions between weather states (sunny, cloudy, rainy, etc.)
- Adjustable parameters for **rain intensity**, **wind strength**, and **sunlight power**
- Smooth, parameter-driven transitions with duration controls
- One click bake weather Data, for the start of the level.

### 🕓 Time of Day System
- Fully dynamic **24-hour cycle** with adjustable time speed
- Realistic sun and moon movement mapped to actual time progression
- Automatic lighting transitions for dawn, day, dusk, and night

### ☁️ Custom Skybox Shader
- Procedurally generated **clouds**, **sky gradients**, and **horizon color transitions**
- Real-time cloud movement and wind direction control
- Dynamic **sun/moon rendering** with lighting synchronization

### 💧 Wet Surface Shader FX
- Procedural growing **puddles** generated using Unity's Built-In shader noise functions.
- Dynamic and auntomatic **world wetness** that builds up and dries over time.
- **Rain streaks**,and procedural **ripples**, and **droplets** simulated in materials in the pixel shader  
- Effects that **conform to the mesh shape** dynamically  
- Adjustable parameters to stylize or achieve higher realism.

---

## Technical Details

- **Engine:** Unity 6000.2.6f2  
- **Render Pipeline:** Universal Render Pipeline (URP)  
- **Core Components:**  
  - Custom Shader Graph materials for wet surfaces  
  - Visual Effect Graph for GPU-based rain particles  **Dependency (VFX graph)**
  - Skybox Shader with procedural cloud generation  
  - Scripted time & weather manager with runtime parameter updates  

---

## Project Goals

This system was created as a **research and experimental project** to:
- Explore **realistic procedural wet effects** using Shader Graph  
- Develop a **unified weather controller** that controls time, rain, and environment  
- Demonstrate how shader-driven visuals can create dynamic mood and atmosphere  

While this version is fully usable, the focus is on **education and transparency**, not performance or production use.

---

## Getting Started

1. Clone or download this repository.
2. Create a new project in **Unity 6000.2.6f2** with **URP** render pipeline.
3. Drop the folder "Dyna-Wet_Previev" into the project
4. Go to package manager and Install the **Visual Effect Graph** package.
5. Load the sample scene, and hit Play to view the system in action.
6. Use the in-scene **Weather Controller** (TopRight) to switch between weather types and observe transitions.

---

## Educational Notes

The Dyna-Wet system integrates:
- **Lerped values** for - rain, wetness, clouds, sun, and puddles buildup 
- **Dynamic shader updates** through material properties exposed in the shader's.
- **Environmental and Time Controllers** controlling the entire system's behavior trough scripting.   
- **VFX Graph** for scalable GPU particles used for rain  

If you’re studying  **environmental systems**, or **procedural world effects**, this project is an excellent breakdown of how all those systems interact and interlock between each other.

---

## License

You are free to learn, modify, and use the code in your own projects — if thats the case attribution is appreciated.

---

## Video Overview

🎥 *Watch the development breakdown and demonstration on YouTube:*  
**[Dyna-Wet Video: making Fully Dynamic Weather & Wet Surface System](https://youtu.be/SYKG3OQRJig?si=PHldFXyvFSClAq7C)**  

---

## 👤 Author

**RayznGames**  
Indie Developer • 3D Generalist • Technical Artist  

- [YouTube](www.youtube.com/@RayznGames)  
- [Itch.io](https://rayzngames.itch.io/)  
- [GitHub](https://github.com/RayznGames)

---

> 🧩 *This preview version is the original unoptimized version— designed for experimentation, learning, and curiosity.*  
> Expect it to look good, run okay, and teach you *a lot*.
