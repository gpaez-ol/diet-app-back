module.exports = {
    apps: [
        {
            name: "qa",
            script: "./diet-app-back.dll",
            watch: false,
            max_restarts: 10,
            interpreter: "dotnet",
            env: {
                "ConnectionStrings:DefaultConnection": "server=algofit-rds.crb4bvjfgzfi.us-east-1.rds.amazonaws.com;userid=algofitadmin;password=algofitpa5sw00rd;database=algofit;charset=utf8;"
            }
        }
    ]
}


