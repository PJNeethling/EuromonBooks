#!/bin/bash

echo "Starting..."

docker-compose -p "euromon-books-db" -f docker-compose.db.yml down --rmi all --remove-orphans

docker-compose -p "euromon-books-db" -f docker-compose.db.yml up -d --force-recreate euromon-books-db

echo "Wating for DB to spin up"

x=1
while [ $x -le 60 ]
do
    sleep 1s
  echo -ne "."
  x=$(( $x + 1 ))
done

read -n 1 -s -r -p "Press any key to continue"