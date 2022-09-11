VAR pokemon = ""

-> main


=== main ===

# ACTOR: Narrator
Hi there.
Welcome to the game!


Press Space to continue.
Use mouse or up/down to select the choices
Do you understand so far?
    + [Understood]
    + [Again?]
        -> main

-
Congratulations...
There is a hidden message over here...

# ACTOR: Professor Oak
Btw, let's select a pokemon:
    * [Charmander]
        ~ pokemon = "Charmander"
    * [Bulbasaur]
        ~ pokemon = "Bulbasaur"
    * [Squirtle]
        ~ pokemon = "Squirtle"
        
- 
You have selected {pokemon}


-> END