# Steamworks Lobbies & Matchmaking in Unity 5.3.2
Steamworks.NET (https://github.com/rlabrecque/Steamworks.NET) is REQUIRED for this to work. This is intended to be a **BAREBONES** implementation of Steamworks.NET, for developers to expand on.

**Common mistakes** :
- Include a file named "steam_appid.txt" in the directory of your executable. In that text file should simply be the number of your unique appid.
- Make sure an instance of steam is running in the background.
- Test your application with Ctrl-B exclusively. If you don't, the Steam API will flag the unity editor as the game process, and it will always be listed as "Running" in your steam library, until you restart Unity. If you don't do this, your steam profile will never exit lobbies, and you'll end up with a bunch of "ghost lobbies"
