#!/bin/bash

cd Damas.Test

rm -rf TestResults
rm -rf TestReports

dotnet test --collect:"XPlat Code Coverage"

for hash in $(ls TestResults); do
    path="$dir/$hash"
    reportgenerator -reports:"TestResults/$hash/coverage.cobertura.xml" -targetdir:"TestReports/$hash" -reporttypes:Html
    start chrome $(realpath TestReports/$hash/index.html)
done