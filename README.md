### ClientLogParser ###

ClientLogParser is a library that parses Path of Exile Clientlog and fires events for system messages, whispers and trade messages etc.

Available on [nuget](https://www.nuget.org/packages/ClientLogParser/).

It currently has default parser implementations for:
 - https://www.pathofexile.com/trade/
 - http://poe.trade (only because they use the same structure as the official site above)
 - https://poeapp.com

## Examples ##

### Creating the parser and subscribing to the TradeMessageEvent ###

```csharp
// Create a new parser to watch the file for new entries
var parser = new Overseer(@"E:\Games\PathOfExile\logs\Client.txt");

// Subscribe to trade message event and display the name of the item and the price he wants to buy for
parser.TradeMessageEvent += (object sender, TradeMessageEventArgs tradeMessage) => Console.WriteLine(tradeMessage.Item.Name + ": " + tradeMessage.Item.Price);
```
