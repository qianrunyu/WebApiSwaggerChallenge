This solution contains 2 projects: WebApiWithSwagger (.net core 2.1) and WebApiWithSwagger.test (Xunit tests)

--> Docker image url:
https://hub.docker.com/repository/docker/qianrunyu/codechallenge

docker pull qianrunyu/codechallenge:firsttry

http://localhost:[docker port]/swagger will be the local url to run the app.

--> HOW TO RUN:

1, Clone solution to local drive

2, Make WebApiWithSwagger  as the start up project (optional)

3, Build the solution. 

4, Click F5 to run from IIS Express. You should be able to see a swagger page with such url http://localhost:[some port number]/swagger/index.html

5, You will see 2 Get api, 1 Delete api, 1 Post api and 1 Put api after running the app. 




--> Project APIs explaination:

*** All APIs must provide a dealer code to query/update stock. There are 2 pre-defined dealerCode in seeding database: A01 and B02. You can create new dealer code by using AddCar API ***

*** Car is defined as DealerCode + Make + Model + Year and car stock is associated with it. For example "A01, Audi, A7, 2020, 5" means dealer A01 has 5 Audi A7 made in 2020 ***

1, GET /api/cars/{dealercode}
"A01" will return an  array with 3 car stocks. Each car stock will have its own stock level. 

2, GET /api/cars/{dealercode}/search
Search by dealer code + Made + Model. It will return an array of car stocks 

3, POST /api/cars/addcar
CarSource field is optional. All other fields are necessary. If a same car with same dealer code is found in database then stockLevel will increase by 1. 
Otherwise a new car with stockLevel = 1 will be added into datase.
You can also create a  car with a new dealer code.

4, DELETE /api/cars/removecar
Reason field is optional. All other fields are necessary. If a car stock with same dealer code + make + model + year is found in database, its stockLevel will decrease by 1.
If no such car is found or stockLevel is already 0 in database then bad request response will return.

5, PUT /api/cars/updatestock
Update car stock by stockId and dealerCode. The unique stockId can be found by calling GET /api/cars/{dealercode}
For example,  dealer "B02" with stockId 7 with new stockLevel 100 will update audi a7 1999 from stockLevel 10 to 100.

{
  "StockId": 7,
  "DealerCode": "B02",
  "StockLevel": 100
}
will return 
{
  "id": 7,
  "make": "AUDI",
  "model": "A7",
  "year": 1999,
  "dealerCode": "B02",
  "stockLevel": 100
}
If stockId or DealerCode is not found, a bad request response will return.



--> Some tips:

1, The project uses dependency injections for injecting different services: carManagementService, validationService and loggerService. This creates better abstraction and easier for testing.
2, In memory database (EFCore) is used for seeding initial car stocks and storing car data.
3, Xunit test (36 tests) project covers ALL public functions and services, including controller functions. 
4, Query string of DealerCode/Make/Model is case-insensitive. For example:  "AUDI" or "Audi" will return same car stock data.


Any questions please contact qianrunyu@gmail.com

Have fun!




