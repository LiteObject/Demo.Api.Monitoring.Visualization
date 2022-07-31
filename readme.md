# Monitoring, Tracing, Visualization... and more

## Prometheus vs Grafana: What's the main difference?
Prometheus is a monitoring solution for storing time series data like metrics. 
Grafana allows to visualize the data stored in Prometheus (and other sources).

## What is OpenTelemetry?
>OpenTelemetry is a collection of tools, APIs, and SDKs. Use it to instrument, generate, collect, and export telemetry data (metrics, logs, and traces) to help you analyze your software’s performance and behavior.

## What is Zipkin?
>Zipkin is a distributed tracing system. It helps gather timing data needed to troubleshoot latency problems in service architectures. Features include both the collection and lookup of this data.

## How to setup Prometheus

## How to setup Grafana?
- Go to Grafana and add a new "Data Source"
  - Since both Grafana and Prometheus are in the docker, we can set the source URL to "http://host.docker.internal:30090"
  - Save & test the datasource

---
## External Links:
- [Get started with Grafana and Prometheus](https://grafana.com/docs/grafana/latest/getting-started/get-started-grafana-prometheus/)
- [OpenTelemetry for .NET](https://opentelemetry.io/docs/instrumentation/net/getting-started/)
- - [More on Zipkin](https://zipkin.io/)
