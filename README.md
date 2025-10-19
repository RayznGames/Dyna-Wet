# 🌧️ Dyna-Wet Preview  (Time and weather effects) 
**Dynamic Time, Weather & Wet Surface System for Unity URP**

![Unity](https://img.shields.io/badge/Engine-Unity_6000.2.6f2-blue.svg)
![URP](https://img.shields.io/badge/Render_Pipeline-Universal_Render_Pipeline-green.svg)
![License](https://img.shields.io/badge/License-MIT-lightgrey.svg)
![Status](https://img.shields.io/badge/Stage-Preview-orange.svg)

---
A Dynamic Realtime Weather system for Unity using the Universal Render Pipeline. with material surface related weather interactive effects. 

This system is a culmination of the motivation and passion in my research regarding performant reltime dynamic game effects, the goal was to create a complete Environment and Weather system with daynicght cycle 
from scratch using all the built-in components of the UnityEngine and 

## 📖 Overview

**Dyna-Wet Preview** is an open-source, experimental **real-time dynamic time and weather system** built for **Unity 6000.2.6f2 (URP)**.  
It simulates full **day–night cycles**, **rain**, **wetness buildup**, **puddle formation**, and **dynamic surface reactions** at the shader level — all updating in real-time.

This is a **preview version**, designed to demonstrate the core logic, shaders, and procedural systems behind a dynamic environment. It’s the original  **unoptimized** version of the video stable and functional, and meant for **learning and educational purposes**.

---

## ✨ Key Features

### 🌦️ Dynamic Weather System
- Real-time transitions between weather states (sunny, cloudy, rainy, etc.)
- Adjustable parameters for **rain intensity**, **wind strength**, and **sunlight power**
- Smooth, parameter-driven transitions (no hard switches)

### 🕓 Time of Day System
- Fully dynamic **24-hour cycle** with adjustable time speed
- Realistic sun and moon movement mapped to actual time progression
- Automatic lighting transitions for dawn, day, dusk, and night

### ☁️ Custom Skybox Shader
- Procedurally generated **clouds**, **sky gradients**, and **horizon color transitions**
- Real-time cloud movement and wind direction control
- Dynamic **sun/moon rendering** with lighting synchronization

### 💧 Wet Surface Shader FX
- Procedural **puddle generation** using noise functions  
- Dynamic **wetness control** that builds up and dries over time  
- **Rain streaks**, **ripples**, and **droplets** simulated in the shader  
- Surface reactions that **conform to the mesh shape** dynamically  
- Adjustable tiling, distortion, and reflectivity to stylize or simulate realism

---

## ⚙️ Technical Details

- **Engine:** Unity 6000.2.6f2  
- **Render Pipeline:** Universal Render Pipeline (URP)  
- **Core Components:**  
  - Custom Shader Graph materials for wet surfaces  
  - Visual Effect Graph for GPU-based rain particles  
  - Skybox Shader with procedural cloud generation  
  - Scripted time & weather manager with runtime parameter updates  

---

## 📂 Project Goals

This system was created as a **research and experimental project** to:
- Explore **realistic procedural wet effects** using Shader Graph  
- Develop a **unified weather controller** that ties together time, rain, and environment  
- Demonstrate how shader-driven visuals can create dynamic mood and atmosphere  

While this version is fully usable, the focus is on **education and transparency**, not performance or production use.

---

## 🚀 Getting Started

1. Clone or download this repository.
2. Open the project in **Unity 6000.2.6f2** with **URP** enabled.
3. Load the sample scene to preview the system in action.
4. Use the in-scene **Weather Controller** to switch between weather types and observe transitions.

---

## 🧠 Educational Notes

The Dyna-Wet system integrates:
- **Lerped environmental values** (rain → wetness → puddle buildup)  
- **Dynamic shader updates** through material property blocks  
- **Noise-based surface wetting**, allowing full procedural puddle control  
- **BFX Graph integration** for scalable GPU particle rain  

If you’re studying **shader logic**, **environmental systems**, or **procedural world effects**, this project is an excellent breakdown of how all those systems interact.

---

## 🪪 License

This project is released under the **MIT License**.  
You are free to learn, modify, and use the code in your own projects — attribution is appreciated but not required.

---

## 📺 Video Overview

🎥 *Watch the development breakdown and demonstration on YouTube:*  
**[Dyna-Wet: Fully Dynamic Weather & Wet Surface System](#)**  
(Replace `#` with your video link)

---

## 👤 Author

**Rayzn Games**  
Indie Developer • 3D Generalist • Technical Artist  

💬 *“I built Dyna-Wet to push the limits of shader-driven realism and explore dynamic weather systems from the ground up.”*

- [YouTube](#)  
- [Itch.io](#)  
- [Twitter / X](#)  
- [GitHub](https://github.com/yourusername)

---

> 🧩 *This preview version is the original unoptimized version— designed for experimentation, learning, and curiosity.*  
> Expect it to look good, run okay, and teach you *a lot*.
