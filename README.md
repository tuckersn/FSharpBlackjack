# F# Blackjack
I created this program for a coding challenge prompt. This is my first F# project!

While the game is imperfect, my goals were met.

## Challenge
1. Generate a deck of cards, with all 4 suits, and all 13 ranks. 
2. Shuffle the deck.
3. Play at least one hand of blackjack, with hit and stand.


## Example Game 1:
```
$ ./BlackJack.exe
PLAYER: (15) ♠Five   ♣Queen
DEALER: (10) ♠Jack   ******
(h)it (s)tand (q)uit: h

Drawn card: ♣Four
PLAYER: (19) ♣Four   ♠Five   ♣Queen
DEALER: (10) ♠Jack   ******
(h)it (s)tand (q)uit: s

Dealer goes now!
PLAYER: (19) ♣Four   ♠Five   ♣Queen
DEALER: (16) ♠Jack   ♣Six
Dealer hits! (26) ♦Queen  ♠Jack   ♣Six
Dealer busted!
★ YOU WIN! ★

------
Play again (y)es/no?
```

## Example Game 2:
```
PLAYER: (18) ♠Queen  ♥Eight
DEALER:  (6) ♥Six    ******
(h)it (s)tand (q)uit: h

Drawn card: ♥Two
PLAYER: (20) ♥Two    ♠Queen  ♥Eight
DEALER:  (6) ♥Six    ******
(h)it (s)tand (q)uit: h

Drawn card: ♦Ace
PLAYER: (21) ♦Ace    ♥Two    ♠Queen  ♥Eight
DEALER:  (6) ♥Six    ******
★ BLACKJACK! ★

Dealer hits! (20) ♦Three  ♥Six    ♣Ace
Dealer hits! (19) ♠Nine   ♦Three  ♥Six    ♣Ace
Dealer hits! (28) ♥Nine   ♠Nine   ♦Three  ♥Six    ♣Ace
Dealer busted!
★ YOU WIN! ★

------
Play again (y)es/no?
```


## Example Game 3:
```
$ ./BlackJack.exe
PLAYER: (13) ♥Ten    ♦Three
DEALER:  (4) ♥Four   ******
(h)it (s)tand (q)uit: h

Drawn card: ♥Five
PLAYER: (18) ♥Five   ♥Ten    ♦Three
DEALER:  (4) ♥Four   ******
(h)it (s)tand (q)uit: h

Drawn card: ♦Eight
PLAYER: (26) ♦Eight  ♥Five   ♥Ten    ♦Three
DEALER: (11) ♥Four   ♠Seven
☹ YOU BUST! ☹

------
Play again (y)es/no?
```