dotnet dev-certs https --check
dotnet dev-certs https --check --trust

mkdir -p "$HOME/.aspnet/https"

dotnet dev-certs https -ep "$HOME/.aspnet/https/aspnetapp.pfx" -p Passw0rd!

openssl pkcs12 -in "$HOME/.aspnet/https/aspnetapp.pfx" \
  -clcerts -nokeys -passin pass:Passw0rd! \
  -out "$HOME/.aspnet/https/localhost.crt"

 # In the container 

sudo apk add --no-cache ca-certificates
sudo cp /https/localhost.crt /usr/local/share/ca-certificates/localhost.crt
sudo update-ca-certificates

# to check

fail: Microsoft.AspNetCore.Antiforgery.DefaultAntiforgery[7]
      An exception was thrown while deserializing the token.
      Microsoft.AspNetCore.Antiforgery.AntiforgeryValidationException: The antiforgery token could not be decrypted.
       ---> System.Security.Cryptography.CryptographicException: The key {294136f9-4649-4548-b648-f840b1b9f761} was not found in the key ring. For more information go to https://aka.ms/aspnet/dataprotectionwarning
         at Microsoft.AspNetCore.DataProtection.KeyManagement.KeyRingBasedDataProtector.UnprotectCore(Byte[] protectedData, Boolean allowOperationsOnRevokedKeys, UnprotectStatus& status)
         at Microsoft.AspNetCore.DataProtection.KeyManagement.KeyRingBasedDataProtector.Unprotect(Byte[] protectedData)
         at Microsoft.AspNetCore.Antiforgery.DefaultAntiforgeryTokenSerializer.Deserialize(String serializedToken)
         --- End of inner exception stack trace ---
         at Microsoft.AspNetCore.Antiforgery.DefaultAntiforgeryTokenSerializer.Deserialize(String serializedToken)
         at Microsoft.AspNetCore.Antiforgery.DefaultAntiforgery.GetCookieTokenDoesNotThrow(HttpContext httpContext)