#!/bin/bash
dotnet clean
dotnet build --no-incremental "/p:UseSharedCompilation=false;RunCodeAnalysis=false"