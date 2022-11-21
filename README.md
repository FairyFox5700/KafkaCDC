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
![image](https://user-images.githubusercontent.com/46414904/202932789-52f385f0-be67-4c13-8630-4121d124bc5c.png)

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
![image](https://user-images.githubusercontent.com/46414904/202932939-c4586215-3fdb-4f82-af58-03b409aa6c6a.png)

![image](https://user-images.githubusercontent.com/46414904/202932953-c963df4c-5adf-42e8-bbea-29fd43bee76a.png)


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

When new deal is added in the same time record wis added to outbox table:
![image](https://user-images.githubusercontent.com/46414904/202933024-6bfb8bd8-b665-46a1-9029-d13d5512bed2.png)

- Create subscrition of trader to deal:
```json
{
  "traderId": "f14490e4-0410-4d44-a2c5-9274167ceb2a",
  "dealId": "7d5f0271-37c3-4586-9481-6e7f302ee405"
}
```
New subscription will be added to the database and dealSubscriptions event will be written to outbox table.

![image](https://user-images.githubusercontent.com/46414904/202933149-3dc84fa5-8996-4c33-a538-3bca7bd08188.png)


## Debezium connector configuration:

configuration for deals service will be configured by calling 
### PUT  http://localhost:8083/connectors/outbox-connector/config
```json
{
  "connector.class": "io.debezium.connector.postgresql.PostgresConnector",
  "tasks.max": "1",
  "database.hostname": "postgres",
  "database.port": "5432",
  "database.user": "postgres",
  "database.password": "root",
  "database.dbname": "kafkacdc.deals",
  "database.server.name": "postgres",
  "schema.include.list": "public",
  "table.include.list": "public.OutboxEvents",
  "tombstones.on.delete": "false",
  "transforms": "outbox",
  "transforms.outbox.type": "io.debezium.transforms.outbox.EventRouter",
  "slot.name": "debezium10",
  "transforms.outbox.table.field.event.id": "Id",
  "transforms.outbox.table.field.event.key": "AggregateId",
  "transforms.outbox.table.field.event.payload": "Payload",
  "transforms.outbox.route.by.field": "AggregateType",
  "transforms.outbox.route.topic.replacement": "${routedByValue}.events",
  "transforms.outbox.table.fields.additional.placement": "Type:header:eventType",
  "transforms.outbox.debezium.expand.json.payload": "true",
  "value.converter": "org.apache.kafka.connect.json.JsonConverter",
  "value.converter.schemas.enable": false,
  "plugin.name": "pgoutput"
}
```

configuration for traders service will be configured by calling 
### PUT  http://localhost:8083/connectors/outbox-connector/config
```json
{
  "connector.class": "io.debezium.connector.postgresql.PostgresConnector",
  "tasks.max": "1",
  "database.hostname": "postgres",
  "database.port": "5432",
  "database.user": "postgres",
  "database.password": "root",
  "database.dbname": "kafkacdc.traders",
  "database.server.name": "postgres",
  "schema.include.list": "public",
  "table.include.list": "public.OutboxEvents",
  "tombstones.on.delete": "false",
  "transforms": "outbox",
  "transforms.outbox.type": "io.debezium.transforms.outbox.EventRouter",
  "transforms.outbox.table.field.event.id": "Id",
  "transforms.outbox.table.field.event.key": "AggregateId",
  "transforms.outbox.table.field.event.payload": "Payload",
  "transforms.outbox.route.by.field": "AggregateType",
  "transforms.outbox.route.topic.replacement": "${routedByValue}.events",
   "slot.name": "debezium11",
  "transforms.outbox.table.fields.additional.placement": "Type:header:eventType",
  "transforms.outbox.debezium.expand.json.payload": "true",
  "value.converter": "org.apache.kafka.connect.json.JsonConverter",
  "value.converter.schemas.enable": false,
  "plugin.name": "pgoutput"
}
```

## status of connectors:
http://localhost:8083/connectors?expand=info&expand=status

Debezium connector now will stream events to subscribers from outbox tables.
First it will create topics in kafka and stream payload to appropriate topic based on configuration defined above.
![image](https://user-images.githubusercontent.com/46414904/202933343-dde2dbc6-58e8-4e56-888b-f6a18adc3943.png)

