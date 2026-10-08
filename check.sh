#!/bin/bash
set -euxo pipefail

dotnet format --verify-no-changes --severity info

dotnet clean && dotnet build
