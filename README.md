# Venue Finder

Leverage external API to find venues that accept cryptocurrency payments.

## Tech Stack

**Client:** React + Material UI

**Server:** ASP.NET Core WebAPI + GraphQL with Hot Chocolate

**Database:** MongoDB with Helm (bitnami) and Kubernetes (minikube)

**APIs:** Coinmap

**Other:** 
- JWT Authentication
- Domain Driven Design
- MongoDB external API caching (1 day for categories, 1 hour for venues)
- In-memory Apollo client caching

## Installation

### 1. Install minikube

[https://minikube.sigs.k8s.io/docs/start/](https://minikube.sigs.k8s.io/docs/start/)

### 2. Start minikube

```PowerShell
minikube start
```

### 3. Install Helm

[https://helm.sh/docs/intro/install/](https://helm.sh/docs/intro/install/)

### 4. Add Bitnami using Helm

```PowerShell
helm repo add bitnami https://charts.bitnami.com/bitnami
```

### 5. Update Bitnami

```PowerShell
helm repo update
```

### 6. Install mongoDB

```PowerShell
helm install my-mongo bitnami/mongodb --set auth.enabled=true,auth.rootPassword=qwerty,auth.username=root,auth.password=qwerty,auth.database=venuefinder
```

## Usage

### 1. Port forward to database

Before running the project, make sure the database is accessible by running this command:

```PowerShell
kubectl port-forward svc/my-mongo-mongodb 27017:27017
```

As long as the port forward is active, the database can be accessed locally.

### 2. Clone the project locally

### 3. Run the project

## Clean-up

After reviewing the project, you can remove the Helm environment:

```PowerShell
helm delete my-mongo
```
