#!/bin/bash

echo "Starting..."

echo "Deploying Flyway scripts"

docker-compose -p "euromon-books-db" -f docker-compose.db.yml up euromon-books-db-flyway

docker-compose -p "euromon-books-db" -f docker-compose.db.yml rm -f -s -v euromon-books-db-flyway

read -n 1 -s -r -p "Press any key to continue"