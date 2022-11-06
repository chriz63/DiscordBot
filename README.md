# DiscordBot

## How to start

- In the root directory of the bot, where you can locate i.e. Program.cs, create a new file called appsettings.json

- Paste following to appsettings.json and replace it with your credentials

```
{
  "Settings": {
    "Token": "Put your Discord credentials here",
    "CommandPrefix": "!"
  },
  "ApiKeys": {
    "OpenWeatherMap": "Put your OpenWeatherMap credentials here",
    "TankerKoenig": "Put your TankerKoenig credentials here"
  },
  "QrCode": {
    "Path": "Put your saving path for qr codes here"
  }
```

- Now you are able to run the bot and/or modify the code to your own requirements

## Available commands

- !help
- !admin clear (only available for bot owners) / clears a text channel
- !admin spammer (only available for bot owners) / spamms a text channel
- !weather city / sends weather information from a specific city to a channel
- !fun penis / gets a random penis length from a user to a channel
