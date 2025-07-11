# 🚀 Projectile Motion Simulation (C# WinForms)

This is a graphical Windows Forms application that simulates **projectile motion** under gravity on different planets. It provides real-time trajectory animation, physics calculations (velocity, acceleration, energy), and planet-specific gravity switching (Earth, Moon, Mars, etc.).

---

## 🖥️ Features

- 🌍 Change gravity for different planets (Earth, Moon, Mars, etc.)
- 🧮 Real-time simulation of:
  - Position (X, Y)
  - Velocity (Vx, Vy)
  - Acceleration (ax, ay)
  - Energies: Kinetic, Potential, Mechanical
- 💣 Sound effects for launch and impact
- 📊 Graphs for position, velocity, acceleration, and energy over time
- 📷 Planet-themed background images
- 🎨 Smooth projectile animation using `Graphics` and `Timers`
- 🧭 Multilingual interface: French and English support

---

## 📁 Project Structure

- `Form1.cs` – Main simulation logic and event handling
- `Resources` – Contains planet images and audio files
- `Details.cs`, `More_Details.cs` – Optional windows for extended information
- `Charts` – Built-in `System.Windows.Forms.DataVisualization.Charting` used to render motion graphs

---

## 🛠️ Requirements

- Visual Studio 2013+
- .NET Framework 4.7.2 or later
- Windows OS (due to WinForms)

---

## 🚀 How to Run

1. Clone all files or download the source.
2. Open the solution (PROJECTILE.cs) file in Visual Studio and run it.
3. Make sure `Boom.wav`, `touch-earth.wav`, and planet images are in the correct `Resources` folder.
4. Press **F5** or click **Start** to run the simulation.

---

## 🌌 Supported Planets & Gravities

| Planet    | Gravity (m/s²) |
|-----------|----------------|
| Earth     | 9.81           |
| Moon      | 1.62           |
| Mars      | 3.721          |
| Venus     | 8.87           |
| Jupiter   | 24.79          |
| Mercury   | 3.7            |
| Uranus    | 8.87           |
| Neptune   | 11.15          |
| Saturn    | 10.44          |

---

## 🧑‍💻 Author

**Racha Zayni**  
Second-year Electrical and Technology Engineering student  

---

## 📜 License

This project is provided for educational and demonstration purposes.  
For commercial or extended academic use, please contact the author.

