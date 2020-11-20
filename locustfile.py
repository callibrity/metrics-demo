from locust import HttpUser, task

class DotnetDemo(HttpUser):
    host = "http://localhost:8080"

    @task
    def foobar(self):
        self.client.get("/foobar")