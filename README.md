# KafkaCDC

# start with:

docker-compose up

## Seq
http://localhost:5555/#/events
![image](https://user-images.githubusercontent.com/46414904/202564011-5e87c5f5-464f-4ada-a439-179113330269.png)


## Confluent Kafka
![image](https://user-images.githubusercontent.com/46414904/202567766-433238fc-1444-45c0-a360-0c92297ef9a4.png)

# run 3 services in VS:

## KafkaCDC.Deals
![image](https://user-images.githubusercontent.com/46414904/202930943-1196bf41-ea77-4047-8f2b-7240c1dd8b88.png)

## KafkaCDC.Traders
![image](https://user-images.githubusercontent.com/46414904/202931044-dbccb6ed-1e95-4c66-a4b7-75c82a824655.png)

## KafkaCDC.Notifications
![Uploading image.png…]()

## postman requests

- Create new trader:

```json
{
    "email": "tyschenk90@gmail.com",
    "firstName": "ira",
    "lastName": "tysh",
    "address": "home",
    "phoneNumber": "09721121",
    "birthDate": "2022-05-24T13:22:25.667Z",
    "gender": "female"
}
```

Record will be added to traders table as well as to outbox table.


-Create new deal 
```json
{
    "id":"6fa85f64-5717-4562-b3fc-2c963f66afa6",
    "shortName": "swr576skj6",
    "dealType": "Equity",
    "dealStatus": "Open",
    "amount": 12,
    "initialPriceRangeLow": 10,
    "initialPriceRangeHigh": 13,
    "revisedPriceRangeLow": 14,
    "revisedPriceRangeHigh": 15
}
```

- Create subscrition of trader to deal:
```json
{
  "traderId": "f14490e4-0410-4d44-a2c5-9274167ceb2a",
  "dealId": "7d5f0271-37c3-4586-9481-6e7f302ee405"
}
```
