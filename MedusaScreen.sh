#!/bin/bash
screen -S medusa -dm bash -c 'cd /home/discord/medusa/NadekoBot/src/NadekoBot; dotnet restore; dotnet run --configuration Release'
