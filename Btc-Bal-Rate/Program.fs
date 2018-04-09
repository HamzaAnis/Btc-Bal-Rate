//http://fssnip.net/7S1/title/Get-Bitcoin-wallet-account-balance-by-public-key
open System
open FSharp.Data
type BtcData = FSharp.Data.JsonProvider<"""{
  "USD":{"15m":1.1,"last":1.1,"buy":1.1,"sell":1.1,"symbol":"$"}}""">

type WalletData = 
    FSharp.Data.JsonProvider<
        """{"unspent_outputs":[{"value":9000000000},{"value":10}]}""">

let getBalance publicKey =
    let balance = 
        try
            WalletData.Load(
                "https://blockchain.info/unspent?active=" + 
                publicKey).UnspentOutputs 
            |> Array.sumBy(fun t -> t.Value)
        with // Can response e.g. "No free outputs to spend"
        | :? System.Net.WebException as ex -> 
            use stream = ex.Response.GetResponseStream()
            use reader = new System.IO.StreamReader(stream)
            let err = reader.ReadToEnd()
            System.Console.WriteLine err
            0L
    (System.Convert.ToDecimal balance) / 100000000m

let price = BtcData.Load("https://blockchain.info/ticker")
System.Console.WriteLine(price.Usd.Sell * getBalance "1DkyBEKt5S2GDtv7aQw6rQepAvnsRyHoYM")
System.Console.ReadLine()