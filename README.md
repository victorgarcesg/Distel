# Distel

Distel is a .NET Orleans solution focused on a hotel application. It consists of the following projects:

- **ConsoleClientApp**: A console application that allows users to interact with the hotel application.
- **Distel.Grains**: A project that contains the implementation of the grains used in the hotel application.
- **Distel.Grains.Interfaces**: A project that contains the interfaces for the grains used in the hotel application.
- **Distel.Host**: A project that hosts the Orleans silo and client.
- **Distel.WebHost**: A web host that provides an API for interacting with the hotel application.

## Getting Started

To get started with Distel, you need to have .NET 7 or later installed on your machine. You can download it from [Microsoft's official website](https://dotnet.microsoft.com/download).

Once you have installed .NET, you can clone this repository and open it in your favorite IDE. You can then build and run the solution.

To use Cosmos DB grain storage with Distel.WebHost, you need to set up the Azure Cosmos DB emulator locally on your machine. You can find more information about how to set up and use the emulator on [Microsoft's official website](https://learn.microsoft.com/en-us/azure/cosmos-db/how-to-develop-emulator?tabs=docker-linux%2Ccsharp&pivots=api-nosql#export-the-emulators-tlsssl-certificate).

## Contributing

If you want to contribute to Distel, please read our [contributing guidelines](CONTRIBUTING.md) before submitting a pull request.

## License

Distel is licensed under the [MIT License](LICENSE).