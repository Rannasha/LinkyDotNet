# NOTE: This project is outdated because Enedis changed its website & has included a captcha.

# LinkyDotNet

This repository introduces a class to interact with a user account on the website of Enedis, the manager of the French electrical grid, for the purposes of retrieving data collected by Linky, the connected power meter.

Enedis doesn't expose a public API to obtain Linky data. Instead, this class was created by analyzing and recreating requests made through the official website. Because no public API is available, the functionality of the class could be broken at any time if Enedis rolls out a change to their website.

# Available Data

Currently, Linky uploads recorded power consumption data once per day during the night. By default, only daily figures are recorded and uploaded. The user has to manually enable tracking of half-hourly data on the Enedis website (page: "Gérer ma consommation horaire"). When enabled, half-hourly data is uploaded along with the total for the day, once per day during the night.

Daily consumption is expressed in kWh. Half-hourly usage is expressed as the average rate of consumption during the period in kW. The LinkyDotNet class converts this value into kWh for consistency.

# Usage

Create an object by passing username (email) and password to the constructor:
````
    LinkyHandler linky = new LinkyHandler("user@domain.com", "password123");
````

Login:
````
    if (linky.Login() == LoginResult.OK)
    {
        // Login OK
    }
````

Get data:
````
    LinkyData lastWeek = linky.GetDailyData(DateTime.Now.AddDays(-8), DateTime.Now.AddDays(-1));
    if (lastWeek.Status == DataResult.OK)
    {
        // Data OK
    }
````

# Repo contents

The project LinkyDotNet contains the LinkyHandler class as well as several data-containers (in LinkyDataResponse.cs). Most of the data-containers are intermediate steps, modeled after the format of the JSON-output of the website. The final results are stored in the LinkyData class.
    
The project LinkyGUI is a very basic demo / test application using WinForms. It allows for basic interaction with all public methods and properties of the LinkyHandler class.

# Dependencies

The LinkyHandler class and associated data classes use Newtonsoft.Json to interact with JSON data.
