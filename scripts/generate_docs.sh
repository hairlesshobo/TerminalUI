#!/bin/sh

SCRIPT_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )"

cd $SCRIPT_DIR/../

dotnet build 

cd ./docs
xmldoc2md ../src/bin/Debug/netstandard2.0/TerminalUI.dll ./api/ --examples-path examples/

# here we will need to generate the api/_sidebar.md file from the input index.md file
cd ./api
cat index.md | grep -v -e '^$' | tail -n +2 | sed 's/##/    -/' | sed 's/#/    -/' | sed 's/\[/        - [/'