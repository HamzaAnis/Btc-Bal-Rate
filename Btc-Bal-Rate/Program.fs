open System
open FSharp.Data

type BtcData = FSharp.Data.JsonProvider<"""{
  "USD":{"15m":1.1,"last":1.1,"buy":1.1,"sell":1.1,"symbol":"$"}}""">

let prices = BtcData.Load("https://blockchain.info/ticker")
System.Console.WriteLine(prices.Usd.Sell)
System.Console.ReadLine()