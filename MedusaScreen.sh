#!/bin/bash
cd /home/discord/medusa
rm screenlog.0
screen -LS medusa -dm bash -c 'cd /home/discord/medusa/NadekoBot/src/NadekoBot; dotnet restore; dotnet run --configuration Release'
