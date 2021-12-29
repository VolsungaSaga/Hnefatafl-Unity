
# HNEFATAFL (nef-a-taa-fl) - A Viking Strategy Game

*Hnefatafl*, and other tafl games similar to it, was a chess-like game played circa 800-1000 C.E. by the Norsemen of medieval Scandinavia. Nobody really knows
what the rules were or how it was played in those times, since the rules weren't written down. However, modern reconstructionists from the 1800s onwards have invented
their own rulesets, resulting in a dizzying array of Tafl variants played by enthusiasts and casual players alike. 

I've always had a great interest in early medieval Scandinavian history, so for my first original game, this seemed like a doable project. 


## How To Play
----------------
The rules used for this implementation of *Hnefatafl* come from Hurstwic, an organization of historical reconstructionists focusing on early medieval Northern Europe. You can find the rules [here](http://www.hurstwic.com/library/how_to/hnefatafl_hurstwic_rev1a.pdf).

There are two teams - the Red Team and the White Team. The White Team starts in the center of the board, the White King surrounded by his warriors. The Red Team starts on the board edges, encircling the White King and his men. 

The teams take turns. Each side may move exactly one piece per turn. A piece can move any number of spaces in a straight line, but it cannot pass through another piece or end its move in the center tile, which is reserved for the White King. You can capture an opposing piece by moving two pieces to opposite sides of the target piece.

The White Team wins by getting their King to any corner tile without the King getting captured by the Red Team. The Red Team wins by capturing the White King -- that is, surrounding it with four warriors, or three warriors and an edge.

## Complexity
----------
*Hnefatafl* is not a particularly complicated game by itself -- the most enduring games seldom are -- but as compared to the other games we've looked at in this course, I think the complexity requirement is well exceeded. There is a way to win as either the Red Team or the White Team. I did all the game logic, including ancillary functions such as UI, music, sound effects, and scene transitioning. The UI images and the chess pieces are from the Unity store. There are four effective "states" to the game:

- The Start Menu Scene
- The Play Scene
- The White Victory Scene
- The Red Victory Scene

--------------------
The game loop roughly follows this logic:

1. Player selects a piece
2. Game checks if that piece is selectable by the player
3. Player clicks on a cell.
4. Game checks if the selected piece, if any, can move to that cell.
5. The move is done.
6. Game checks if there were any captured pieces as a result of the move, or if a team's victory conditions were met. If a team is victorious, go to the appropriate victory scene.
7. Go to 1.

The ruleset is implemented as a seperate static class (Scripts/Game/Ruleset.cs) from the rest of the code because, if I wanted to use one of the other *tafl* variants out there, I could theoretically just replace the current Ruleset with another one that implemented the same functions.

I made the menu manager and other associated scripts (Scripts/UI/*.cs) so that I'd have a way to swap between different sets of buttons, images, and text elements. It's not as generically applicable as I'd like, since it only handles two menus and doesn't handle every type of UI element, but it's serviceable for this small game.

I also made a music manager (Scripts/Game/MusicManager.cs) that can be loaded with an arbitrarily large amount of royalty-free music to be played during the game. 

I did the usual sort of encapsulation for OO-Design. Each game object is the sole owner of its fields, with some publically exposed methods for getting information that other objects need to know. 

The objects of the game are arranged thusly:

A GameBoard is made up of a 2D array of GameBoardCells, and a GameBoardCell potentially has a GamePiece as an occupant. A GameManager oversees the actions of the GameBoard and responds to certain events accordingly, regulating turn order and such. When a piece is moved, it's being transferred from one GameBoardCell to another. A GameBoard knows about its Cells, and each Cell knows whether there's a GamePiece on it or not. However, a GamePiece doesn't know anything about its Cell, and a Cell doesn't know anything about its Board. 

## Distinctiveness

To my knowledge, the content of CS50-G has not included medieval board games or turn-based board games in general. Therefore, I think this project meets the distinctiveness requirement.