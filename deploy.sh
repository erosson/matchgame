#!/bin/sh -eux
./build.sh
rsync -auv --delete build/deploy/ erosson_erosson@ssh.phx.nearlyfreespeech.net:games/orbitris/
