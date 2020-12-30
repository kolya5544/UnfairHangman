# UnfairHangman
A concept of hangman game against a CPU, except CPU is cheating

## How does it work?

First, CPU decides a length of a word. Out of all words from the wordlist, he now only stores ones matching the length.

Then, the game starts. After every guess, CPU tries to limit the wordlist to just the words that dont have any of letters, chosen by the player. This way, the player is guaranteed to lose until there is no more words matching the lengths and having no letters. Then, a random word out of a final list is chosen, and it's considered to be the 'real' one.

Now, the real hangman starts, where the only word left is the "real" word, and from this point, the player still has a chance to win.

## But that's impossible to win!

Never intended to be possible

## I still won!

Good job! You can try again.