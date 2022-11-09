# DiscordBot

## What do you need

- Microsoft Visual Studio 2022
- Microsoft .NET Core 3.1 SDK

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
    "TankerKoenig": "Put your TankerKoenig credentials here",
    "IpInfoIo": "Put your IPInfo.io credentials here",
    "Tenor": "Put your TENOR credentials here",
    "IMDB": "Put your IMDB credentials here",
    "LastFM": "Put your LastFM credentials here",
    "Cuttly": "Put your Cuttly credentials here"
  },
  "QrCode": {
    "Path": "Put your saving path for qr codes here"
  }
```

- In the same directory creare a file called devsettings.json too and paste the follwing in this file

```
{
  "Settings": {
    "Token": "Put your Discord credentials here",
    "CommandPrefix": "?"
  }
}
```

- With the seperate settings you are able to run a second bot in developing with a second discord key, to prevent to close the bot in older versions

* Now you are able to run the bot and/or modify the code to your own requirements

## Available commands

- !help

---

- !admin clear (only available for bot owners) / clears a text channel
- !admin spammer (only available for bot owners) / spamms a text channel
- !admin userstatuslist | !admin usl / sends a list from all available users in guild with their online statuses

---

- !weather city / sends weather information from a specific city to a channel
- !weather forecast / sends a weatherforecast in 3 hour interval to a channel

---

- !fun penis / gets a random penis length from a user to a channel
- !fun joke / sends a random joke in german or english to a channel
- !fun gif / send a random gif by a category to a channel
- !fun song / sends a random song from LastFM to a channel
- !fun artist / sends a random song by a artist from LastFM to a channel

---

- !info ip / sends iformations about a ip address to a channel

---

- !qr generate | !qr gen / generates a qr code with user specified content

---

- !useful gasolineprice | !useful gp / sends the Gas Stations with current prices in 3 km radius to a channel
- !useful url / sends a via cutt.ly shortenend URL to a channel

## How to build the bot

- In Visual Studio Developer Console simply type following command, replace linux-x64 with the operating system the bot will run on it and press enter

```
dotnet publish -c Release -r linux-x64 --self-contained false
```
