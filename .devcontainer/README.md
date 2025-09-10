# HTTPS Development Certificates
Configure HTTPS development certificates for your application.

## Configure Development Certificate in your [HOST]

### Verify if Development Certificate is Already Trusted
```bash
dotnet dev-certs https --check
```

### Trust Development Certificate
```bash
dotnet dev-certs https --check --trust
```

Depending on your OS, the certificate will be created in different locations.
- Windows = Windows Certificate Store (~/AppData/Roaming/Microsoft/SystemCertificates/My/Certificates)
- macOS = Keychain
- Linux = Keyring

### Create cert folder 
```bash
mkdir -p "$HOME/.aspnet/https"
```

### Export Development Certificate (PKCS #12)
It can package certificates (public keys), associated private keys, and even the trust chain (intermediate, root CAs).

```bash
dotnet dev-certs https -ep "$HOME/.aspnet/https/aspnetapp.pfx" -p Passw0rd!
```

### Extract Development Certificate (PEM)
This is a secure file to distribute. It does not contain the private key.
It is useful to install in trusted locations on client machines.

```bash
openssl pkcs12 -in "$HOME/.aspnet/https/aspnetapp.pfx" \
  -clcerts -nokeys -passin pass:Passw0rd! \
  -out "$HOME/.aspnet/https/localhost.crt"
```

## Configure Development Certificate in your [CONTAINER]

Copy the certificate to trusted locations in your container.

```bash
sudo apk add --no-cache ca-certificates
sudo cp /https/localhost.crt /usr/local/share/ca-certificates/localhost.crt
sudo update-ca-certificates
```