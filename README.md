# SOStringCipher
C# string encrypt and decrypt API(s)

## To run this console app under docker

Run the following command:
docker run --rm -it mydockercity/samples:encryptorconsole-net6-alpine

## To build this console app

1. Clone this repo
2. Run following command:
docker build -t mydockercity/samples:encryptorconsole-net6-alpine -f Dockerfile-StringEncryptorConsole .

## To run tests

docker compose run --build --rm dev dotnet test /source/SOStringCipherTests
