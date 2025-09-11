#!/bin/bash
set -e

DB_NAME="propertydb"
MONGO_URI="mongodb://localhost:27017"

echo "Data is uploading $DB_NAME..."

mongoimport --uri=$MONGO_URI/$DB_NAME --collection=owners --file=/docker-entrypoint-initdb.d/owners.json --jsonArray --drop
mongoimport --uri=$MONGO_URI/$DB_NAME --collection=properties --file=/docker-entrypoint-initdb.d/properties.json --jsonArray --drop
mongoimport --uri=$MONGO_URI/$DB_NAME --collection=propertyImages --file=/docker-entrypoint-initdb.d/propertyImages.json --jsonArray --drop
mongoimport --uri=$MONGO_URI/$DB_NAME --collection=propertyTraces --file=/docker-entrypoint-initdb.d/propertyTraces.json --jsonArray --drop

echo "Uploaded data in the database $DB_NAME"
