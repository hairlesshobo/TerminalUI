#!/bin/sh
# requirements
#   - dotnet
#   - xmldoc2md (https://charlesdevandiere.github.io/xmldoc2md/)
#   - dotnet tool install -g XMLDoc2Markdown


SCRIPT_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )"

cd $SCRIPT_DIR/../

dotnet build 

cd ./docs
xmldoc2md ../src/bin/Debug/netstandard2.0/TerminalUI.dll ./api/ --examples-path examples/

# here we will need to generate the api/_sidebar.md file from the input index.md file
cd ./api
cat ../_sidebar.md index.md \
    | grep -v -e '^$' \
    | grep -v -e '^# TerminalUI$' \
    | sed 's/##/    -/' \
    | sed 's/#/    -/' \
    | sed 's/^\[/        - [/' > _sidebar.md