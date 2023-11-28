# DotNetCoreWebApp

This is a sample repo demonstrating how to integrate [Security-Code-Scan action](https://github.com/marketplace/actions/securitycodescan) into CI/CD process.


## CODEQL CLI Tooling

### Create Database Cluster
```
codeql database create codeql-database --db-cluster --language=javascript,csharp --command="sh build.sh" --no-run-unnecessary-builds
```

### Analyze
```
codeql database analyze codeql-database/csharp --format=sarif-latest --sarif-category=csharp --output=results-cluster-csharp.sarif  
```

```
codeql database analyze codeql-database/javascript --format=sarif-latest --sarif-category=javascript --output=results-cluster-javascript.sarif
```