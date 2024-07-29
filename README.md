# Historical Quiz Telegram Bot

A simple Telegram bot written in C# that serves as a historical quiz, primarily focused on the 20th century. The bot also provides a selection of interesting historical facts.

## Features

- Engage in a historical quiz with questions mostly about the 20th century.
- Discover interesting historical facts.
- Easy to set up and configure.

## Getting Started

### Prerequisites

- .NET SDK
- PostgreSQL database
- Telegram bot token

### Configure the bot:

   - Open `Program/appsettings.json`.
   - Set your Telegram bot token in the `BotToken` field.
   - Set your PostgreSQL connection string in the `ConnectionString` field.

   ```json
   {
       "BotToken": "your-telegram-bot-token",
       "ConnectionString": "your-postgresql-connection-string"
   }
   ```

### Apply SQL scripts:

   - Navigate to the `DataAccess` folder.
   - Execute the SQL scripts in your PostgreSQL database to set up the necessary tables and data.

## Usage

- Start a chat with your bot on Telegram.
- Follow the prompts to answer historical quiz questions.
- Enjoy learning new and interesting historical facts.

## Folder Structure

- `Abstractions/`: Contains the interfaces for DataAccess layer.
- `App/`: Contains the main services.
- `Contracts/`: Contains the interfaces for services layer.
- `Models/`: Contains the models for all project.
- `TelegramBot/`: Contains code that interacts with Telegram.
- `Program/`: Contains the main start application code and configuration file.
- `DataAccess/`: Contains repositories and SQL scripts for inserting data in the PostgreSQL database.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.