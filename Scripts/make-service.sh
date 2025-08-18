#!/bin/bash

if [ -z "$1" ]; then
    echo "Usage: ./Scripts/make-service.sh <Name>"
    exit 1
fi

NAME=$1
PROJECT_NAME=$(basename "$PWD")

TARGET_DIR="Services/${NAME}Service"
mkdir -p "$TARGET_DIR"

# File Interface
cat <<EOL > "$TARGET_DIR/I${NAME}Service.cs"
namespace $PROJECT_NAME.Services.${NAME}Service
{
    public interface I${NAME}Service
    {
        public Task<bool> CreateAsync(string name);
    }
}
EOL

# File Implementation
cat <<EOL > "$TARGET_DIR/${NAME}Service.cs"
using $PROJECT_NAME.Services.${NAME}Service;
namespace $PROJECT_NAME.Services.${NAME}Service
{
    public class ${NAME}Service : I${NAME}Service
    {
        public async Task<bool> CreateAsync(string name)
        {
            return true;
        }
    }
}
EOL

chmod -R 777 "$TARGET_DIR"
echo "✅ Service $NAME created in $TARGET_DIR with rwx access"
