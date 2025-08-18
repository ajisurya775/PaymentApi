#!/bin/bash

if [ -z "$1" ] || [ -z "$2" ]; then
    echo "Usage: ./Scripts/make-dto.sh <FolderName> <DtoName>"
    exit 1
fi

FOLDER=$1
DTO=$2
PROJECT_NAME=$(basename "$PWD")

TARGET_DIR="DTOs/$FOLDER"
mkdir -p "$TARGET_DIR"

FILE="$TARGET_DIR/${DTO}DTO.cs"

cat <<EOL > "$FILE"
namespace $PROJECT_NAME.DTOs.$FOLDER
{
    public class ${DTO}DTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
EOL

# kasih akses rwx ke folder & file
chmod -R 777 "$TARGET_DIR"

echo "✅ DTO ${DTO} created in namespace $PROJECT_NAME.DTOs.$FOLDER with rwx access"
