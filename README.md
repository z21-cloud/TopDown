# Top Down Shooter (Learning Project)

This is a personal Unity project created to practice C# programming and game development skills.

## Goals
- Practice Object-Oriented Programming (OOP) principles
- Apply SOLID design principles
- Implement Object Pooling pattern

## Features
- PlayerController with movement and shooting
- Player can dash, pick up weapons and health points
- Wave Spawner & EnemyPool (object pooling for enemy spawning) || in progress
- Basic UI & Effects (player health)
- Main Menu, Pause menu, EndGame menu

## Game Loop
- Player spawns
- Wave spawner starts spawn enemy waves
- After waves, boss spawnes (2 stage)
- 1-st stage: patrol from one point to another, spawn enemies while moving, when reaches point spawn stone spikes
- 2-nd stage: chase player, spawn enemies while chasing

## Enemies behaviour
1. Melee enemy -> chase player, tries attack, continue chasing
2. Summoner -> patrols same point, as boss, spawns minions at a point. If player to close -> tries attack (same as for melee enemy)
3. Minion -> chase player, tries attack, continue chasing (same as for melee enemy)
4. Range Enemy -> chase player, tries attack, creates projectile, continue chasing (same as for melee enemy)

## To-do:
- Implement object pooling for enemies and integrate it with the wave spawner || in progress
- Add a second phase for the game boss  || in progress
- Experiment with object pooling & queue  || in progress
- Apply object pooling to both player and enemy projectiles || in progress  
- Add more UI elements and visual effects (menus, pause screen, victory screen, screen shake, dash effects, boss attack effects) || in progress 
- Reduce current code dependencies to facilitate easier future scaling  || in progress

## Future project development:  
- The core loop fits well as a foundation for a roguelike (inspired by Soul Knight)  
- Room generation  
- Companion system  
- Shops, inventory, and reward chests
