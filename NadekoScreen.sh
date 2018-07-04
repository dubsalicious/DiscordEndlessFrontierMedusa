#!/bin/bash
chown -R nadeko:nadeko /home/nadeko/NadekoBot
screen -SL nadeko -dm su -c "/home/nadeko/NadekoARN.sh" -s /bin/bash nadeko
