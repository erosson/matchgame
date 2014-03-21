#!/bin/sh -eux
cd build
name=orbitris
mv -f $name www/ || true

cp -rp ../www/* .
# Mac has a single dir, $name.app - supposedly macs run this directory like
# a program?
# Other systems have an executable plus a data dir, and build.bat packages
# those into a directory "$name".
# TODO: installer
for dir in win mac linux32 linux64; do
    cd $dir
    rm -f $name.zip
    zip -r $name.zip $name*
    cd -
done

rsync -auv --delete . deploy --exclude "deploy/" --exclude "*/orbitris/" --exclude "mac/orbitris.app/" 
