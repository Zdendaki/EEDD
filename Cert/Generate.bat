@echo off
cd .
openssl genrsa -passout pass:qNF7Rw9AXtQKIv0j -out .\temp\ca-secret.key 4096
openssl rsa -passin pass:qNF7Rw9AXtQKIv0j -in .\temp\ca-secret.key -out .\temp\ca.key
openssl req -new -x509 -days 3650 -subj "/C=CZ/ST=Czech Republic/L=Ostrava/O=EEDD root CA/OU=EEDD CA unit/CN=zdendaki.net" -key .\temp\ca.key -out .\temp\ca.crt
openssl pkcs12 -export -passout pass:aJQfuwYlrBJxh37j -inkey .\temp\ca.key -in .\temp\ca.crt -out out\ca.pfx
openssl pkcs12 -passin pass:aJQfuwYlrBJxh37j -passout pass:UFFqlOgwXoxnDwcx -in out\ca.pfx -out out\ca.pem
openssl genrsa -passout pass:UFFqlOgwXoxnDwcx -out .\temp\server-secret.key 4096
openssl rsa -passin pass:UFFqlOgwXoxnDwcx -in .\temp\server-secret.key -out .\temp\server.key
openssl req -new -subj "/C=CZ/ST=Czech Republic/L=Ostrava/O=EEDD server/OU=EEDD/CN=vps.zdendaki.net" -key .\temp\server.key -out .\temp\server.csr
openssl x509 -req -days 3650 -in .\temp\server.csr -CA .\temp\ca.crt -CAkey .\temp\ca.key -set_serial 01 -out .\temp\server.crt
openssl pkcs12 -export -passout pass:maERH51w7ecFHKt9 -inkey .\temp\server.key -in .\temp\server.crt -out out\server.pfx
openssl pkcs12 -passin pass:maERH51w7ecFHKt9 -passout pass:Zcn9W7buuZ7I6Zli -in out\server.pfx -out out\server.pem
openssl genrsa -passout pass:fkG1TV8yDTEcwufn -out .\temp\client-secret.key 4096
openssl rsa -passin pass:fkG1TV8yDTEcwufn -in .\temp\client-secret.key -out .\temp\client.key
openssl req -new -subj "/C=CZ/ST=Czech Republic/L=Ostrava/O=EEDD client/OU=EEDD/CN=vps.zdendaki.net" -key .\temp\client.key -out .\temp\client.csr
openssl x509 -req -days 3650 -in .\temp\client.csr -CA .\temp\ca.crt -CAkey .\temp\ca.key -set_serial 01 -out .\temp\client.crt
openssl pkcs12 -export -passout pass:xPalHxgyIfOeHg7x -inkey .\temp\client.key -in .\temp\client.crt -out out\client.pfx
openssl pkcs12 -passin pass:xPalHxgyIfOeHg7x -passout pass:DNAH32uysjHfdJEo -in out\client.pfx -out out\client.pem