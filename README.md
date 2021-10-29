# stock-quote-alert
A C# .NET core application that monitors a stock quote in realtime and alerts (via e-mail) the user if the price is higher or lower than the chosen bound parameter.

## Motivation
This app was built because of a challenge proposed during a job interview and also because i would like to improve my back-end skills using C# .NET

## How does the application works
The program will show via console the realtime price of the chosen stock. If the price is higher than the sale price bound or lower than the purchase price bound, it will send an email warning about the purchase or the sale of the stock. When the email is sent, the application finishes running.

## How does the application works internally
When the main application is being executed, it is continuously listening to the stock quote API data provided by https://finnhub.io/. The emails are asynchronously sent by acessing a SMTP server and getting the credentials stored in a Google Cloud Storage database, which also stores the API key.

## Installation
(You can skip steps 2 and 3 if you want to manually build the projecto using MSBuild, but i really do not recommend it)
1. Exctract the project zip or clone it to your local files
2. Open it using Visual Studio or JetBrainsRider or any other .NET compatible IDE
3. Build the project

If you want to globally use the application (not mattering where you will be located via terminal) you need to add the {path_to_solution}/bin/Debug/net5.0/ (generated after building the project) into your system environment variables PATH.
- To do it on Linux: `export PATH="{path_to_solution}/bin/Debug/net5.0/:$PATH"`
- To do it on Windows: `setx path "%path%;{{path_to_solution}/bin/Debug/net5.0/}`

(Of course, do not forget to replace {path_to_solution} with the actual project solution path.

## Usage
`stock-quote-alert {stock_symbol} {sale-price-bound} {purchase-price-bound}`
<br/><br/>For example:<br/>`stock-quote-alert petr4.sa 22.67 22.59` <br/>will monitors PETR4 stock (Petróleo Brasileiro S.A stock). 22.67 for the sale action and 22.59 for the purchase action.
