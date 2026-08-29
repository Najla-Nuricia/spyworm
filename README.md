markdown_content = """# 🐛 BLOOPER (Bottle + Looper)

*The Ultimate Tactical Stealth Physics Puzzle* 

[![Platform](https://img.shields.io/badge/Platform-PC%20%7C%20Android-blue.svg)]()
[![Engine](https://img.shields.io/badge/Engine-Unity%202022+-black.svg)]()
[![Event](https://img.shields.io/badge/Event-GAMESEED%202026-brightgreen.svg)]()

## 📖 Game Overview

**BLOOPER** is a hilarious yet tense tactical stealth game where you control an elastic inchworm on a high-stakes mission to retrieve your mother's sacred lost bottle while avoiding deadly security lasers. 

It starts with a silly mistake—losing your mom's favorite water bottle. To avoid the ultimate household catastrophe, you must sneak through dangerous household environments disguised as an inchworm to bring it back safely. It translates a highly stressful, hyper-local household drama into a serious espionage stealth setting!

## ✨ Key Features

*   **Unique Inchworm Controls:** Forget normal walking. Movement requires tactical two-way controls. You drag the Head and Tail nodes independently using your mouse, causing your squishy body to bend, loop, and stretch dynamically.
*   **Dynamic Bezier-Curve Physics:** The middle of your body bends and stretches smoothly based on realistic custom physics. It feels floppy, organic, and highly responsive.
*   **The "Half-Safe" Stealth Rule:** If your Head or Tail touches a laser or a trap, it's instant game over. However, your middle body is completely safe! You must loop and bend your body to dodge hazards.
*   **Moving Obstacles:** Dodge moving lasers, security sensors, and patrolling traps that require perfect timing to bypass.
*   **Satirical Domestic Drama Theme:** A stressful spy mission with a hilarious twist—you must sneak into your own house to recover Mom’s favorite water bottle before she finds out it's missing!
*   **Juicy Audio & Visuals:** Enjoy smooth UI animations, seamless scene transitions, and satisfying squishy sound effects every time you stretch your worm.

## 🕹️ How to Play (Core Loop)

The gameplay revolves around a constant loop of observation, tactical reshaping, and precise movement:

1.  **Observe:** Study the moving security lasers, patrol patterns, and obstacles.
2.  **Drag to Move:** Use your **Left Mouse Click** to grab either the **Head** or the **Tail** of the worm. Drag them forward to extend or pull them close to bend the body into a curved loop to squeeze through tight spaces.
3.  **Mind the Ends:** Navigate the elastic worm body safely across checkpoints without letting the head or tail touch any sensor trigger. 
4.  **Retrieve:** Reach the sacred bottle to unlock the next level!

## ⚙️ Technical Architecture

This prototype was developed for the **GAMESEED 2026** Game Jam and features several robust technical implementations:

*   **Custom Two-Way Physics Constraints:** Engineered dynamic two-way constraint movement using a custom `LineRenderer` Bezier Curve approach. Both the Head and Tail can pull each other dynamically, creating satisfying tactile feedback.
*   **Event-Driven UI System:** Button events are managed via dynamic runtime listeners (`AddListener`) to completely eliminate `MissingReferenceException` errors upon scene reloads.
*   **Modular Level Management:** Structured game progression using level management data arrays and a Singleton `GameManager` utilizing `DontDestroyOnLoad` for state persistence.
*   **Juicy UX (DOTween):** 
    *   Programmed rhythmic obstacle patrols and swing patterns (`ObstacleMovement.cs`) using smooth DOTween loop cycles.
    *   Engineered elastic "bubbly" UI animations.
    *   Implemented a global black screen fader via CanvasGroup transitions for seamless level loading.
*   **Dynamic Audio System:** Dynamic looping squishy sound effects during worm drag states and distinct win/lose outcome audio streams.

## 📅 Development Roadmap (3-Week Production Log)

*   **Week 1: Core Mechanics & Scene Architecture:** Built the Bezier Curve physics engine, 2D collider dragging functionality, and resolved core scene architecture bugs.
*   **Week 2: Level Design, Data Persistence & Juice:** Designed 5 MVP levels, implemented DOTween for moving obstacles and UI, integrated dynamic audio, and added the global screen fader. Cut the automated comic sequence to optimize scope.
*   **Week 3: Build, Documentation & Deployment:** Conducted bug hunting, compiled final standalone formats (.exe and .apk), localized tooltips, and prepared the itch.io launch page.


