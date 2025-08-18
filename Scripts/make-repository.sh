#!/bin/bash

# Usage: ./Scripts/make-repository.sh <Name>
if [ -z "$1" ]; then
    echo "Usage: ./Scripts/make-repository.sh <Name>"
    exit 1
fi

NAME=$1
PROJECT_NAME=$(basename "$PWD")
TARGET_DIR="Repositories/${NAME}Repository"
mkdir -p "$TARGET_DIR"

# File Interface
cat <<EOL > "$TARGET_DIR/I${NAME}Repository.cs"
using $PROJECT_NAME.Models;

namespace $PROJECT_NAME.Repositories.${NAME}Repository
{
    public interface I${NAME}Repository
    {
        public Task<$NAME> AddAsync($NAME entity);
        public Task<List<$NAME>> GetAllAsync();
        public Task<$NAME?> GetByIdAsync(long id);
    }
}
EOL

# File Implementation
cat <<EOL > "$TARGET_DIR/${NAME}Repository.cs"
using Microsoft.EntityFrameworkCore;
using $PROJECT_NAME.Data;
using $PROJECT_NAME.Models;

namespace $PROJECT_NAME.Repositories.${NAME}Repository
{
    public class ${NAME}Repository : I${NAME}Repository
    {
        private readonly ApplicationDbContext _context;

        public ${NAME}Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<$NAME> AddAsync($NAME entity)
        {
            _context.Set<$NAME>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<$NAME>> GetAllAsync()
        {
            return await _context.Set<$NAME>().ToListAsync();
        }

        public async Task<$NAME?> GetByIdAsync(long id)
        {
            return await _context.Set<$NAME>().FindAsync(id);
        }
    }
}
EOL

chmod -R 777 "$TARGET_DIR"
echo "✅ Repository $NAME created in $TARGET_DIR with DbContext included"
