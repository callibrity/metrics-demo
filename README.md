# How to run

## Prerequesites
- `python 3.6` or higher (alternatively, you could try running locust in a docker container)
- `docker`
- `docker-compose`

## How to Run

#### 1. Install Locust
  - Run `pip3 install locust` and validate the install by running `locust -V`

#### 2. Build the Spring Boot demo image
  - Run `./mvnw spring-boot:build-image` in the `java-metrics-demo` folder

#### 3. Spin up everything
  - Run `docker-compose --build`

#### 4. Start up Locust
  - Run `locust` in the root directory

Once everything is spun up you can access:
- The Spring Boot service at [http://localhost:8080/actuator/prometheus](http://localhost:8080/actuator/prometheus)
- The .NET Core service at [http://localhost:5000/metrics-text](http://localhost:5000/metrics-text)
- The Prometheus UI at [http://localhost:9090](http://localhost:9090)
- Grafana at [http://localhost:3000](http://localhost:3000)
  - Username: `admin`
  - Password: `admin`
- The Locust UI at [http://localhost:8089](http://localhost:8089)

## To Switch the Target for Locust
When you access the locust ui, it will ask you for the number of users, the spawn rate, and the host.

For users and spawn rate, you can pick whatever, I usually just use 50 users and a 10 spawn rate.

Use these hosts:
- For Spring Boot: `localhost:8080`
- For .NET: `localhost:5000`

## Some Example Queries
These examples are based on the Spring Boot example. Make sure you're hitting it with Locust so you have data.

#### Latency Percentiles for Successful Requests
```
histogram_quantile(.99, sum(rate(http_server_requests_seconds_bucket{uri="/foobar", outcome!="SERVER_ERROR"}[5m])) by (le))
```
(You can change out `.99` for any percentile that you would like prometheus to calculate)

#### Throughput
```
sum(rate(http_server_requests_seconds_count{uri="/foobar"}[5m]))
```

#### Saturation (Memory)
```
sum(jvm_memory_used_bytes{area="heap"})
```

#### 7 Day Rolling Average Success Rate
This shows the success rate (percentage of successful requests) averaged over a 7 day period.
```
sum(rate(http_server_requests_seconds_count{uri!="/actuator/prometheus", outcome!="SERVER_ERROR"}[7d])) / sum(rate(http_server_requests_seconds_count{uri!="/actuator/prometheus"}[7d]))
```

## Cleaning up
Make sure you kill off the Locust process.

You can stop the docker compose with `Ctrl` + `C`, but running `docker-compose down -v` will remove all volumes associated with the compose file.